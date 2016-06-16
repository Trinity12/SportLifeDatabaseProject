using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace SportLife.Models.IdentityModels {
    public class RoleManager : RoleManager<Role, int> {
        // PASS CUSTOM APPLICATION ROLE AND INT AS TYPE ARGUMENTS TO CONSTRUCTOR:
        public RoleManager ( IRoleStore<Role, int> roleStore )
            : base(roleStore) {

        }

        // PASS CUSTOM APPLICATION ROLE AS TYPE ARGUMENT:
        public static RoleManager Create (
            IdentityFactoryOptions<RoleManager> options, IOwinContext context ) {
            return new RoleManager(
                new RoleStore(context.Get<ApplicationDbContext>()));
        }


    }
}
