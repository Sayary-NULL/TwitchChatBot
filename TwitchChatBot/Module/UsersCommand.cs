﻿using System;
using TwitchChatBot.Attribute;
using TwitchChatBot.Core.Module;
using TwitchChatBot.Core.Attribute;

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
            if (ConstVaribtls.ReferentceGames == "")
            {
                SendMessage("Игра не установлена!");
            }
            else SendMessage($"Мы играем в {ConstVaribtls.ReferentceGames}");
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
    }
}
