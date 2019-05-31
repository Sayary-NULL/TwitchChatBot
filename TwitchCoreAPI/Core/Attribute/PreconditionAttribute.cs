using TwitchCoreAPI.Core.Result;
using TwitchLib.Client.Events;

namespace TwitchCoreAPI.Core.Attribute
{
    public abstract class PreconditionAttribute : System.Attribute
    {
        public abstract PreconditionResult CheckPermissions(OnMessageReceivedArgs e);       
    }
}
