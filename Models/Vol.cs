using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlightCompanyApi.Models
{
	public class Vol
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("numVol")]
        public string NumVol { get; set; } = null!;

        [BsonElement("villeDep")]
        public string VilleDep { get; set; } = null!;

        [BsonElement("villeArr")]
        public string VilleArr { get; set; } = null!;

        [BsonElement("heureDep")]
        public string HeureDep { get; set; } = null!;

        [BsonElement("heureArr")]
        public string HeureArr { get; set; } = null!;

        [BsonElement("aeroportDep")]
        public string AeroportDep { get; set; } = null!;

        [BsonElement("aeroportArr")]
        public string AeroportArr { get; set; } = null!;


        public int PrixBillet { get; set; }

        [BsonElement("idAvion")]
        public string IdAvion { get; set; } = null!;
    }
}


