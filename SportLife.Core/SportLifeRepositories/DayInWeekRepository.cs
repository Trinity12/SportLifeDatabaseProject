using System.Data.Entity;
using SportLife.Core.Database;
using SportLife.Core.Generic;
using SportLife.Core.SportLifeRepositories.Interfaces;

namespace SportLife.Core.SportLifeRepositories {
    class DayInWeekRepository : GenericRepository<DaysInWeek>, IDaysInWeekRepository {
        public DayInWeekRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
