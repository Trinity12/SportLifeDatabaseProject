using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SportLife.Core.Database;
using SportLife.Core.Generic;
using SportLife.Core.SportLifeRepositories.Interfaces;

namespace SportLife.Core.SportLifeRepositories {
    class SportRepository : GenericRepository<SportKind>, ISportRepository {
        public SportRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<SportKind> GetSportByCategory(SportCategory category)
        {
            return GetAll().Where(sport => sport.SportCategory == category);
        }

        public IEnumerable<IGrouping<SportCategory, SportKind>> GroupSportKinds()
        {
            return GetAll().GroupBy( sport => sport.SportCategory );
        }
    }
}
