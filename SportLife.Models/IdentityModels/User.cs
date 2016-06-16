using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SportLife.Models.IdentityModels {
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim> {
        #region Properties

        public string UserFirstName { get; set; }

        public string UserSurname { get; set; }

        #endregion

        #region methods

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync ( MyUserManager userManager ) {
            var userIdentity = await userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        #endregion
    }
}
