using System.Collections.Generic;
using Backend_DTO.DTOs;

namespace Backend_Logic_Interface.Containers
{
    public interface IComponentContainer
    {
        public ComponentDTO GetComponent(int component_id);
        public List<ComponentDTO> GetComponents();
    }
}
