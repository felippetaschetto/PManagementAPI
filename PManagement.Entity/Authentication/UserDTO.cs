using System;
using System.Collections.Generic;

namespace PManagement.Entity.Authentication
{
    public class UserDTO
    {
        public List<string> Roles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
    }
}
