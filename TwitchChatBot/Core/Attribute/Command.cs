using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChatBot.Core.Attribute
{
    public class Command : System.Attribute
    {
        public string NameCommand = "";
        public Command(string name)
        {
            NameCommand = name;
        }
    }
}
