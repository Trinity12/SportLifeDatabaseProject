using System.Data.Entity;
using SportLife.Core.Database;
using SportLife.Core.Generic;
using SportLife.Core.SportLifeRepositories.Interfaces;

namespace SportLife.Core.SportLifeRepositories {
    public class UserRepository : GenericRepository<User>, IUserRepository {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
