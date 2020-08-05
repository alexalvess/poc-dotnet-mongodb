using Application.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class Author : BaseEntity
    {
        public Author(string name, IEnumerable<Book> books) =>
            (Name, Books) = (name, books);

        [BsonElement("name")]
        public string Name { get; }

        [BsonElement("books")]
        public IEnumerable<Book> Books { get; }
    }
}
