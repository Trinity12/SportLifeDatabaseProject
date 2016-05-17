using SportLife.Core.Database;
using SportLife.Core.Interfaces;

namespace SportLife.Core.SportLifeRepositories.Interfaces {
    public interface ISportRepository : IRepository<SportKind> {
        SportKind GetSportByCategory ( SportCategory category );
    }

    public interface ISportCategoryRepository : IRepository<SportCategory> {
    }
}
