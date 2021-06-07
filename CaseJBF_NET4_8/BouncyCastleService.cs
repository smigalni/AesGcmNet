using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Text;

namespace CaseJBF_NET4_8
{
    public class BouncyCastleService
    {
        private const string ALGORITHM = "AES";

        private const byte AesIvSize = 12;
        private const byte GcmTagSize = 16; // in bytes

        private readonly CipherMode _cipherMode = CipherMode.GCM;

        private string _algorithm = $"{ALGORITHM}/{CipherMode.GCM}/{Padding.NoPadding}";
        public byte[] Encrypt()
        {
            var random = new SecureRandom();
            var iv = random.GenerateSeed(AesIvSize);

            var key = Convert.FromBase64String(Constants.Key);

            var keyParameters = CreateKeyParameters(key, iv, GcmTagSize * 8);

            var cipher = CipherUtilities.GetCipher(_algorithm);
            cipher.Init(true, keyParameters);

            var plainTextData = Encoding.UTF8.GetBytes(Constants.StringToEncrypt);
            var cipherText = cipher.DoFinal(plainTextData);

            byte[] nonceIntro = { 0, 0, 0, 12 };

            var byteToReturn = new byte[41];
            for (int inn = 0; inn <= 3; inn++)
            {
                byteToReturn[inn] = nonceIntro[inn];
            }
            var i1 = 4;
            for (int i = 0; i < iv.Length; i++)
            {
                byteToReturn[i1] = iv[i];
                i1++;
            }

            var ii2 = 16;

            for (int i = 0; i < cipherText.Length; i++)
            {
                byteToReturn[ii2] = cipherText[i];
                ii2++;
            }
            return byteToReturn;
        }

        public string Decrypt(string cipherText)
        {
            var key = Convert.FromBase64String(Constants.Key);
            var (encryptedBytes, iv, tagSize) = UnpackCipherData(cipherText);
            var keyParameters = CreateKeyParameters(key, iv, tagSize * 8);
            var cipher = CipherUtilities.GetCipher(_algorithm);
            cipher.Init(false, keyParameters);

            var decryptedData = cipher.DoFinal(encryptedBytes);
            return Encoding.UTF8.GetString(decryptedData);
        }
        private (byte[], byte[], byte) UnpackCipherData(string encodedString)
        {
            var bytes = Convert.FromBase64String(encodedString);
            byte[] iv = new byte[12];
            byte[] encryptedBytes = new byte[25];

            Array.Copy(bytes, 4, iv, 0, 12);
            Array.Copy(bytes, 16, encryptedBytes, 0, 25);

            return (encryptedBytes, iv, 16);
        }     

        private ICipherParameters CreateKeyParameters(byte[] key, byte[] iv, int macSize)
        {
            var keyParameter = new KeyParameter(key);
            if (_cipherMode == CipherMode.CBC)
            {
                return new ParametersWithIV(keyParameter, iv);
            }
            else if (_cipherMode == CipherMode.GCM)
            {
                return new AeadParameters(keyParameter, macSize, iv);
            }

            throw new Exception("Unsupported cipher mode");
        }     
    }
    public enum CipherMode
    {
        CBC,
        GCM
    }

    public enum Padding
    {
        NoPadding,
        PKCS7
    }
}
