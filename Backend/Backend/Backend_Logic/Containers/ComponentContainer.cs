using System;
using System.Collections.Generic;
using System.Text;
using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Backend_Logic.Models;
using Backend_Logic_Interface.Containers;
using Backend_Logic_Interface.Models;

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
            return _componentDAL.GetComponents();
        }

        public List<int> GetPreviousActions(int component_id, int amountOfWeeks)
        {
            return _componentDAL.GetPreviousActions(component_id, amountOfWeeks);
        }
    }
}
