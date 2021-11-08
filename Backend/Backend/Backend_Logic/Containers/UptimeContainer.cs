using System.Collections.Generic;
using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Backend_Logic_Interface.Containers;
using Backend_Logic_Interface.Models;
using Backend_Logic.Models;
using System.Linq;
using System;

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
            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int second = DateTime.Now.Second;
            DateTime fakeNow = new(2020, 9, 30, hour, minute, second);

            if (productions.Count == 0)
            {
                return new() { new Uptime(0, productionLine_id, System.DateTime.Now.AddDays(-1), System.DateTime.Now, false) };
            }

            Uptime firstUptime = new(count, productionLine_id, fakeNow.AddDays(-1), productions[0].Timestamp, false);
            count++;
            uptimes.Add(firstUptime);

            Uptime current = new(count, productions[0].ProductionLineId, productions[0].Timestamp, productions[0].Timestamp.AddSeconds(productions[0].ShotTime), true);

            foreach (ProductionsDTO p in productions.Skip(1))
            {
                double diff = p.ShotTime * 6;

                if (p.Timestamp >= current.End.AddSeconds(diff)) // Down
                {
                    uptimes.Add(current);
                    count++;

                    uptimes.Add(new Uptime(count, p.ProductionLineId, current.End, p.Timestamp, false));
                    count++;

                    current = new Uptime(count, p.ProductionLineId, p.Timestamp, p.Timestamp.AddSeconds(p.ShotTime), true);
                }
                else // Up
                {
                    current.End = p.Timestamp;
                }
            }

            Uptime lastUptime = new(uptimes.Count + 1, productionLine_id, productions[^1].Timestamp.AddSeconds(productions[^1].ShotTime), fakeNow, false);

            uptimes.Add(current);
            uptimes.Add(lastUptime);

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
