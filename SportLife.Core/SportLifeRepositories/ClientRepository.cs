using System.Data.Entity;
using SportLife.Core.Database;
using SportLife.Core.Generic;
using SportLife.Core.SportLifeRepositories.Interfaces;

namespace SportLife.Core.SportLifeRepositories {
    public class ClientRepository : GenericRepository<Client>, IClientRepository {
        public ClientRepository ( DbContext dbContext ) : base(dbContext) {
        }
    }
}
