using Backend_Logic.Models;
using Backend_Logic_Interface.Containers;
using Backend_Logic_Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic.Containers
{
    public class MachineContainer: IMachineContainer
    {
        public List<IMachine> GetMachines()
        {
            return ConvertToIMachine();
        }

        private List<IMachine> ConvertToIMachine()
        {
            return new List<IMachine>();
        }
    }
}
