using System;
using TwitchChatBot.Module;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchChatBot.BotAPI
{
    public class TwitchBot
    {
        TwitchClient client;
        string ReferentceGames = "";

        public TwitchBot()
        {
            ConnectionCredentials credentials = new ConnectionCredentials(Base.CodeConnect.NameBot, Base.CodeConnect.Oauth);

            client = new TwitchClient();
            client.Initialize(credentials, "sharonvoice");

            client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnConnected += Client_OnConnected;

            ConstVaribtls._UserCommandServes.GetClass<UsersCommand>();
            ConstVaribtls._AdminCommandServers.GetClass<AdminCommand>();

            client.Connect();
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            if (ConstVaribtls.StartBot)
                ConstVaribtls._logger.Info($"{e.BotUsername} - {e.Data}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("Бот подключился");
            ConstVaribtls._logger.Info($"Бот {e.BotUsername} подключен к каналу {e.Channel}");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            int argpos = 0;
            if (HasStringPrefix(e.ChatMessage.Message, "$", ref argpos))
            {
                ConstVaribtls._UserCommandServes.Invoke(argpos, client, e);
                ConstVaribtls._AdminCommandServers.Invoke(argpos, client, e);
            }
        }

        private bool HasStringPrefix(string mess, string pref, ref int argpos)
        {
            if(mess.IndexOf(pref) == 0)
            {
                argpos = pref.Length;
                return true;
            }
            else
            {
                argpos = -1;
                return false;
            }
        }

        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            if (e.WhisperMessage.Username == "my_friend")
                client.SendWhisper(e.WhisperMessage.Username, "Hey! Whispers are so cool!!");
        }
    }
}
