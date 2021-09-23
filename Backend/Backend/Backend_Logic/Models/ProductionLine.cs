using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic
{
    public class ProductionLine
    {
        public int Id { get; private set; }
        public string Description { get; private set; }

        public ProductionLine(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
