using Microsoft.AspNet.Identity.EntityFramework;

namespace SportLife.Models.IdentityModels {
    public class Role : IdentityRole<int, UserRole> {
        public Role () : base() {
        }

        public Role ( string name ) : this() {
            Name = name;
        }
    }
}
