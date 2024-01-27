using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlightCompanyApi.Models
{
	public class Avion
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("idAvion")]
        public string IdAvion { get; set; } = null!;

        [BsonElement("model")]
        public string Model { get; set; } = null!;

        [BsonElement("nbPlace")]
        public int NombrePlaces { get; set; }
        
	}
}

