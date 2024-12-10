using System.Text.Json.Serialization;

namespace YouduSdk.Entity;
public class YdRequest
{
    /// <summary>
    /// 总机号
    /// </summary>
    [JsonPropertyName("buin")]
    public int Buin { get; set; }
    /// <summary>
    /// 应用号
    /// </summary>
    [JsonPropertyName("appId")]
    public required string AppId { get; set; }
    /// <summary>
    /// 加密后信息
    /// </summary>
    [JsonPropertyName("encrypt")]
    public required string EncryptString { get; set; }
}
public class YdCallBackRequest
{
    /// <summary>
    /// 总机号
    /// </summary>
    [JsonPropertyName("toBuin")]
    public int ToBuin { get; set; }
    /// <summary>
    /// 应用号
    /// </summary>
    [JsonPropertyName("toApp")]
    public required string ToApp { get; set; }
    /// <summary>
    /// 加密后信息
    /// </summary>
    [JsonPropertyName("encrypt")]
    public required string EncryptString { get; set; }
}
