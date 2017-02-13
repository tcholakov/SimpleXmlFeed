namespace SimpleXmlFeed.Services.Data
{
    using System;
    using System.Linq;

    using Contracts;
    using SimpleXmlFeed.Data.Contracts;
    using Models.Data;

    public class MatchesService : IMatchesService
    {
        private readonly IRepository<Match> matches;

        public MatchesService(IRepository<Match> matches)
        {
            this.matches = matches;
        }

        public Match Add(string name, int feedId, Event evnt, DateTime startDate, string matchType, bool saveChanges = true)
        {
            Match matchToAdd = new Match
            {
                Name = name,
                FeedId = feedId,
                Event = evnt,
                StartDate = startDate,
                MatchType = matchType
            };

            this.matches.Add(matchToAdd);

            if (saveChanges)
            {
                this.SaveChanges();
            }
           
            return matchToAdd;
        }

        public IQueryable<Match> AllMatchesWithOddsForNextHours(int nextHours)
        {
            DateTime dateFrom = DateTime.UtcNow;
            DateTime dateTo = DateTime.UtcNow.AddHours(nextHours);

            var matches = this.matches
                                .All()
                                .Where(match => (match.StartDate > dateFrom && match.StartDate <= dateTo)
                                                && (match.Bets.Any(bet => bet.Odds.Count > 0))
                                );

            return matches;
        }

        public Match GetMatchByFeedId(int feedId)
        {
            Match resultMatch = this.matches
                                .All()
                                .Where(match => match.FeedId == feedId)
                                .FirstOrDefault();

            return resultMatch;
        }

        public Guid GetMatchIdByFeedId(int feedId)
        {
            Guid matchId = this.matches
                                .All()
                                .Where(match => match.FeedId == feedId)
                                .Select(match => match.Id)
                                .FirstOrDefault();

            return matchId;
        }

        public Match UpdateMatch(string name, int feedId, DateTime startDate, string matchType, bool saveChanges = true)
        {
            Match matchToUpdate = this.GetMatchByFeedId(feedId);

            if (matchToUpdate != null)
            {
                if (matchToUpdate.Name != name || matchToUpdate.StartDate != startDate || matchToUpdate.MatchType != matchType)
                {
                    matchToUpdate.Name = name;
                    matchToUpdate.StartDate = startDate;
                    matchToUpdate.MatchType = matchType;
                }

                this.matches.Update(matchToUpdate);

                if (saveChanges)
                {
                    this.SaveChanges();
                }
            }

            return matchToUpdate;
        }

        public int SaveChanges()
        {
            return this.matches.SaveChanges();
        }
    }
}
