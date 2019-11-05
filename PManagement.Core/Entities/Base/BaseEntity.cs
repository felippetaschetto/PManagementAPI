using PManagement.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.Core.Entities.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime InsertStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }
    }
}
