using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SportLife.Core.Database;
using SportLife.Core.Interfaces;

namespace SportLife.Core.SportLifeRepositories.Interfaces {
    public interface ISportRepository : IRepository<SportKind> {
        IEnumerable<SportKind> GetSportByCategory ( SportCategory category );
        IEnumerable<IGrouping<SportCategory, SportKind>> GroupSportKinds ();
    }

    public interface ISportCategoryRepository : IRepository<SportCategory> {
    }
}
