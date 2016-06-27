using System;
using SportLife.Core.Database;
using SportLife.Core.Interfaces;

namespace SportLife.Core.SportLifeRepositories.Interfaces {
    public interface IClientRepository : IRepository<Client> {
        void Add ( int userId );
        void Add ( int userId, DateTime birthdate );
    }
}
