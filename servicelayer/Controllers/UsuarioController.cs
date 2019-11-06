using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dal;
using Microsoft.AspNetCore.Authorization;



namespace servicelayer.Controllers
{
  [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly bl.UsuarioController _usuarioService;


        public UsuarioController(bl.UsuarioController usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost("Crear")]
        public IActionResult Post([FromBody]Usuario usuario)
        {
            try
            {
                var usuarioRetorno = _usuarioService.AgregarUsuario(usuario);

                if (usuarioRetorno != null)
                {
                    return Ok(new { result = true, message = "Usuario ingresado correctamente" });
                }
                else
                {
                    return Ok(new { result = false, message = "El Usuario ya existe" });
                }

            }
            catch (Exception)
            {
                return Ok(new { result = false, message = "Los datos del Usuario no tienen el formato esperado." });
            }
        }
        [AllowAnonymous]
        [HttpGet]
         public ActionResult<List<Usuario>> Get() =>
         _usuarioService.Get(); 

    }
}
