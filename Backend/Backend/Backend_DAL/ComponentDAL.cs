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

        public List<ProductionsDTO> GetPreviousActions(int component_id, DateTime beginDate, DateTime endDate)
        {
            ComponentDTO component = _Context.Components.Where(c => c.Id == component_id)
                .Include(c => c.History)
                .FirstOrDefault();

            List<ProductionsDTO> productions = new();
            foreach (ProductionLineHistoryDTO historyDTO in component.History)
            {
                List<ProductionsDTO> productionsDTOs = _Context.Productions.Where(
                    p => p.ProductionLineId == historyDTO.ProductionLineId 
                    && beginDate <= p.Timestamp 
                    && endDate >= p.Timestamp 
                    && historyDTO.StartDate <= p.Timestamp
                    && historyDTO.EndDate >= p.Timestamp)
                    .ToList();
                foreach (ProductionsDTO productionsDTO in productionsDTOs)
                {
                    productions.Add(productionsDTO);
                }
            }

            return productions.OrderBy(p => p.Timestamp).ToList();

            //List<int> actions = new List<int>();
            //for (int i = 0; i < amountOfWeeks; i++)
            //{
            //    int actionsThisWeek = 0;
            //    //DateTime firstDatetime = DateTime.Now.AddDays(i * -7);
            //    //DateTime secondDatetime = DateTime.Now.AddDays((i + 1) * -7);
            //    DateTime MockNowDate = new DateTime(2020, 9, 30);

            //    // Default sorting is by weeks
            //    DateTime firstDatetime = MockNowDate.AddDays(i * -7);
            //    DateTime secondDatetime = firstDatetime.AddDays(-7);

            //    if (type == "months")
            //    {
            //        firstDatetime = MockNowDate.AddMonths(-i);
            //        secondDatetime = firstDatetime.AddMonths(-1);
            //    }


            //    foreach (ProductionsDTO production in productions)
            //    {
            //        if (production.Timestamp < firstDatetime && production.Timestamp > secondDatetime)
            //        {
            //            actionsThisWeek++;
            //        }
            //    }
            //    actions.Add(actionsThisWeek);
            //}

            //return actions;
        }

        public void SetMaxAction(int component_id, int max_actions)
        {
            _Context.Components.Where(c => c.Id == component_id).FirstOrDefault().MaxActions = max_actions;
            _Context.SaveChanges();
        }
    }
}
