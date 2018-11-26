using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CMSECMultiplObj.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int CountryID { get; set; }
        public int ProvinceID { get; set; }
        public int WardID { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ECMultipleConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<IdentityUserLogin>().Map(c =>
                {
                    c.ToTable("SYS_USERLOGIN");
                    c.Properties(p => new
                    {
                        p.UserId,
                        p.ProviderKey,
                        p.LoginProvider
                    });
                }).HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });

            modelBuilder.Entity<RoleModels>().Map(c =>
                {
                    c.ToTable("SYS_ROLES");
                    c.Property(p => p.Id).HasColumnName("RoleId");
                    c.Properties(p => new
                        {
                            p.Name,
                            p.Description
                        });
                }).HasKey(p => p.Id);

            modelBuilder.Entity<IdentityRole>().HasMany(c => c.Users).WithRequired().HasForeignKey(c => c.RoleId);
            modelBuilder.Entity<ApplicationUser>().Map(c =>
                {
                    c.ToTable("SYS_USERS");
                    c.Property(p => p.Id).HasColumnName("UserId");
                    c.Properties(p => new
                    {
                        p.UserName,
                        p.Email,
                        p.EmailConfirmed,
                        p.PasswordHash,
                        p.PhoneNumber,
                        p.PhoneNumberConfirmed,
                        p.TwoFactorEnabled,
                        p.SecurityStamp,
                        p.LockoutEnabled,
                        p.LockoutEndDateUtc,
                        p.Address,
                        p.CountryID,
                        p.ProvinceID,
                        p.WardID
                    });
                }).HasKey(c => c.Id);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Logins).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Claims).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Roles).WithOptional().HasForeignKey(c => c.UserId);

            modelBuilder.Entity<IdentityUserRole>().Map(c =>
                {
                    c.ToTable("SYS_USERROLES");
                    c.Properties(p => new
                    {
                        p.UserId,
                        p.RoleId
                    });
                }).HasKey(c => new { c.RoleId, c.UserId });

            modelBuilder.Entity<IdentityUserClaim>().Map(c =>
                {
                    c.ToTable("SYS_USERCLAIMS");
                    c.Property(p => p.Id).HasColumnName("UserClaimId");
                    c.Properties(p => new
                        {
                            p.UserId,
                            p.ClaimType,
                            p.ClaimValue
                        });
                }).HasKey(c => c.Id);
        }
    }
}