using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StepByStep.Web.Entity;
using StepByStep.Web.Infra.Flow;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StepByStep.Web.Models
{

    public class ApplicationUserRole : IdentityUserRole<int> { }
    public class ApplicationRole : IdentityRole<int, ApplicationUserRole> { }
    public class ApplicationLogin : IdentityUserLogin<int> { }
    public class ApplicationUserClaim : IdentityUserClaim<int> { }
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, ApplicationLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public WorkFlow PassoFeito { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, int,
    ApplicationLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUserStore(StepByStepContext context)
            : base(context)
        {
        }
    }

    public class ApplicationRoleStore : RoleStore<ApplicationRole, int, ApplicationUserRole>
    {
        public ApplicationRoleStore(StepByStepContext context)
            : base(context)
        {
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}