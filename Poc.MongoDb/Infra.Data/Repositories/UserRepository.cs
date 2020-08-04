using Domain.Entities;
using Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IRepositoryUser
    {
        public UserRepository(
            IMongoClient mongoClient, 
            IClientSessionHandle clientSessionHandle) : base(mongoClient, clientSessionHandle, "user")
        {
        }

        public async Task<User> GetUserAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(f => f.Id, id);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync() =>
            await Collection.AsQueryable().ToListAsync();
    }
}
