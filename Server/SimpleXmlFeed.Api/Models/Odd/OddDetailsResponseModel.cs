namespace SimpleXmlFeed.Api.Models.Odd
{
    using Abstractions;
    using Infrastructure.Mappings;
    using SimpleXmlFeed.Models.Data;

    public class OddDetailsResponseModel : BaseItemResponseModel,IMapFrom<Odd>
    {
        public decimal Value { get; set; }

        public string SpecialBetValue { get; set; }
    }
}