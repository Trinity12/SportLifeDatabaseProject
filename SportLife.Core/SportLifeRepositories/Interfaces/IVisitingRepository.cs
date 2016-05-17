using System.Collections.Generic;
using SportLife.Core.Database;
using SportLife.Core.Interfaces;

namespace SportLife.Core.SportLifeRepositories.Interfaces {
    public interface IVisitingRepository : IRepository<Visiting>
    {
        IEnumerable<Visiting> GetVisitingByUser(User user);
        IEnumerable<Visiting> GetVisitingByUserAbonement(User user, Abonement abonement);
    }
}
