using Newtonsoft.Json;

namespace AutoItControl.Configuration
{
    public class Send
    {
        public string keys { get; set; }
        public bool? flag { get; set; }
    }
    public class ControlSend
    {
        public string title { get; set; }
        public string text { get; set; }
        public string controlID { get; set; }
        [JsonProperty("string")]
        public string stringText { get; set; }
        public bool? flag { get; set; }
    }
    [JsonObject("TimeScript")]
    public class JSONTimeScript
    {
        public string SongTime { get; set; }
        public Send Send { get; set; }
        public ControlSend ControlSend { get; set; }
    }

    public class AutoItScriptJson
    {
        [JsonProperty("TimeScript")]
        public JSONTimeScript[] JsonTimeScript { get; set; }
        public string WinActivate { get; set; }
    }
}
