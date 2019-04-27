using System;
using TwitchChatBot.Core.Module;
using TwitchLib.Client.Events;

namespace TwitchChatBot.Core.Atribute
{
    abstract class PreconditionAttribute : System.Attribute
    {
        public abstract PreconditionResult CheckPermissionsAsync(OnMessageReceivedArgs e);       
    }
}
