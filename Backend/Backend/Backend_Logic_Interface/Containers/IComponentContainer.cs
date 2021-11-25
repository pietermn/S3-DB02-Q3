using System;
using System.Collections.Generic;
using Backend_DTO.DTOs;

namespace Backend_Logic_Interface.Containers
{
    public interface IComponentContainer
    {
        public ComponentDTO GetComponent(int component_id);
        public List<ComponentDTO> GetComponents();
        public List<int> GetPreviousActions(int component_id, int amount, string type);
        public void SetMaxActions(int component_id, int max_actions);
        public DateTime PredictMaxActions(int component_id);
    }
}
