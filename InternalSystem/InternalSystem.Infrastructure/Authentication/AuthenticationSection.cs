using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace InternalSystem.Infrastructure.Authentication
{
    public class AuthenticationSection : ConfigurationSection
    {
        [ConfigurationProperty("users")]
        [ConfigurationCollection(typeof(UserCollection))]
        public UserCollection Users
        {
            get
            {
                return (UserCollection)base["users"];
            }
        }
    }
}
