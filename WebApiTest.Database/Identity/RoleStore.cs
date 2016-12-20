namespace WebApiTest.Database.Data.Identity
{
    using Contracts;
    using Database.Repositories;
    using Microsoft.AspNet.Identity;
    using Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class RoleStore<TRole> : IQueryableRoleStore<TRole, int>
        where TRole : Role
    {
        private IWebApiTestDbContext dbContext;

        public RoleStore(IWebApiTestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<TRole> Roles
        {
            get
            {
                return this.dbContext.Roles.Select(r => r as TRole).AsQueryable();
            }
        }

        public Task CreateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this.dbContext.Roles.Attach(role);
            this.dbContext.Roles.Add(role);
            this.dbContext.SaveChanges();

            return Task.FromResult<TRole>(null);
        }

        public Task DeleteAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this.dbContext.Roles.Attach(role);
            this.dbContext.Roles.Remove(role);
            this.dbContext.SaveChanges();

            return Task.FromResult<TRole>(null);
        }

        public void Dispose()
        {
            if (this.dbContext != null)
            {
                this.dbContext.Dispose();
                this.dbContext = null;
            }
        }

        public Task<TRole> FindByIdAsync(int roleId)
        {
            TRole role = this.dbContext.Roles.Find(roleId) as TRole;

            if (role == null)
            {
                throw new ArgumentOutOfRangeException("roleId", "Role with that id was not found");
            }

            return Task.FromResult<TRole>(role);
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException("roleName");
            }

            TRole role = this.dbContext.Roles
                .FirstOrDefault(r => r.Name == roleName) as TRole;

            if (role == null)
            {
                throw new ArgumentOutOfRangeException("roleName", "Role with that name was not found");
            }

            return Task.FromResult<TRole>(role);
        }

        public Task UpdateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this.dbContext.Roles.Attach(role);
            this.dbContext.SaveChanges();

            return Task.FromResult<TRole>(null);
        }
    }
}
