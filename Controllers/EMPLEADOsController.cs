using Proyecto_Final_API.Clases;
using Proyecto_Final_API.Models;
using System.Web.Http;

namespace Proyecto_Final_API.Controllers
{
    [RoutePrefix("api/empleados")]
    public class EMPLEADOsController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public IHttpActionResult ConsultarTodos()
        {
            var lista = new clsEMPLEADO().ConsultarTodos();
            return Ok(lista);
        }

        [HttpGet]
        [Route("Consultar")]
        public IHttpActionResult Consultar(int id)
        {
            var empleado = new clsEMPLEADO().Consultar(id);
            return empleado == null ? (IHttpActionResult)NotFound() : Ok(empleado);
        }

        [HttpPost]
        [Route("Insertar")]
        public IHttpActionResult Insertar([FromBody] EMPLEADO entidad)
        {
            if (entidad == null)
                return BadRequest("Datos de entrada inválidos.");

            var clase = new clsEMPLEADO { entidad = entidad };
            var resultado = clase.Insertar();

            return resultado.StartsWith("Error") || resultado.StartsWith("Ya existe")
                ? (IHttpActionResult)BadRequest(resultado)
                : Ok(resultado);
        }

        [HttpPut]
        [Route("Actualizar")]
        public IHttpActionResult Actualizar([FromBody] EMPLEADO entidad)
        {
            if (entidad == null)
                return BadRequest("Datos de entrada inválidos.");

            var clase = new clsEMPLEADO { entidad = entidad };
            var resultado = clase.Actualizar();

            return resultado.StartsWith("Error") || resultado.Contains("no encontrado")
                ? (IHttpActionResult)BadRequest(resultado)
                : Ok(resultado);
        }

        [HttpDelete]
        [Route("Eliminar")]
        public IHttpActionResult Eliminar([FromBody] EMPLEADO entidad)
        {
            if (entidad == null || entidad.ID_EMPLEADO <= 0)
                return BadRequest("ID no válido para eliminación.");

            var clase = new clsEMPLEADO { entidad = entidad };
            var resultado = clase.Eliminar();

            return resultado.StartsWith("Error") || resultado.Contains("no encontrado")
                ? (IHttpActionResult)BadRequest(resultado)
                : Ok(resultado);
        }
    }
}
