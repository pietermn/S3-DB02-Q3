using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic_Interface.Models
{
    public interface IProductionLine
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public bool Active { get; }
        public int Port { get; }
        public int Board { get; }
        List<IMachine> Machines { get; }
        List<IComponent> Components { get; }
    }
}
