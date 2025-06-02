using Proyecto_Final_API.Clases;
using Proyecto_Final_API.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Proyecto_Final_API.Controllers
{
    [RoutePrefix("api/tipos_proveedor")]
    public class TIPOS_PROVEEDORsController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<object> ConsultarTodos()
        {
            return new clsTIPOS_PROVEEDOR().ConsultarTodos();
        }

        [HttpGet]
        [Route("Consultar")]
        public object Consultar(int id)
        {
            return new clsTIPOS_PROVEEDOR().Consultar(id);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] TIPOS_PROVEEDOR entidad)
        {
            var clase = new clsTIPOS_PROVEEDOR { entidad = entidad };
            return clase.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] TIPOS_PROVEEDOR entidad)
        {
            var clase = new clsTIPOS_PROVEEDOR { entidad = entidad };
            return clase.Actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] TIPOS_PROVEEDOR entidad)
        {
            var clase = new clsTIPOS_PROVEEDOR { entidad = entidad };
            return clase.Eliminar();
        }
    }
}
