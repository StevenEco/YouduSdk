using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using YouduSdk.Common.Exceptions;
using YouduSdk.Common.Encrypt;
using YouduSdk.Entity;
using YouduSdk.Entity.Entities;

namespace YouduSdk.Common.Utils;
public class TokenUtils
{
    public static async Task<YdToken> GetToken(YdRequest param,
        string encodingAESKey,string appId,
        HttpClient? client = null, string? apiEndPoint = null)
    {
        try
        {
            apiEndPoint = string.IsNullOrEmpty(apiEndPoint)? ApiEndPoint.API_GET_TOKEN : apiEndPoint;
            client ??= new HttpClient();
            var rsp = await client.PostAsJsonAsync(apiEndPoint, param);
            await VerifyStatus.CheckHttpStatus(rsp);
            var body = await rsp.Content.ReadFromJsonAsync<YdResult>() ?? throw new Exception();
            VerifyStatus.CheckApiStatus(body);
            var buffer = AESCrypto.AESDecrypt(body.EncryptString,encodingAESKey,appId);
            var tokenInfo = JsonSerializer.Deserialize<YdToken>(AESCrypto.ToString(buffer));
            tokenInfo.ActiveTime = GetSecondTimeStamp();
            return tokenInfo;
        }
        catch (WebException e)
        {
            throw new HttpRequestException(0, e.Message, e);
        }
        catch (Exception e)
        {
            if (e is GeneralEntAppException)
            {
                throw;
            }
            else
            {
                throw new Exception(e.Message, e);
            }
        }
    }

    public static async Task<YdToken> CheckAndRefreshToken(YdRequest param,
        string encodingAESKey, string appId,
        HttpClient? client = null, string? apiEndPoint = null)
    {
        var token = await GetToken(param, encodingAESKey, appId, client, apiEndPoint);
        var endTime = token.ActiveTime + token.Expire;
        if (endTime <= GetSecondTimeStamp())
        {
            token = await GetToken(param, encodingAESKey, appId,client,apiEndPoint);
        }
        return token;
    }


    public static long GetSecondTimeStamp()
    {
        var start = new DateTime(1970, 1, 1);
        var timeZone = TimeZoneInfo.Local.BaseUtcOffset;
        var startTime = start.Add(timeZone);
        long duration = (long)(DateTime.Now - startTime).TotalMilliseconds / 1000; // 相差毫秒秒数
        return duration;
    }
}