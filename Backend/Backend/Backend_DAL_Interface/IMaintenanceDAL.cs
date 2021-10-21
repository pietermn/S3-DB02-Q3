using Backend_DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_DAL_Interface
{
    public interface IMaintenanceDAL
    {
        public MaintenanceDTO GetMaintenance(int componentId);
        public List<MaintenanceDTO> GetAllMaintenance();
    }
}
