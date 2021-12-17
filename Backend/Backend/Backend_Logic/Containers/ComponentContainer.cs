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

        private List<ProductionsDateDTO> FillProductionDates(int dayDifference, DateTime endDate, DateTime beginDate)
        {
            List<ProductionsDateDTO> productionsDateDTOs = new();

            if (dayDifference <= 10)
            {
                for (int i = 0; i < dayDifference; i++)
                {
                    productionsDateDTOs.Add(new ProductionsDateDTO()
                    {
                        TimespanIndicator = "Day",
                        CurrentTimespan = endDate.AddDays(-i).Day.ToString(),
                        CurrentDateTime = endDate.AddDays(-i)
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
                        CurrentTimespan = GetWeekOfYear(endDate.AddDays(-i * 7)).ToString(),
                        CurrentDateTime = endDate.AddDays(-i * 7)
                    });
                }
            }
            else if (dayDifference > 7 * 10 && dayDifference < 30 * 10)
            {
                int forloopMax = dayDifference % 30 == 0 ? dayDifference / 30 : dayDifference / 30 + 1;
                for (int i = 0; i < forloopMax; i++)
                {
                    DateTime currentDatetime = new(endDate.AddMonths(-i).Year, endDate.AddMonths(-i).Month, i == forloopMax - 1 ? beginDate.Day : 1);
                    productionsDateDTOs.Add(new ProductionsDateDTO()
                    {
                        TimespanIndicator = "Month",
                        CurrentTimespan = endDate.AddMonths(-i).ToString("MMMM"),
                        CurrentDateTime = currentDatetime
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
                        CurrentTimespan = endDate.AddYears(-i).Year.ToString(),
                        CurrentDateTime = endDate.AddYears(-i)
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
                        CurrentTimespan = (endDate.AddYears(-i * 10).Year / 10).ToString(),
                        CurrentDateTime = endDate.AddYears(-i * 10)
                    });
                }
            }

            return productionsDateDTOs;
        }

        public List<ProductionsDateDTO> GetPreviousActions(int component_id, DateTime beginDate, DateTime endDate)
        {
            DateTime mockDate = new(2021, 6, 1);
            DateTime newEndDate = mockDate > endDate ? endDate : mockDate;

            ComponentDTO component = _componentDAL.GetComponent(component_id);

            List<ProductionsDateDTO> ProductionDates = FillProductionDates((endDate - beginDate).Days, endDate, beginDate).OrderBy(pd => pd.CurrentDateTime).ToList();
            List<ProductionsDateDTO> newProductionDates = new();

            foreach (ProductionsDateDTO productionsDateDTO in ProductionDates.OrderBy(p => p.CurrentDateTime))
            {
                if (mockDate > productionsDateDTO.CurrentDateTime && productionsDateDTO.CurrentDateTime >= new DateTime(2020, 9, 1))
                {
                    newProductionDates.Add(productionsDateDTO);
                }
            }

            for (int i = 0; i < newProductionDates.Count; i++)
            {
                List<ProductionsDateTimespanDTO> timespans = new();
                foreach (ProductionLineHistoryDTO history in component.History.OrderBy(h => h.StartDate).ToList())
                {
                    DateTime newHistoryEndDate = history.EndDate.Year == 1 ? endDate : history.EndDate;
                    try
                    {
                        if (history.StartDate < ProductionDates[i + 1].CurrentDateTime && history.EndDate > ProductionDates[i].CurrentDateTime)
                        {
                            ProductionsDateTimespanDTO timespan = new()
                            {
                                ProductionLineId = history.ProductionLineId,
                                Begin = history.StartDate > ProductionDates[i].CurrentDateTime ? history.StartDate : ProductionDates[i].CurrentDateTime,
                                End = history.EndDate < ProductionDates[i + 1].CurrentDateTime ? history.EndDate : ProductionDates[i + 1].CurrentDateTime.AddSeconds(-1)
                            };
                            timespans.Add(timespan);
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        if (history.StartDate < newHistoryEndDate && newHistoryEndDate > newProductionDates[i].CurrentDateTime && history.StartDate < endDate)
                        {
                            ProductionsDateTimespanDTO timespan = new()
                            {
                                ProductionLineId = history.ProductionLineId,
                                Begin = history.StartDate > ProductionDates[i].CurrentDateTime ? history.StartDate : ProductionDates[i].CurrentDateTime,
                                End = newHistoryEndDate
                            };
                            timespans.Add(timespan);
                        }
                    }
                }
                if (timespans.Any())
                {
                    ProductionDates[i].Productions = _componentDAL.GetPreviousActionsPerDate(timespans);
                }
            }

            return newProductionDates;
        }

        public void SetMaxActions(int component_id, int max_actions)
        {
            _componentDAL.SetMaxAction(component_id, max_actions);
        }

        private static async Task<int> GetAverageProductions(DateTime begin, DateTime end, int componentId, int productionlineId)
        {
            long beginTimestamp = ((DateTimeOffset)begin).ToUnixTimeSeconds();
            long endTimestamp = ((DateTimeOffset)end).ToUnixTimeSeconds();

            var data = await $"http://ml-python:5000/averageactions/{beginTimestamp}/{endTimestamp}/{componentId}/{productionlineId}".GetAsync();
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

        public List<ProductionsDateDTO> GetPredictedActions(int component_id, DateTime beginDate, DateTime endDate)
        {
            DateTime mockDate = new(2021, 6, 1);

            if (endDate < mockDate || beginDate > endDate)
            {
                return new();
            }

            List<ProductionsDateDTO> productionsDates = FillProductionDates((int)(endDate - beginDate).TotalDays, endDate, beginDate);
            List<ProductionsDateDTO> newProductionDates = new();
            List<ProductionLineHistoryDTO> historys = _componentDAL.GetComponent(component_id).History;

            foreach (ProductionsDateDTO newProductionsDate in productionsDates.OrderBy(p => p.CurrentDateTime))
            {
                if (mockDate <= newProductionsDate.CurrentDateTime)
                {
                    newProductionsDate.IsPredicted = true;
                    newProductionDates.Add(newProductionsDate);
                }
            }

            foreach (ProductionLineHistoryDTO history in historys)
            {
                if (history.EndDate >= beginDate && history.StartDate <= endDate || history.EndDate.Year == 1)
                {
                    DateTime newEndDate = history.EndDate.Year == 1 ? endDate : history.EndDate;
                    int average = GetAverageProductions(history.StartDate, newEndDate, component_id, history.ProductionLineId).Result;

                    for (int i = 0; i < newProductionDates.Count; i++)
                    {
                        try
                        {
                            if (history.StartDate < newProductionDates[i + 1].CurrentDateTime && newEndDate > newProductionDates[i].CurrentDateTime)
                            {
                                DateTime currentBeginDate = history.StartDate < newProductionDates[i].CurrentDateTime ? newProductionDates[i].CurrentDateTime : history.StartDate;
                                DateTime currentEndDate = newEndDate > newProductionDates[i + 1].CurrentDateTime ? newProductionDates[i + 1].CurrentDateTime : newEndDate;

                                newProductionDates[i].Productions += (int)(currentEndDate - currentBeginDate).TotalMinutes * average;
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            if (newProductionDates[i].TimespanIndicator == "Day")
                            {
                                if (history.StartDate < newEndDate && newEndDate > newProductionDates[i].CurrentDateTime)
                                {
                                    DateTime currentBeginDate = history.StartDate < newProductionDates[i].CurrentDateTime ? newProductionDates[i].CurrentDateTime : history.StartDate;
                                    newProductionDates[i].Productions += (int)(endDate.AddDays(1) - currentBeginDate).TotalMinutes * average;
                                }
                            }
                            else
                            {
                                if (history.StartDate < newEndDate && newEndDate > newProductionDates[i].CurrentDateTime)
                                {
                                    DateTime currentBeginDate = history.StartDate < newProductionDates[i].CurrentDateTime ? newProductionDates[i].CurrentDateTime : history.StartDate;
                                    newProductionDates[i].Productions += (int)(endDate - currentBeginDate).TotalMinutes * average;
                                }
                            }

                        }
                    }
                }
            }

            return newProductionDates;
        }
    }
}
