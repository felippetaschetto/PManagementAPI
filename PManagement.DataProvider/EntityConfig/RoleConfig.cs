using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PManagement.Core.Entities;
using PManagement.DataProvider.EntityConfig.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.DataProvider.EntityConfig
{
    public class RoleConfig : KeyValuePairConfig<Role>
    {
        public RoleConfig() : base("Role")
        {

        }
    }
}
