using NeoSmart.Utils;
using System;

namespace CaseJBF_NET4_8
{
    class Program
    {
        static void Main(string[] args)
        {
            var bouncyCastleService = new BouncyCastleService();
            var bytes = bouncyCastleService.Encrypt();
            var encryptetString = Convert.ToBase64String(bytes);
            Console.WriteLine($"Encrypted string {encryptetString}");

            var urlEncodedBytes = UrlBase64.Decode(encryptetString);
            var urlEncodedString = Convert.ToBase64String(urlEncodedBytes);
            Console.WriteLine($"URL encoded string {urlEncodedString}");
        }
    }
}
