using Prowler.Cipher;
using Prowler.KeyPass.Core.Helper;
using System;
using System.Collections.Generic;

namespace Prowler.KeyPass.Core.Repository
{
    internal class LoginRepository : ILogin, ILoginCipher
    {
        public string EncryptedPassword { get; set; } = string.Empty;
        public string EncryptedGui { get; set; } = string.Empty;        
        
        public static ILogin Create()
        {
            return new LoginRepository();
        }
       
        public ILoginCipher CreateLogin(string password)
        {
            EncryptedGui = Guid.NewGuid().ToString();
            EncryptedPassword = StringCipher.Encrypt(password, EncryptedGui);

            return this;
        }                
    }
}
