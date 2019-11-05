using PManagement.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.Core.Entities.Base
{
    public abstract class CompanyBaseEntity : BaseEntity, ICompanyBaseEntity
    {
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

    }
}
