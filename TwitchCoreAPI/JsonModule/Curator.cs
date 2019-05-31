using Newtonsoft.Json;

namespace TwitchCoreAPI.JsonModule
{
    public class Curator
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("display_name")]
        public string display_name { get; set; }

        [JsonProperty("channel_url")]
        public string channel_url { get; set; }

        [JsonProperty("logo")]
        public string logo { get; set; }
    }
}
