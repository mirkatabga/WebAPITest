namespace WebApiTest.Database.Models
{
    using Identity;
    using System.Collections.Generic;

    public class Role : IdentityRole
    {
        private ICollection<User> users;

        public Role()
        {
            this.users = new HashSet<User>();
        }

        public override int Id { get; set; }

        public override string Name { get; set; }

        public int RoleType { get; set; }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }
    }
}
