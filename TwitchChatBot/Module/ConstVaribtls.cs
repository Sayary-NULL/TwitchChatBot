﻿using NLog;
using TwitchChatBot.BotAPI;
using TwitchChatBot.Core.Module;

namespace TwitchChatBot.Module
{
    public static class ConstVaribtls
    {

#if DEBUG
        public static bool IsDebuge = true;
#else
        public static bool IsDebuge = false;
#endif
        public static bool StartBot = false;

        public static string ReferentceGames = "";

        public static Logger _logger = LogManager.GetCurrentClassLogger();
        public static CommandServes _UserCommandServes = new CommandServes();
        public static CommandServes _AdminCommandServers = new CommandServes();
        public static TwitchBot Bot;
    }
}