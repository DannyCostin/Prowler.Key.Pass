using Prowler.KeyPass.Core.Entities;
using Prowler.Cipher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prowler.KeyPass.Core.Helper;


namespace Prowler.KeyPass.Core
{
    internal class KeyPassFile : IKeyPassFile
    {        
        public string? PasswordHash { get; set; }
        public IList<KeyPassEntity> DataSource { get; set; }        

        public KeyPassFile()
        {
            DataSource = new List<KeyPassEntity>();            
        }

        public IKeyPassEntity Add()
        {
            var entry = new KeyPassEntity { ID = GetNewEntryID() };
            DataSource.Add(entry);

            return entry;
        }

        public void Delete(KeyPassEntity entity)
        {
            DataSource.Remove(entity);
        }
      
        public static IKeyPassFile Create(ILoginCipher chipher)
        {
            return new KeyPassFile()
            {
                PasswordHash = chipher.GetPasswordHash()
            };        
        }

        private int GetNewEntryID()
        {
            if (DataSource == null || !DataSource.Any())
            {
                return 1;
            }

            return DataSource.Max(i => i.ID) + 1;
        }
    }
}
