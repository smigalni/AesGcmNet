using NeoSmart.Utils;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CaseJBF_NET5.Tests
{
    public class AesGcmServiceTests
    {
        [Fact]
        public async Task AesGcmServiceTest()
        {
            var aesGcmService = new AesGcmService();

            var bytes = await aesGcmService.Encrypt();

            var encryptetString = Convert.ToBase64String(bytes);

            var decryptetString = await aesGcmService.Decrypt(encryptetString);
            decryptetString.ShouldBe(Constants.StringToEncrypt);
        }

        [Fact]
        public async Task TestForJavaEncodedString()
        {
            var aesGcmService = new AesGcmService();

            var base64String = "AAAADE6u1eBZ+mh+lL9flcLN7KOy8Zul0JcPzYQWZ4I+dr8hfPd9ybA=";

            var decoded = UrlBase64.Decode(base64String);
            var stringToDecrypt = Convert.ToBase64String(decoded);
           
            var decryptetString = await aesGcmService.Decrypt(stringToDecrypt);
            decryptetString.ShouldBe(Constants.StringToEncrypt);
        }
    }
}
