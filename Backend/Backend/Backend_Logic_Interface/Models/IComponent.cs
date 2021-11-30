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
        //public int Port { get; }
        //public int Board { get; }
        public int TotalActions { get; }
        public int MaxActions { get;  }
        public int CurrentActions {get;}
        public List<IProductionLineHistory> History { get; }
        public List<IMaintenance> MaintenanceHistory { get; }

    }
}
