using System;
using System.Collections.Generic;
using System.Text;
using Backend_Logic_Interface.Models;

namespace Backend_Logic_Interface.Containers
{
    public interface IComponentContainer
    {
        public List<IComponent> GetComponents();
    }
}
