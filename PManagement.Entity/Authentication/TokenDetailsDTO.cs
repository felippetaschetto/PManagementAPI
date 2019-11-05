using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.Entity.Authentication
{
    public class TokenDetailsDTO
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
    }
}
