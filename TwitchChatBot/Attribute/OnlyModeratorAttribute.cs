using TwitchCoreAPI.Core.Attribute;
using TwitchCoreAPI.Core.Result;
using TwitchLib.Client.Events;

namespace TwitchChatBot.Attribute
{
    class OnlyModeratorAttribute : PreconditionAttribute
    {
        public override PreconditionResult CheckPermissions(OnMessageReceivedArgs e)
        {
            PreconditionResult res = new PreconditionResult();
            if (e.ChatMessage.IsModerator || e.ChatMessage.IsBroadcaster)
            {
                res.Successfully();
            }
            else res.Unsuccessful("Нет доступа!");
            return res;
        }
    }
}
