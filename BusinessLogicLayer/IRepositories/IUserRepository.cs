using BusinessModels;
using DataAccessLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        bool CheckLogin(string username, string password);
        bool ValidateRefreshToken(string refreshToken);
        User GetUser(string username);
        List<string> ValidateUser(User user);
    }
}
