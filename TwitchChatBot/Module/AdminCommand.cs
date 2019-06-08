using TwitchChatBot.Attribute;
using TwitchCoreAPI.Core.Module;
using TwitchCoreAPI.Core.Attribute;

namespace TwitchChatBot.Module
{
    class AdminCommand : CommandContext
    {
        [Command("start"), OnlyModerator]
        public void StartBot()
        {
            SendMessage($"{Context.Username}, бот переведен на функцию автоматического влючения/выключения");
            return;

            if (!ConstVaribtls.StartBot)
            {
                ConstVaribtls.StartBot = true;
                SendMessage("Привет ребята! Я бот \"Шарового Легиона\"! Приятного просмотра");
            }
            else SendMessage($"{Context.Username}, я уже включен");
        }

        [Command("stop"), OnlyModerator]
        public void StopBot()
        {
            SendMessage($"{Context.Username}, бот переведен на функцию автоматического влючения/выключения");
            return;

            if (ConstVaribtls.StartBot)
            {
                ConstVaribtls.StartBot = false;
                SendMessage("Получена команда остановки работы!");
            }
            else SendMessage($"{Context.Username}, я уже выключен");

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

        [Command("версия"), OnlyModerator]
        public void Version()
        {
            SendMessage($"Версия моего программного обеспечения : {ConstVaribtls.VersionOfTheBot}");
        }
    }
}
