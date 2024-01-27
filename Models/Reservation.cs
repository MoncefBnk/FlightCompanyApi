using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlightCompanyApi.Models
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("idReservation")]
        public string IdReservation { get; set; } = null!;

        [BsonElement("numVol")]
        public string NumVol { get; set; } = null!;

        [BsonElement("idClient")]
        public string IdClient { get; set; } = null!;
    }
}
