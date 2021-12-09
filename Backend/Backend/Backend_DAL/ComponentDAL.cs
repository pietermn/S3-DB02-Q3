using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend_DAL
{
    public class ComponentDAL : IComponentDAL
    {
        public Q3Context _Context = new();

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

        public List<ProductionsDTO> GetPreviousActions(int component_id, DateTime beginDate, DateTime endDate)
        {
            ComponentDTO component = _Context.Components.Where(c => c.Id == component_id)
                .Include(c => c.History)
                .FirstOrDefault();

            List<ProductionsDTO> productions = new();

            int monthDifference = ((beginDate.Year - endDate.Year) * 12) + (endDate.Month - beginDate.Month);
            if (endDate.Year > beginDate.Year) monthDifference *= -1;

            if (endDate < beginDate) return new List<ProductionsDTO>();

            for (int i = 0; i <= monthDifference; i++)
            {
                DateTime currentDatetime = beginDate.AddMonths(i);
                if (currentDatetime >= new DateTime(2020, 9, 1) && currentDatetime < new DateTime(2021, 10, 1))
                {
                    _Context = new Q3Context(currentDatetime);
                    List<ProductionsDTO> monthlyProductions =
                    _Context.Productions.Where(
                        p => beginDate <= p.Timestamp
                        && endDate >= p.Timestamp)
                    .AsNoTracking()
                        .ToList();

                    foreach (ProductionsDTO productionsDTO in monthlyProductions)
                    {
                        foreach (ProductionLineHistoryDTO historyDTO in component.History)
                        {
                            if (productionsDTO.ProductionLineId == historyDTO.ProductionLineId
                                && productionsDTO.Timestamp > historyDTO.StartDate
                                && productionsDTO.Timestamp < historyDTO.EndDate)
                            {
                                productions.Add(productionsDTO);
                            }
                        }
                    }
                }
            }

            return productions.OrderBy(p => p.Timestamp).ToList();
        }

        public void SetMaxAction(int component_id, int max_actions)
        {
            _Context.Components.Where(c => c.Id == component_id).FirstOrDefault().MaxActions = max_actions;
            _Context.SaveChanges();
        }
    }
}
