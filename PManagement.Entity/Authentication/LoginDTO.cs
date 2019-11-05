using System;
using System.ComponentModel.DataAnnotations;

namespace PManagement.Entity.Authentication
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
