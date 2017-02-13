namespace SimpleXmlFeed.Models.Xml
{
    using Abstractions;

    public class XmlOddModel : XmlBaseFeedItemModel
    {
        public decimal Value { get; set; }

        public string SpecialBetValue { get; set; }
    }
}
