using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UploadFileToFTPSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(Properties.Settings.Default.Username, Properties.Settings.Default.Password);

                    client.UploadFile(Properties.Settings.Default.Site + Properties.Settings.Default.Filename, WebRequestMethods.Ftp.UploadFile, Properties.Settings.Default.FilePath);

                    string message = $"Transfer succeeded of {Properties.Settings.Default.FilePath}";
                    Console.WriteLine(message);
                    logToFile(message);
                }
            }
            catch (Exception ex)
            {
                StringBuilder bld = new StringBuilder();

                Console.WriteLine(ex.Message);
                bld.AppendLine(ex.Message);

                Console.WriteLine(ex.StackTrace);
                bld.AppendLine(ex.StackTrace);
                bld.AppendLine();

                Exception inner = ex.InnerException;

                while(inner != null)
                {
                    Console.WriteLine(inner.Message);
                    bld.AppendLine(inner.Message);

                    Console.WriteLine(inner.StackTrace);
                    bld.AppendLine(inner.StackTrace);
                    bld.AppendLine();

                    inner = inner.InnerException;
                }

                logToFile(bld.ToString());
            }

            Console.WriteLine("Press anykey to exit ...");
            Console.Read();
        }

        private static void logToFile(string data)
        {
            File.AppendAllText(Properties.Settings.Default.LogFile, Environment.NewLine);
            File.AppendAllText(Properties.Settings.Default.LogFile, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            File.AppendAllText(Properties.Settings.Default.LogFile, Environment.NewLine);
            File.AppendAllText(Properties.Settings.Default.LogFile, data);
            File.AppendAllText(Properties.Settings.Default.LogFile, Environment.NewLine);
            File.AppendAllText(Properties.Settings.Default.LogFile, "------------------");
            File.AppendAllText(Properties.Settings.Default.LogFile, Environment.NewLine);
        }
    }
}
