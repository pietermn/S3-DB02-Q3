using Backend_Logic_Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic_Interface.Containers
{
    public interface IMachineContainer
    {
        public List<IMachine> GetMachines();
    }
}
