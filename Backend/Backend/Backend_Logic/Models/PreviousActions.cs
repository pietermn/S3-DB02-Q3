using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic.Models
{
    public class PreviousActions
    {
        public int Week { get; private set; }
        public int Actions { get; private set; }

        public PreviousActions(int week, int actions)
        {
            Week = week;
            Actions = actions;
        }
    }
}
