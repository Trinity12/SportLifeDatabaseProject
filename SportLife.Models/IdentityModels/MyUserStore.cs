using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SportLife.Models.IdentityModels;

namespace SportLife.Models {
    public class MyUserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>, IUserStore<User, int> {
        public MyUserStore ( DbContext context ) : base(context) {
        }

        public MyUserStore ( ) : this(new ApplicationDbContext()) {
        }
    }
}
