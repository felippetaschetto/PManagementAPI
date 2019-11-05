using PManagement.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.Core.Entities
{
    public class Company : BaseEntity
    {
        public Company()
        {
            Users = new HashSet<User>();
        }

        public string Name { get; set; }
        //public Address Address { get; set; }
        public string Email { get; set; }
        public bool IsEnabled { get; set; }
        public virtual ICollection<User> Users { get; private set; }
    }
}
