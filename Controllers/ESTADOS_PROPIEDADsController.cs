using Borrador.Clases;
using Borrador.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Borrador.Controllers
{
    [RoutePrefix("api/estados_propiedad")]
    public class ESTADOS_PROPIEDADsController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<object> ConsultarTodos()
        {
            return new clsESTADOS_PROPIEDAD().ConsultarTodos();
        }

        [HttpGet]
        [Route("Consultar")]
        public object Consultar(int id)
        {
            return new clsESTADOS_PROPIEDAD().Consultar(id);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] ESTADOS_PROPIEDAD entidad)
        {
            var clase = new clsESTADOS_PROPIEDAD { entidad = entidad };
            return clase.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] ESTADOS_PROPIEDAD entidad)
        {
            var clase = new clsESTADOS_PROPIEDAD { entidad = entidad };
            return clase.Actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] ESTADOS_PROPIEDAD entidad)
        {
            var clase = new clsESTADOS_PROPIEDAD { entidad = entidad };
            return clase.Eliminar();
        }
    }
}
