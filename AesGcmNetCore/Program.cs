using System;
using System.Threading.Tasks;

namespace AesGcmNetCore
{
    public class Program
    {        
        public static async Task Main()
        {
            await new EncryptionService().Encrypt();
        }
    }
}
