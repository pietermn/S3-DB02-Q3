using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Backend_Logic_Interface.Containers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic.Containers
{
    public class MaintenanceContainer : IMaintenanceContainer
    {
        readonly IMaintenanceDAL _maintenanceDAL;
        public MaintenanceContainer(IMaintenanceDAL componentDAL)
        {
            _maintenanceDAL = componentDAL;
        }


        public List<MaintenanceDTO> GetMaintenance(int component_id)
        {
            return _maintenanceDAL.GetMaintenance(component_id);
        }
        
        public List<MaintenanceDTO> GetAllMaintenance(bool done)
        {
            return _maintenanceDAL.GetAllMaintenance(done);
        }

        public void AddMaintenance(MaintenanceDTO maintenance)
        {
            _maintenanceDAL.AddMaintenance(maintenance);
        }

        public void FinishMaintance(int MaintenancId)
        {
            _maintenanceDAL.FinishMaintance(MaintenancId);
        }
    }
}
