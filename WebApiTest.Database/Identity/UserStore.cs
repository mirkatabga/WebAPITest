namespace WebApiTest.Database.Identity
{
    using Contracts;
    using Microsoft.AspNet.Identity;
    using Models;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserStore<TUser> : IUserStore<TUser, int>,
        IUserRoleStore<TUser, int>,
        IUserPasswordStore<TUser, int>,
        IUserEmailStore<TUser, int>
        where TUser : User
    {
        private IWebApiTestDbContext dbContext;

        public UserStore(IWebApiTestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task AddToRoleAsync(TUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Null or empty argument: roleName", "roleName");
            }

            Role role = this.dbContext.Roles
                .FirstOrDefault(r => r.Name == roleName);

            if (role == null)
            {
                throw new ArgumentException("Role with the provided name was not found", "roleName");
            }

            this.dbContext.Users.Attach(user);
            user.Roles.Add(role);
            this.dbContext.SaveChanges();

            return Task.FromResult<TUser>(null);
        }

        public Task CreateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            this.dbContext.Users.Add(user);
            this.dbContext.SaveChanges();

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            this.dbContext.Users.Attach(user);
            this.dbContext.Users.Remove(user);
            this.dbContext.SaveChanges();

            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            if (this.dbContext != null)
            {
                this.dbContext.Dispose();
            }
        }

        public Task<TUser> FindByIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Argument should be greater then zero: userId");
            }

            TUser result = this.dbContext.Users.Find(userId) as TUser;
            if (result != null)
            {
                return Task.FromResult<TUser>(result);
            }

            return Task.FromResult<TUser>(null);
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Null or empty argument", "userName");
            }

            IList<User> users = this.dbContext.Users
                .Where(u => u.UserName == userName)
                .ToList();


            if (users.Count > 1)
            {
                throw new ArgumentOutOfRangeException("userName",
                    "More then 1 users were found with that username");
            }

            TUser result = users.FirstOrDefault() as TUser;

            if (result != null)
            {
                return Task.FromResult<TUser>(result);
            }

            return Task.FromResult<TUser>(null);
        }

        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            this.dbContext.Users.Attach(user);

            IList<string> rolesNames = user.Roles
                .Select(r => r.Name)
                .ToList();

            return Task.FromResult<IList<string>>(rolesNames);
        }

        public Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Null or empty argument: roleName", "roleName");
            }

            this.dbContext.Users.Attach(user);
            bool isInRole = user.Roles.Any(r => r.Name == roleName);

            return Task.FromResult<bool>(isInRole);
        }

        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Null or empty argument: roleName", "roleName");
            }

            Role role = this.dbContext.Roles
                .FirstOrDefault(r => r.Name == roleName);

            if (role != null)
            {
                user.Roles.Remove(role);
                this.dbContext.SaveChanges();
            }

            return Task.FromResult<TUser>(null);
        }

        public Task UpdateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            this.dbContext.Users.Attach(user);
            this.dbContext.SaveChanges();

            return Task.FromResult<object>(null);
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(passwordHash))
            {
                throw new ArgumentException("Null or empty argument: passwordHash");
            }

            user.PasswordHash = passwordHash;

            return Task.FromResult<TUser>(null);
        }

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult<string>(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult<bool>(string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetEmailAsync(TUser user, string email)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Null or empty argument: email", "email");
            }

            user.Email = email;

            return Task.FromResult<TUser>(user);
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult<string>(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult<bool>(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.EmailConfirmed = confirmed;

            return Task.FromResult<TUser>(null);
        }

        public Task<TUser> FindByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Null or empty argument: email", "email");
            }

           TUser user = this.dbContext.Users.FirstOrDefault(u => u.Email == email) as TUser;

            return Task.FromResult<TUser>(user);
        }
    }
}

