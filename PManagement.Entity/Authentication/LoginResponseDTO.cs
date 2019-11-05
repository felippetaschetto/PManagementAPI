using System;

namespace PManagement.Entity.Authentication
{
    public class LoginResponseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string RenewKey { get; set; }
    }
}
