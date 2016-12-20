namespace WebApiTest
{
    using System.Data.Entity;
    using WebApiTest.Database;
    using WebApiTest.Database.Data.Migrations;

    public static class DatabaseConfig
    {
        public static void RegisterDatabase()
        {
            System.Data.Entity.Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<WebApiTestDbContext, Configuration>());
        }
    }
}