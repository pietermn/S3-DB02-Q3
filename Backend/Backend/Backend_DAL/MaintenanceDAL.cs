using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend_DAL
{
    public class MaintenanceDAL: IMaintenanceDAL
    {
        readonly Q3Context _Context;

        public MaintenanceDAL(Q3Context context)
        {
            _Context = context;
        }

        public MaintenanceDTO GetMaintenance(int componentId)
        {
            return _Context.Maintenance
                .Where(m => m.ComponentId == componentId)
                    .AsNoTracking()
                .FirstOrDefault();
        }

        public List<MaintenanceDTO> GetAllMaintenance() //Nog toevoegen
        {
            return _Context.Maintenance
                    .AsNoTracking()
                .ToList();
        }
    }
}
