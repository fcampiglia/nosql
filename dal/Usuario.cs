using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace dal
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalId { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }       
        //public List<Comentario> ComentariosUsuario { get; set; }
    }
}

