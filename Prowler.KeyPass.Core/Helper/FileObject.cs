using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Prowler.KeyPass.Core.Entities;
using Prowler.Cipher;

namespace Prowler.KeyPass.Core.Helper
{
    internal static class FileObject
    {
        internal static void SaveFile(object entity, string file, ILoginCipher cipher)
        {
            string jsonString = JsonSerializer.Serialize(entity);

            string dataObject = cipher.Encrypt(jsonString);

            File.WriteAllText(file, dataObject);
        }

        internal static IKeyPassFile? Open(string file, ILoginCipher cipher)
        {
            string dataObject = File.ReadAllText(file);
            string jsonString = cipher.Decrypt(dataObject);

            return JsonSerializer.Deserialize<KeyPassFile>(jsonString);
        }

        internal static bool IsValidCipher(IKeyPassFile? entity, ILoginCipher cipher)
        {
            if (entity == null)
            {
                return false;
            }

            return StringHash.IsValid(entity.PasswordHash, StringHash.GetHash(cipher.Decrypt()));
        }
    }
}
