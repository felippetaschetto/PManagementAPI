using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.Core.Interfaces.Entities
{
    public interface IKeyValuePairEntity
    {
        int Id { get; set; }

        string Value{ get; set; }
    }
}
