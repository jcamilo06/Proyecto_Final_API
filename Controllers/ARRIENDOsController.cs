using Proyecto_Final_API.Clases;
using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Proyecto_Final_API.Controllers
{
    [RoutePrefix("api/arriendos")]
    public class ARRIENDOsController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public IHttpActionResult ConsultarTodos()
        {
            var lista = new clsARRIENDO().ConsultarTodos();
            return Ok(lista);
        }

        [HttpGet]
        [Route("Consultar")]
        public IHttpActionResult Consultar(int id)
        {
            var arriendo = new clsARRIENDO().Consultar(id);
            if (arriendo == null)
                return NotFound();
            return Ok(arriendo);
        }

        [HttpPost]
        [Route("Insertar")]
        public IHttpActionResult Insertar([FromBody] ARRIENDO entidad)
        {
            var clase = new clsARRIENDO { entidad = entidad };
            var resultado = clase.Insertar();

            return resultado.StartsWith("Error") ? (IHttpActionResult)BadRequest(resultado) : Ok(resultado);
        }

        [HttpPut]
        [Route("Actualizar")]
        public IHttpActionResult Actualizar([FromBody] ARRIENDO entidad)
        {
            var clase = new clsARRIENDO { entidad = entidad };
            var resultado = clase.Actualizar();

            return resultado.StartsWith("Error") ? (IHttpActionResult)BadRequest(resultado) : Ok(resultado);
        }

        [HttpDelete]
        [Route("Eliminar")]
        public IHttpActionResult Eliminar([FromBody] ARRIENDO entidad)
        {
            var clase = new clsARRIENDO { entidad = entidad };
            var resultado = clase.Eliminar();

            return resultado.StartsWith("Error") ? (IHttpActionResult)BadRequest(resultado) : Ok(resultado);
        }
    }
}