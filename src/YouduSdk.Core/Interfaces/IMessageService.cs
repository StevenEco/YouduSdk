using YouduSdk.Entity;
using YouduSdk.Entity.Entities;

namespace YouduSdk.Core.Interfaces;

/// <summary>
/// Post http://[SERVER:7080]/cgi/msg/send?accessToken=$access_token
/// </summary>
public interface IMessageService
{
    Task<YdResultBase> SendTextMsg(YdTextMsg msg);
    Task<YdResultBase> SendImageMsg(YdImageMsg msg);
    Task<YdResultBase> SendFileMsg(YdFileMsg msg);
    Task<YdResultBase> SendMpnewsMsg(YdMpnews msg);
    Task<YdResultBase> SendLinkMsg(YdLinkMsg msg);
    Task<YdResultBase> SendExLinkMsg(YdExLinkMsg msg);
    Task<YdResultBase> SendSysMsg(YdSysMsg msg);
    Task<YdResultBase> SendSmsMsg(YdSmsMsg msg);
    Task<YdResultBase> SendMailMsg(YdMailMsg msg);
}
