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
        [HttpPost("CrearUsuario")]
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
                    return Ok(new { result = false, message = "Algo salio mal" });
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
 
        // GET api/user
        /*[AllowAnonymous]
        [HttpGet]
        public IActionResult List()
        {
            bl.UsuarioController userLogic = new bl.UsuarioController();
            var users = userLogic.ListarUsuarios();

            return Ok(users);
        }

        // POST api/user
        //[Authorize(Roles = "SuperAdmin")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]Usuario usuario)
        {
            try
            {
                bl.UsuarioController userLogic = new bl.UsuarioController();
                userLogic.AgregarUsuario(usuario);
                return Ok(new { result = true, message = "Usuario creado correctamente" });
            }
            catch (Exception ex)
            {
                return Ok(new { result = false, message = ex.Message });
            }
        }

        // POST api/user/login
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]Usuario usuario)
        {
            bl.UsuarioController userLogic = new bl.UsuarioController();
            var buscarUsuario = userLogic.Login(usuario.Email, usuario.Password);

            if (buscarUsuario == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, Enum.GetName(typeof(RolUsuario), buscarUsuario.Rol))
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var returnToken = tokenHandler.WriteToken(token);

            return Ok(new { buscarUsuario.Email, token = returnToken });
        }

        // POST api/user/login
        [AllowAnonymous]
        [HttpPost("login/social")]
        public async Task<IActionResult> SocialLoginAsync([FromQuery]string _token, [FromQuery]bool _existe, [FromBody]Usuario _usuario)
        {
            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(_token);

            if (_existe)
            {
                bl.UsuarioController userLogic = new bl.UsuarioController();
                var buscarUsuario = userLogic.BuscarUsuario(_usuario.Email);

                if (buscarUsuario == null)
                    return Unauthorized();

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Role, Enum.GetName(typeof(RolUsuario), buscarUsuario.Rol))
            }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var returnToken = tokenHandler.WriteToken(token);

                return Ok(new { buscarUsuario.Email, token = returnToken });
            } else
            {
                bl.UsuarioController userLogic = new bl.UsuarioController();
                userLogic.AgregarUsuario(_usuario);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Role, Enum.GetName(typeof(RolUsuario), RolUsuario.UsuarioFinal))
            }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var returnToken = tokenHandler.WriteToken(token);

                return Ok(new { _usuario.Email, token = returnToken });
            }


        }

        // GET api/user/5
        //[Authorize(Roles = "SuperAdmin")]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            bl.UsuarioController userLogic = new bl.UsuarioController();
            var usuario = userLogic.ObtenerUsuario(id);

            if (usuario == null)
            {
                return NotFound("No existe el usuerio.");
            } else
            {
                return Ok(new { usuario.ID, usuario.Nombre, usuario.Apellido, usuario.Email });
            }
        }

        // DELETE api/user/5
        //[Authorize(Roles = "SuperAdmin")]
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bl.UsuarioController userLogic = new bl.UsuarioController();
            try
            {
                userLogic.EliminarUsuario(id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("No existe el usuerio.");
            }
        }

        // PATCH api/user/5
        //[Authorize(Roles = "SuperAdmin")]
        [AllowAnonymous]
        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody]Usuario usuario)
        {
            bl.UsuarioController userLogic = new bl.UsuarioController();
            try
            {
                userLogic.UpdateUser(id, usuario.Nombre, usuario.Apellido, usuario.Email);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }*/
    }
}
