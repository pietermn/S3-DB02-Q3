using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Backend_Logic.Models;
using Backend_Logic_Interface.Containers;
using Flurl.Http;

namespace Backend_Logic.Containers
{
    public class ComponentContainer : IComponentContainer
    {
        readonly IComponentDAL _componentDAL;
        readonly IProductionLineDAL _productionLineDAL;
        readonly IProductionDAL _productionDAL;

        public ComponentContainer(IComponentDAL componentDAL, IProductionLineDAL productionLineDAL, IProductionDAL productionDAL)
        {
            _componentDAL = componentDAL;
            _productionLineDAL = productionLineDAL;
            _productionDAL = productionDAL;
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
                for (int i = 0; i < dayDifference; i++)
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
            else
            {
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
            DateTime mockDate = new DateTime(2021, 6, 1);

            List<ProductionsDTO> productions = _componentDAL.GetPreviousActions(component_id, beginDate, endDate);

            List<ProductionsDateDTO> ProductionDates = FillProductionDates((endDate - beginDate).Days, endDate);

            foreach (ProductionsDTO production in productions)
            {
                if (production.Timestamp < mockDate)
                {
                    foreach (ProductionsDateDTO productionsDate in ProductionDates)
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
            }

            ComponentDTO component = _componentDAL.GetComponent(component_id);

            foreach (ProductionLineHistoryDTO history in component.History.Where(h => h.EndDate > mockDate).OrderBy(h => h.StartDate))
            {
                DateTime newBeginDate = history.StartDate > mockDate ? history.StartDate : mockDate;

                ProductionDates.Reverse();

                foreach (ProductionsDateDTO productionsDate in ProductionDates)
                {
                    switch(productionsDate.TimespanIndicator)
                    {
                        case "Day":
                            DateTime newEndDateDay = newBeginDate.AddDays(1) < history.EndDate ? newBeginDate.AddDays(1) : history.EndDate;

                            if (newBeginDate < newEndDateDay)
                            {
                                int productionsInTimespan = ProductionsBetweenTimes(newBeginDate, newEndDateDay, component_id, history.ProductionLineId);
                                productionsDate.Productions = productionsInTimespan;
                                newBeginDate = newBeginDate.AddDays(1);
                            }
                            break;
                        case "Week":
                            DateTime newEndDateWeek = newBeginDate.AddDays(7) < history.EndDate ? newBeginDate.AddDays(7) : history.EndDate;

                            if (newBeginDate < newEndDateWeek)
                            {
                                int productionsInTimespan = ProductionsBetweenTimes(newBeginDate, newEndDateWeek, component_id, history.ProductionLineId);
                                productionsDate.Productions = productionsInTimespan;
                                newBeginDate = newBeginDate.AddDays(7);
                            }
                            break;
                        case "Month":
                            DateTime newEndDateMonth = newBeginDate.AddMonths(1) < history.EndDate ? newBeginDate.AddMonths(1) : history.EndDate;

                            if (newBeginDate < newEndDateMonth)
                            {
                                int productionsInTimespan = ProductionsBetweenTimes(newBeginDate, newEndDateMonth, component_id, history.ProductionLineId);
                                productionsDate.Productions = productionsInTimespan;
                                newBeginDate = newBeginDate.AddMonths(1);
                            }
                            break;
                        case "Year":
                            DateTime newEndDateYear = newBeginDate.AddYears(1) < history.EndDate ? newBeginDate.AddYears(1) : history.EndDate;

                            if (newBeginDate < newEndDateYear)
                            {
                                int productionsInTimespan = ProductionsBetweenTimes(newBeginDate, newEndDateYear, component_id, history.ProductionLineId);
                                productionsDate.Productions = productionsInTimespan;
                                newBeginDate = newBeginDate.AddYears(1);
                            }
                            break;
                        case "Decenium":
                            DateTime newEndDateDecenium = newBeginDate.AddYears(10) < history.EndDate ? newBeginDate.AddYears(10) : history.EndDate;

                            if (newBeginDate < newEndDateDecenium)
                            {
                                int productionsInTimespan = ProductionsBetweenTimes(newBeginDate, newEndDateDecenium, component_id, history.ProductionLineId);
                                productionsDate.Productions = productionsInTimespan;
                                newBeginDate = newBeginDate.AddYears(10);
                            }
                            break;

                    }


                }
            }


            return ProductionDates;
        }

        private int ProductionsBetweenTimes(DateTime begin, DateTime end, int componentId, int productionLineId)
        {
            int differenceMinutes = (int)(end - begin).TotalMinutes;
            int average = GetAverageProductions(begin, end, componentId, productionLineId).Result;

            return differenceMinutes * average;
        }

        public void SetMaxActions(int component_id, int max_actions)
        {
            _componentDAL.SetMaxAction(component_id, max_actions);
        }

        private static async Task<int> GetAverageProductions(DateTime begin, DateTime end, int componentId, int productionlineId)
        {
            long beginTimestamp = ((DateTimeOffset)begin).ToUnixTimeSeconds();
            long endTimestamp = ((DateTimeOffset)end).ToUnixTimeSeconds();

            var data = await $"http://localhost:5600/averageactions/{beginTimestamp}/{endTimestamp}/{componentId}/{productionlineId}".GetAsync();
            string value = data.GetStringAsync().Result;
            int valueInt = Convert.ToInt32(value.Split(".")[0]);
            return Convert.ToInt32(valueInt) / 60;
        }


        public DateTime PredictMaxActions(int component_id)
        {
            DateTime MockDateNow = new(2021, 6, 1);

            ComponentDTO component = GetComponent(component_id);
            List<ProductionLineHistoryDTO> productionLineHistories = component.History.OrderBy(h => h.StartDate).ToList();

            int tempCurrent = component.CurrentActions;

            if (component.CurrentActions >= component.MaxActions)
            {
                return MockDateNow;
            }

            foreach (ProductionLineHistoryDTO p in productionLineHistories)
            {
                if (p.EndDate >= MockDateNow || p.EndDate.ToString("yyyy-MM-dd") == "0001-01-01")
                {
                    DateTime differenceDatetime = p.StartDate > MockDateNow ? p.StartDate : MockDateNow;
                    int difference = p.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" ? (int)(p.EndDate - differenceDatetime).TotalMinutes : (int)(differenceDatetime - new DateTime(2021 - 08 - 30)).TotalMinutes;
                    int avg = GetAverageProductions(p.StartDate, p.EndDate, component_id, p.ProductionLineId).Result;
                    int TotalProductionsFromProductionLine = difference * avg;
                    int minutesSpend = 0;

                    if (tempCurrent + TotalProductionsFromProductionLine >= component.MaxActions)
                    {
                        minutesSpend = (component.MaxActions - component.CurrentActions) / avg;
                        return differenceDatetime.AddMinutes(minutesSpend);
                    }

                    tempCurrent += TotalProductionsFromProductionLine;

                }
            }
            return new DateTime(1, 1, 1);
        }
    }
}
