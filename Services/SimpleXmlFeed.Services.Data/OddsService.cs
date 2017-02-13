namespace SimpleXmlFeed.Services.Data
{
    using System;
    using System.Linq;
    
    using Contracts;
    using SimpleXmlFeed.Data.Contracts;
    using Models.Data;

    public class OddsService : IOddsService
    {
        private readonly IRepository<Odd> odds;

        public OddsService(IRepository<Odd> odds)
        {
            this.odds = odds;
        }

        public Odd Add(string name, int feedId, Bet bet, decimal value, string specialValue, bool saveChanges = true)
        {
            Odd oddToAdd = new Odd
            {
                Name = name,
                FeedId = feedId,
                Bet = bet,
                Value = value,
                SpecialBetValue = specialValue
            };

            this.odds.Add(oddToAdd);

            if (saveChanges)
            {
                this.SaveChanges();
            }

            return oddToAdd;
        }

        public Guid GetOddIdByFeedId(int feedId)
        {
            Guid oddId = this.odds
                            .All()
                            .Where(odd => odd.FeedId == feedId)
                            .Select(odd => odd.Id)
                            .FirstOrDefault();

            return oddId;
        }

        public Odd GetOddByFeedId(int feedId)
        {
            Odd resultOdd = this.odds
                                    .All()
                                    .Where(odd => odd.FeedId == feedId)
                                    .FirstOrDefault();

            return resultOdd;
        }
        
        public Odd UpdateOdd(string name, int feedId, decimal value, string specialValue, bool saveChanges = true)
        {
            Odd oddToUpdate = this.GetOddByFeedId(feedId);

            if (oddToUpdate != null)
            {
                if (oddToUpdate.Name != name || oddToUpdate.Value != value || oddToUpdate.SpecialBetValue != specialValue)
                {
                    oddToUpdate.Name = name;
                    oddToUpdate.Value = value;
                    oddToUpdate.SpecialBetValue = specialValue;
                }

                this.odds.Update(oddToUpdate);
                
                if (saveChanges)
                {
                    this.SaveChanges();
                }
            }

            return oddToUpdate;
        }

        public int SaveChanges()
        {
            return this.odds.SaveChanges();
        }
    }
}
