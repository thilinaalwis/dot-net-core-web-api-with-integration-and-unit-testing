using AutoFixture;
using AutoMapper;
using BusinessLogicLayer.AWS;
using BusinessLogicLayer.IRepositories;
using BusinessLogicLayer.UOW;
using BusinessModels;
using DataAccessLayer.IRepositories;
using EcommerceAPI.Controllers;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using Xunit;

namespace EcommerceAPIUnitTest
{
    public class ProductControllerTests
    {
        private int _productId;

        private Mock<IProductRepository> _productRepo;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IStorageService> _storageService;
        private Mock<IConfiguration> _config;
        private Mock<IMapper> _mapper;

        public ProductControllerTests()
        {
            _productRepo = new Mock<IProductRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _storageService = new Mock<IStorageService>();
            _config = new Mock<IConfiguration>();
            _mapper = new Mock<IMapper>();
        }


        [Fact]
        public async void TestCreateProduct()
        {
            Product _product = null;

            _productRepo.As<IGenericRepository<Product>>();
            _productRepo.Setup(x => x.Add(It.IsAny<Product>())).Callback<Product>(x=> _product = x);
            _unitOfWork.Setup(x => x.Products).Returns(_productRepo.Object);

            //Arrange
            var controller = new ProductsController(_unitOfWork.Object, _storageService.Object, _config.Object, _mapper.Object);

            var product = new Product {             
                Name = "Test Product",
                Category = "Test Category",
                Price = 10000,
                DiscountRate = 5,
                MinimumQty = 1,            
            };


            //Act
            var actionResult = await controller.CreateProduct(product);

            //Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void TestGetProduct()
        {
            //Arrange
            var _unitOfWork = A.Fake<IUnitOfWork>();
            var _storageService = A.Fake<IStorageService>();
            var _config = A.Fake<IConfiguration>();
            var _mapper = A.Fake<IMapper>();
            var controller = new ProductsController(_unitOfWork, _storageService, _config, _mapper);


            var product = new Product
            {
                Name = "Test Product Update",
                Category = "Test Category Update",
                Price = 12000,
                DiscountRate = 4
            };

            //Act
            var actionResult = await controller.UpdateItem(_productId, product);

            //Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }


        [Fact]
        public async void TestUpdateProduct()
        {
            //Arrange
            var _unitOfWork = A.Fake<IUnitOfWork>();
            var _storageService = A.Fake<IStorageService>();
            var _config = A.Fake<IConfiguration>();
            var _mapper = A.Fake<IMapper>();
            var controller = new ProductsController(_unitOfWork, _storageService, _config, _mapper);


            var product = new Product
            {
                Name = "Test Product Update",
                Category = "Test Category Update",
                Price = 12000,
                DiscountRate = 4
            };

            //Act
            var actionResult = await controller.UpdateItem(_productId, product);

            //Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
