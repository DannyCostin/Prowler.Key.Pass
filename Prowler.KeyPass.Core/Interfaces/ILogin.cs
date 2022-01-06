using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prowler.KeyPass.Core
{
    public interface ILogin
    {
        public ILoginCipher CreateLogin(string password);        
    }
}
