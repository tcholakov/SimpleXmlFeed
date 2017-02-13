namespace SimpleXmlFeed.Data
{
    using Models.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface ISimpleXmlFeedDbContext
    {
        IDbSet<Sport> Sports { get; set; }

        IDbSet<Event> Events { get; set; }

        IDbSet<Match> Matches { get; set; }

        IDbSet<Bet> Bets { get; set; }

        IDbSet<Odd> Odds { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();

        int SaveChanges();
    }
}
