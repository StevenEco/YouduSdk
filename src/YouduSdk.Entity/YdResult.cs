using System.Text.Json.Serialization;
using YouduSdk.Entity.Enum;

namespace YouduSdk.Entity;
public class YdResult
{
    /// <summary>
    /// 返回代码
    /// </summary>
    [JsonPropertyName("errcode")]
    public required StatusCode StatusCode { get; set; }
    /// <summary>
    /// 返回信息
    /// </summary>
    [JsonPropertyName("errmsg")]
    public required string Message { get; set; }
    /// <summary>
    /// 加密返回体
    /// </summary>
    [JsonPropertyName("encrypt")]
    public string? EncryptString { get; set; }
}
