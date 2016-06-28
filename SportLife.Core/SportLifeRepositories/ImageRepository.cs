using System.Data.Entity;
using SportLife.Core.Database;
using SportLife.Core.Generic;
using SportLife.Core.SportLifeRepositories.Interfaces;

namespace SportLife.Core.SportLifeRepositories {
    class ImageRepository : GenericRepository<Image>, IImageRepository {
        public ImageRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
