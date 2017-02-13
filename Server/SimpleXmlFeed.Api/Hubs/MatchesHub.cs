namespace SimpleXmlFeed.Api.Hubs
{
    using Microsoft.AspNet.SignalR;
    using Models.Match;
    using Common;
    using SimpleXmlFeed.Services.Data.Contracts;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.SignalR.Hubs;
    using Common.Infrastructure;
    using Newtonsoft.Json;
    
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