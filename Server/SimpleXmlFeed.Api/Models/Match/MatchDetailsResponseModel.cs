namespace SimpleXmlFeed.Api.Models.Match
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using Abstractions;
    using Bet;
    using Infrastructure.Mappings;
    using SimpleXmlFeed.Models.Data;
    
    public class MatchDetailsResponseModel : BaseItemResponseModel,IMapFrom<Match>,IHaveCustomMappings
    {
        public DateTime StartDate { get; set; }

        public string MatchType { get; set; }

        public string Event { get; set; }

        public string Sport { get; set; }

        public ICollection<BetDetailsResponseModel> Bets { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Match, MatchDetailsResponseModel>()
                .ForMember(match => match.Event, opts => opts.MapFrom(match => match.Event.Name))
                .ForMember(match => match.Sport, opts => opts.MapFrom(Match => Match.Event.Sport.Name));
        }
    }
}