using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic
{
    public class ComponentType
    {
        public int Id { get; private set; }
        public string Description { get; private set; }

        public ComponentType(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
