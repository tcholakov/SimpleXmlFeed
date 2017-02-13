namespace SimpleXmlFeed.Data
{
    using Models.Data;
    using System.Data.Entity;

    public class SimpleXmlFeedDbContext : DbContext, ISimpleXmlFeedDbContext
    {
        public SimpleXmlFeedDbContext()
            : base("DefaultConnection")
        {
        }

        public virtual IDbSet<Sport> Sports { get; set; }

        public virtual IDbSet<Event> Events { get; set; }

        public virtual IDbSet<Match> Matches { get; set; }

        public virtual IDbSet<Bet> Bets { get; set; }

        public virtual IDbSet<Odd> Odds { get; set; }

        public static SimpleXmlFeedDbContext Create()
        {
            return new SimpleXmlFeedDbContext();
        }
    }
}
