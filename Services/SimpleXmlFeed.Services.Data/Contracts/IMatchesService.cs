namespace SimpleXmlFeed.Services.Data.Contracts
{
    using System;
    using System.Linq;

    using Models.Data;

    public interface IMatchesService : ITransactable
    {
        Match Add(string name, int feedId, Event evnt, DateTime startDate, string matchType, bool saveChanges = true);

        Guid GetMatchIdByFeedId(int feedId);

        Match GetMatchByFeedId(int feedId);

        IQueryable<Match> AllMatchesWithOddsForNextHours(int nextHours);

        Match UpdateMatch(string name, int feedId, DateTime startDate, string matchType, bool saveChanges = true);
    }
}
