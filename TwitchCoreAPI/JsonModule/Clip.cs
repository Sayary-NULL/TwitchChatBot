using System;
using Newtonsoft.Json;

namespace TwitchCoreAPI.JsonModule
{
    public class Clip
    {
        [JsonProperty("slug")]
        public string slug { get; set; }

        [JsonProperty("tracking_id")]
        public string tracking_id { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }

        [JsonProperty("embed_url")]
        public string embed_url { get; set; }

        [JsonProperty("embed_html")]
        public string embed_html { get; set; }

        [JsonProperty("broadcaster")]
        public Broadcaster broadcaster { get; set; }

        [JsonProperty("curator")]
        public Curator curator { get; set; }

        [JsonProperty("vod")]
        public Vod vod { get; set; }

        [JsonProperty("broadcast_id")]
        public string broadcast_id { get; set; }

        [JsonProperty("game")]
        public string game { get; set; }

        [JsonProperty("language")]
        public string language { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("views")]
        public int views { get; set; }

        [JsonProperty("duration")]
        public double duration { get; set; }

        [JsonProperty("created_at")]
        public DateTime created_at { get; set; }

        [JsonProperty("thumbnails")]
        public Thumbnails thumbnails { get; set; }
    }
}
