using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prowler.KeyPass.Core.Helper;

namespace Prowler.KeyPass.Core.Entities
{
    [Serializable]
    public class KeyPassEntity : IKeyPassEntity
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string GroupIconID { get; set; } = string.Empty;
        public string KeyIconID { get; set; } = string.Empty;

        public string GetPassword(ILoginCipher cipher)
        {
            if (Password == null)
            {
                return string.Empty;
            }

            return cipher.Decrypt(Password);
        }

        public void SetPassword(ILoginCipher cipher, string password)
        {
            Password = cipher.Encrypt(password);
        }
    }
}
