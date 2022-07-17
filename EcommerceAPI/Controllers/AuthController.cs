using BusinessLogicLayer.Auth;
using BusinessLogicLayer.IRepositories;
using BusinessLogicLayer.Tools;
using BusinessLogicLayer.UOW;
using BusinessModels.Auth;
using BusinessModels.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jWTAuthenticationManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;

        public AuthController(IJwtAuthenticationManager jWTAuthenticationManager, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this.jWTAuthenticationManager = jWTAuthenticationManager;
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync(LoginDto loginDto)
        {
            string username = loginDto.username;
            string password = loginDto.password;

            if (!userRepository.CheckLogin(username,password)) return Unauthorized();

            var token = jWTAuthenticationManager.Authenticate(username);

            if (token == null)
                return Unauthorized();

            var user = userRepository.GetUser(username);
            if (user != null) {
                user.LastLoginTime = DateTime.Now;
                user.RefreshToken = token.RefreshToken;
                unitOfWork.Users.Update(user);
                await unitOfWork.CompleteAsync();
                unitOfWork.Dispose();
            } 

            return Ok(token);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(AuthenticationModel refresh)
        {
            string oldtoken = Cleaners.CleanString(refresh.Token);
            string refreshToken = Cleaners.CleanString(refresh.RefreshToken);


            if (oldtoken == "" || refreshToken == "")
            {
                throw new SecurityTokenException("invalid token passed");
            }

            if (!userRepository.ValidateRefreshToken(refreshToken))
            {
                throw new SecurityTokenException("invalid token passed");
            }

            string username = "";

            var token = jWTAuthenticationManager.RefreshToken(refresh, out username);

            if (token == null)
                return Unauthorized();

            var user = userRepository.GetUser(username);
            if (user != null)
            {
                user.LastLoginTime = DateTime.Now;
                user.RefreshToken = token.RefreshToken;   
                unitOfWork.Users.Update(user);
                await unitOfWork.CompleteAsync();
                unitOfWork.Dispose();
            }

            return Ok(token);

        }

    }
}
