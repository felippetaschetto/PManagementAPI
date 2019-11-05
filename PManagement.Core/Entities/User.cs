using PManagement.Core.Entities.Base;
using PManagement.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.Core.Entities
{
    public class User : CompanyBaseEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImg { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsEnabled { get; set; }
        public Address Address { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
}
}
