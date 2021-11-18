using System;
using System.Collections.Generic;
using System.Globalization;
using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Backend_Logic_Interface.Containers;

namespace Backend_Logic.Containers
{
    public class ComponentContainer : IComponentContainer
    {
        readonly IComponentDAL _componentDAL;
        public ComponentContainer(IComponentDAL componentDAL)
        {
            _componentDAL = componentDAL;
        }

        public ComponentDTO GetComponent(int component_id)
        {
            return _componentDAL.GetComponent(component_id);
        }

        public List<ComponentDTO> GetComponents()
        {
            List<ComponentDTO> Components = _componentDAL.GetComponents();
            return Components;

        }

        private string GetTimespanIndication(int differenceDays)
        {
            if (differenceDays <= 10) return "Days";
            if (differenceDays > 10 && differenceDays < 7 * 10) return "Weeks";
            if (differenceDays > 7 * 10 && differenceDays < 12 * 30 * 10) return "Months";
            if (differenceDays > 12 * 30 * 10 && differenceDays < 365 * 10) return "Years";
            else return "Decenia";
        }

        private int GetWeekOfYear(DateTime timestamp)
        {
            CultureInfo cul = CultureInfo.CurrentCulture;
            return cul.Calendar.GetWeekOfYear(timestamp, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        private string GetCurrentTimespan(string timespanIndicator, DateTime timestamp)
        {
            return timespanIndicator switch
            {
                "Days" => Convert.ToString(timestamp.Day),
                "Weeks" => Convert.ToString(GetWeekOfYear(timestamp)),
                "Months" => Convert.ToString(timestamp.Month),
                "Year" => Convert.ToString(timestamp.Year),
                _ => Convert.ToString(timestamp.Year / 10),
            };
        }

        public List<ProductionsDateDTO> GetPreviousActions(int component_id, DateTime beginDate, DateTime endDate)
        {
            List<ProductionsDTO> productions = _componentDAL.GetPreviousActions(component_id, beginDate, endDate);

            List<ProductionsDateDTO> ProductionDates = new();

            // First determine the timespan indicator
            string timespanIndicator = GetTimespanIndication((endDate - beginDate).Days);

            ProductionsDateDTO productionDate = new()
            {
                TimespanIndicator = timespanIndicator,
            };

            bool ChangeCurrentTimespan = true;
            int index = 0;
            foreach (ProductionsDTO production in productions)
            {
                if (ChangeCurrentTimespan)
                    productionDate.CurrentTimespan = GetCurrentTimespan(timespanIndicator, production.Timestamp);

                ChangeCurrentTimespan = false;
                productionDate.Productions++;
                
                ProductionsDTO previousProduction = index + 1 < productions.Count ? productions[index + 1] : productions[index];
                CultureInfo cul = CultureInfo.CurrentCulture;
                if ((timespanIndicator == "Days" && production.Timestamp.Day != previousProduction.Timestamp.Day)
                    || (timespanIndicator == "Weeks" && GetWeekOfYear(production.Timestamp) != GetWeekOfYear(previousProduction.Timestamp))
                    || (timespanIndicator == "Months" && production.Timestamp.Month != previousProduction.Timestamp.Month)
                    || (timespanIndicator == "Years" && production.Timestamp.Year != previousProduction.Timestamp.Year)
                    || (timespanIndicator == "Decenia" && production.Timestamp.Year / 10 != previousProduction.Timestamp.Year / 10))
                {
                    ChangeCurrentTimespan = true;
                    ProductionDates.Add(productionDate);
                    productionDate = new()
                    {
                        TimespanIndicator = timespanIndicator,
                    };
                }

                index++;
            }
            ProductionDates.Add(productionDate);

            return ProductionDates;
        }

        public void SetMaxActions(int component_id, int max_actions)
        {
            _componentDAL.SetMaxAction(component_id, max_actions);
        }
    }
}
