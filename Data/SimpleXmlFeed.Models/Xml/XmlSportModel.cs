namespace SimpleXmlFeed.Models.Xml
{
    using SimpleXmlFeed.Models.Xml.Abstractions;
    using System.Collections.Generic;

    public class XmlSportModel : XmlBaseFeedItemModel
    {
        private ICollection<XmlEventModel> events;

        public XmlSportModel()
            :this(new List<XmlEventModel>())
        {
        }

        public XmlSportModel(ICollection<XmlEventModel> events)
        {
            this.events = events;
        }

        public ICollection<XmlEventModel> Events { get { return this.events; } }
    }
}
