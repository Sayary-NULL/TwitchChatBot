using TwitchChatBot.Attribute;
using TwitchChatBot.Core.Module;
using TwitchChatBot.Core.Attribute;

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
                SendMessage("Я работаю!");
            }
            else SendMessage("Я сплю!");
        }

        [Command("setgame"), OnlyModerator]
        public void SetGame(string text)
        {
            ConstVaribtls.ReferentceGames = text;
            SendMessage("Игра установлена!");
        }

        [Command("версия"), OnlyModerator]
        public void Version()
        {
            SendMessage($"Версия моего программного обеспечения : {ConstVaribtls.VersionOfTheBot}");
        }
    }
}
