using System;
using TwitchChatBot.Module;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchCoreAPI.Core.Result;

namespace TwitchChatBot.BotAPI
{
    public class TwitchBot
    {
        public TwitchClient client;
        public TwitchBot()
        {
            ConnectionCredentials credentials = new ConnectionCredentials(Base.CodeConnect.NameBot, Base.CodeConnect.Oauth);

            client = new TwitchClient();
            client.Initialize(credentials,"sharonvoice");

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
#if DEBUG
            string symv = "$";
#else
            string symv = "!";
#endif

            if (HasStringPrefix(e.ChatMessage.Message, symv, ref argpos))
            {
                var res1 = ConstVaribtls._UserCommandServes.Invoke(argpos, client, e);
                var res2 = ConstVaribtls._AdminCommandServers.Invoke(argpos, client, e);

                if(res1.Result != ErrorsType.Successful)
                {
                    switch (res1.Result)
                    {
                        case ErrorsType.ParseFailed:
                            {
                                ConstVaribtls.Bot.client.SendMessage(ConstVaribtls.Bot.client.JoinedChannels[0], $"Ошибка: Неправильные аргументы!");
                                ConstVaribtls._logger.Error($"Ошибка: {res1.ErrorsMessage}");
                                break;
                            }
                        case ErrorsType.Unsuccessful:
                            {
                                ConstVaribtls.Bot.client.SendMessage(ConstVaribtls.Bot.client.JoinedChannels[0], $"Ошибка: Unsuccessful");
                                ConstVaribtls._logger.Error($"Ошибка: {res1.ErrorsMessage}");
                                break;
                            }
                        case ErrorsType.Exception:
                            {
                                ConstVaribtls._logger.Error($"Ошибка: {res1.ErrorsMessage}");
                                break;
                            }
                        case ErrorsType.UnmetPrecondition:
                            {
                                ConstVaribtls.Bot.client.SendMessage(ConstVaribtls.Bot.client.JoinedChannels[0], $"{res1.ErrorsMessage}");
                                ConstVaribtls._logger.Error($"Ошибка: {res1.ErrorsMessage}");
                                break;
                            }
                        case ErrorsType.BadArgCount:
                            {
                                ConstVaribtls.Bot.client.SendMessage(ConstVaribtls.Bot.client.JoinedChannels[0], $"Ошибка: Не соответсвие кол-ва аргументов функции");
                                ConstVaribtls._logger.Error($"Ошибка: {res1.ErrorsMessage}");
                                break;
                            }
                    }
                }

                if (res2.Result != ErrorsType.Successful)
                {
                    switch (res2.Result)
                    {
                        case ErrorsType.ParseFailed:
                            {
                                ConstVaribtls.Bot.client.SendMessage(ConstVaribtls.Bot.client.JoinedChannels[0], $"Ошибка: Неправильные аргументы!");
                                ConstVaribtls._logger.Error($"Ошибка: {res2.ErrorsMessage}, сообщение: '{e.ChatMessage.Message}'");
                                break;
                            }
                        case ErrorsType.Unsuccessful:
                            {
                                ConstVaribtls.Bot.client.SendMessage(ConstVaribtls.Bot.client.JoinedChannels[0], $"Ошибка: Unsuccessful");
                                ConstVaribtls._logger.Error($"Ошибка: {res2.ErrorsMessage}");
                                break;
                            }
                        case ErrorsType.Exception:
                            {
                                ConstVaribtls._logger.Error($"Ошибка: {res2.ErrorsMessage}");
                                break;
                            }
                        case ErrorsType.UnmetPrecondition:
                            {
                                ConstVaribtls.Bot.client.SendMessage(ConstVaribtls.Bot.client.JoinedChannels[0], $"{res2.ErrorsMessage}");
                                ConstVaribtls._logger.Error($"Ошибка: {res2.ErrorsMessage}");
                                break;
                            }
                        case ErrorsType.BadArgCount:
                            {
                                ConstVaribtls.Bot.client.SendMessage(ConstVaribtls.Bot.client.JoinedChannels[0], $"Ошибка: Не соответсвие кол-ва аргументов функции");
                                ConstVaribtls._logger.Error($"Ошибка: {res2.ErrorsMessage}");
                                break;
                            }
                    }
                }

                if(res1.Result == ErrorsType.ObjectNotFound && res2.Result == ErrorsType.ObjectNotFound)
                {
                    ConstVaribtls.Bot.client.SendMessage(ConstVaribtls.Bot.client.JoinedChannels[0], $"Команда не найдена!");
                    ConstVaribtls._logger.Error($"Ошибка: {res1.ErrorsMessage} : {res2.ErrorsMessage}");
                }
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
