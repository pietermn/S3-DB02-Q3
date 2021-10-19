using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic_Interface.Models
{
    public interface IMaintenance
    {
        public int Id { get; }
        public int ComponentId { get; }
        public string Description { get; }
        public DateTime TimeDone { get; }
        public bool Done { get; }
    }
}
