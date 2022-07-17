using BusinessLogicLayer.IRepositories;
using BusinessLogicLayer.Tools;
using BusinessModels;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(EcomContext context) : base(context)
        {
        }

        /// <summary>
        /// check the user credentials
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckLogin(string username, string password)
        {
            try
            {
                string passwordHash = EncriptionTools.MD5Hash(password);

                return dbSet.Any(x => x.Username.Equals(username) && x.Password.Equals(passwordHash));
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Check whether the generated refresh token is equal to the refresh token sent by the user
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public bool ValidateRefreshToken(string refreshToken)
        {
            try
            {
                return dbSet.Any(x => x.RefreshToken.Equals(refreshToken));
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Get user object by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUser(string username)
        {
            return dbSet.FirstOrDefault(x => x.Username.Equals(username));
        }

        /// <summary>
        /// Valdate user details before saving
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<string> ValidateUser(User user)
        {
            List<string> errors = new List<string>();

            if(Cleaners.CleanString(user.Username) == "")
                errors.Add("username is required");

            if (Cleaners.CleanString(user.Password) == "")
                errors.Add("password is required");

            if (dbSet.Any(x => x.Username.Equals(user.Username)))
                errors.Add("username is taken");

            if (!Cleaners.ValidateEmail(user.Email))
                errors.Add("invalid email");

            if (dbSet.Any(x => x.Email.Equals(user.Email)))
                errors.Add("there is already an account with this email");

            return errors;
        }
    }
}
