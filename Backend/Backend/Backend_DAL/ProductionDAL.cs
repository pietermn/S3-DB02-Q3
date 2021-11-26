using System;
using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Backend_DAL
{
    public class ProductionDAL : IProductionDAL
    {
        public Q3Context _Context = new();

        public List<ProductionsDTO> GetProductionsFromLastDay()
        {
            return _Context.Productions
                .Where(p => p.Timestamp > new DateTime(2020, 9, 30).AddDays(-1))
                .OrderBy(p => p.ProductionLineId)
                .AsNoTracking()
            .ToList();
        }

        public List<ProductionsDTO> GetProductionsByIdFromLastDay(int productionLine_id)
        {
            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int second = DateTime.Now.Second;
            DateTime fakeNow = new(2020, 9, 25, hour, minute, second);

            if (fakeNow >= new DateTime(2020, 9, 1) && fakeNow.AddDays(1) < new DateTime(2021, 11, 1))
            {
                _Context = new Q3Context(fakeNow);
                return
                _Context.Productions.Where(
                    p => fakeNow <= p.Timestamp
                    && fakeNow.AddDays(1) >= p.Timestamp
                    && p.ProductionLineId == productionLine_id)
                .OrderBy(p => p.Timestamp)
                    .ToList();
            }

            return new List<ProductionsDTO>();
        }
    }
}
