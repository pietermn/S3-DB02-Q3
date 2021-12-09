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

        private int GetWeekOfYear(DateTime timestamp)
        {
            CultureInfo cul = CultureInfo.CurrentCulture;
            return cul.Calendar.GetWeekOfYear(timestamp, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        private List<ProductionsDateDTO> FillProductionDates(int dayDifference, DateTime beginDate)
        {
            List<ProductionsDateDTO> productionsDateDTOs = new();

            if (dayDifference <= 10)
            {
                for(int i = 0; i < dayDifference; i++)
                {
                    productionsDateDTOs.Add(new ProductionsDateDTO()
                    {
                        TimespanIndicator = "Day",
                        CurrentTimespan = beginDate.AddDays(-i).Day.ToString()
                    });
                }
            }
            else if (dayDifference > 10 && dayDifference < 7 * 10)
            {
                int forloopMax = dayDifference % 7 == 0 ? dayDifference / 7 : dayDifference / 7 + 1;
                for (int i = 0; i < forloopMax; i++)
                {
                    productionsDateDTOs.Add(new ProductionsDateDTO()
                    {
                        TimespanIndicator = "Week",
                        CurrentTimespan = GetWeekOfYear(beginDate.AddDays(-i * 7)).ToString()
                    });
                }
            }
            else if (dayDifference > 7 * 10 && dayDifference < 30 * 10)
            {
                int forloopMax = dayDifference % 30 == 0 ? dayDifference / 30 : dayDifference / 30 + 1;
                for (int i = 0; i < forloopMax; i++)
                {
                    productionsDateDTOs.Add(new ProductionsDateDTO()
                    {
                        TimespanIndicator = "Month",
                        CurrentTimespan = beginDate.AddMonths(-i).ToString("MMMM")
                    });
                }
            }
            else if (dayDifference > 30 * 10 && dayDifference < 365 * 10)
            {
                int forloopMax = dayDifference % 365 == 0 ? dayDifference / 365 : dayDifference / 365 + 1;
                for (int i = 0; i < forloopMax; i++)
                {
                    productionsDateDTOs.Add(new ProductionsDateDTO()
                    {
                        TimespanIndicator = "Year",
                        CurrentTimespan = beginDate.AddYears(-i).Year.ToString()
                    });
                }
            }
            else {
                int forloopMax = dayDifference % 3650 == 0 ? dayDifference / 3650 : dayDifference / 3650 + 1;

                for (int i = 0; i < forloopMax; i++)
                {
                    productionsDateDTOs.Add(new ProductionsDateDTO()
                    {
                        TimespanIndicator = "Decenium",
                        CurrentTimespan = (beginDate.AddYears(-i * 10).Year / 10).ToString()
                    });
                }
            }

            return productionsDateDTOs;
        }

        public List<ProductionsDateDTO> GetPreviousActions(int component_id, DateTime beginDate, DateTime endDate)
        {
            List<ProductionsDTO> productions = _componentDAL.GetPreviousActions(component_id, beginDate, endDate);

            List<ProductionsDateDTO> ProductionDates = FillProductionDates((endDate - beginDate).Days, endDate);

            foreach (ProductionsDTO production in productions)
            {
                foreach(ProductionsDateDTO productionsDate in ProductionDates)
                {
                    string timespanIndicator = productionsDate.TimespanIndicator;
                    if ((timespanIndicator == "Day" && productionsDate.CurrentTimespan == production.Timestamp.Day.ToString())
                        || (timespanIndicator == "Week" && productionsDate.CurrentTimespan == GetWeekOfYear(production.Timestamp).ToString())
                        || (timespanIndicator == "Month" && productionsDate.CurrentTimespan == production.Timestamp.ToString("MMMM"))
                        || (timespanIndicator == "Year" && productionsDate.CurrentTimespan == production.Timestamp.Year.ToString())
                        || (timespanIndicator == "Decenium" && productionsDate.CurrentTimespan == (production.Timestamp.Year / 10).ToString()))
                    {
                        productionsDate.Productions++;
                    }
                }
            }

            return ProductionDates;
        }

        public void SetMaxActions(int component_id, int max_actions)
        {
            _componentDAL.SetMaxAction(component_id, max_actions);
        }
    }
}
