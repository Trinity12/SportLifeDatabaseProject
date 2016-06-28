using System.Data.Entity;
using System.Linq;
using SportLife.Core.Database;
using SportLife.Core.Generic;
using SportLife.Core.SportLifeRepositories.Interfaces;

namespace SportLife.Core.SportLifeRepositories {
    public class FileTypeRepository : GenericRepository<FileType>, IFileTypeRepository {
        public FileTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public FileType GetByName(string name)
        {
            return GetAll().First(x => x.TypeName == name);
        }
    }
}
