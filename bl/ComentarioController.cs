using dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace bl
{
    public class ComentarioController
    {
        private readonly IMongoCollection<Comentario> _comentarios;
        private readonly IMongoCollection<Usuario> _usuarios;

        public ComentarioController(MyContextMongoDB settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _comentarios = database.GetCollection<Comentario>(settings.ComentarioCollectionName);
            _usuarios = database.GetCollection<Usuario>(settings.UsuarioCollectionName);
        }

        public List<Comentario> Get() =>
          _comentarios.Find(comentario => true).ToList();

        public Comentario Create(Comentario comentario)
        {

            var usuario = _usuarios.Find<Usuario>(usu => usu.Email == comentario.Usuario.Email).FirstOrDefault();


            comentario.Usuario = usuario;                      

            _comentarios.InsertOne(comentario);

            return comentario;          
            
        }        
       

        public void Update(string id, Comentario comentarioIn) =>
           _comentarios.ReplaceOne(comentario => comentario.InternalId == id, comentarioIn);

           
        
    }
}
