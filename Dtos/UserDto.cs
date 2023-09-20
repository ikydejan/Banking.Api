using System.Collections.Generic;
using Banking.Api.Models;
using System;
namespace Banking.Api.Dtos
{

    public class UserDto : User
    {
        public string Token { get; set; }  
        public DateTime? TokenExpires { get; set; }
        
    }
    public class UserRegisterDto 
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string RegisterPwd { get; set; } 
        public string UserNameBy { get; set; }
        
    }

    public class UserLoginDto 
    {
        public string UserName { get; set; } 
        public string Password { get; set; }
        public string ComputerName { get; set; }   
        
    }

    public class UserChangePasswordDto
    {
        public string UserName { get; set; }
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
    }
}