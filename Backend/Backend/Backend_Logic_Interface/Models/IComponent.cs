using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic_Interface.Models
{
    public interface IComponent
    {
        public int Id { get; }
        public string Name { get; }
        public ComponentType Type { get; }
        public string Description { get; }
        public int Port { get; }
        public int Board { get; }
        public int TotalActions { get; }
        public List<IProductionLineHistory> History { get; }
    }
}
