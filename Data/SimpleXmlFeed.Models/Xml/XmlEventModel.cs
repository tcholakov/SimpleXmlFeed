namespace SimpleXmlFeed.Models.Xml
{
    using Contracts;
    using SimpleXmlFeed.Models.Xml.Abstractions;
    using System.Collections.Generic;

    public class XmlEventModel : XmlBaseFeedItemModel, ILiveable
    {
        private ICollection<XmlMatchModel> matches;

        public XmlEventModel()
            :this(new List<XmlMatchModel>())
        {
        }

        public XmlEventModel(ICollection<XmlMatchModel> matches)
        {
            this.matches = matches;
        }

        public bool IsLive { get; set; }

        public int CategoryId { get; set; }

        public ICollection<XmlMatchModel> Matches { get { return this.matches; } }
    }
}
