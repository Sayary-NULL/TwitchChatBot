using System;
using System.Threading;

namespace TwitchChatBot.Module
{
    public class ThreadClass
    {
        Thread _send = new Thread(SendMessage);

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
