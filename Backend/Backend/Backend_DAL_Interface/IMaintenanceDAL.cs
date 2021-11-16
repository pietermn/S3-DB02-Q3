using Backend_DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_DAL_Interface
{
    public interface IMaintenanceDAL
    {
        public MaintenanceDTO GetMaintenance(int componentId);
        public List<MaintenanceDTO> GetAllMaintenance(bool done);
        public void AddMaintenance(MaintenanceDTO maintenance);
        public void FinishMaintance(int MaintenancId);
    }
}
