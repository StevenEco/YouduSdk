using YouduSdk.Common;
using YouduSdk.Common.Encrypt;
using YouduSdk.Common.Utils;
using YouduSdk.Entity;

namespace TestSdk
{
    public class Tests
    {
        private const string Address = @"http://192.168.212.128:7080"; // ����д�жȷ�������ַ

        private const int Buin = 16118759; // ����д��ҵ�ܻ�����

        private const string EncodingaesKey = "6mW0wFh2RsGjlervH3dveTMWRz5ju676T/3w/fpOFQo="; // ����д��ҵӦ�õ�EncodingaesKey

        private const string AppId = "ydAB2A13431F4E44509D9F146925303996"; // ����д��ҵӦ��AppId

        private const string ToUsers = "cs1|cs2"; // ����������Ϣ���˺�
        YdRequest baseParam;
        [SetUp]
        public void Setup()
        {
            var now = TokenUtils.GetSecondTimeStamp();
            var timestamp = AESCrypto.ToBytes(now.ToString());
            var encryptTime = AESCrypto.AESEncrypt(timestamp, EncodingaesKey, AppId);
            baseParam = new YdRequest()
            { AppId = AppId,Buin = Buin,EncryptString = encryptTime };
        }

        [Test]
        public async Task GetTokenTest()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri(Address)
            };
            var rs = await TokenUtils.GetToken(baseParam, 
                EncodingaesKey, AppId, 
                client, ApiEndPoint.API_GET_TOKEN);
            Assert.That(string.IsNullOrEmpty(rs.Token), Is.False);
        }

        [Test]
        public async Task RefreshTokenTest()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri(Address)
            };
            var token = await TokenUtils.GetToken(baseParam,
                            EncodingaesKey, AppId,
                            client, ApiEndPoint.API_GET_TOKEN);
            var rs = await TokenUtils.CheckAndRefreshToken(baseParam,
                EncodingaesKey, AppId,
                client, ApiEndPoint.API_GET_TOKEN);
            rs.ActiveTime += 7200000;
            Assert.That(string.IsNullOrEmpty(rs.Token), Is.False);
        }
    }
}