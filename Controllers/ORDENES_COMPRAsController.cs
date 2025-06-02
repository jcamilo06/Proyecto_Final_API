using Proyecto_Final_API.Clases;
using Proyecto_Final_API.Models;
using System.Web.Http;

namespace Proyecto_Final_API.Controllers
{
    [RoutePrefix("api/ordenes_compra")]
    public class ORDENES_COMPRAsController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public IHttpActionResult ConsultarTodos()
        {
            var lista = new clsORDENES_COMPRA().ConsultarTodos();
            return Ok(lista);
        }

        [HttpGet]
        [Route("Consultar")]
        public IHttpActionResult Consultar(int id)
        {
            var orden = new clsORDENES_COMPRA().Consultar(id);
            return orden == null ? (IHttpActionResult)NotFound() : Ok(orden);
        }

        [HttpPost]
        [Route("Insertar")]
        public IHttpActionResult Insertar([FromBody] ORDENES_COMPRA entidad)
        {
            if (entidad == null)
                return BadRequest("Datos inválidos para inserción.");

            var clase = new clsORDENES_COMPRA { entidad = entidad };
            var resultado = clase.Insertar();

            return resultado.StartsWith("Error") ? (IHttpActionResult)BadRequest(resultado) : Ok(resultado);
        }

        [HttpPut]
        [Route("Actualizar")]
        public IHttpActionResult Actualizar([FromBody] ORDENES_COMPRA entidad)
        {
            if (entidad == null)
                return BadRequest("Datos inválidos para actualización.");

            var clase = new clsORDENES_COMPRA { entidad = entidad };
            var resultado = clase.Actualizar();

            return resultado.StartsWith("Error") || resultado.Contains("no encontrado")
                ? (IHttpActionResult)BadRequest(resultado)
                : Ok(resultado);
        }

        [HttpDelete]
        [Route("Eliminar")]
        public IHttpActionResult Eliminar([FromBody] ORDENES_COMPRA entidad)
        {
            if (entidad == null || entidad.ID_ORDEN <= 0)
                return BadRequest("ID inválido para eliminación.");

            var clase = new clsORDENES_COMPRA { entidad = entidad };
            var resultado = clase.Eliminar();

            return resultado.StartsWith("Error") || resultado.Contains("no encontrada")
                ? (IHttpActionResult)BadRequest(resultado)
                : Ok(resultado);
        }
    }
}
