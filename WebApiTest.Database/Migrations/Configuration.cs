namespace WebApiTest.Database.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Common.Enms;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<WebApiTestDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "WebApiTest.Database.Data.WebApiTestDbContext";
        }

        protected override void Seed(WebApiTestDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            int[] roleTypeValues = Enum.GetValues(typeof(RoleType)) as int[];
            string[] roleTypeNames = Enum.GetNames(typeof(RoleType));
            for (int i = 0; i < roleTypeValues.Length; i++)
            {
                context.Roles.AddOrUpdate(
                    p => p.RoleType,
                    new Role { Name = roleTypeNames[i], RoleType = roleTypeValues[i] });
            }
        }
    }
}
