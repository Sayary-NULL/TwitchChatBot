using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchChatBot.Core.Interface;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchChatBot.Core.Module
{
    class CommandContext : ICommandContext
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
