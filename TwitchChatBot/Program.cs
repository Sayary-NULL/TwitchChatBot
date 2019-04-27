using System;
using TwitchChatBot.Module;
using TwitchChatBot.BotAPI;

namespace TwitchChatBot
{
    class Program
    {
        static void Main(string[] args)
        {
            ConstVaribtls.Bot = new TwitchBot();

            Console.ReadLine();
        }
    }
}