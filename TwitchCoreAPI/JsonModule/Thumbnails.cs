using Newtonsoft.Json;

namespace TwitchCoreAPI.JsonModule
{
    public class Thumbnails
    {
        [JsonProperty("medium")]
        public string medium { get; set; }

        [JsonProperty("small")]
        public string small { get; set; }

        [JsonProperty("tiny")]
        public string tiny { get; set; }
    }
}
