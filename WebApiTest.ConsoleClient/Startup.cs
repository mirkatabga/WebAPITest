using System.Data.Entity;
using System.Linq;
using WebApiTest.Database.Data;
using WebApiTest.Database.Repositories;
using WebApiTest.Database.Models;
using WebApiTest.Database.Data.Migrations;
using WebApiTest.Database;

namespace WebApiTest.ConsoleClient
{
    class Startup
    {
        static void Main(string[] args)
        {
            System.Data.Entity.Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<WebApiTestDbContext, Configuration>());

            IRepository<User> usersRepo = new Repository<User>(new WebApiTestDbContext());
            User user = usersRepo.GetById(1);
        }
    }
}
