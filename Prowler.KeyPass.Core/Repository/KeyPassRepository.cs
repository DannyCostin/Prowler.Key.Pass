using Prowler.Cipher;
using Prowler.KeyPass.Core.Entities;
using Prowler.KeyPass.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prowler.KeyPass.Core.Repository
{
    internal class KeyPassRepository : IKeyPass
    {
        private const string INVALID_PASSWORD = "Invalid password";        

        public IResult<IKeyPassFile> Open(string file, ILoginCipher cipher)
        {
            var result = Result<IKeyPassFile>.Create();

            try
            {
                var keyPassFileEntity = FileObject.Open(file, cipher);

                if (!FileObject.IsValidCipher(keyPassFileEntity, cipher))
                {
                    result.ErrorMessage = INVALID_PASSWORD;
                }

                result.Value = keyPassFileEntity;
            }
            catch
            {
                result.ErrorMessage = INVALID_PASSWORD;
            }

            return result;
        }

        public Task<IResult<IKeyPassFile>> OpenAsync(string file, ILoginCipher cipher)
        {
            return Task.Run(() => Open(file, cipher));
        }

        public IResult<bool> Save(string file, ILoginCipher cipher, IKeyPassFile entity)
        {
            var result = Result<bool>.Create();

            try
            {
                FileObject.SaveFile(entity, file, cipher);
                result.Value = true;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public Task<IResult<bool>> SaveAsync(string file, ILoginCipher cipher, IKeyPassFile entity)
        {
            return Task.Run(() => Save(file, cipher, entity));
        }

        public IResult<IKeyPassFile> New(ILoginCipher cipher)
        {
            var result = Result<IKeyPassFile>.Create();

            try
            {
                result.Value = KeyPassFile.Create(cipher);
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public Task<IResult<IKeyPassFile>> NewAsync(ILoginCipher cipher)
        {
            return Task.Run(() => New(cipher));
        }

        public IResult<bool> ChangePassword(ILoginCipher newChipher, ILoginCipher cipher, IKeyPassFile entity)
        {
            var result = Result<bool>.Create();

            try
            {
                foreach (var item in entity.DataSource)
                {
                    var password = item.GetPassword(cipher);
                    item.SetPassword(newChipher, password);
                }

                entity.PasswordHash = newChipher.GetPasswordHash();

                result.Value = true;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public Task<IResult<bool>> ChangePasswordAsync(ILoginCipher newChipher, ILoginCipher cipher, IKeyPassFile entity)
        {
            return Task.Run(() => ChangePassword(newChipher, cipher, entity));
        }

        public static IKeyPass Create()
        {
            return new KeyPassRepository();
        }
    }
}
