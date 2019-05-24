using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChatBot.Core.Attribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AliasAttribute : System.Attribute
    {
        public string NameAlias = "";

        public AliasAttribute(string name)
        {
            NameAlias = name;
        }
    }
}
