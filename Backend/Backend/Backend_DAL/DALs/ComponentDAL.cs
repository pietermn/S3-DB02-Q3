﻿using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend_DAL.DALs
{
    public class ComponentDAL : IComponentDAL
    {
        readonly Q3Context _Context;
        public ComponentDAL(Q3Context context)
        {
            _Context = context;
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
