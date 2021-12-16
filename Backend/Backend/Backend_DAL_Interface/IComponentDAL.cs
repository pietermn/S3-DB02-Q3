using Backend_DTO.DTOs;
using System;
using System.Collections.Generic;

namespace Backend_DAL_Interface
{
    public interface IComponentDAL
    {
        public List<ComponentDTO> GetComponents();
        public ComponentDTO GetComponent(int component_id);
        public List<ProductionsDTO> GetPreviousActions(int component_id, DateTime beginDate, DateTime endDate);
        public void SetMaxAction(int component_id, int max_actions);
        public int GetPreviousActionsPerDate(List<ProductionsDateTimespanDTO> timespans);
    }
}
