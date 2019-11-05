using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.Entity.UserProfile
{
    public class UserProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string FileName { get; set; }
    }
}
