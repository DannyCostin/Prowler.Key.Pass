using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prowler.KeyPass.Core
{
    public interface IKeyPass
    {
        public IResult<IKeyPassFile> Open(string file, ILoginCipher cipher);
        public Task<IResult<IKeyPassFile>> OpenAsync(string file, ILoginCipher cipher);
        public IResult<bool> Save(string file, ILoginCipher cipher, IKeyPassFile entity);
        public Task<IResult<bool>> SaveAsync(string file, ILoginCipher cipher, IKeyPassFile entity);
        public IResult<IKeyPassFile> New(ILoginCipher cipher);
        public Task<IResult<IKeyPassFile>> NewAsync(ILoginCipher cipher);
        public IResult<bool> ChangePassword(ILoginCipher newChipher, ILoginCipher cipher, IKeyPassFile entity);
        public Task<IResult<bool>> ChangePasswordAsync(ILoginCipher newChipher, ILoginCipher cipher, IKeyPassFile entity);
    }
}
