using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SportLife.Models.IdentityModels {
    public class RoleStore : RoleStore<Role, int, UserRole> 
        {
        public RoleStore ()
            : base(new IdentityDbContext()) {
            base.DisposeContext = true;
        }

        public RoleStore ( DbContext context )
            : base(context) {
        }
    }
}
