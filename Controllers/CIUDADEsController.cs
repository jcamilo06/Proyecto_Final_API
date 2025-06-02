using Proyecto_Final_API.Clases;
using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Proyecto_Final_API.Controllers
{
    [RoutePrefix("api/ciudades")]
    public class CIUDADEsController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public IHttpActionResult ConsultarTodos()
        {
            var lista = new clsCIUDAD().ConsultarTodos();
            return Ok(lista);
        }

        [HttpGet]
        [Route("Consultar")]
        public IHttpActionResult Consultar(int id)
        {
            var ciudad = new clsCIUDAD().Consultar(id);
            if (ciudad == null)
                return NotFound();
            return Ok(ciudad);
        }

        [HttpPost]
        [Route("Insertar")]
        public IHttpActionResult Insertar([FromBody] CIUDADE entidad)
        {
            if (entidad == null)
                return BadRequest("Datos no válidos");

            var clase = new clsCIUDAD { entidad = entidad };
            var resultado = clase.Insertar();

            return resultado.Contains("Error") || resultado.Contains("Ya existe")
                ? (IHttpActionResult)BadRequest(resultado)
                : Ok(resultado);
        }

        [HttpPut]
        [Route("Actualizar")]
        public IHttpActionResult Actualizar([FromBody] CIUDADE entidad)
        {
            if (entidad == null || entidad.ID_CIUDAD <= 0)
                return BadRequest("Datos no válidos");

            var clase = new clsCIUDAD { entidad = entidad };
            var resultado = clase.Actualizar();

            return resultado.Contains("Error") || resultado.Contains("Ya existe") || resultado.Contains("inválidos")
                ? (IHttpActionResult)BadRequest(resultado)
                : Ok(resultado);
        }

        [HttpDelete]
        [Route("Eliminar")]
        public IHttpActionResult Eliminar([FromBody] CIUDADE entidad)
        {
            if (entidad == null || entidad.ID_CIUDAD <= 0)
                return BadRequest("ID no válido para eliminación");

            var clase = new clsCIUDAD { entidad = entidad };
            var resultado = clase.Eliminar();

            return resultado.Contains("Error") || resultado.Contains("no encontrada")
                ? (IHttpActionResult)BadRequest(resultado)
                : Ok(resultado);
        }
    }
}