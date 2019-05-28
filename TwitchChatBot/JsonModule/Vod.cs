using Newtonsoft.Json;

namespace TwitchChatBot.JsonModule
{
    public class Vod
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }

        [JsonProperty("offset")]
        public int offset { get; set; }

        [JsonProperty("preview_image_url")]
        public string preview_image_url { get; set; }
    }
}
