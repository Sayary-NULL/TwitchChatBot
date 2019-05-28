using System;
using Newtonsoft.Json;

namespace TwitchChatBot.JsonModule
{
    public class Stream
    {
        [JsonProperty("_id")]
        public long _id { get; set; }

        [JsonProperty("game")]
        public string game { get; set; }

        [JsonProperty("viewers")]
        public int viewers { get; set; }

        [JsonProperty("video_height")]
        public int video_height { get; set; }

        [JsonProperty("average_fps")]
        public float average_fps { get; set; }

        [JsonProperty("delay")]
        public int delay { get; set; }

        [JsonProperty("created_at")]
        public DateTime created_at { get; set; }

        [JsonProperty("is_playlist")]
        public bool is_playlist { get; set; }

        [JsonProperty("preview")]
        public Preview preview { get; set; }

        [JsonProperty("channel")]
        public Channel channel { get; set; }
    }

    public class Streams
    {
        [JsonProperty("stream")]
        public Stream stream { get; set; }
    }
}
