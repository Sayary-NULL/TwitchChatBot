using TwitchCoreAPI.Core.Interface;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchCoreAPI.Core.Module
{
    public class CommandContext : ICommandContext
    {
        public ChatMessage Context;
        public TwitchClient Client;

        public void SendMessage(string text, bool dryRun = false)
        {
            Client.SendMessage(Context.Channel, text, dryRun);
        }
        public void SetContext(OnMessageReceivedArgs e, TwitchClient cl)
        {
            Context = e.ChatMessage;
            Client = cl;
        }
    }
}
