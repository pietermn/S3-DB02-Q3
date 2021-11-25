using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend_DAL
{
    public class MaintenanceDAL : IMaintenanceDAL
    {
        readonly Q3Context _Context;

        public MaintenanceDAL(Q3Context context)
        {
            _Context = context;
        }

        public List<MaintenanceDTO> GetMaintenance(int componentId)
        {
            return _Context.Maintenance
                .Include(m => m.Component)
                .Where(m => m.Component.Id == componentId)
                    .AsNoTracking()
                .ToList();
        }

        public List<MaintenanceDTO> GetAllMaintenance(bool done)
        {
            return _Context.Maintenance
                .Where(m => m.Done == done)
                .Include(m => m.Component)
                    .AsNoTracking()
                .ToList();
        }

        public void AddMaintenance(MaintenanceDTO maintenance)
        {
            _Context.Maintenance.Add(maintenance);
            _Context.SaveChanges();
        }

        public void FinishMaintance(int MaintenancId)
        {
            MaintenanceDTO maintenanceDTO = _Context.Maintenance.Where(m => m.Id == MaintenancId).FirstOrDefault();
            maintenanceDTO.TimeDone = DateTime.Now;
            maintenanceDTO.Done = true;
            _Context.SaveChanges();
        }


    }
}
