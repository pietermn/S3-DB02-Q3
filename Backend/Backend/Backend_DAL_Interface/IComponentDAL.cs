using Backend_DTO.DTOs;
using System.Collections.Generic;

namespace Backend_DAL_Interface
{
    public interface IComponentDAL
    {
        public List<ComponentDTO> GetComponents();
        public ComponentDTO GetComponent(int component_id);
        public List<int> GetPreviousActions(int component_id, int amount, string type);
    }
}
