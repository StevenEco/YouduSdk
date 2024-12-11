using System.Net;
using System.Security.Cryptography;
using System.Text;
using YouduSdk.Common.Exceptions;

namespace YouduSdk.Common.Encrypt;
public class AESCrypto
{
    public static byte[] AESDecrypt(string input,ApiOptions options)
    {
        try
        {
            var Key = Convert.FromBase64String(options.EncodingaesKey);
            var Iv = new byte[16];
            Array.Copy(Key, Iv, 16);
            var btmpMsg = AESDecrypt(input, Iv, Key);

            var len = BitConverter.ToInt32(btmpMsg, 16);
            len = IPAddress.NetworkToHostOrder(len);

            var bMsg = new byte[len];
            var bAppId = new byte[btmpMsg.Length - 20 - len];
            Array.Copy(btmpMsg, 20, bMsg, 0, len);
            Array.Copy(btmpMsg, 20 + len, bAppId, 0, btmpMsg.Length - 20 - len);
            var decryptAppId = Encoding.UTF8.GetString(bAppId);
            if (!decryptAppId.Equals(options.AppId))
            {
                throw new AESCryptoException("appId not match", new ArgumentException("appId not match"));
            }
            return bMsg;
        }
        catch (Exception e)
        {
            throw new AESCryptoException(e.Message, e);
        }
    }
    public static string AESEncrypt(byte[] input, ApiOptions options)
    {
                try
        {
            var Key = Convert.FromBase64String(options.EncodingaesKey);
            var Iv = new byte[16];
            Array.Copy(Key, Iv, 16);
            var Randcode = CreateRandCode(16);
            var bRand = Encoding.UTF8.GetBytes(Randcode);
            var bAppId = Encoding.UTF8.GetBytes(options.AppId);
            var bMsgLen = BitConverter.GetBytes(HostToNetworkOrder(input.Length));
            var bMsg = new byte[bRand.Length + bMsgLen.Length + bAppId.Length + input.Length];

            Array.Copy(bRand, bMsg, bRand.Length);
            Array.Copy(bMsgLen, 0, bMsg, bRand.Length, bMsgLen.Length);
            Array.Copy(input, 0, bMsg, bRand.Length + bMsgLen.Length, input.Length);
            Array.Copy(bAppId, 0, bMsg, bRand.Length + bMsgLen.Length + input.Length, bAppId.Length);

            return AESEncrypt(bMsg, Iv, Key);
        }
        catch (Exception e)
        {
            throw new AESCryptoException(e.Message, e);
        }
    }
    public static byte[] ToBytes(string input)
    {
        return Encoding.UTF8.GetBytes(input);
    }

    public static string ToString(byte[] input)
    {
        return Encoding.UTF8.GetString(input);
    }


    private static int HostToNetworkOrder(int inval)
    {
        var outval = 0;
        for (var i = 0; i < 4; i++)
        {
            outval = (outval << 8) + ((inval >> (i * 8)) & 255);
        }
        return outval;
    }

    private static string CreateRandCode(int codeLen)
    {
        var codeSerial = "2,3,4,5,6,7,a,c,d,e,f,h,i,j,k,m,n,p,r,s,t,A,C,D,E,F,G,H,J,K,M,N,P,Q,R,S,U,V,W,X,Y,Z";
        if (codeLen == 0)
        {
            codeLen = 16;
        }
        var arr = codeSerial.Split(',');
        var code = "";
        var rand = new Random(unchecked((int)DateTime.Now.Ticks));
        for (var i = 0; i < codeLen; i++)
        {
            var randValue = rand.Next(0, arr.Length - 1);
            code += arr[randValue];
        }
        return code;
    }

    private static string AESEncrypt(byte[] Input, byte[] Iv, byte[] Key)
    {
        var aes = Aes.Create();
        aes.KeySize = Key.Length * 8;
        aes.BlockSize = 128;
        aes.Padding = PaddingMode.None;
        aes.Mode = CipherMode.CBC;
        aes.Key = Key;
        aes.IV = Iv;
        var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
        byte[]? xBuff = null;

        var msg = new byte[Input.Length + 32 - Input.Length % 32];
        Array.Copy(Input, msg, Input.Length);
        var pad = KCS7Encoder(Input.Length);
        Array.Copy(pad, 0, msg, Input.Length, pad.Length);

        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
            {
                cs.Write(msg, 0, msg.Length);
            }
            xBuff = ms.ToArray();
        }

        var Output = Convert.ToBase64String(xBuff);
        return Output;
    }

    private static byte[] KCS7Encoder(int text_length)
    {
        var block_size = 32;
        var amount_to_pad = block_size - (text_length % block_size);
        if (amount_to_pad == 0)
        {
            amount_to_pad = block_size;
        }
        var pad_chr = Chr(amount_to_pad);
        var tmp = "";
        for (var index = 0; index < amount_to_pad; index++)
        {
            tmp += pad_chr;
        }
        return Encoding.UTF8.GetBytes(tmp);
    }

    private static char Chr(int a)
    {

        var target = (byte)(a & 0xFF);
        return (char)target;
    }

    private static byte[] AESDecrypt(string Input, byte[] Iv, byte[] Key)
    {
        var aes = Aes.Create();
        aes.KeySize = Key.Length * 8;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.None;
        aes.Key = Key;
        aes.IV = Iv;
        var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);
        byte[]? xBuff = null;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
            {
                var rawInput = Convert.FromBase64String(Input);
                var msg = new byte[rawInput.Length + 32 - rawInput.Length % 32];
                Array.Copy(rawInput, msg, rawInput.Length);
                cs.Write(rawInput, 0, rawInput.Length);
            }
            xBuff = Decode(ms.ToArray());
        }
        return xBuff;
    }

    private static byte[] Decode(byte[] decrypted)
    {
        var pad = (int)decrypted[^1];
        if (pad < 1 || pad > 32)
        {
            pad = 0;
        }
        var res = new byte[decrypted.Length - pad];
        Array.Copy(decrypted, 0, res, 0, decrypted.Length - pad);
        return res;
    }
}