using Newtonsoft.Json;
using NLog;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using TwitchChatBot.BotAPI;
using TwitchCoreAPI.Core.Module;

namespace TwitchChatBot.Module
{
    [DataContract]
    public class DateBaseJSON
    {
        [DataMember]
        public string ConnectionStringKey = "";
    }

    public static class ConstVaribtls
    {
        public static ThreadClass Message = new ThreadClass();

#if DEBUG
        public static bool IsDebuge = true;
#else
        public static bool IsDebuge = false;
#endif
        public static bool StartBot = false;
        public static bool ComeSayary = false;

        public static string ReferentceGames = "";
        public static string VersionOfTheBot = "0.0.13D";

        public static Logger _logger = LogManager.GetCurrentClassLogger();
        public static CommandServes _UserCommandServes = new CommandServes();
        public static CommandServes _AdminCommandServers = new CommandServes();
        public static TwitchBot Bot;

        public static DateBaseJSON DateBase;

        public static void Init()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(DateBaseJSON));

            using (FileStream fs = new FileStream(@"Base/DateBase.json", FileMode.Open))
            {
                DateBase = jsonFormatter.ReadObject(fs) as DateBaseJSON;
            }
        }

        public static string GetRequest(string url)
        {
            url = "https://api.twitch.tv/kraken/" + url;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.Method = "POST";
            req.Accept = "application/vnd.twitchtv.v5+json";
            req.Headers.Add("Client-ID", Base.CodeConnect.Cliend_ID);
            req.Headers.Add("Authorization", Base.CodeConnect.Oauth2);

            string result = "";
            try
            {
                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                using (StreamReader sr = new StreamReader(res.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }

                return result;
            }
            catch (WebException ex)
            {
                HttpWebResponse webResponse = (HttpWebResponse)ex.Response;
                _logger.Error($"Статусный код ошибки: {(int)webResponse.StatusCode} - {webResponse.StatusCode}");
                return default;
            }
        }

        public static T GetRequest<T>(string url)
        {
            url = "https://api.twitch.tv/kraken/" + url;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.Method = "GET";
            req.Accept = "application/vnd.twitchtv.v5+json";
            req.Headers.Add("Client-ID", Base.CodeConnect.Cliend_ID);
            req.Headers.Add("Authorization", Base.CodeConnect.Oauth2);

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
