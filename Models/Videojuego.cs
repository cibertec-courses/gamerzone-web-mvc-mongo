
using MongoDB.Bson.Serialization.Attributes;

namespace gamerzone_web_mvc_mongo.Models
{
   public class Videojuego
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("titulo")]
        public string Titulo { get; set; } = string.Empty;

        [BsonElement("genero")]
        public string Genero { get; set; } = string.Empty;
        [BsonElement("plataformas")]
        public List<string> Plataformas { get; set; } = new List<string>();
        [BsonElement("precio")]
        public decimal Precio { get; set; }
        [BsonElement("stock")]
        public int Stock { get; set; }

        [BsonElement("lanzamiento")]
        public int Lanzamiento { get; set; }
        [BsonElement("multijugador")]
        public bool Multijugador { get; set; }

        [BsonElement("desarrollador")]
        public Desarrollador Desarrollador { get; set; } = new Desarrollador();

    }
    public class Desarrollador
    {
    
        [BsonElement("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [BsonElement("pais")]
        public string Pais { get; set; } = string.Empty;

    }
}