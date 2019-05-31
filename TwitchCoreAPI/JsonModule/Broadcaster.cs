using Newtonsoft.Json;

namespace TwitchCoreAPI.JsonModule
{
    public class Broadcaster
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string Display_name { get; set; }

        [JsonProperty("channel_url")]
        public string Channel_url { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }
    }
}
