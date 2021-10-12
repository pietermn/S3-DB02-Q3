using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Backend_DAL
{
    public class MachineDAL : IMachineDAL
    {
        readonly Q3Context _Context;

        public MachineDAL(Q3Context context)
        {
            _Context = context;
        }

        public MachineDTO GetMachine(int machine_id)
        {
            return _Context.Machines
                .Where(m => m.Id == machine_id)
                    .AsNoTracking()
                .FirstOrDefault();
        }

        public List<MachineDTO> GetMachines()
        {
            return _Context.Machines
                    .AsNoTracking()
                .ToList();
        }
    }
}
