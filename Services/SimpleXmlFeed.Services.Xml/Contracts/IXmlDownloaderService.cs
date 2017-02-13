namespace SimpleXmlFeed.Services.Xml.Contracts
{
    using System.IO;

    public interface IXmlDownloaderService
    {
        StreamReader DownloadFeed(string url);
    }
}
