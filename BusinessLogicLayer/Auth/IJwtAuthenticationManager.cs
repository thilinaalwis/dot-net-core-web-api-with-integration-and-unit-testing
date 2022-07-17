using BusinessModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Auth
{
    public interface IJwtAuthenticationManager
    {
        AuthenticationModel Authenticate(string username);
        AuthenticationModel RefreshToken(AuthenticationModel refreshCred, out string username);
    }
}
