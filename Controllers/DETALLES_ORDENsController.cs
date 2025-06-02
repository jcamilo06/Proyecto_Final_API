using Proyecto_Final_API.Clases;
using Proyecto_Final_API.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Proyecto_Final_API.Controllers
{
    [RoutePrefix("api/detalles_orden")]
    public class DETALLES_ORDENsController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public IHttpActionResult ConsultarTodos()
        {
            var lista = new clsDETALLES_ORDEN().ConsultarTodos();
            return Ok(lista);
        }

        [HttpGet]
        [Route("Consultar")]
        public IHttpActionResult Consultar(int id)
        {
            var detalle = new clsDETALLES_ORDEN().Consultar(id);
            return detalle == null ? (IHttpActionResult)NotFound() : Ok(detalle);
        }

        [HttpPost]
        [Route("Insertar")]
        public IHttpActionResult Insertar([FromBody] DETALLES_ORDEN entidad)
        {
            if (entidad == null)
                return BadRequest("Datos de entrada inválidos.");

            var clase = new clsDETALLES_ORDEN { entidad = entidad };
            var resultado = clase.Insertar();

            return resultado.StartsWith("Error") ? (IHttpActionResult)BadRequest(resultado) : Ok(resultado);
        }

        [HttpPut]
        [Route("Actualizar")]
        public IHttpActionResult Actualizar([FromBody] DETALLES_ORDEN entidad)
        {
            if (entidad == null)
                return BadRequest("Datos de entrada inválidos.");

            var clase = new clsDETALLES_ORDEN { entidad = entidad };
            var resultado = clase.Actualizar();

            return resultado.StartsWith("Error") || resultado.Contains("no se encontró")
                ? (IHttpActionResult)BadRequest(resultado)
                : Ok(resultado);
        }

        [HttpDelete]
        [Route("Eliminar")]
        public IHttpActionResult Eliminar([FromBody] DETALLES_ORDEN entidad)
        {
            if (entidad == null || entidad.ID_DETALLE <= 0)
                return BadRequest("ID inválido para eliminación.");

            var clase = new clsDETALLES_ORDEN { entidad = entidad };
            var resultado = clase.Eliminar();

            return resultado.StartsWith("Error") || resultado.Contains("no se encontró")
                ? (IHttpActionResult)BadRequest(resultado)
                : Ok(resultado);
        }
    }
}
