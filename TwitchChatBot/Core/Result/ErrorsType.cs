using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChatBot.Core.Result
{
    public enum ErrorsType
    {
        ParseFailed,
        Unsuccessful,
        Exception,
        ObjectNotFound,
        Successful,
        UnmetPrecondition,
        BadArgCount
    }

    public struct ErrorsReturnType
    {
        public ErrorsType Result;
        public string ErrorsMessage;

        public ErrorsReturnType(ErrorsType result, string errors)
        {
            ErrorsMessage = errors;
            Result = result;
        }
    }

}
