using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic_Interface.Models
{
    public interface IComponentType
    {
        public int Id { get; }
        public string Description { get; }
    }
}
