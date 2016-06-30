using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SportLife.Core.Database;
using SportLife.Core.Generic;
using SportLife.Core.SportLifeRepositories.Interfaces;

namespace SportLife.Core.SportLifeRepositories {
    class SportGroupRepository : GenericRepository<SportGroup>, ISportGroupRepository {
        public SportGroupRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<SportGroup> GetGroupsBySportCategory(SportCategory sportCategory)
        {
            return GetAll().Where(group => group.SportKind.SportCategory == sportCategory);
        }

        public IEnumerable<SportGroup> GetGroupsByCoach(Coach coachUser)
        {
            return GetAll().Where(group => group.Coach == coachUser);
        }
    }
}
