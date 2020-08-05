using Application.Entities;
using Application.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepositoryAuthor : IRepositoryBase<Author>
    {
        Task<IEnumerable<Book>> GetBooksAsync(string id);

        Task<IEnumerable<Author>> GetAuthorsAsync();

        Task<Author> GetAuthorByIdAsync(string id);

        Task<IEnumerable<Author>> GetByAuthorsNameAsync(string name);
    }
}
