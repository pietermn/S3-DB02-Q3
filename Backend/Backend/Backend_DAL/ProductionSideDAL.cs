using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Backend_DAL
{
    public class ProductionSideDAL : IProductionSideDAL
    {
        readonly Q3Context _Context;

        public ProductionSideDAL(Q3Context context)
        {
            _Context = context;
        }

        public ProductionSideDTO GetProductionSide(int ProductionSide_id)
        {
            return _Context.ProductionSides
                .Where(ps => ps.Id == ProductionSide_id)
                .Include(ps => ps.ProductionLines)
                    .AsNoTracking()
                .FirstOrDefault();
        }

        public List<ProductionSideDTO> GetProductionSides()
        {
            return _Context.ProductionSides
                .Include(ps => ps.ProductionLines)
                    .AsNoTracking()
                .ToList();
        }
    }
}
