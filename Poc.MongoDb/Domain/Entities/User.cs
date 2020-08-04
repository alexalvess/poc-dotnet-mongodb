using Domain.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public User(string name, string nin) =>
            (Name, Nin) = (name, nin);

        [BsonElement("name")]
        public string Name { get; }

        [BsonElement("national_insurance_number")]
        public string Nin { get; }
    }
}
