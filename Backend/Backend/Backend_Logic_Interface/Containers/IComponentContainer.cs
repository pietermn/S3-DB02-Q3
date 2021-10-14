using System;
using System.Collections.Generic;
using System.Text;
using Backend_DTO.DTOs;
using Backend_Logic_Interface.Models;

namespace Backend_Logic_Interface.Containers
{
    public interface IComponentContainer
    {
        public ComponentDTO GetComponent(int component_id);
        public List<ComponentDTO> GetComponents();
        public List<int> GetPreviousActions(int component_id, int amountOfWeeks);
    }
}
