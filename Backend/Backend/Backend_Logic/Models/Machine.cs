using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic
{
    public class Machine
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int BuildYear { get; private set; }
        public bool Active { get; private set; }
        public int Port { get; private set; }
        public int Board { get; private set; }

        public Machine(int id, string name, string description, int buildYear,bool active, int port, int board)
        {
            Id = id;
            Name = name;
            Description = description;
            BuildYear = buildYear;
            Active = active;
            Port = port;
            Board = board;
        }
    }
}
