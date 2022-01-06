using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prowler.KeyPass.Core
{
    public interface IKeyPassEntity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Group { get; set; }
        public string UserName { get; set; }
        public string Password { get; }
        public string Url { get; set; }
        public string Notes { get; set; }
        public string GroupIconID { get; set; }
        public string KeyIconID { get; set; }

        public void SetPassword(ILoginCipher cipher, string password);
        public string GetPassword(ILoginCipher cipher);
    }
}
