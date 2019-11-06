using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace dal
{
    public class SDTComentarComentario
    {
        public string IdPadre { get; set; }

        public Comentario Comentario { get; set; }
        
    }
}
