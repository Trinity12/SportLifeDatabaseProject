using System;
using System.Data.Entity;
using SportLife.Core.Database;
using SportLife.Core.Generic;
using SportLife.Core.SportLifeRepositories.Interfaces;
using SportLife.Models.LogicModel;

namespace SportLife.Core.SportLifeRepositories {
    public class ClientRepository : GenericRepository<Client>, IClientRepository {
        public override string TableName => "[dbo].[Client]";

        public ClientRepository ( DbContext dbContext ) : base(dbContext) {
        }

        public void Add ( int userId ) {
            Add(new Client() {
                ClientId = userId
            });
        }

        public void Add ( int userId, DateTime birthdate ) {
            Add(new Client() {
                ClientId = userId,
                BirthDate = birthdate
            });
        }
    }
}
