using SportLife.Core.Database;
using SportLife.Core.Interfaces;

namespace SportLife.Core.SportLifeRepositories.Interfaces {
    public interface IUserRepository : IRepository<User> {
    }

    public interface IRoleRepository : IRepository<Role> {
    }

    public interface IUserClameRepository : IRepository<UserClaim> {
    }

    public interface IUserLoginRepository : IRepository<UserLogin> {
    }

}
