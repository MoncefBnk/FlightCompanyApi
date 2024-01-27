using FlightCompanyApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FlightCompanyApi.Services
{
    public class VolsService
    {
        private readonly IMongoCollection<Vol> _volsCollection;

        public VolsService(IOptions<FlightCompanyApiSettings> flightCompanyApiSettings)
        {
            var mongoClient = new MongoClient(flightCompanyApiSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(flightCompanyApiSettings.Value.DatabaseName);
            _volsCollection = mongoDatabase.GetCollection<Vol>(flightCompanyApiSettings.Value.VolsCollectionName);
        }

        public async Task<List<Vol>> GetAsync() =>
            await _volsCollection.Find(_ => true).ToListAsync();

        public async Task<Vol?> GetAsync(string id) =>
            await _volsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Vol?> GetByVillesAsync(string villeDep, string villeArr) =>
        await _volsCollection.Find(x => x.VilleDep == villeDep && x.VilleArr == villeArr).FirstOrDefaultAsync();

        public async Task CreateAsync(Vol newVol) =>
            await _volsCollection.InsertOneAsync(newVol);

        public async Task UpdateAsync(string id, Vol updatedVol) =>
            await _volsCollection.ReplaceOneAsync(x => x.Id == id, updatedVol);

        public async Task RemoveAsync(string id) =>
            await _volsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
