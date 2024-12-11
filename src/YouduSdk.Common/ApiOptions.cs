namespace YouduSdk.Common;

public class ApiOptions
{
    public required string Scheme { get; set; }
    public required string Buin { get; set; }
    public required string AppId { get; set; }
    public required string EncodingaesKey { get; set; }
    public ApiOptions(string scheme,string buin, string appId,string encodingAESKey)
    {
        Scheme = scheme;
        Buin = buin;
        AppId = appId;
        EncodingaesKey = encodingAESKey;
    }
}
