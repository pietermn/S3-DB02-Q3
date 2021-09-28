using Backend_Logic.Models;
using Backend_Logic_Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic.Containers
{
    public class MachineContainer
    {
        public List<IMachine> GetMachines()
        {
            return new List<IMachine>();
        }
    }
}
