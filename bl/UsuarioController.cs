using dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace bl
{
    public class UsuarioController
    {
        private readonly IMongoCollection<Usuario> _usuarios;

        public UsuarioController(MyContextMongoDB settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _usuarios = database.GetCollection<Usuario>(settings.UsuarioCollectionName);
        }
        
    
        public Usuario AgregarUsuario(Usuario usuario)
        {
            if (this.ExisteUsuario(usuario.Email))
            {
                return null;
            }
            else
            {                
                _usuarios.InsertOne(usuario);
                return usuario;
            }
            
        }
                                    
        public List<Usuario> Get() =>
           _usuarios.Find(Usuario => true).ToList();

        private bool ExisteUsuario(string email)
        {
            Usuario _usuario = _usuarios.Find<Usuario>(usu => usu.Email == email).FirstOrDefault();
            
            if (_usuario is null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }



    }
}
