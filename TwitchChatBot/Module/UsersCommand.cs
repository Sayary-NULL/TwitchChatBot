using System;
using TwitchChatBot.Attribute;
using TwitchCoreAPI.Core.Module;
using TwitchCoreAPI.Core.Attribute;
using TwitchCoreAPI.JsonModule;

namespace TwitchChatBot.Module
{
    class UsersCommand : CommandContext
    {
        [Command("discord"), OnlyOnBot]
        public void RefDiscord()
        {
            SendMessage($"{Context.Username}, держи, ссылка на дискорд легиона https://discord.gg/C7DUgVd");
        }

        [Command("help"), OnlyOnBot]
        public void HelpMess()
        {
            string text = "Вот что я сечас умею: \r\n";
            text += "!discord - дискорд сервер \"Шарового легиона\"\r\n";
            text += "!game - игра в которую мы сейчас играем\r\n";
            text += "!конфа - время до конфы\r\n";
            text += "!uptime - время трансляции стрима\r\n";
            text += "!help - начнем рекурсиююююю)\r\n";

            if (Context.IsModerator)
            {
                text += "Команды для модераторов\r\n";
                text += "!start\r\n";
                text += "!status\r\n";
                text += "!stopbot\r\n";
                text += "!setgame\r\n";
            }

            SendMessage(text);
        }

        [Command("game"), OnlyOnBot]
        public void Game()
        {
            Streams stream = ConstVaribtls.GetRequest<Streams>("streams/95844270");
            if (stream.stream == null)
            {
                SendMessage($"{Context.Username}, стрим не запущен!");
                return;
            }

            SendMessage($"Мы играем в {stream.stream.game}");
        }

        [Command("конфа"), OnlyOnBot]
        public void Konf()
        {
            int day = 0;
            int hour = DateTime.Now.Hour;
            int minut = DateTime.Now.Minute;
            DayOfWeek weeck = DateTime.Now.DayOfWeek;

            switch (weeck.ToString())
            {
                case "Monday":
                    {
                        day = 6;
                        break;
                    }
                case "Tuesday":
                    {
                        day = 5;
                        break;
                    }
                case "Wednesday":
                    {
                        day = 4;
                        break;
                    }
                case "Thursday":
                    {
                        day = 3;
                        break;
                    }
                case "Friday":
                    {
                        day = 2;
                        break;
                    }
                case "Saturday":
                    {
                        day = 1;
                        break;
                    }
                case "Sunday":
                    {
                        day = 0;
                        break;
                    }
            }

            hour = 20 - hour;
            minut = 60 - minut;

            if (day != 0)
            {
                if (hour < 0)
                {
                    day--;
                    hour = 24 - Math.Abs(hour) - 1;
                }
            }
            else
            {
                if (hour < 0)
                {
                    day = 6;
                    hour = 24 - Math.Abs(hour) - 1;
                }
            }

            string StrockDay = "";
            if (day >= 5)
            {
                StrockDay = $"{day} дней";
            }
            else if (day >= 2)
            {
                StrockDay = $"{day} дня";
            }
            else if (day == 1)
            {
                StrockDay = $"{day} день";
            }

            SendMessage($"До конфы осталось {StrockDay} {hour} часов {minut} минут");
        }

        [Command("uptime"), OnlyOnBot]
        public void UpTime()
        {
            Streams rez = ConstVaribtls.GetRequest<Streams>($"streams/95844270");// 

            if (rez.stream == null)
            {
                SendMessage($"{Context.Username}, стрим не запущен!");
            }
            else
            {
                TimeSpan date = DateTime.UtcNow - rez.stream.created_at;
                SendMessage($"{Context.Username}, {date.Hours}:{date.Minutes}:{date.Seconds}");
            }
        }

        [Command("test"), OnlyOwner]
        public void Test()
        {
            //Clip rez = ConstVaribtls.GetRequest<Clip>("clips/PluckySneakyAsteriskStinkyCheese");
            //Streams rez = ConstVaribtls.GetRequest<Streams>("streams/95844270");
            //string rez = ConstVaribtls.GetRequest($"channels/{Context.Channel}/subscriptions");
            string rez = ConstVaribtls.GetRequest($"oauth2/token");
            if (rez != null)
            {
                SendMessage($"ok");
            }
            else SendMessage("noooo");
        }
    }
}
