using NeoSmart.Utils;
using System;
using System.Threading.Tasks;

namespace CaseJBF_NET5
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var bytes = await new AesGcmService().Encrypt();

            var encryptetString = Convert.ToBase64String(bytes);
            Console.WriteLine($"Encrypted string {encryptetString}");

            var urlEncodedBytes = UrlBase64.Decode(encryptetString);
            var urlEncodedString = Convert.ToBase64String(urlEncodedBytes);
            Console.WriteLine($"URL encoded string {urlEncodedString}");

        }
    }
}
