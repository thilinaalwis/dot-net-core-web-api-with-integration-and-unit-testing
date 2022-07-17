using AutoMapper;
using BusinessLogicLayer.Auth;
using BusinessLogicLayer.IRepositories;
using BusinessLogicLayer.Tools;
using BusinessLogicLayer.UOW;
using BusinessModels;
using BusinessModels.DTO;
using DataAccessLayer.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;

            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await unitOfWork.Users.GetById(id);

            if (user == null)
                return NotFound(); // 404 http status code 

            var userDto = mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await unitOfWork.Users.All();
            var usersDtos = mapper.Map<List<UserDto>>(users);

            return Ok(usersDtos);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(User user)
        {

            List<string> erros = unitOfWork.Users.ValidateUser(user);

            if (erros != null && erros.Count > 0)
                return new JsonResult(JsonConvert.SerializeObject(erros)) { StatusCode = 500 };

            user.LastLoginTime = DateTime.Now;
            user.Password = EncriptionTools.MD5Hash(user.Password);


            await unitOfWork.Users.Add(user);
            await unitOfWork.CompleteAsync();
            unitOfWork.Dispose();

            var userDto = mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserPassword(int id, User user)
        {

            var existingUser = await unitOfWork.Users.GetById(id);

            if (existingUser == null)
                return BadRequest();

            existingUser.Password = EncriptionTools.MD5Hash(user.Password);

            unitOfWork.Users.Update(existingUser);
            await unitOfWork.CompleteAsync();
            unitOfWork.Dispose();

            var userDto = mapper.Map<UserDto>(existingUser);
            return Ok(userDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUser(int id)
        {

            var item = await unitOfWork.Users.Delete(id);

            if (item == false)
                return new JsonResult("No record Found") { StatusCode = 500 };


            await unitOfWork.CompleteAsync();
            unitOfWork.Dispose();

            return Ok(item);
        }

    }
}
