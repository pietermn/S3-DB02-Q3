using System.Collections.Generic;
using Backend_DTO.DTOs;

namespace Backend_Logic_Interface.Containers
{
    public interface IMachineContainer
    {
        public MachineDTO GetMachine(int machine_id);
        public List<MachineDTO> GetMachines();
    }
}
