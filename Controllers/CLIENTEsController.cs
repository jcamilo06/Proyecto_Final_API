using Proyecto_Final_API.Clases;
using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Proyecto_Final_API.Controllers
{
    [RoutePrefix("api/clientes")]
    public class CLIENTEsController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public IHttpActionResult ConsultarTodos()
        {
            var lista = new clsCLIENTE().ConsultarTodos();
            return Ok(lista);
        }

        [HttpGet]
        [Route("Consultar")]
        public IHttpActionResult Consultar(int id)
        {
            var cliente = new clsCLIENTE().Consultar(id);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        [Route("Insertar")]
        public IHttpActionResult Insertar([FromBody] CLIENTE entidad)
        {
            if (entidad == null)
                return BadRequest("Datos de entrada no válidos.");

            var clase = new clsCLIENTE { entidad = entidad };
            var resultado = clase.Insertar();

            return resultado.Contains("Error") || resultado.Contains("Ya existe")
                ? (IHttpActionResult)BadRequest(resultado)
                : Ok(resultado);
        }

        [HttpPut]
        [Route("Actualizar")]
        public IHttpActionResult Actualizar([FromBody] CLIENTE entidad)
        {
            if (entidad == null)
                return BadRequest("Datos de entrada no válidos.");

            var clase = new clsCLIENTE { entidad = entidad };
            var resultado = clase.Actualizar();

            return resultado.Contains("Error") || resultado.Contains("no encontrado") || resultado.Contains("Ya existe")
                ? (IHttpActionResult)BadRequest(resultado)
                : Ok(resultado);
        }

        [HttpDelete]
        [Route("Eliminar")]
        public IHttpActionResult Eliminar([FromBody] CLIENTE entidad)
        {
            if (entidad == null || entidad.ID_CLIENTE <= 0)
                return BadRequest("ID no válido para eliminación.");

            var clase = new clsCLIENTE { entidad = entidad };
            var resultado = clase.Eliminar();

            return resultado.Contains("Error") || resultado.Contains("No se encontró")
                ? (IHttpActionResult)BadRequest(resultado)
                : Ok(resultado);
        }
    }
}