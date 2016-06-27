using System;
using SportLife.Core.Database;
using SportLife.Core.Interfaces;
using SportLife.Core.SportLifeRepositories;
using SportLife.Core.SportLifeRepositories.Interfaces;

namespace SportLife.Core.Generic {
    public class UnitOfWork : IUnitOfWork {

        private readonly SportLifeEntities _dbEntities = new SportLifeEntities();
        private bool _disposed;

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

        public IClientRepository ClientRepository
            => _clientRepository ?? (_clientRepository = new ClientRepository(_dbEntities));

        public ICoachRepository CoachRepository { get; }
        public IHallRepository HallRepository { get; }
        public IPriceRepository PriceRepository { get; }
        public ISheduleRepository SheduleRepository { get; }
        public ISportCategoryRepository SportCategoryRepository { get; }
        public ISportGroupRepository SportGroupRepository { get; }
        public ISportRepository SportRepository { get; }
        public IUserRepository UserRepository { get; }
        public IVisitingRepository VisitingRepository { get; }

        public void Dispose () {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges () {
            _dbEntities.SaveChanges();
        }

        protected virtual void Dispose ( bool disposing ) {
            if ( !_disposed ) {
                if ( disposing ) {
                    _dbEntities.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
