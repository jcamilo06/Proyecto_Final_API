using Proyecto_Final_API.Clases;
using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Proyecto_Final_API.Controllers
{
    [RoutePrefix("api/visitas")]
    public class VISITAsController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<object> ConsultarTodos()
        {
            return new clsVISITA().ConsultarTodos();
        }

        [HttpGet]
        [Route("Consultar")]
        public object Consultar(int id)
        {
            return new clsVISITA().Consultar(id);
        }


        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] VISITA entidad)
        {
            var clase = new clsVISITA { entidad = entidad };
            return clase.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] VISITA entidad)
        {
            var clase = new clsVISITA { entidad = entidad };
            return clase.Actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] VISITA entidad)
        {
            var clase = new clsVISITA { entidad = entidad };
            return clase.Eliminar();
        }
    }
}