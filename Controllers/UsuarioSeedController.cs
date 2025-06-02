using Inmobiliaria.Models;
using Proyecto_Final_API.Clases;
using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Proyecto_Final_API.Controllers
{
    [RoutePrefix("api/UsuarioSeed")]
    public class UsuarioSeedController : ApiController
    {
        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult RegistrarUsuario(Login login)
        {
            try
            {
                using (var db = new INMOBILIARIAEntities1())
                {
                    clsCypher cifrar = new clsCypher
                    {
                        Password = login.Clave
                    };
                    cifrar.CifrarClave();

                    var nuevo = new USUARIO
                    {
                        USERNAME = login.Usuario,
                        CLAVE = cifrar.PasswordCifrado,
                        SALT = cifrar.Salt,
                        ES_EMPLEADO = true, // cambia a false si es cliente
                        ID_EMPLEADO = 1,    // cambia según el ID real
                        ID_CLIENTE = null   // o pon ID_CLIENTE si es cliente
                    };

                    db.USUARIOs.Add(nuevo);
                    db.SaveChanges();

                    return Ok("Usuario registrado exitosamente");
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest("Error al registrar: " + ex.Message);
            }
        }
    }
}
