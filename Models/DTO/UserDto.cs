﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModels.DTO
{
    public class UserDto
    {

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
