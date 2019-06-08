using System;
using TwitchChatBot.Module;
using TwitchChatBot.BotAPI;
using System.Threading;

namespace TwitchChatBot
{
    class Program
    {
        static void Main()
        {
            ConstVaribtls.Bot = new TwitchBot();

            Thread.Sleep(1000);
            ConstVaribtls.Message.StartThread();
            ConstVaribtls.Message.StartWorckBot();

            Console.ReadLine();
        }
    }
}