using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalSystem.Infrastructure.Authentication
{
    public class UserCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new UserElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((UserElement)element).Name;
        }

        public UserElement this[int index]
        {
            get
            {
                return (UserElement)BaseGet(index);
            }
        }
    }
}
