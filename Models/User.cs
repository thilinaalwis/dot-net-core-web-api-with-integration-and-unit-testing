using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModels
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public string RefreshToken { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
