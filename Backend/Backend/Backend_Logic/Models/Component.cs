using Backend_Logic_Interface.Models;
using Enums;
using System;
using System.Collections.Generic;

namespace Backend_Logic.Models
{
    public class Component: IComponent
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ComponentType Type { get; private set; }
        public string Description { get; private set; }
        public int Port { get; private set; }
        public int Board { get; private set; }
        public int TotalActions { get; private set; }
        public List<IProductionLineHistory> History { get; private set; }

        public Component(int id, string name, ComponentType type ,string description, int port, int board, int totalActions, List<IProductionLineHistory> list)
        {
            Id = id;
            Name = name;
            Type = type;
            Description = description;
            Port = port;
            Board = board;
            TotalActions = totalActions;
            History = list;
        }


    }
}
