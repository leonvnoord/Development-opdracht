using Renci.SshNet;
using System.Configuration;
using Renci.SshNet.Common;
using System.Xml;
using Renci.SshNet.Sftp;

namespace Development_opdracht
{
    public static class SFTP
    {
        private static string sHost = ConfigurationManager.AppSettings.Get("host");
        private static string sUsername = ConfigurationManager.AppSettings.Get("username");
        private static string sPassword = ConfigurationManager.AppSettings.Get("password");

        private static bool throwExceptions = false;

        /// <summary>
        /// Downloads a PDF file from the remote path on the SFTP server to the local path.
        /// SFTP server is configured in the App.config file
        /// </summary>
        /// <param name="localPath">Local path</param>
        /// <param name="remotePath">Remote path</param>
        /// <returns>Returns true if succesfull, otherwise retuns false.</returns>
        public static bool DownloadPDF(string localPath, string remotePath)
        {
            try
            {

                var connectionInfo = new ConnectionInfo(sHost, sUsername, new PasswordAuthenticationMethod(sUsername, sPassword));

                using (var sftp = new SftpClient(connectionInfo))
                {
                    sftp.Connect();

                    Stream file = File.OpenWrite(localPath);
                    sftp.DownloadFile(remotePath, file);
                }

                Console.WriteLine("Download succeeded");
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (throwExceptions) throw;
                return false;
            }
        }

        /// <summary>
        /// Downloads an XML file from the remote path on the SFTP server to the local path.
        /// SFTP server is configured in the App.config file
        /// </summary>
        /// <param name="localPath">Local path</param>
        /// <param name="remotePath">Remote path</param>
        /// <returns>Returns true if succesfull, otherwise retuns false. Will fail if the XML is not valid</returns>
        public static bool DownloadXML(string localPath, string remotePath)
        {
            try
            {
                var connectionInfo = new ConnectionInfo(sHost, sUsername, new PasswordAuthenticationMethod(sUsername, sPassword));

                using (var sftp = new SftpClient(connectionInfo))
                {
                    sftp.Connect();

                    Stream fileWrite = File.OpenWrite(localPath);
                    sftp.DownloadFile(remotePath, fileWrite);
                    fileWrite.Close();

                    Stream fileRead = File.OpenRead(localPath);
                    XmlDocument xml = new XmlDocument();

                    xml.Load(fileRead);

                }

                Console.WriteLine("Download succeeded");
                return true;
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (throwExceptions) throw;
                return false;
            }
        }

        /// <summary>
        /// Downloads a directory from the remote path on the SFTP server to the local path.
        /// SFTP server is configured in the App.config file
        /// </summary>
        /// <param name="localPath">Local path</param>
        /// <param name="remotePath">Remote path</param>
        /// <returns>Returns true if succesfull, otherwise retuns false.</returns>
        public static bool DownloadDirectory(string localPath, string remotePath)
        {
            try
            {

                var connectionInfo = new ConnectionInfo(sHost, sUsername, new PasswordAuthenticationMethod(sUsername, sPassword));

                using (var sftp = new SftpClient(connectionInfo))
                {
                    sftp.Connect();
                    foreach (SftpFile sftpFile in sftp.ListDirectory(remotePath))
                    {
                        Stream file = File.OpenWrite(Path.Join(localPath, sftpFile.Name));
                        sftp.DownloadFile(sftpFile.FullName, file);
                    }

                }

                Console.WriteLine("Download succeeded");
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (throwExceptions) throw;
                return false;
            }
        }
    }

}
