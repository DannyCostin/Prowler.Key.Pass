using Prowler.KeyPass.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prowler.KeyPass.Core
{
    public interface IKeyPassFile
    {
        public string? PasswordHash { get; set; }
        public IList<KeyPassEntity> DataSource { get; set; }
        public IKeyPassEntity Add();
        public void Delete(KeyPassEntity entity);
    }
}
