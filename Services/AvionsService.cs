using FlightCompanyApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FlightCompanyApi.Services
{
    public class AvionsService
    {
        private readonly IMongoCollection<Avion> _avionsCollection;

        public AvionsService(IOptions<FlightCompanyApiSettings> flightCompanyApiSettings)
        {
            var mongoClient = new MongoClient(flightCompanyApiSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(flightCompanyApiSettings.Value.DatabaseName);
            _avionsCollection = mongoDatabase.GetCollection<Avion>(flightCompanyApiSettings.Value.AvionsCollectionName);
        }

        public async Task<List<Avion>> GetAsync() =>
            await _avionsCollection.Find(_ => true).ToListAsync();

        public async Task<Avion?> GetAsync(string id) =>
            await _avionsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Avion newAvion) =>
            await _avionsCollection.InsertOneAsync(newAvion);

        public async Task UpdateAsync(string id, Avion updatedAvion) =>
            await _avionsCollection.ReplaceOneAsync(x => x.Id == id, updatedAvion);

        public async Task RemoveAsync(string id) =>
            await _avionsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
