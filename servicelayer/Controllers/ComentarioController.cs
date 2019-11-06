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
        [HttpGet("BuscarComentariosDeUnComentario")]
        public IActionResult Get([FromQuery]String idComentario)
        {
            List<Comentario> comentarios = _comentarioService.GetComentario(idComentario);
            //List<Comentario> comentariosFinal = null;
            //foreach (Comentario com in comentarios)
            //{
            //    com.ComentarioPadre = null;
            //    comentariosFinal.Add(com);
            //}

            if (comentarios is null)
            {
                return Ok(new { result = false, message = "El usuario ingresado no tiene comentarios o no existe" });
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
                    return Ok(new { result = false, message = "El usuario no existe, verifique!!" });
                }
                
            }
            catch (Exception)
            {
                return Ok(new { result = false, message = "Los datos del Comentario no tienen el formato esperado." });
            }
        }

        [AllowAnonymous]
        [HttpPost("ComentarComentario")]
        public IActionResult Post([FromBody]SDTComentarComentario comentario)
        {
            try
            {
                var comentarioRetorno = _comentarioService.ComentarComentario(comentario.IdPadre, comentario.Comentario);

                if (comentarioRetorno != null)
                {
                    return Ok(new { result = false, message = comentarioRetorno });
                }
                else
                {
                    return Ok(new { result = true, message = "Comentario ingresado correctamente" });
                }

            }
            catch (Exception)
            {
                return Ok(new { result = false, message = "Los datos del Comentario no tienen el formato esperado." });
            }
        }
    }
}
