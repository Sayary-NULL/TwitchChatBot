using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace TwitchChatBot.Core.Interface
{
    interface ICommandContext
    {
        void SetContext(OnMessageReceivedArgs e, TwitchClient cl);
    }
}
