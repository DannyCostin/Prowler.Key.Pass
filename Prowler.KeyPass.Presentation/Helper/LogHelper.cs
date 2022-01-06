using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prowler.KeyPass.Presentation.Helper
{
    internal static class LogHelper
    {
        private static string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Prowler Key Pass");

        public static void WriteLog(string message)
        {
            try
            {
                File.AppendAllText(Path.Combine(folder, "_err.log"), message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
