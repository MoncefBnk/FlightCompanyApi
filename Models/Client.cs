using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlightCompanyApi.Models
{
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("idClient")]
        public string IdClient { get; set; } = null!;

        [BsonElement("firstName")]
        public string FirstName { get; set; } = null!;

        [BsonElement("lastName")]
        public string LastName { get; set; } = null!;

        [BsonElement("address")]
        public string Address { get; set; } = null!;

        [BsonElement("email")]
        public string Email { get; set; } = null!;

        [BsonElement("tel")]
        public string Tel { get; set; } = null!;

        [BsonElement("birthday")]
        public string Birthday { get; set; } = null!;

        [BsonElement("passportNumber")]
        public string PassportNumber { get; set; } = null!;
    }
}
