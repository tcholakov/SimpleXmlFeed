namespace SimpleXmlFeed.Services.Xml.Contracts
{
    using System.Xml;
    using System.Collections.Generic;
    
    using Models.Xml;

    public interface IXmlConverterService
    {
        XmlSportModel ConvertToXmlSportModel(XmlNode sportNode);

        IEnumerable<XmlSportModel> ConvertXmlDocumentToCollectionOfXmlSportModel(XmlDocument document);
    }
}
