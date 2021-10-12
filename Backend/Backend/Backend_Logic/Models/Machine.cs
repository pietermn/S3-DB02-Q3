using Backend_Logic_Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic.Models
{
    public class Machine: IMachine
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Machine(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
