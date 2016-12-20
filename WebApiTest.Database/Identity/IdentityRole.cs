namespace WebApiTest.Database.Identity
{
    using Microsoft.AspNet.Identity;

    public class IdentityRole : IRole<int>
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
    }
}