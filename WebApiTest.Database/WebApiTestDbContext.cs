using System;
using System.Data.Entity;
using WebApiTest.Database.Contracts;
using WebApiTest.Database.Models;

namespace WebApiTest.Database
{
    public class WebApiTestDbContext : DbContext, IWebApiTestDbContext
    {
        public WebApiTestDbContext()
            :base("DefaultConnection")
        {
        }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Role> Roles { get; set; }

        IDbSet<TEntity> IWebApiTestDbContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public static Func<IDisposable> Create()
        {
            return () => new WebApiTestDbContext();
        }
    }
}
