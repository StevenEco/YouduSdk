using System.Net;
using YouduSdk.Entity;
using YouduSdk.Entity.Enum;

namespace YouduSdk.Common.Utils;
public class VerifyStatus
{
    public static async Task CheckHttpStatus(HttpResponseMessage rsp)
    {
        if (rsp.StatusCode != HttpStatusCode.OK)
        {
            throw new HttpRequestException(message: await rsp.Content.ReadAsStringAsync(), null, statusCode: rsp.StatusCode);
        }
    }

    public  static void CheckApiStatus(YdResult result)
    {
        if((StatusCode)result.StatusCode!= StatusCode.STATUSCODE_OK)
        {
            throw new ArgumentException("invalid errcode or errmsg"); ;
        }
    }
}

