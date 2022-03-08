
using Development_opdracht;

string localDirectory = Directory.GetCurrentDirectory();


//SFTP.DownloadPDF(Path.Join(localDirectory, "pdffile.pdf"), "pdffile.pdf");
bool result = SFTP.DownloadXML(Path.Join(localDirectory, "xmlfile.xml"), "xmlfile.xml");
//SFTP.DownloadXML(Path.Join(localDirectory, "xmlfile_invalid.xml"), "xmlfile_invalid.xml");
//SFTP.DownloadDirectory(localDirectory, "/");


