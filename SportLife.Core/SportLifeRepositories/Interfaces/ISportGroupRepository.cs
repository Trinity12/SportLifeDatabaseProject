using System.Collections;
using System.Collections.Generic;
using SportLife.Core.Database;
using SportLife.Core.Interfaces;

namespace SportLife.Core.SportLifeRepositories.Interfaces {
    public interface ISportGroupRepository : IRepository<SportGroup> {
        IEnumerable<SportGroup> GetGroupsBySportCategory ( SportCategory sportCategory );
        IEnumerable<SportGroup> GetGroupsByCoach ( User coachUser );
    }
}
