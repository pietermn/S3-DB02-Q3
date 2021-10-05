using System;
using System.Collections.Generic;
using System.Text;
using Backend_Logic.Models;
using Backend_Logic_Interface.Containers;
using Backend_Logic_Interface.Models;

namespace Backend_Logic.Containers
{
    public class ComponentContainer: IComponentContainer
    {
        public IComponent GetComponent()
        {
            return ConvertToIComponent();
        }

        public List<IComponent> GetComponents()
        {
            return ConvertToIComponentList();
        }

        private IComponent ConvertToIComponent()
        {
            return new Component(1, "", Enums.ComponentType.Coldhalf, "", 1, 1, 1, new List<IProductionLineHistory>());
        }
        private List<IComponent> ConvertToIComponentList()
        {
            return new List<IComponent>();
        }
    }
}
