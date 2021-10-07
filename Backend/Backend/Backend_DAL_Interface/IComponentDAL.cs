using Backend_DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_DAL_Interface
{
    public interface IComponentDAL
    {
        public List<ComponentDTO> GetComponents();
    }
}
