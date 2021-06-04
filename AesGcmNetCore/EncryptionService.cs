using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace AesGcmNetCore
{
    public class EncryptionService
    {
        

        public Task<string> Encrypt()
        {
            var stringToEncrypt = Constants.StringToEncrypt;
            var key = Constants.Key;

            using (AesManaged myAes = new AesManaged())
            {
                // Encrypt the string to an array of bytes.
                byte[] encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);

                byte[] stringToEncryptByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            byte[] keyByteArray = Encoding.UTF8.GetBytes(key);

            byte[] encryptedStringByteArray = new byte[];

            var aesGcm = new AesGcm(keyByteArray);

            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            var nonceBitSize = 32; // 4 bytes
            byte[] nonce = new byte[nonceBitSize / 8]; 
            rng.GetBytes(nonce);

            aesGcm.Encrypt(
                nonce: nonce,
                plaintext: stringToEncryptByteArray,
                ciphertext: encryptedStringByteArray,
                tag: nonce
                ) ;

            var encryptedString = Encoding.UTF8.GetString(encryptedStringByteArray);
            return Task.FromResult(encryptedString);
        }
    }
}