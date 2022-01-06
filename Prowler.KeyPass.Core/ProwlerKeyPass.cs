using Prowler.KeyPass.Core.Helper;
using Prowler.KeyPass.Core.Repository;
using System.Threading.Tasks;

namespace Prowler.KeyPass.Core
{
    public static class ProwlerKeyPass
    {
        public static ILoginCipher CreateLogin(string password)
        {
            return LoginRepository.Create().CreateLogin(password);
        }

        public static Task<ILoginCipher> CreateLoginAsync(string password)
        {
            return Task.Run(() => CreateLogin(password));
        }

        public static IKeyPass ProwlerKeyPassFile()
        {
            return KeyPassRepository.Create();
        }
    }
}
