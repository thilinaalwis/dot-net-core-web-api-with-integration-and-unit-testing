using BusinessLogicLayer.IRepositories;
using BusinessLogicLayer.Repositories;
using BusinessModels.Auth;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Auth
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly string tokenKey;

        public JwtAuthenticationManager(string tokenKey)
        {
            this.tokenKey = tokenKey;
        }

        /// <summary>
        /// Generate JWT Token
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public AuthenticationModel Authenticate(string username)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = GenerateToken();


            AuthenticationModel authenticationResponse = new AuthenticationModel { 
                Token = tokenHandler.WriteToken(token), 
                RefreshToken = refreshToken 
            };

            return authenticationResponse;
        }

        /// <summary>
        /// Refreshing JWT Token
        /// </summary>
        /// <param name="refreshCred"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="SecurityTokenException"></exception>
        public AuthenticationModel RefreshToken(AuthenticationModel refreshCred, out string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            SecurityToken validateToken;
            var principal = tokenHandler.ValidateToken(refreshCred.Token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out validateToken);

            var jwtToken = validateToken as JwtSecurityToken;

            if(jwtToken == null || jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("invalid token passed");
            }

            username = principal.Identity.Name;

            AuthenticationModel authenticationResponse = Authenticate(username);

            return authenticationResponse;
        }

        private string GenerateToken()
        {
            var randomNumber = new byte[32];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
