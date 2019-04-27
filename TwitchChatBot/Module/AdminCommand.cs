using TwitchChatBot.Atribute;
using TwitchChatBot.Core.Module;

namespace TwitchChatBot.Module
{
    class AdminCommand : CommandContext
    {
        [Command("start"), OnlyModerator]
        public void StartBot()
        {
            ConstVaribtls.StartBot = true;
            SendMessage("Привет ребята! Я бот \"Шарового Легиона\"! Приятного просмотра");
        }

        [Command("stop"), OnlyModerator]
        public void StopBot()
        {
            ConstVaribtls.StartBot = false;
            SendMessage("Получена команда остановки работы!");
        }

        [Command("status"), OnlyModerator]
        public void Status()
        {
            if (ConstVaribtls.StartBot)
            {
                SendMessage("Я рабоатю!");
            }
            else SendMessage("Я сплю!");
        }

        [Command("setgame"), OnlyModerator]
        public void SetGame(string text)
        {
            ConstVaribtls.ReferentceGames = text;
            SendMessage("Игра установлена!");
        }
    }
}
