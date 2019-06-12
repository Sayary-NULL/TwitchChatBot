using System;
using System.Data.SqlClient;
using System.Threading;
using TwitchCoreAPI.JsonModule;

namespace TwitchChatBot.Module
{
    public class ThreadClass
    {
        Thread _send = new Thread(SendMessage);
        Thread _WorckBot = new Thread(WorckBot);

        public void StartWorckBot()
        {
            _WorckBot.Start();
        }

        private static void WorckBot(object obj)
        {
            while (!ConstVaribtls.Bot.client.IsConnected) ;

            while (true)
            {
                var res = ConstVaribtls.GetRequest<Streams>("streams/95844270");

                if (res.stream == null && ConstVaribtls.StartBot)
                {
                    ConstVaribtls.Bot.client.SendMessage(ConstVaribtls.Bot.client.JoinedChannels[0], "Стрим выключен, и я пойду спать");
                    ConstVaribtls.StartBot = false;
                    ConstVaribtls._logger.Info("Automatic; Выключение бота");

                    using (SqlConnection conect = new SqlConnection(ConstVaribtls.DateBase.ConnectionStringKey))
                    {
                        try
                        {
                            conect.Open();
                            using (SqlCommand command = new SqlCommand("sp_SetNotComeUsers", conect) { CommandType = System.Data.CommandType.StoredProcedure })
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                        catch(Exception e)
                        {
                            ConstVaribtls._logger.Error(e);
                        }
                    }
                }
                else if(res.stream != null && !ConstVaribtls.StartBot)
                {
                    ConstVaribtls.Bot.client.SendMessage(ConstVaribtls.Bot.client.JoinedChannels[0], "Стрим запущен, я иду работать!");
                    ConstVaribtls.StartBot = true;
                    ConstVaribtls._logger.Info("Automatic; Включение бота");
                    ConstVaribtls.ComeSayary = false;
                }

                Thread.Sleep(60000);
            }
        }

        public void StartThread()
        {
            _send.Start();
        }

        private static void SendMessage(Object obj)
        {
            while (!ConstVaribtls.Bot.client.IsConnected) ;
            int i = 0;
            do
            {
                if(!ConstVaribtls.StartBot)
                {
                    Thread.Sleep(60000);
                    continue;
                }

                switch (i)
                {
                    case 0:
                        {
                            ConstVaribtls.Bot.client.SendMessage(ConstVaribtls.Bot.client.JoinedChannels[0], "Привет, вступай в группу https://vk.com/sharonvoice");
                            i++;
                            break;
                        }
                    case 1:
                        {
                            ConstVaribtls.Bot.client.SendMessage(ConstVaribtls.Bot.client.JoinedChannels[0], "Общайся на нашем дискорд-сервере https://discord.gg/C7DUgVd");
                            i++;
                            break;
                        }

                    case 2:
                        {
                            ConstVaribtls.Bot.client.SendMessage(ConstVaribtls.Bot.client.JoinedChannels[0], "Подписывайся на мой телеграмм https://t.me/Sharon_AL");
                            i++;
                            break;
                        }
                    case 3:
                        {
                            ConstVaribtls.Bot.client.SendMessage(ConstVaribtls.Bot.client.JoinedChannels[0], "Нравятся стримы, поддержи стримера: https://www.donationalerts.ru/r/sharoh");
                            i = 0;
                            break;
                        }
                }

                Thread.Sleep(600000);
            } while (true);
        }
    }
}
