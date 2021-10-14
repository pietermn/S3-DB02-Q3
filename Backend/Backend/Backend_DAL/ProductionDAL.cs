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
        readonly Q3Context _Context;

        public ProductionDAL(Q3Context context)
        {
            _Context = context;
        }

        public List<ProductionsDTO> GetProductionsFromLastDay()
        {
            return _Context.Productions
                .Where(p => p.Timestamp > new DateTime(2020,9,30).AddDays(-1))
                .OrderBy(p => p.ProductionLineId)
                .AsNoTracking()
            .ToList();
        }

        public List<ProductionsDTO> GetProductionsByIdFromLastDay(int productionLine_id)
        {
            return _Context.Productions
                .Where(p => p.ProductionLineId == productionLine_id
                && p.Timestamp > new DateTime(2020, 9, 30).AddDays(-1))
                .OrderBy(p => p.Timestamp)
                .AsNoTracking()
            .ToList();
        }
    }
}
