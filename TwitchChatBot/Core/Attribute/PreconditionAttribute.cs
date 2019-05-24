using System;
using TwitchChatBot.Core.Result;
using TwitchLib.Client.Events;

namespace TwitchChatBot.Core.Attribute
{
    abstract class PreconditionAttribute : System.Attribute
    {
        public abstract PreconditionResult CheckPermissions(OnMessageReceivedArgs e);       
    }
}
