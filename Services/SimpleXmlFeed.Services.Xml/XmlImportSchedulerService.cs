namespace SimpleXmlFeed.Services.Xml
{
    using System.Threading;
    using System.IO;
    using System.Xml;
    using System.Collections.Generic;

    using Contracts;
    using Common;
    using Models.Xml;
    using Common.Infrastructure;

    public class XmlImportSchedulerService : IXmlImportSchedulerService
    {
        private readonly IXmlDownloaderService xmlDownloaderService;
        private readonly IXmlConverterService xmlConverterService;
        private readonly IXmlImporterService xmlImporterService;

        private Timer timer;

        public XmlImportSchedulerService(IXmlDownloaderService xmlDownloaderService, IXmlConverterService xmlConverterService, IXmlImporterService xmlImporterService)
        {
            this.xmlDownloaderService = xmlDownloaderService;
            this.xmlConverterService = xmlConverterService;
            this.xmlImporterService = xmlImporterService;
        }

        public void ExecuteImportInInterval()
        {
            this.timer = new Timer(TimerCallback, null, 0, Timeout.Infinite);
        }

        private void TimerCallback(object state)
        {
            using (StreamReader contentReader = this.xmlDownloaderService.DownloadFeed(GlobalConstants.XmlFeedUrl))
            {
                XmlDocument document = new XmlDocument();
                document.Load(contentReader);

                IEnumerable<XmlSportModel> xmlSports = this.xmlConverterService.ConvertXmlDocumentToCollectionOfXmlSportModel(document);
                this.xmlImporterService.Import(xmlSports);

                this.timer.Change(GlobalConstants.ExecutionIntervalInSeconds * 1000, Timeout.Infinite);
            }
        }

        public static XmlImportSchedulerService Initialize()
        {
            return new XmlImportSchedulerService(ObjectFactory.Get<IXmlDownloaderService>(), ObjectFactory.Get<IXmlConverterService>(), ObjectFactory.Get<IXmlImporterService>());
        }
    }
}
