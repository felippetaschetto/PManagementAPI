using PManagement.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.Core.Entities
{
    public class TokenInfo : BaseEntity
    {
        public string Token { get; set; }

        public string RenewKey { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}
