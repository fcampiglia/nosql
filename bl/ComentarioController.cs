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

        public List<Comentario> ComentariosUsuario(String email)
        {

            var usuario = _usuarios.Find<Usuario>(usu => usu.Email == email).FirstOrDefault();
            if (usuario is null)
            {
                return null;
            }
            else {
                List<Comentario> listaRetorno = _comentarios.Find<Comentario>(Comentario => Comentario.Usuario.Email == email).ToList();
                return listaRetorno;
            }

        }

        public object GetComentario(string id)
        {
            var comentario = _comentarios.Find<Comentario>(com => com.InternalId == id).FirstOrDefault();
            if (comentario is null)
            {
                return null;
            }
            else
            {
                return comentario;
            }

        }

        public Comentario ComentarComentario(String id,Comentario comentarioNuevo)
        {

            var comentarioPadre = _comentarios.Find<Comentario>(com => com.InternalId == id).FirstOrDefault();
            //Buscamos que el id exista
            if (comentarioPadre is null)
            {
                return null;
            }
            else { 
                var usuario = _usuarios.Find<Usuario>(usu => usu.Email == comentarioNuevo.Usuario.Email).FirstOrDefault();
                //Comprobamos el email
                if (usuario is null)
                {
                    return null;
                }
                else {
                    comentarioNuevo.Usuario = usuario;
                    //Guardamos el comentarios en la bd y luego se lo agregamos al padre (De esta manera si tendriamos Id)
                    //_comentarios.InsertOne(comentarioNuevo);

                    //Inicializamos la lista en caso de que sea null 
                    if (comentarioPadre.ListaComentarios.ToList() is null) {
                        comentarioPadre.ListaComentarios = new List<Comentario>();
                    }
                    
                    comentarioPadre.ListaComentarios.Add(comentarioNuevo);
                    var filter = Builders<Comentario>.Filter.Eq(c => c.InternalId, comentarioPadre.InternalId);
                    _comentarios.ReplaceOne(comentario => comentario.InternalId == comentarioPadre.InternalId, comentarioPadre );
                    return comentarioPadre;
                }
                
            }
            

        }


        public void Update(string id, Comentario comentarioIn) =>
           _comentarios.ReplaceOne(comentario => comentario.InternalId == id, comentarioIn);

           
        
    }
}
