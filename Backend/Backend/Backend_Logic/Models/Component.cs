using Backend_Logic_Interface.Models;
using System;

namespace Backend_Logic.Models
{
    public class Component: IComponent
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int BuildYear { get; private set; }
        public int Port { get; private set; }
        public int Board { get; private set; }
        public int AmountOfUses { get; private set; }

        public Component(int id, string name, string description, int buildYear, int port, int board, int amountOfUses)
        {
            Id = id;
            Name = name;
            Description = description;
            BuildYear = buildYear;
            Port = port;
            Board = board;
            AmountOfUses = amountOfUses;
        }


    }
}
