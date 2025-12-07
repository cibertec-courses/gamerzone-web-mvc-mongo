
using gamerzone_web_mvc_mongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace gamerzone_web_mvc_mongo.Services
{
    public class VideojuegoService
    {
        private readonly IMongoCollection<Videojuego> _videojuegos;

        public VideojuegoService(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _videojuegos = database.GetCollection<Videojuego>(settings.Value.CollectionName);
        }

        public async Task<List<Videojuego>> GetAllAsync() => await _videojuegos.Find(_ => true).ToListAsync();

        public async Task<Videojuego?> GetByIdAsync(string id) => await _videojuegos.Find(v => v.Id == id).FirstOrDefaultAsync();


        public async Task CreateAsync(Videojuego videojuego) => await _videojuegos.InsertOneAsync(videojuego);

        public async Task UpdateAsync(string id, Videojuego videojuego) => await _videojuegos.ReplaceOneAsync(v => v.Id == id, videojuego);

        public async Task DeleteAsync(string id) => await _videojuegos.DeleteOneAsync(v => v.Id == id);

        public async Task<long> CountAsync() => await _videojuegos.CountDocumentsAsync(_ => true);

        public async Task<List<string>> GetGenerosAsync() =>
            await _videojuegos.Distinct<string>("genero", FilterDefinition<Videojuego>.Empty).ToListAsync();

        public async Task<List<string>> GetPaisesAsync() =>
            await _videojuegos.Distinct<string>("desarrollador.pais", FilterDefinition<Videojuego>.Empty).ToListAsync();

    }
}