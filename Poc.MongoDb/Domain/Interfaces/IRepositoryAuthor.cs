﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryAuthor : IRepositoryBase<Author>
    {
        Task<IEnumerable<Book>> GetBooksAsync(string id);

        Task<IEnumerable<Author>> GetAuthorsAsync();

        Task<Author> GetAuthorByIdAsync(string id);

        Task<IEnumerable<Author>> GetByAuthorsNameAsync(string name);
    }
}