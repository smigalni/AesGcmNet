using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AesGcmNetCore.Tests
{
    public class EncryptServiceTests
    {
        [Fact]
        public async Task EncryptServiceTest()
        {
            var encryptionService = new EncryptionService();
            var encryptedString = await encryptionService.Encrypt();
            encryptedString.ShouldBe("AAAADDEjE9A5VVdjHgWKKtfY0l01+MKXHb+Ri/wSQ+44NKs7XbalGoM=");
        }
    }
}
