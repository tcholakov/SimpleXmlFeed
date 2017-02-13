namespace SimpleXmlFeed.Services.Data
{
    using System;
    using System.Linq;

    using Contracts;
    using SimpleXmlFeed.Data.Contracts;
    using Models.Data;

    public class BetsService : IBetsService
    {
        private readonly IRepository<Bet> bets;

        public BetsService(IRepository<Bet> bets)
        {
            this.bets = bets;
        }

        public Bet Add(string name, int feedId, Match match, bool isLive)
        {
            Bet betToAdd = new Bet
            {
                Name = name,
                FeedId = feedId,
                Match = match,
                IsLive = isLive
            };

            this.bets.Add(betToAdd);

            return betToAdd;
        }

        public Bet Add(string name, int feedId, Match match, bool isLive, bool saveChanges = true)
        {
            Bet betToAdd = new Bet
            {
                Name = name,
                FeedId = feedId,
                Match = match,
                IsLive = isLive
            };

            this.bets.Add(betToAdd);

            if (saveChanges)
            {
                this.SaveChanges();
            }

            return betToAdd;
        }

        public Bet GetBetByFeedId(int feedId)
        {
            Bet resultBet = this.bets
                                .All()
                                .Where(bet => bet.FeedId == feedId)
                                .FirstOrDefault();

            return resultBet;
        }

        public Guid GetBetIdByFeedId(int feedId)
        {
            Guid betId = this.bets
                                .All()
                                .Where(bet => bet.FeedId == feedId)
                                .Select(bet => bet.Id)
                                .FirstOrDefault();

            return betId;
        }
       
        public Bet UpdateBet(string name, int feedId, bool isLive, bool saveChanges = true)
        {
            Bet betToUpdate = this.GetBetByFeedId(feedId);

            if (betToUpdate != null)
            {
                if (betToUpdate.Name != name || betToUpdate.IsLive != isLive)
                {
                    betToUpdate.Name = name;
                    betToUpdate.IsLive = isLive;
                }

                this.bets.Update(betToUpdate);


                if (saveChanges)
                {
                    this.SaveChanges();
                }
            }

            return betToUpdate;
        }

        public int SaveChanges()
        {
            return this.bets.SaveChanges();
        }
    }
}
