using FlightCompanyApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FlightCompanyApi.Services
{
    public class ClientsService
    {
        private readonly IMongoCollection<Client> _clientsCollection;

        public ClientsService(IOptions<FlightCompanyApiSettings> flightCompanyApiSettings)
        {
            var mongoClient = new MongoClient(flightCompanyApiSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(flightCompanyApiSettings.Value.DatabaseName);
            _clientsCollection = mongoDatabase.GetCollection<Client>(flightCompanyApiSettings.Value.ClientsCollectionName);
        }

        public async Task<List<Client>> GetAsync() =>
            await _clientsCollection.Find(_ => true).ToListAsync();

        public async Task<Client?> GetAsync(string id) =>
            await _clientsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Client newClient) =>
            await _clientsCollection.InsertOneAsync(newClient);

        public async Task UpdateAsync(string id, Client updatedClient) =>
            await _clientsCollection.ReplaceOneAsync(x => x.Id == id, updatedClient);

        public async Task RemoveAsync(string id) =>
            await _clientsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
