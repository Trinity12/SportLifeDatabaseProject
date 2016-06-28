using SportLife.Core.Database;
using SportLife.Core.Interfaces;

namespace SportLife.Core.SportLifeRepositories.Interfaces {
    public interface IFileTypeRepository : IRepository<FileType>
    {
        FileType GetByName(string name);
    }
}
