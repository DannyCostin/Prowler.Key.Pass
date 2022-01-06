using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prowler.KeyPass.Core
{
    public interface ILoginCipher
    {
        public string EncryptedPassword { get; set; }
        public string EncryptedGui { get; set; }
    }
}
