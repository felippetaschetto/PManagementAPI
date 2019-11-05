using PManagement.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.Core.Entities
{
    public class Role : KeyValuePairEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
