using System.Collections.Generic;
using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Backend_Logic_Interface.Containers;

namespace Backend_Logic.Containers
{
    public class MachineContainer: IMachineContainer
    {
        readonly IMachineDAL _machineDAL;
        public MachineContainer(IMachineDAL machineDAL)
        {
            _machineDAL = machineDAL;
        }

        public MachineDTO GetMachine(int machine_id)
        {
            return _machineDAL.GetMachine(machine_id);
        }

        public List<MachineDTO> GetMachines()
        {
            return _machineDAL.GetMachines();
        }
    }
}
