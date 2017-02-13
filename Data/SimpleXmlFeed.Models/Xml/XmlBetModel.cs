namespace SimpleXmlFeed.Models.Xml
{
    using Abstractions;
    using Contracts;
    using System.Collections.Generic;

    public class XmlBetModel : XmlBaseFeedItemModel, ILiveable
    {
        private ICollection<XmlOddModel> odds;
        
        public XmlBetModel()
            :this(new List<XmlOddModel>())
        {
        }

        public XmlBetModel(ICollection<XmlOddModel> odds)
        {
            this.odds = odds;
        }

        public bool IsLive { get; set; }

        public XmlMatchModel Parent { get; set; }

        public ICollection<XmlOddModel> Odds { get { return this.odds; } }
    }
}
