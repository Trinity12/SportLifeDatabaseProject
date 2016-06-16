using System;
using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace SportLife.Models.IdentityModels {
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext> {
        protected override void Seed ( ApplicationDbContext context ) {
            InitializeIdentityForEf(context);
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEf ( ApplicationDbContext db ) {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<MyUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<RoleManager>();
            const string name = "admin@example.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if ( role == null ) {
                role = new Role(roleName);
                var roleresult = roleManager.Create(role);
                if ( !roleresult.Succeeded ) throw new Exception();
            }

            var user = userManager.FindByName(name);
            if ( user == null ) {
                user = new User { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                if (!result.Succeeded) throw new Exception();
                result = userManager.SetLockoutEnabled(user.Id, false);
                if ( !result.Succeeded ) throw new Exception();
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if ( !rolesForUser.Contains(role.Name) ) {
                var result = userManager.AddToRole(user.Id, role.Name);
                if ( !result.Succeeded ) throw new Exception();
            }
        }
    }
}
