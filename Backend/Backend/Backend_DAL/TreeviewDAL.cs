using System;
using System.Collections.Generic;
using System.Linq;
using Backend_DAL.DataAccess.DataObjects;
using Backend_DAL_Interface;
using Backend_DTO;

namespace Backend_DAL
{
    public class TreeviewDAL : ITreeviewDAL
    {
        private readonly q3_mms_dbContext _Context;

        public TreeviewDAL(q3_mms_dbContext context)
        {
            _Context = context;
        }

        public TreeviewDTO Get(TreeviewDTO treeviewDTO)
        {
            return _Context.Treeviews.Where(u => u.Parent == treeviewDTO.Parent).FirstOrDefault();
        }

        public List<TreeviewDTO> GetAll()
        {
            var treeviews = _Context.Treeviews;
            return treeviews.ToList();
        }
    }
}
