namespace SimpleXmlFeed.Services.Data
{
    using System;
    using System.Linq;

    using SimpleXmlFeed.Services.Data.Contracts;
    using SimpleXmlFeed.Data.Contracts;
    using Models.Data;

    public class SportsService : ISportsService
    {
        private readonly IRepository<Sport> sports;

        public SportsService(IRepository<Sport> sports)
        {
            this.sports = sports;
        }

        public Sport Add(string name, int feedId, bool saveChanges)
        {
            Sport sportToAdd = new Sport
            {
                Name = name,
                FeedId = feedId
            };

            this.sports.Add(sportToAdd);

            if (saveChanges)
            {
                this.SaveChanges();
            }

            return sportToAdd;
        }



        public Guid GetSportIdByFeedId(int feedId)
        {
            Guid sportId = this.sports
                                .All()
                                .Where(sport => sport.FeedId == feedId)
                                .Select(sport => sport.Id)
                                .FirstOrDefault();

            return sportId;
        }

        public Sport GetSportByFeedId(int feedId)
        {
            Sport resultSport = this.sports
                                        .All()
                                        .Where(sport => sport.FeedId == feedId)
                                        .FirstOrDefault();

            return resultSport;
        }

        public Sport UpdateSport(string name, int feedId, bool saveChanges = true)
        {
            Sport sportToUpdate = this.GetSportByFeedId(feedId);

            if (sportToUpdate != null)
            {
                if (sportToUpdate.Name != name)
                {
                    sportToUpdate.Name = name;
                }

                this.sports.Update(sportToUpdate);

                if (saveChanges)
                {
                    this.SaveChanges();
                }
            }

            return sportToUpdate;
        }

        public int SaveChanges()
        {
            return this.sports.SaveChanges();
        }
    }
}
