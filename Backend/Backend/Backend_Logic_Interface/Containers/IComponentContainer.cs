using System;
using System.Collections.Generic;
using Backend_DTO.DTOs;

namespace Backend_Logic_Interface.Containers
{
    public interface IComponentContainer
    {
        public ComponentDTO GetComponent(int component_id);
        public List<ComponentDTO> GetComponents();
        public List<ProductionsDateDTO> GetPreviousActions(int component_id, DateTime beginDate, DateTime endDate);
        public List<ProductionsDateDTO> GetPredictedActions(int component_id, DateTime beginDate, DateTime endDate);
        public void SetMaxActions(int component_id, int max_actions);
        public DateTime PredictMaxActions(int component_id);
    }
}
