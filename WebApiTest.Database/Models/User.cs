namespace WebApiTest.Database.Models
{
    using Identity;
    using System.Collections.Generic;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;

    public class User : IdentityUser
    {
        private ICollection<Role> roles;

        public User()
        {
            this.roles = new HashSet<Role>();
        }

        public string IdToken { get; set; }

        public virtual ICollection<Role> Roles
        {
            get { return this.roles; }
            set { this.roles = value; }
        }
    }
}
