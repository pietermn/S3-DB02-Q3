using System;
using System.Collections.Generic;
using Backend_DTO;

namespace Backend_DAL_Interface
{
    public interface ITreeviewDAL
    {
        public TreeviewDTO Get(TreeviewDTO treeviewDTO);

        public List<TreeviewDTO> GetAll();
    }
}
