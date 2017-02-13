namespace SimpleXmlFeed.Api.Hubs
{
    using System.Linq;
    
    using Microsoft.AspNet.SignalR;
    
    using AutoMapper.QueryableExtensions;

    using Common;
    using Common.Infrastructure;
    using SimpleXmlFeed.Services.Data.Contracts;
    using Models.Match;
    
    public class MatchesHub : Hub
    {
        private readonly IMatchesService matchesService;

        public MatchesHub()
            : this(ObjectFactory.Get<IMatchesService>())
        {
        }

        public MatchesHub(IMatchesService matchesService)
        {
            this.matchesService = matchesService;
        }

        public void MatchesWitOddsForToday()
        {
            var result = this.matchesService
                .AllMatchesWithOddsForNextHours(GlobalConstants.HoursForOneDay)
                .ProjectTo<MatchDetailsResponseModel>()
                .OrderBy(match => match.StartDate)
                .ToList(); ;

            Clients.Caller.getMatches(result);
        }
    }
}