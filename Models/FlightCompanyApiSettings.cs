using System;
namespace FlightCompanyApi.Models
{
	public class FlightCompanyApiSettings
	{
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string VolsCollectionName { get; set; } = null!;

        public string AvionsCollectionName { get; set; } = null!;

        public string ClientsCollectionName { get; set; } = null!;

        public string ReservationsCollectionName { get; set; } = null!;



    }
}

