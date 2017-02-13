namespace SimpleXmlFeed.Services.Xml
{
    using System.IO;
    using System.Net;

    using SimpleXmlFeed.Services.Xml.Contracts;

    public class XmlDownloaderService : IXmlDownloaderService
    {
        private WebClient webClient;

        public XmlDownloaderService()
            : this(new WebClient())
        {
        }

        public XmlDownloaderService(WebClient webClient)
        {
            this.webClient = webClient;
        }
        
        public StreamReader DownloadFeed(string url)
        {
            Stream content = this.webClient.OpenRead(url);
            StreamReader contentReader = new StreamReader(content);

            return contentReader;
        }
    }
}
