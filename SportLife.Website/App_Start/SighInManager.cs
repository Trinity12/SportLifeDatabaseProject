using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SportLife.Models.IdentityModels;
using SportLife.Website.Models;

namespace SportLife.Website {
    // Configure the application sign-in manager which is used in this application.
    public class SignInManager : SignInManager<User, int> {
        public SignInManager ( MyUserManager userManager, IAuthenticationManager authenticationManager )
            : base(userManager, authenticationManager) {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync ( User user ) {
            return user.GenerateUserIdentityAsync((MyUserManager)
                UserManager);
        }

        public static SignInManager Create ( IdentityFactoryOptions<SignInManager> options, IOwinContext context ) {
            return new SignInManager(context.GetUserManager<MyUserManager>(), context.Authentication);
        }
    }
}
