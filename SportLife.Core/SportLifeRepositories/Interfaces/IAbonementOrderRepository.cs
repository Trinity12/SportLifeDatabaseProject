using SportLife.Core.Database;
using SportLife.Core.Interfaces;

namespace SportLife.Core.SportLifeRepositories.Interfaces {
    public interface IAbonementOrderRepository : IRepository<AbonementOrder>
    {
        AbonementOrder GetAbonementBySport(SportKind sport);
        AbonementOrder GetAbonementBySportGroup(SportGroup sportGroup);
    }
}
