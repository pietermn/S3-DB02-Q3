using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic_Interface.Models
{
    public interface IComponent
    {
        public int Id { get;}
        public string Name { get;}
        public string Description { get;}
        public int BuildYear { get;}
        public int Port { get;}
        public int Board { get;}
        public int AmountOfUses { get;}
    }
}
