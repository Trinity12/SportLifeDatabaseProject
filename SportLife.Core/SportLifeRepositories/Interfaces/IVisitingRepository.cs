using System.Collections.Generic;
using SportLife.Core.Database;
using SportLife.Core.Interfaces;

namespace SportLife.Core.SportLifeRepositories.Interfaces {
    public interface IVisitingRepository : IRepository<Visiting>
    {
        IEnumerable<Visiting> GetVisitingByClient ( Client user );
        IEnumerable<Visiting> GetVisitingByClientAbonement ( Client user, Abonement abonement );
    }
}
