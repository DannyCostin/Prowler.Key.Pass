using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prowler.Cipher;

namespace Prowler.KeyPass.Core.Helper
{
    internal static class LoginCipher
    {
        public static string Encrypt(this ILoginCipher cipher, string plainText)
        {
            return plainText.Encrypt(cipher.Decrypt());
        }

        public static Task<string> EncryptAsync(this ILoginCipher cipher, string plainText)
        {
            return Task.Run(() => Encrypt(cipher, plainText));
        }

        public static string Decrypt(this ILoginCipher cipher, string cipherText)
        {
            return cipherText.Decrypt(cipher.Decrypt());
        }

        internal static string Decrypt(this ILoginCipher cipher)
        {
            return cipher.EncryptedPassword.Decrypt(cipher.EncryptedGui);
        }

        public static Task<string> DecryptAsync(this ILoginCipher cipher, string cipherText)
        {
            return Task.Run(() => Decrypt(cipher, cipherText));
        }

        public static string GetPasswordHash(this ILoginCipher cipher)
        {
            return StringHash.GetHash(cipher.Decrypt());
        }

        internal static Task<string> DecryptAsync(this ILoginCipher cipher)
        {
            return Task.Run(() => Decrypt(cipher));
        }
    }
}
