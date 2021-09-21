using System;

namespace Backend_Logic
{
    public class Component
    {
        public int Id { get;}
        public string Name { get;}
        public string Description { get;}
        public int BuildYear { get;}
        public int Port { get; }
        public int Board { get; }
        public int AmountOfUses { get; }

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
