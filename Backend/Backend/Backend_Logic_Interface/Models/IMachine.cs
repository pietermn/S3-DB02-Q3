using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic_Interface.Models
{
    public interface IMachine
    {
        public int Id { get; }
        public string Name { get;}
        public string Description { get; }
        public int BuildYear { get;}
        public bool Active { get;}
        public int Port { get;}
        public int Board { get;}
    }
}
