using Backend_DTO.DTOs;
using System.Collections.Generic;

namespace Backend_DAL_Interface
{
    public interface IMachineDAL
    {
        public MachineDTO GetMachine(int machine_id);
        public List<MachineDTO> GetMachines();
    }
}
