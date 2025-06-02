using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Proyecto_Final_API.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Proyecto_Final_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API

            // ✅ Habilitar CORS globalmente (acepta todo)
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // ✅ Evitar errores de referencias circulares en objetos relacionados (Entity Framework)
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            json.SerializerSettings.ContractResolver = new DefaultContractResolver(); // <-- ESTA ES CLAVE

            // ✅ Validación de tokens para endpoints protegidos
            config.MessageHandlers.Add(new TokenValidationHandler());

            // ✅ Rutas por atributo
            config.MapHttpAttributeRoutes();

            // ✅ Ruta por defecto para compatibilidad
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
