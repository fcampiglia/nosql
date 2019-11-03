using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dal;
using Microsoft.AspNetCore.Authorization;



namespace servicelayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComentarioController : ControllerBase
    {
        private readonly bl.ComentarioController _comentarioService;
        

        public ComentarioController(bl.ComentarioController comentarioService)
        {
            _comentarioService = comentarioService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Comentario>> Get() =>
        _comentarioService.Get();


        [AllowAnonymous]
        [HttpGet("ComentariosDeUsuario")]
        public IActionResult List([FromQuery]String email)
        {
           var comentarios= _comentarioService.ComentariosUsuario(email);
            if (comentarios is null)
            {
                return Ok(new { result = false, message = "El usuario ingresado no tiene comentarios o no existe" });
            }
            else {
                return Ok(comentarios);
            }
            
        }

        [AllowAnonymous]
        [HttpGet("Comentario")]
        public IActionResult Get([FromQuery]String id)
        {
            var comentarios = _comentarioService.GetComentario(id);
            if (comentarios is null)
            {
                return Ok(new { result = false, message = "No existe el comentario" });
            }
            else
            {
                return Ok(comentarios);
            }

        }


        [AllowAnonymous]
        [HttpPost("Comentar")]
        public IActionResult Post([FromBody]Comentario comentario)
        {
            try
            {               
                var comentarioRetorno = _comentarioService.Create(comentario);
                
                if (comentarioRetorno != null)
                {                   
                    return Ok(new { result = true, message = "Comentario ingresado correctamente" });
                }
                else
                {
                    return Ok(new { result = false, message = "Algo salio mal" });
                }
                
            }
            catch (Exception)
            {
                return Ok(new { result = false, message = "Los datos del Comentario no tienen el formato esperado." });
            }
        }

        [AllowAnonymous]
        [HttpPost("ComentarComentario")]
        public IActionResult Post(String id,[FromBody]Comentario comentario)
        {
            try
            {
                var comentarioRetorno = _comentarioService.ComentarComentario(id,comentario);

                if (comentarioRetorno != null)
                {
                    return Ok(new { result = true, message = "Comentario ingresado correctamente" });
                }
                else
                {
                    return Ok(new { result = false, message = "Algo salio mal" });
                }

            }
            catch (Exception)
            {
                return Ok(new { result = false, message = "Los datos del Comentario no tienen el formato esperado." });
            }
        }

        /* [AllowAnonymous]
         [HttpPost("IniciarComentario")]    
         public IActionResult Post([FromQuery]int idEmpresa, int idVehiculo, string codigo)
         {
             try
             {
                 var mensaje = _comentarioService.IniciarComentario(idEmpresa, idVehiculo, codigo);

                 if (mensaje == "OK") //Los datos son correctos: se da por iniciado el comentario
                 {  
                     return Ok(new { result = true, message = "Comentario iniciado con exito" });
                 }
                 else
                 {                    
                     return Ok(new { result = false, message = mensaje });
                 }
             }
             catch (Exception)
             {
                 return Ok(new { result = false, message = "Algo salio mal, no se puede iniciar el Comentario." });
             }            

         }

         [AllowAnonymous]
         [HttpPost("FinalizarComentario")]        
         public IActionResult FinalizarComentario([FromQuery]int idEmpresa, int idVehiculo, string codigo)
         {
             try
             {
                 var mensaje = _comentarioService.FinalizarComentario(idEmpresa, idVehiculo, codigo);

                 if (mensaje == "OK") 
                 {
                     //Ver si esto del vehiculo va en la logica
                     bl.VehiculoController controladorVehiculo = new bl.VehiculoController();                    
                     var vehiculo = controladorVehiculo.ObtenerVehiculo(idVehiculo, idEmpresa);
                     vehiculo.Estado = "Disponible";
                     controladorVehiculo.ModificarVehiculo(idEmpresa, vehiculo);

                     return Ok(new { result = true, message = "Comentario finalizado con éxito ", precio = vehiculo.TarifaFija.ToString()});
                 }
                 else
                 {
                     return Ok(new { result = false, message = mensaje });
                 }
             }
             catch (Exception)
             {
                 return Ok(new { result = false, message = "Error, verificar datos." });
             }

         }*/



    }
}
