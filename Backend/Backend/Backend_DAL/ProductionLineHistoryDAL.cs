using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Backend_DAL
{
    public class ProductionLineHistoryDAL : IProductionLineHistoryDAL
    {
        readonly Q3Context _Context;

        public ProductionLineHistoryDAL(Q3Context context)
        {
            _Context = context;
        }

        public ProductionLineHistoryDTO GetProductionLineHistory(int ProductionLineHistory_id)
        {
            return _Context.ProductionLinesHistory
                .Where(plh => plh.Id == ProductionLineHistory_id)
                    .AsNoTracking()
                .FirstOrDefault();
        }

        public List<ProductionLineHistoryDTO> GetProductionLinesHistory()
        {
            return _Context.ProductionLinesHistory
                    .AsNoTracking()
                .ToList();
        }
    }
}
