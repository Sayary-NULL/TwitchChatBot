using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace TwitchCoreAPI.Core.Interface
{
    interface ICommandContext
    {
        void SetContext(OnMessageReceivedArgs e, TwitchClient cl);
    }
}
