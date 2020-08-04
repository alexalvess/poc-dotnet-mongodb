using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Book
    {
        public Book(string name, int year) =>
            (Name, Year) = (name, year);

        [BsonElement("name")]
        public string Name { get; }

        [BsonElement("year")]
        public int Year { get; }
    }
}
