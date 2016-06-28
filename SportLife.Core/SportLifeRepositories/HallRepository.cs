using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SportLife.Core.Database;
using SportLife.Core.Generic;
using SportLife.Core.SportLifeRepositories.Interfaces;

namespace SportLife.Core.SportLifeRepositories {
    class HallRepository : GenericRepository<Hall>, IHallRepository {
        public HallRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Hall> GetHallsByCity(string city)
        {
            return GetAll().Where(hall => hall.Adress.AdressCity == city);
        }
    }
}
