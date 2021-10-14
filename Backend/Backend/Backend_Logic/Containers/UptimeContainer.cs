using System.Collections.Generic;
using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Backend_Logic_Interface.Containers;
using Backend_Logic_Interface.Models;
using Backend_Logic.Models;
using System.Linq;

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
            List<IUptime> uptimes = new();
            int count = 0;

            if (productions.Count == 0)
            {
                return new() { new Uptime(0, productionLine_id, System.DateTime.Now, System.DateTime.Now.AddDays(-1), false) };
            }

            Uptime current = new(count, productions[0].ProductionLineId, productions[0].Timestamp, productions[0].Timestamp, true);

            foreach (ProductionsDTO p in productions.Skip(1))
            {
                double diff = p.ShotTime * 6;

                if (p.Timestamp >= current.End.AddSeconds(diff)) // Down
                {
                    uptimes.Add(current);
                    count++;

                    uptimes.Add(new Uptime(count, p.ProductionLineId, current.End, p.Timestamp, false));
                    count++;

                    current = new Uptime(count, p.ProductionLineId, p.Timestamp, p.Timestamp, true);
                }
                else // Up
                {
                    current.End = p.Timestamp;
                }
            }

            uptimes.Add(current);

            return uptimes;
        }

        //public List<IUptime> GetUptimeFromLastDay()
        //{
        //    List<ProductionsDTO> productions = _productionsDAL.GetProductionsFromLastDay();

        //    List<IUptime> Uptimes = new();

        //    foreach (ProductionsDTO p in productions)
        //    {
        //        double diff = p.ShotTime * 3;
        //        bool act = true;

        //        if (Uptimes.Count != 0)
        //        {
        //            if (Uptimes[^1].Timestamp.AddSeconds(diff) < p.Timestamp)
        //            {
        //                act = false;
        //            }
        //        }

        //        Uptimes.Add(new Uptime(Uptimes.Count + 1, p.ProductionLineId, p.Timestamp, act));
        //    }

        //    return Uptimes;
        //}
    }
}
