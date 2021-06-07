using NeoSmart.Utils;
using Shouldly;
using System;
using Xunit;

namespace CaseJBF_NET4_8.Tests
{
    public class BouncyCastleServiceTests
    {      
        [Fact]
        public void TestEncryptAndDecrypt()
        {
            var bouncyCastleService = new BouncyCastleService();

            var bytes = bouncyCastleService.Encrypt();

            var encryptetString = Convert.ToBase64String(bytes);           

            var decryptet = bouncyCastleService.Decrypt(encryptetString);
            decryptet.ShouldBe(Constants.StringToEncrypt);           
        }
        [Fact]
        public void DecryptJavaGeneratedString()
        {
            var bouncyCastleService = new BouncyCastleService();

            var base64String = "AAAADE6u1eBZ+mh+lL9flcLN7KOy8Zul0JcPzYQWZ4I+dr8hfPd9ybA=";

            var decoded = UrlBase64.Decode(base64String);
            var stringToDecrypt = Convert.ToBase64String(decoded);

            var decryptet = bouncyCastleService.Decrypt(stringToDecrypt);
            decryptet.ShouldBe(Constants.StringToEncrypt);
        }
    }
}
