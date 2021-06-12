# AesGcmNet

Played around with AES GSM no padding Encryption algorithm in .NET 4.8 and .NET 5.0.
In this exercise I have encrypted and decrypted string “123456789” with base 64 encrypted key “FnAD72andADb3AtmsXRv9w==”.
The encrypted string has following structure:

- Length of Nonce (4 bytes) 
(0,0,0,12)

- The IV/Nonce (12 bytes)	

- Encrypted Ciphertext 	(9 bytes)

- The Tag (16 bytes) (128 bits)

In Total encryption string is byte[41] array.

For .NET 5.0 I used AesGcm class from Namespace: System.Security.Cryptography.
For .NET 4.8 I used BouncyCastle NuGet package.
