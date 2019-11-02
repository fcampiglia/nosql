using System;
using System.Collections.Generic;
using System.Text;

namespace dal
{
    public class MyContextMongoDB
    {
              
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UsuarioCollectionName { get; set; }
        public string ComentarioCollectionName { get; set; }
    }
}
