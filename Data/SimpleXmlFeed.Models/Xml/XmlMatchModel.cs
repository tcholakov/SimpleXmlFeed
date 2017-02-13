namespace SimpleXmlFeed.Models.Xml
{
    using Abstractions;
    using System;
    using System.Collections.Generic;

    public class XmlMatchModel : XmlBaseFeedItemModel
    {
        private ICollection<XmlBetModel> bets;

        public XmlMatchModel()
            :this(new List<XmlBetModel>())
        {
        }

        public XmlMatchModel(ICollection<XmlBetModel> bets)
        {
            this.bets = bets;
        }

        public DateTime StartDate { get; set; }

        public string MatchType { get; set; }

        public ICollection<XmlBetModel> Bets { get { return this.bets; } }
    }
}
