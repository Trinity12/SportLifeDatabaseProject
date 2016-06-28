using System.Collections;
using System.Collections.Generic;
using SportLife.Core.Database;
using SportLife.Core.Interfaces;

namespace SportLife.Core.SportLifeRepositories.Interfaces {
    public interface ICoachRepository : IRepository<Coach> {
        IEnumerable<Coach> GetCoachesByOccupation ( SportKind sport );

        void Add(int userId);
    }
}
