using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace lifetoolsinc.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BusinessName { get; set; }
        public string AccountType { get; set; }
        public string Zipcode { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("FirstName", (this.FirstName == null ? "" : this.FirstName)));
            userIdentity.AddClaim(new Claim("LastName", (this.LastName == null ? "" : this.LastName)));
            userIdentity.AddClaim(new Claim("BusinessName", (this.BusinessName ==null?"": this.BusinessName)));
            userIdentity.AddClaim(new Claim("AccountType", (this.AccountType == null ? "" : this.AccountType)));
            userIdentity.AddClaim(new Claim("Zipcode", (this.Zipcode == null ? "" : this.Zipcode)));
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<lifetoolsinc.Models.Accounts> Accounts { get; set; }

        public System.Data.Entity.DbSet<lifetoolsinc.Models.Promoters> Promoters { get; set; }

        public System.Data.Entity.DbSet<lifetoolsinc.Models.MasterPrices> MasterPrices { get; set; }

        public System.Data.Entity.DbSet<lifetoolsinc.Models.Attributes> Attributes { get; set; }

        public System.Data.Entity.DbSet<lifetoolsinc.Models.AttPages> AttPages { get; set; }

        public System.Data.Entity.DbSet<lifetoolsinc.Models.Answers> Answers { get; set; }

        public System.Data.Entity.DbSet<lifetoolsinc.Models.CandidatesResults> CandidatesResults { get; set; }

        public System.Data.Entity.DbSet<lifetoolsinc.Models.Contents> Contents { get; set; }

        public System.Data.Entity.DbSet<lifetoolsinc.Models.AttClasses> AttClasses { get; set; }

        public System.Data.Entity.DbSet<lifetoolsinc.Models.Profiles> Profiles { get; set; }
    }
}