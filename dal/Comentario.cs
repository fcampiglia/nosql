using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace dal
{
    public class Comentario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalId { get; set; }

        public Usuario Usuario { get; set; }
        
        public string Texto { get; set; }

        public List<Comentario> ListaComentarios { get; set; }
        
    }
}
