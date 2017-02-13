namespace SimpleXmlFeed.Api
{
    using SimpleXmlFeed.Data;
    using SimpleXmlFeed.Data.Migrations;
    using System.Data.Entity;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SimpleXmlFeedDbContext, Configuration>());
            SimpleXmlFeedDbContext.Create().Database.Initialize(true);
        }
    }
}