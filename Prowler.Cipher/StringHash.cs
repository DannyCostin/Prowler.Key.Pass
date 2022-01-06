using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Prowler.Cipher
{
    public static class StringHash
    {
        public static bool IsValid(string? input, string hash)
        {
            return input == hash;
        }

        public static Task<bool> IsValidAsync(string? input, string hash)
        {
            return Task.Run(() => IsValid(input, hash));
        }

        public static string GetHash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return GetHash(sha256Hash, input);
            }
        }

        public static Task<string> GetHashAsync(string input)
        {
            return Task.Run(() => GetHash(input));
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }       
    }
}
