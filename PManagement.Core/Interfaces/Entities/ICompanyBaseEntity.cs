using PManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.Core.Interfaces.Entities
{
    public interface ICompanyBaseEntity : IBaseEntity
    {
        Company Company { get; }
    }
}
