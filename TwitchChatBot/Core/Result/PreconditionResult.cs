using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChatBot.Core.Result
{
    public struct PreconditionResult
    {
        public enum Result
        {
            Unsuccessful,
            Successfully
        }

        public Result res;

        public string ErrorResult;

        public void Successfully()
        {
            res = Result.Successfully;
        }

        public void Unsuccessful(string result)
        {
            ErrorResult = result;
            res = Result.Unsuccessful;
        }
    }
}
