using SportLife.Core.Database;
using SportLife.Core.Interfaces;
using SportLife.Core.SportLifeRepositories.Interfaces;

namespace SportLife.Core.Generic {
    public class UnitOfWork : IUnitOfWork {

        private readonly SportLifeEntities _dbEntities;

        private IAbonementOrderRepository _abonementOrderRepository;
        private IAbonementRepository _abonementRepository;
        private IClientRepository _clientRepository;
        private ICoachRepository _coachRepository;
        private IHallRepository _hallRepository;
        private IPriceRepository _priceRepository;
        private ISheduleRepository _sheduleRepository;
        private ISportGroupRepository _sportGroupRepository;
        private ISportCategoryRepository _categoryRepository;
        private ISportRepository _sportRepository;
        private IUserRepository _userRepository;
        private IVisitingRepository _visitingRepository;

        public IAbonementOrderRepository AbonementOrderRepository { get; }
        public IAbonementRepository AbonementRepository { get; }
        public IClientRepository ClientRepository { get; }
        public ICoachRepository CoachRepository { get; }
        public IHallRepository HallRepository { get; }
        public IPriceRepository PriceRepository { get; }
        public ISheduleRepository SheduleRepository { get; }
        public ISportCategoryRepository SportCategoryRepository { get; }
        public ISportRepository SportRepository { get; }
        public IUserRepository UserRepository { get; }
        public IVisitingRepository VisitingRepository { get; }

        public UnitOfWork ( SportLifeEntities dbEntities ) {
            _dbEntities = dbEntities;

        }

        public void SaveChanges () {
            _dbEntities.SaveChanges();
        }

        public void Dispose () {
            _dbEntities.SaveChanges();
        }
    }
}
