using System;
using System.Collections.Generic;

#nullable disable

namespace Backend_DAL.DataAccess.DataObjects
{
    public partial class MachineMonitoringPoorten
    {
        public int Id { get; set; }
        public int Board { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
        public int Volgorde { get; set; }
        public bool Visible { get; set; }
    }
}
