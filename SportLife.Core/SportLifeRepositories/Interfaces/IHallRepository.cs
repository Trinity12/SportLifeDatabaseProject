using System.Collections;
using System.Collections.Generic;
using SportLife.Core.Database;
using SportLife.Core.Interfaces;

namespace SportLife.Core.SportLifeRepositories.Interfaces {
    public interface IHallRepository : IRepository<Hall> {
        IEnumerable<Hall> GetHallsByCity ( string city );
    }
}
