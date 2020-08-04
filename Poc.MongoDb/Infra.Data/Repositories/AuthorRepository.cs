using Domain.Entities;
using Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IRepositoryAuthor
    {
        public AuthorRepository(
            IMongoClient mongoClient, 
            IClientSessionHandle clientSessionHandle) : base(mongoClient, clientSessionHandle, "author")
        {
        }

        public async Task<Author> GetAuthorByIdAsync(string id)
        {
            var filter = Builders<Author>.Filter.Eq(s => s.Id, id);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync() =>
            await Collection.AsQueryable().ToListAsync();

        public async Task<IEnumerable<Book>> GetBooksAsync(string id)
        {
            var filter = Builders<Author>.Filter.Eq(s => s.Id, id);
            return await Collection.Find(filter).Project(p => p.Books).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Author>> GetByAuthorsNameAsync(string name)
        {
            var filter = Builders<Author>.Filter.Eq(s => s.Name, name);
            return await Collection.Find(filter).ToListAsync();
        }
    }
}
