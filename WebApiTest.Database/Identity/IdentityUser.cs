namespace WebApiTest.Database.Identity
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class IdentityUser : IUser<int>
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        /// <summary>
        ///     Email Address
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        ///     True if the email is confirmed, default is false
        /// </summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        ///     The salted/hashed form of the user password
        /// </summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        ///     A random value that should change whenever a users credentials have changed (password changed, login removed)
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        ///     DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        ///     Is lockout enabled for this user
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        ///     Used to record failures for the purposes of lockout
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync<TUser>(UserManager<TUser, int> userManager, string authenticationType)
            where TUser:IdentityUser
        {
            var userIdentity =
                await userManager.CreateIdentityAsync(this as TUser,
                    DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

}
