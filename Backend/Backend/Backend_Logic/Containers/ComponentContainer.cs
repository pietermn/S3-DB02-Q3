using System;
using System.Collections.Generic;
using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Backend_Logic.Models;
using Backend_Logic_Interface.Containers;

namespace Backend_Logic.Containers
{
    public class ComponentContainer: IComponentContainer
    {
        readonly IComponentDAL _componentDAL;
        readonly IProductionLineDAL _productionLineDAL;

        public ComponentContainer(IComponentDAL componentDAL, IProductionLineDAL productionLineDAL)
        {
            _componentDAL = componentDAL;
            _productionLineDAL = productionLineDAL;
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

        public List<int> GetPreviousActions(int component_id, int amount, string type)
        {
            return _componentDAL.GetPreviousActions(component_id, amount, type);
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
            ComponentDTO component = GetComponent(component_id);
            List<ProductionLineHistoryDTO> productionLineHistories = new List<ProductionLineHistoryDTO>(); //TOEKOMST // component.History
            int tempCurrent= component.CurrentActions ;

            foreach(ProductionLineHistoryDTO p in productionLineHistories) 
            {
                int gem = CalaculateAverageProductions(p, component);
                int difference = (int)(p.StartDate - p.EndDate).TotalMinutes;
                int TotalProductionsFromProductionLine = difference * gem;
                int minutesSpend=0;

                if(tempCurrent + TotalProductionsFromProductionLine >= component.MaxActions)
                {
                    minutesSpend = (component.MaxActions - component.CurrentActions) / gem;
                    return p.StartDate.AddMinutes(minutesSpend);
                }
                
                tempCurrent += TotalProductionsFromProductionLine;
            }
            return new DateTime(1,1,1);
        }

        private int CalaculateAverageProductions(ProductionLineHistoryDTO productionLineHistory, ComponentDTO component) //VERLEDEN
        {
            int productions = 0; // Count van de productions van prodcutionLineHistory terwijl component erop zit 


            return 0;
        }
    }
}
