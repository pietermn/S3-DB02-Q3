using System.Collections.Generic;
using Backend_DTO.DTOs;
using Backend_Logic_Interface.Models;

namespace Backend_Logic_Interface.Containers
{
    public interface IUptimeContainer
    {
        //public List<IUptime> GetUptimeFromLastDay();
        public List<IUptime> GetUptimeByIdFromLastDay(int productionLine_id);
    }
}
