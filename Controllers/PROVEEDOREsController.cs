using Proyecto_Final_API.Clases;
using Proyecto_Final_API.Models;
using System.Web.Http;

namespace Proyecto_Final_API.Controllers
{
    [RoutePrefix("api/proveedores")]
    public class PROVEEDOREsController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public IHttpActionResult ConsultarTodos()
        {
            var lista = new clsPROVEEDORE().ConsultarTodos();
            return Ok(lista);
        }

        [HttpGet]
        [Route("Consultar")]
        public IHttpActionResult Consultar(int id)
        {
            var entidad = new clsPROVEEDORE().Consultar(id);
            return entidad == null ? (IHttpActionResult)NotFound() : Ok(entidad);
        }

        [HttpPost]
        [Route("Insertar")]
        public IHttpActionResult Insertar([FromBody] PROVEEDORE entidad)
        {
            if (entidad == null)
                return BadRequest("Datos inválidos para insertar.");

            var clase = new clsPROVEEDORE { entidad = entidad };
            var resultado = clase.Insertar();
            return resultado.StartsWith("Error") ? (IHttpActionResult)BadRequest(resultado) : Ok(resultado);
        }

        [HttpPut]
        [Route("Actualizar")]
        public IHttpActionResult Actualizar([FromBody] PROVEEDORE entidad)
        {
            if (entidad == null)
                return BadRequest("Datos inválidos para actualizar.");

            var clase = new clsPROVEEDORE { entidad = entidad };
            var resultado = clase.Actualizar();
            return resultado.StartsWith("Error") ? (IHttpActionResult)BadRequest(resultado) : Ok(resultado);
        }

        [HttpDelete]
        [Route("Eliminar")]
        public IHttpActionResult Eliminar([FromBody] PROVEEDORE entidad)
        {
            if (entidad == null || entidad.ID_PROVEEDOR <= 0)
                return BadRequest("ID inválido para eliminar.");

            var clase = new clsPROVEEDORE { entidad = entidad };
            var resultado = clase.Eliminar();
            return resultado.StartsWith("Error") ? (IHttpActionResult)BadRequest(resultado) : Ok(resultado);
        }
    }
}
