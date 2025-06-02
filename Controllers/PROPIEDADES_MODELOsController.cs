using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proyecto_Final_API.Clases;
using Proyecto_Final_API.Models;

namespace Proyecto_Final_API.Controllers
{
    [RoutePrefix("api/propiedades_modelo")]
    public class PROPIEDADES_MODELOsController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public IHttpActionResult ConsultarTodos()
        {
            var lista = new clsPROPIEDADES_MODELO().ConsultarTodos();
            return Ok(lista);
        }

        [HttpGet]
        [Route("Consultar")]
        public IHttpActionResult Consultar(int id)
        {
            var entidad = new clsPROPIEDADES_MODELO().Consultar(id);
            return entidad == null ? (IHttpActionResult)NotFound() : Ok(entidad);
        }

        [HttpPost]
        [Route("Insertar")]
        public IHttpActionResult Insertar([FromBody] PROPIEDADES_MODELO entidad)
        {
            if (entidad == null)
                return BadRequest("Datos no válidos para insertar.");

            var clase = new clsPROPIEDADES_MODELO { entidad = entidad };
            var resultado = clase.Insertar();
            return resultado.StartsWith("Error") ? (IHttpActionResult)BadRequest(resultado) : Ok(resultado);
        }

        [HttpPut] git fetch origin
        [Route("Actualizar")]
        public IHttpActionResult Actualizar([FromBody] PROPIEDADES_MODELO entidad)
        {
            if (entidad == null)
                return BadRequest("Datos no válidos para actualizar.");

            var clase = new clsPROPIEDADES_MODELO { entidad = entidad };
            var resultado = clase.Actualizar();
            return resultado.StartsWith("Error") ? (IHttpActionResult)BadRequest(resultado) : Ok(resultado);
        }

        [HttpDelete]
        [Route("Eliminar")]
        public IHttpActionResult Eliminar([FromBody] PROPIEDADES_MODELO entidad)
        {
            if (entidad == null || entidad.ID_PROPIEDAD <= 0)
                return BadRequest("ID inválido para eliminar.");

            var clase = new clsPROPIEDADES_MODELO { entidad = entidad };
            var resultado = clase.Eliminar();
            return resultado.StartsWith("Error") ? (IHttpActionResult)BadRequest(resultado) : Ok(resultado);
        }
    }
}