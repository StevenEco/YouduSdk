using System.Text.Json.Serialization;

namespace YouduSdk.Entity.Entities;
public class YdToken
{

    [JsonPropertyName("accessToken")]
    public required string Token { get; set; }
    [JsonIgnore]
    public long ActiveTime { get; set; } = 0;
    [JsonPropertyName("expireIn")]
    public required int Expire { get; set; }
    public YdToken()
    {
        
    }
    public YdToken(string token, long activeTime, int expire)
    {
        Token = token;
        ActiveTime = activeTime;
        Expire = expire;
    }

}