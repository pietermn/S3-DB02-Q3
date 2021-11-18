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
    }
}
