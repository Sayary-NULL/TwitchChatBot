using TwitchCoreAPI.Core.Attribute;
using TwitchCoreAPI.Core.Result;
using TwitchLib.Client.Events;

namespace TwitchChatBot.Attribute
{
    class OnlyOwner : PreconditionAttribute
    {
        public override PreconditionResult CheckPermissions(OnMessageReceivedArgs e)
        {
            PreconditionResult res = new PreconditionResult();
            if (e.ChatMessage.UserId == "420174531")
                res.Successfully();
            else res.Unsuccessful("Эта команда только для Sayary_NULL");
            return res;
        }
    }
}
