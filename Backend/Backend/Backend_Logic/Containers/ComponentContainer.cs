using System;
using System.Collections.Generic;
using System.Text;
using Backend_Logic_Interface.Containers;
using Backend_Logic_Interface.Models;

namespace Backend_Logic.Containers
{
    public class ComponentContainer: IComponentContainer
    {
        public List<IComponent> GetComponents()
        {
            return ConvertToIComponentList();
        }



        private List<IComponent> ConvertToIComponentList()
        {
            return new List<IComponent>();
        }
    }
}
