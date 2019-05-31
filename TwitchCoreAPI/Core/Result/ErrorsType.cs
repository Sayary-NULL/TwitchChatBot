
namespace TwitchCoreAPI.Core.Result
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
