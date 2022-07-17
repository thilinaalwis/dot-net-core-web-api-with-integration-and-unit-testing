
using AutoMapper;
using BusinessLogicLayer.AWS;
using BusinessLogicLayer.IRepositories;
using BusinessLogicLayer.UOW;
using BusinessModels;
using BusinessModels.AWS;
using BusinessModels.DTO;
using DataAccessLayer.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IStorageService storageService;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public ProductsController(IUnitOfWork unitOfWork,IStorageService storageService, IConfiguration config, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.storageService = storageService;
            this.config = config;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] Product product)
        {
            if (ModelState.IsValid)
            {
                List<string> erros = unitOfWork.Products.ValidateProduct(product,true);

                if (erros != null && erros.Count > 0)
                    return new JsonResult(JsonConvert.SerializeObject(erros)) { StatusCode = 500 };


                var s3Response = await UploadImage(product.file);

                if (s3Response.StatusCode == 200)
                {
                    product.Image = "https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/" + s3Response.ImageName;
                }
                
                product.ProductCode = unitOfWork.Products.GetNextProductCode();

                await unitOfWork.Products.Add(product);
                await unitOfWork.CompleteAsync();
                unitOfWork.Dispose();

                var produtDto = mapper.Map<ProductDto>(product);

                return Ok(produtDto);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var Product = await unitOfWork.Products.GetById(id);

            if (Product == null)
                return NotFound(); // 404 http status code 

            var produtDto = mapper.Map<ProductDto>(Product);

            return Ok(produtDto);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Products = await unitOfWork.Products.All();
            var produtDtos = mapper.Map<List<ProductDto>>(Products);

            return Ok(produtDtos);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromForm] Product product)
        {
            if (id != product.ProductId)
                return BadRequest();

            List<string> erros = unitOfWork.Products.ValidateProduct(product);

            if (erros != null && erros.Count > 0)
                return new JsonResult(JsonConvert.SerializeObject(erros)) { StatusCode = 500 };

            if (product.file != null)
            {
                var s3Response = await UploadImage(product.file);

                if (s3Response.StatusCode == 200)
                {
                    product.Image = "https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/" + s3Response.ImageName;
                }
            }

            unitOfWork.Products.Update(product);
            await unitOfWork.CompleteAsync();
            unitOfWork.Dispose();

            var produtDto = mapper.Map<ProductDto>(product);

            return Ok(produtDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await unitOfWork.Products.Delete(id);

            if (item == false)
                return new JsonResult("No record Found") { StatusCode = 500 };


            await unitOfWork.CompleteAsync();
            unitOfWork.Dispose();

            return Ok(item);
        }

        private async Task<S3ResponseDto> UploadImage(IFormFile file)
        {
            // Process the file
            await using var memoryStr = new MemoryStream();
            await file.CopyToAsync(memoryStr);

            var fileExt = Path.GetExtension(file.FileName);
            var objName = $"{Guid.NewGuid()}{fileExt}";

            var s3Obj = new S3Object()
            {
                BucketName = "keeneyepracticaltest",
                InputStream = memoryStr,
                Name = objName
            };

            var cred = new AwsCredentials()
            {
                AwsKey = config["AwsConfiguration:AWSAccessKey"],
                AwsSecretKey = config["AwsConfiguration:AWSSecretKey"]
            };

            var result = await storageService.UploadFileAsync(s3Obj, cred);

            result.ImageName = s3Obj.Name;

            return result;

        }

    }
}
