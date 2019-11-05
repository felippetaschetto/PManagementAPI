using PManagement.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.Core.Entities.Base
{
    public abstract class KeyValuePairEntity : IKeyValuePairEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
