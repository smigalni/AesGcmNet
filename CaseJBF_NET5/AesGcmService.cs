using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Linq;

namespace CaseJBF_NET5
{
    public class AesGcmService
    {
        public Task<byte[]> Encrypt()
        {
            byte[] keyByteArray = Convert.FromBase64String(Constants.Key);
            byte[] plainBytes = Encoding.UTF8.GetBytes(Constants.StringToEncrypt);

            byte[] key = keyByteArray;
            byte[] nonce = new byte[Constants.NonceLength];
            byte[] authTag = new byte[Constants.TagLength];

            RandomNumberGenerator.Fill(nonce);

            byte[] cipher = new byte[plainBytes.Length];

            using (var aesgcm = new AesGcm(key))
                aesgcm.Encrypt(nonce, plainBytes, cipher, authTag);

            byte[] nonceIntro = { 0, 0, 0, 12 };

            var cipherTextTag = cipher.Concat(authTag).ToArray();
            var nonceWithIntro = nonceIntro.Concat(nonce).ToArray();

            var bytesToReturn = nonceWithIntro.Concat(cipherTextTag).ToArray();

            var tt = Convert.ToBase64String(authTag);

            return Task.FromResult(bytesToReturn);
        }

        public Task<string> Decrypt(string base64String)
        {
            byte[] key = Convert.FromBase64String(Constants.Key);
            var bytes = Convert.FromBase64String(base64String);

            var nonce = FillByteArray(bytes, 4, Constants.NonceLength);         

            var authTag = FillByteArray(bytes, 25, Constants.TagLength);

            var chiper = FillByteArray(bytes, 16, Constants.ChiperLength);

            byte[] decryptedBytes = new byte[chiper.Length];

            using (var aesgcm = new AesGcm(key))
            aesgcm.Decrypt(nonce, chiper, authTag, decryptedBytes);

            var stringDecryptet = Encoding.UTF8.GetString(decryptedBytes);

            return Task.FromResult(stringDecryptet);
        }

        private static byte[] FillByteArray(byte[] bytes, int sourceIndex, int length)
        {
            byte[] byteArray = new byte[length];
            Array.Copy(bytes, sourceIndex, byteArray, 0, length);
            return byteArray;
        }
    }
}