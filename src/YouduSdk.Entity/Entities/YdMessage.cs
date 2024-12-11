using System.Text.Json.Serialization;

namespace YouduSdk.Entity.Entities;

public class YdMessage
{
    /// <summary>
    /// 发送的用户
    /// </summary>
    [JsonPropertyName("toUser")]
    public string? ToUser { get; set; }
    /// <summary>
    /// 发送的部门
    /// </summary>
    [JsonPropertyName("toDept")]
    public string? ToDept { get; set; }
    /// <summary>
    /// 信息类型
    /// </summary>
    [JsonPropertyName("msgType")]
    public required string MsgType { get; set; }
}

#region  BasicContent
public class BasicContent
{
}
public class YdContent : BasicContent
{
    [JsonPropertyName("content")]
    public required string Content { get; set; }
}
public class YdMpnews : BasicContent
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    [JsonPropertyName("media_id")]
    public required string MediaId { get; set; }
    [JsonPropertyName("content")]
    public required string Content { get; set; }
    [JsonPropertyName("digest")]
    public string? Digest { get; set; }
    [JsonPropertyName("showFront")]
    public bool? ShowFront { get; set; }
}
public class YdImage : BasicContent
{
    [JsonPropertyName("media_id")]
    public required string MediaId { get; set; }
}
public class YdFile : BasicContent
{
    [JsonPropertyName("media_id")]
    public required string MediaId { get; set; }
}
public class YdLink : BasicContent
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    [JsonPropertyName("url")]
    public required string Url { get; set; }
    [JsonPropertyName("action")]
    public bool? Action { get; set; }
}
public class YdExLink
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    [JsonPropertyName("url")]
    public required string Url { get; set; }
    [JsonPropertyName("digest")]
    public string? Digest { get; set; }
    [JsonPropertyName("media_id")]
    public required string MediaId { get; set; }
}
public class YdSys
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    [JsonPropertyName("popDuration")]
    public int? PopDuration { get; set; }
    [JsonPropertyName("msg")]
    public required BasicContent Msg { get; set; }
}
public class YdSms
{
    [JsonPropertyName("from")]
    public required string From { get; set; }
    [JsonPropertyName("content")]
    public required string Content { get; set; }
}
public class YdMail
{
    //TODO 懒得写了，没啥屌用
    /*
    action 否   邮件消息类型。new: 新邮件，unread: 未读邮件数
    subject 否 邮件主题。action为new时有效，可为空
    fromUser    否 发送者帐号，action为new时有效
    fromEmail   否 发送者邮件帐号，action为new时有效。fromUser不为空，fromEmail值无效
    time    否 邮件发送时间。为空默认取服务器接收到消息的时间
    link    否 邮件链接。action为new时有效，点此链接即可打开邮件，为空时点击邮件消息默认执行企业邮箱单点登录
    unreadCount 否 未读邮件数。action为unread时有效
    */
}
#endregion

public class YdTextMsg : YdMessage
{
    [JsonPropertyName("text")]
    public required YdContent Text { get; set; }
}
public class YdImageMsg : YdMessage
{
    [JsonPropertyName("image")]
    public required YdImage Image { get; set; }
}
public class YdFileMsg : YdMessage
{
    [JsonPropertyName("file")]
    public required YdFile File { get; set; }
}
public class YdMpnewsMsg : YdMessage
{
    [JsonPropertyName("mpnews")]
    public required YdMpnews[] Mpnews { get; set; }
}
public class YdLinkMsg : YdMessage
{
    [JsonPropertyName("link")]
    public required YdLink Link { get; set; }
}
public class YdExLinkMsg : YdMessage
{
    [JsonPropertyName("exlink")]
    public required YdExLink ExLink { get; set; }
}
public class YdSysMsg : YdMessage
{
    public class ToAll
    {
        [JsonPropertyName("onliyOnline")]
        public bool? OnlyOnline { get; set; }
    }
    [JsonPropertyName("toAll")]
    public ToAll? ForAll { get; set; }
    [JsonPropertyName("sysMsg")]
    public required YdSysMsg SysMsg { get; set; }
}
public class YdSmsMsg : YdMessage
{
    [JsonPropertyName("sms")]
    public required YdSms SmsMsg { get; set; }
}
public class YdMailMsg : YdMessage
{
    [JsonPropertyName("toEmail")]
    public string? ToEMail { get; set; }
    [JsonPropertyName("mail")]
    public required YdSms SmsMsg { get; set; }
}