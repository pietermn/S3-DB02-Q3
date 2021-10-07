using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend_DAL
{
    public class ComponentDAL : IComponentDAL
    {
        readonly Q3Context _Context;
        public ComponentDAL(Q3Context context)
        {
            _Context = context;
        }

        public ComponentDTO GetComponent(int component_id)
        {
            return _Context.Components
                .Where(c => c.Id == component_id)
                .Include(c => c.History)
                    .ThenInclude(h => h.ProductionLine)
                    .AsNoTracking()
                .FirstOrDefault();
        }

        public List<ComponentDTO> GetComponents()
        {
            return _Context.Components
                .Include(c => c.History)
                    .ThenInclude(h => h.ProductionLine)
                    .AsNoTracking()
                .ToList();
        }
    }
}
