using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.Core.Interfaces.Entities
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        DateTime InsertStamp { get; set; }

        DateTime? UpdateStamp { get; set; }
    }
}
