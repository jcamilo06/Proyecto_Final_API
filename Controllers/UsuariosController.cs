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
    [RoutePrefix("api/Usuarios")]
    public class UsuariosController : ApiController
    {
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] USUARIO usuario, int idPerfil)
        {
            clsUsuario Usuario = new clsUsuario();
            Usuario.entidad = usuario;
            return Usuario.Insertar(idPerfil);
        }
    }
}
