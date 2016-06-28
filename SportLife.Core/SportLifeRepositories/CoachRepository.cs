using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using SportLife.Core.Database;
using SportLife.Core.Generic;
using SportLife.Core.Interfaces;
using SportLife.Core.SportLifeRepositories.Interfaces;

namespace SportLife.Core.SportLifeRepositories {
    class CoachRepository : GenericRepository<Coach>, ICoachRepository{
        public CoachRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Coach> GetCoachesByOccupation(SportKind sport)
        {
            return GetAll()
                .Where(coach => coach.SportGroup.Any(group => group.SportKind == sport));
        }
    }
}
