namespace WebApiTest.Database.Contracts
{
    using System.Data.Entity;
    using Models;
    using System.Data.Entity.Infrastructure;

    public interface IWebApiTestDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Role> Roles { get; set; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        void Dispose();

        int SaveChanges();
    }
}
