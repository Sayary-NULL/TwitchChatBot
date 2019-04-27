using TwitchChatBot.Core.Atribute;
using TwitchChatBot.Core.Module;
using TwitchLib.Client.Events;

namespace TwitchChatBot.Atribute
{
    class OnlyModeratorAttribute : PreconditionAttribute
    {
        public override PreconditionResult CheckPermissionsAsync(OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.IsModerator)
                return PreconditionResult.Successfully;
            else return PreconditionResult.Unsuccessfully;
        }
    }
}
