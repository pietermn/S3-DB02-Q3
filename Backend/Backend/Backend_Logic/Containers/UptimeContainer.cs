using System.Collections.Generic;
using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Backend_Logic_Interface.Containers;
using Backend_Logic_Interface.Models;
using Backend_Logic.Models;

namespace Backend_Logic.Containers
{
    public class UptimeContainer : IUptimeContainer
    {
        readonly IProductionDAL _productionsDAL;
        public UptimeContainer(IProductionDAL productionsDAL)
        {
            _productionsDAL = productionsDAL;
        }

        public List<IUptime> GetUptimeByIdFromLastDay(int productionLine_id)
        {
            List<ProductionsDTO> productions = _productionsDAL.GetProductionsByIdFromLastDay(productionLine_id);

            List<IUptime> Uptimes = new();

            foreach (ProductionsDTO p in productions)
            {
                double diff = p.ShotTime * 3;
                bool act = true;

                if (Uptimes.Count != 0)
                {
                    if (Uptimes[^1].Timestamp.AddSeconds(diff) < p.Timestamp)
                    {
                        act = false;
                    }
                }

                Uptimes.Add(new Uptime(Uptimes.Count + 1, p.ProductionLineId, p.Timestamp, act));
            }

            return Uptimes;
        }

        public List<IUptime> GetUptimeFromLastDay()
        {
            List<ProductionsDTO> productions = _productionsDAL.GetProductionsFromLastDay();

            List<IUptime> Uptimes = new();

            foreach (ProductionsDTO p in productions)
            {
                double diff = p.ShotTime * 3;
                bool act = true;

                if (Uptimes.Count != 0)
                {
                    if (Uptimes[^1].Timestamp.AddSeconds(diff) < p.Timestamp)
                    {
                        act = false;
                    }
                }

                Uptimes.Add(new Uptime(Uptimes.Count + 1, p.ProductionLineId, p.Timestamp, act));
            }

            return Uptimes;
        }
    }
}
