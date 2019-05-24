using TwitchChatBot.Core.Attribute;
using TwitchChatBot.Core.Result;
using TwitchChatBot.Module;
using TwitchLib.Client.Events;

namespace TwitchChatBot.Attribute
{
    class OnlyOnBot : PreconditionAttribute
    {
        public override PreconditionResult CheckPermissions(OnMessageReceivedArgs e)
        {
            PreconditionResult res = new PreconditionResult();
            if (ConstVaribtls.IsDebuge)
            {
                res.Successfully();
                return res;
            }

            if (ConstVaribtls.StartBot)
            {
                res.Successfully();
            }
            else res.Unsuccessful("Бот выключен!");

            return res;
        }
    }
}
