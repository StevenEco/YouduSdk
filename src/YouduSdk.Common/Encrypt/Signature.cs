using System.Collections;
using System.Security.Cryptography;
using System.Text;
using YouduSdk.Common.Exceptions;

namespace YouduSdk.Common.Encrypt;
public class Signature
{
    /// <summary>
    /// 生成回调校验签名
    /// </summary>
    /// <param name="token">回调Token</param>
    /// <param name="timestamp">时间戳，从回调URL参数里取</param>
    /// <param name="nonce">回调随机字符串，从回调URL参数取</param>
    /// <param name="encrypt">加密内容，从回调的json数据中取</param>
    /// <returns>返回签名</returns>
    /// <exception cref="SignatureException">如果出现错误则抛出异常</exception>
    public static string GenerateSignature(string token, string timestamp, string nonce, string encrypt)
    {
        ArrayList AL = [token, timestamp, nonce, encrypt];
        AL.Sort(new DictionarySort());
        var raw = "";
        for (var i = 0; i < AL.Count; ++i)
        {
            raw += AL[i];
        }

        SHA1 sha;
        ASCIIEncoding enc;
        string hash;
        try
        {
            sha = SHA1.Create();
            enc = new ASCIIEncoding();
            var dataToHash = enc.GetBytes(raw);
            var dataHashed = sha.ComputeHash(dataToHash);
            hash = BitConverter.ToString(dataHashed).Replace("-", "");
            hash = hash.ToLower();
        }
        catch (Exception e)
        {
            throw new SignatureException(e.Message, e);
        }
        return hash;
    }

    private class DictionarySort : IComparer
    {
        public int Compare(object oLeft, object oRight)
        {
            var sLeft = oLeft as string;
            var sRight = oRight as string;
            var iLeftLength = sLeft.Length;
            var iRightLength = sRight.Length;
            var index = 0;
            while (index < iLeftLength && index < iRightLength)
            {
                if (sLeft[index] < sRight[index])
                {
                    return -1;
                }
                else if (sLeft[index] > sRight[index])
                {
                    return 1;
                }
                else
                {
                    index++;
                }
            }
            return iLeftLength - iRightLength;

        }
    }
}