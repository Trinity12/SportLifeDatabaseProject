using System.Data.Entity;
using SportLife.Core.Database;
using SportLife.Core.Generic;
using SportLife.Core.SportLifeRepositories.Interfaces;

namespace SportLife.Core.SportLifeRepositories {
    class SportCategoryRepository : GenericRepository<SportCategory>, ISportCategoryRepository{
        public SportCategoryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
