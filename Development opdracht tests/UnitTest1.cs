using Xunit;
using Development_opdracht;
using System.IO;
using System;
using System.Configuration;

namespace Development_opdracht_tests
{
    public class SFTP_UnitTest
    {
        private void setConfig()
        {
            ConfigurationManager.AppSettings["host"] = "127.0.0.1";
            ConfigurationManager.AppSettings["username"] = "leonvnoord";
            ConfigurationManager.AppSettings["password"] = "asdfasdf";
        }

        [Fact]
        public void Test_DownloadXML()
        {
            // arrange
            string localDirectory = Directory.GetCurrentDirectory();
            string file = "xmlfile.xml";
            string localPath = Path.Combine(localDirectory, file);

            setConfig();

            // act
            bool result = SFTP.DownloadXML(localPath, file);

            // assert
            Assert.True(result);
        }


        [Fact]
        public void Test_DownloadPDF()
        {
            // arrange
            string localDirectory = Directory.GetCurrentDirectory();
            string file = "pdffile.pdf";
            string localPath = Path.Combine(localDirectory, file);

            setConfig();

            // act
            bool result = SFTP.DownloadPDF(localPath, file);

            // assert
            Assert.True(result);
        }


        [Fact]
        public void Test_DownloadDirectory()
        {
            // arrange
            string localDirectory = Directory.GetCurrentDirectory();
            string remoteDirectory = "/";

            setConfig();

            // act
            bool result = SFTP.DownloadDirectory(localDirectory, remoteDirectory);

            // assert
            Assert.True(result);
        }




        [Fact]
        public void Test_DownloadXML_InvalidXML()
        {
            // arrange
            string localDirectory = Directory.GetCurrentDirectory();
            string file = "xmlfile_invalid.xml";
            string localPath = Path.Combine(localDirectory, file);

            setConfig();

            // act
            bool result = SFTP.DownloadXML(localPath, file);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void Test_DownloadXML_InvalidConfig()
        {
            // arrange
            string localDirectory = Directory.GetCurrentDirectory();
            string file = "xmlfile.xml";
            string localPath = Path.Combine(localDirectory, file);

            ConfigurationManager.AppSettings["host"] = "abcdefg";
            ConfigurationManager.AppSettings["username"] = "abcdefg";
            ConfigurationManager.AppSettings["password"] = "abcdefg";

            // act
            bool result = SFTP.DownloadXML(localPath, file);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void Test_DownloadXML_InvalidRemoteFile()
        {
            // arrange
            string localDirectory = Directory.GetCurrentDirectory();
            string file = "asdfasdf";
            string localPath = Path.Combine(localDirectory, file);

            setConfig();

            // act
            bool result = SFTP.DownloadXML(localPath, file);

            // assert
            Assert.False(result);
        }


    }

}