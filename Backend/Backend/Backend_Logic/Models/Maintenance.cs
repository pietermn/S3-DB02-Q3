using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic.Models
{
    public class Maintenance
    {

        public int Id { get; private set; }
        public int ComponentId { get; private set; }
        public string Description { get; private set; }
        public DateTime TimeDone { get; private set; }
        public bool Done { get; private set; }

        public Maintenance(int id, int componentId, string description, DateTime timeDone, bool done)
        {
            Id = id;
            ComponentId = componentId;
            Description = description;
            TimeDone = timeDone;
            Done = done;
        }
    }
}
