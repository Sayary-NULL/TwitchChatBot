using Newtonsoft.Json;
using NLog;
using System.IO;
using System.Net;
using TwitchChatBot.BotAPI;
using TwitchCoreAPI.Core.Module;

namespace TwitchChatBot.Module
{
    public static class ConstVaribtls
    {
        public static ThreadClass Message = new ThreadClass();

#if DEBUG
        public static bool IsDebuge = true;
#else
        public static bool IsDebuge = false;
#endif
        public static bool StartBot = false;

        public static string ReferentceGames = "";
        public static string VersionOfTheBot = "0.0.13D";

        public static Logger _logger = LogManager.GetCurrentClassLogger();
        public static CommandServes _UserCommandServes = new CommandServes();
        public static CommandServes _AdminCommandServers = new CommandServes();
        public static TwitchBot Bot;

        public static T GetRequest<T>(string url)
        {
            url = "https://api.twitch.tv/kraken/" + url;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.Method = "GET";
            req.Accept = "application/vnd.twitchtv.v5+json";
            req.Headers.Add("Client-ID", Base.CodeConnect.Cliend_ID);

            string result = "";
            try
            {
                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                using (StreamReader sr = new StreamReader(res.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }

                T par = JsonConvert.DeserializeObject<T>(result);
                return par;
            }
            catch (WebException ex)
            {
                HttpWebResponse webResponse = (HttpWebResponse)ex.Response;
                _logger.Error($"Статусный код ошибки: {(int)webResponse.StatusCode} - {webResponse.StatusCode}");
                return default;
            }
        }
    }
}
