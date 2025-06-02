using Inmobiliaria.Models;
using Proyecto_Final_API.Clases;
using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Proyecto_Final_API.Controllers
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
