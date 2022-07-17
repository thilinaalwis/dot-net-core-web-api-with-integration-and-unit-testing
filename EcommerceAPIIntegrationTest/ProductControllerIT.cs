using BusinessModels;
using BusinessModels.Auth;
using BusinessModels.DTO;
using EcommerceAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

//Optional
[assembly: CollectionBehavior(DisableTestParallelization = true)]
//Optional
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
//Optional
[assembly: TestCollectionOrderer("Xunit.Extensions.Ordering.CollectionOrderer", "Xunit.Extensions.Ordering")]
namespace EcommerceAPIIntegrationTest
{
    [Order(1)]
    public class ProductControllerIT : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;
        private readonly WebApplicationFactory<Startup> _factory;

        private static string baseURL = "https://localhost:44328/api";
        private static string Token { get; set; } = "";
        private static Product product { get; set; }

        public ProductControllerIT(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            client = _factory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var logininfor = new LoginDto
            {
                username = "Thilina",
                password = "Akalanka"
            };

            var content = new StringContent(JsonConvert.SerializeObject(logininfor), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(baseURL + "/auth/login", content);

            var registrationResponse = await response.Content.ReadFromJsonAsync<AuthenticationModel>();

            Token = registrationResponse.Token;
            return Token;
        }

        [Fact, Order(0)]
        public async Task Test1CreateProduct()
        {
            //Arrange
            await AuthenticateAsync();

            product = new Product
            {
                Name = "Test Product",
                Category = "Test Category",
                Price = 10000,
                DiscountRate = 5,
                MinimumQty = 1,
            };


            await using var stream = File.OpenRead(@"TestFiles\winebottle.png");

            using var _content = new MultipartFormDataContent
            {
                // file
                { new StreamContent(stream), "file", "winebottle.png" },

                // payload
                { new StringContent(product.Name), "Name" },
                { new StringContent(product.Category), "Category"},
                { new StringContent(product.Price.ToString()), "Price" },
                { new StringContent(product.DiscountRate.ToString()), "DiscountRate" },
                { new StringContent(product.MinimumQty.ToString()), "MinimumQty" }

            };


            //Act
            var response = await client.PostAsync(baseURL + "/products", _content);


            //Assert
            response.EnsureSuccessStatusCode();


            var createdproduct = await response.Content.ReadFromJsonAsync<ProductDto>();

            if (createdproduct != null)
            {
                product.ProductId = createdproduct.ProductId;
                product.ProductCode = createdproduct.ProductCode;
                product.Name = createdproduct.Name;
                product.Category = createdproduct.Category;
                product.Price = createdproduct.Price;
                product.DiscountRate = createdproduct.DiscountRate;
                product.MinimumQty = createdproduct.MinimumQty;
                product.Image = createdproduct.Image;
            }


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, Order(1)]
        public async Task Test2UpdateProduct()
        {
            //Arrange
            //await AuthenticateAsync();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

            product.Name = "Product Name Updated";


            using var _content = new MultipartFormDataContent
            {
                { new StringContent(product.ProductId.ToString()), "ProductId" },
                { new StringContent(product.ProductCode), "ProductCode" },
                { new StringContent(product.Name), "Name" },
                { new StringContent(product.Image), "Image" },
                { new StringContent(product.Category), "Category"},
                { new StringContent(product.Price.ToString()), "Price" },
                { new StringContent(product.DiscountRate.ToString()), "DiscountRate" },
                { new StringContent(product.MinimumQty.ToString()), "MinimumQty" }

            };

            //Act
            var response = await client.PatchAsync(baseURL + "/products/" + product.ProductId, _content);
            
            //Assert
            response.EnsureSuccessStatusCode();

            var createdproduct = await response.Content.ReadFromJsonAsync<ProductDto>();

            if (createdproduct != null)
            {
                product.ProductId = createdproduct.ProductId;
                product.ProductCode = createdproduct.ProductCode;
                product.Name = createdproduct.Name;
                product.Category = createdproduct.Category;
                product.Price = createdproduct.Price;
                product.DiscountRate = createdproduct.DiscountRate;
                product.MinimumQty = createdproduct.MinimumQty;
                product.Image = createdproduct.Image;
            }

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        }


        [Fact, Order(2)]
        public async Task Test3GetAllProducts()
        {
            //Arrange
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

            //Act
            var response = await client.GetAsync(baseURL + "/products");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, Order(3)]
        public async Task Test4GetProduct()
        {
            //Arrange
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

            //Act
            var response = await client.GetAsync(baseURL + "/products/" + product.ProductId);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, Order(4)]
        public async Task Test5RemoveProduct()
        {
            //Arrange
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

            //Act
            var response = await client.DeleteAsync(baseURL + "/products/" + product.ProductId);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


    }
}
