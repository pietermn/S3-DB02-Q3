using System;
using System.Collections.Generic;
using System.Globalization;
using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Backend_Logic.Models;
using Backend_Logic_Interface.Containers;

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


        //private Component ConvertDTOToModel(List<ComponentDTO> componentDTOs)
        //{
        //    List<Component> components = new List<Component>();
        //    foreach (ComponentDTO d in componentDTOs)
        //    {
        //        List<ProductionLineHistory> history = new List<ProductionLineHistory>();
                
        //        foreach (ProductionLineHistoryDTO p in d.History)
        //        {
        //            ProductionLine line = new ProductionLine(p.ProductionLine.Id, p.ProductionLine.Name, p.ProductionLine.Description, p.ProductionLine.Active, p.ProductionLine.Port, p.ProductionLine.Board);
                    
        //            history.Add(new ProductionLineHistory(p.Id, line, p.StartDate, p.EndDate));
        //        }

        //        components.Add(new Component(d.Id, d.Name, d.Type, d.Description, d.TotalActions, d.MaxActions, d.CurrentActions, history, d.MaintenanceHistory));
        //    }
        //}

        public DateTime PredictMaxActions(int component_id)
        {
            DateTime MockDateNow = new DateTime(2021, 6, 1);

            ComponentDTO component = GetComponent(component_id);
            List<ProductionLineHistoryDTO> productionLineHistories = component.History;

            int tempCurrent= component.CurrentActions ;

            foreach(ProductionLineHistoryDTO p in productionLineHistories) 
            {
                if (p.EndDate >= MockDateNow)
                {
                int difference = (int)(p.StartDate - p.EndDate).TotalMinutes;
                int gem = CalaculateAverageProductions(p.ProductionLineId, component, MockDateNow);
                int TotalProductionsFromProductionLine = difference * gem;
                int minutesSpend=0;

                if(tempCurrent + TotalProductionsFromProductionLine >= component.MaxActions)
                {
                    minutesSpend = (component.MaxActions - component.CurrentActions) / gem;
                    return p.StartDate.AddMinutes(minutesSpend);
                }
                
                tempCurrent += TotalProductionsFromProductionLine;

                }
            }
            return new DateTime(1,1,1);
        }

        private int CalaculateAverageProductions(int productionLineId, ComponentDTO component, DateTime mockDate) //VERLEDEN
        {
            List<ProductionsDTO> productions = _productionDAL.GetAllProductionsFromProductionLine(productionLineId);
            

            List<ProductionLineHistoryDTO> productionLineHistories = component.History.Where(p => p.EndDate < mockDate && p.ProductionLineId == productionLineId).ToList();

            List<int> averages = new();

            foreach(ProductionLineHistoryDTO plH in productionLineHistories)
            {
                int AmountOfProductions = 0;
                int difference = (int)(plH.StartDate - plH.EndDate).TotalMinutes;
                foreach (ProductionsDTO p in productions)
                {

                    if(plH.StartDate <= p.Timestamp && p.Timestamp <= plH.EndDate)
                    {
                        AmountOfProductions++;
                    }
                }
                averages.Add(AmountOfProductions / difference);
            }
            int average = 0;
            foreach(int a in averages)
            {
                average += a;
            }
            return average / averages.Count();
        }


        //private int PredictedProductions(ProductionLineHistory productionLineHistory, Component component)
        //{
        //    List<ProductionsDTO> productions = _productionDAL.GetAllProductionsFromProductionLine(productionLineHistory.ProductionLine.Id);
        //    int AmountOfProductions = 0;
        //    foreach (ProductionsDTO p in productions)
        //    {
        //        if (productionLineHistory.StartDate <= p.Timestamp && p.Timestamp <= productionLineHistory.EndDate)
        //        {
        //            AmountOfProductions++;
        //        }
        //    }
        //    return 0;
        //}
    }
}
