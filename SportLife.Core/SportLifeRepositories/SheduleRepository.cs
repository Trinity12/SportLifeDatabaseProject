using System.Data.Entity;
using SportLife.Core.Database;
using SportLife.Core.Generic;
using SportLife.Core.SportLifeRepositories.Interfaces;

namespace SportLife.Core.SportLifeRepositories {
    class SheduleRepository : GenericRepository<Shedule>, ISheduleRepository {
        public SheduleRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
