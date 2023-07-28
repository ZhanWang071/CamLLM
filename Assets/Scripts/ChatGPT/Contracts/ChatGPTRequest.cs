using Newtonsoft.Json;

public class ChatGPTRequest
{
    //[JsonProperty(PropertyName = "model")]
    //public string Model { get; set; }

    //[JsonProperty(PropertyName = "messages")]
    //public ChatGPTMessage[] Messages { get; set; }

    [JsonProperty(PropertyName = "question")]
    public string Question { get; set; }

    [JsonProperty(PropertyName = "position")]
    public string Position { get; set; }
}
