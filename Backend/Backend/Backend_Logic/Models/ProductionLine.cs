using Backend_Logic_Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic.Models
{
    public class ProductionLine: IProductionLine
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public int Port { get; private set; }
        public int Board { get; private set; }
        List<IMachine> Machines { get; set; }
        List<IComponent> Components { get; set; }

        public ProductionLine(int id, string name, string description, bool active, int port, int board)
        {
            Id = id;
            Name = name;
            Description = description;
            Active = active;
            Port = port;
            Board = board;
        }
        public ProductionLine(int id, string name, string description, bool active, int port, int board, List<IMachine> machines, List<IComponent> components)
        {
            Id = id;
            Name = name;
            Description = description;
            Active = active;
            Port = port;
            Board = board;
            Machines = machines;
            Components = components;
        }
    }
}
