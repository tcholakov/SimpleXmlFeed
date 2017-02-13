namespace SimpleXmlFeed.Api.Models.Bet
{
    using System.Collections.Generic;

    using Abstractions;
    using Odd;
    using Infrastructure.Mappings;
    using SimpleXmlFeed.Models.Data;

    public class BetDetailsResponseModel : BaseItemResponseModel, IMapFrom<Bet>
    {
        public bool IsLive { get; set; }

        public ICollection<OddDetailsResponseModel> Odds { get; set; }
    }
}