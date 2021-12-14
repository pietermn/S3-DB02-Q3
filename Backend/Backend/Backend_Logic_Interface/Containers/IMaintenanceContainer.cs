using Backend_DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic_Interface.Containers
{
    public interface IMaintenanceContainer
    {
        public List<MaintenanceDTO> GetMaintenance(int component_id);
        public List<MaintenanceDTO> GetAllMaintenance(bool done);
        public void AddMaintenance(MaintenanceDTO maintenance);
        public void FinishMaintance(int MaintenancId);
    }
}
