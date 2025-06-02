using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Proyecto_Final_API.Clases
{
    public class clsUpload
    {
        public HttpRequestMessage request { get; set; }
        public string Datos { get; set; }
        public string Proceso { get; set; }

        public async Task<HttpResponseMessage> GrabarArchivo(bool Actualizar)
        {
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Archivos");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await request.Content.ReadAsMultipartAsync(provider);
                List<string> Archivos = new List<string>();
                string errores = "";

                foreach (var file in provider.FileData)
                {
                    string fileName = file.Headers.ContentDisposition.FileName?.Trim('"');
                    fileName = Path.GetFileName(fileName);
                    string filePath = Path.Combine(root, fileName);

                    if (File.Exists(filePath))
                    {
                        if (Actualizar)
                        {
                            File.Delete(filePath);
                            File.Move(file.LocalFileName, filePath);
                            Archivos.Add(fileName);
                        }
                        else
                        {
                            File.Delete(file.LocalFileName);
                            errores += $"El archivo {fileName} ya existe. ";
                        }
                    }
                    else
                    {
                        File.Move(file.LocalFileName, filePath);
                        Archivos.Add(fileName);
                    }
                }

                if (Archivos.Count > 0)
                {
                    string respuesta = ProcesarArchivos(Archivos);
                    return request.CreateResponse(HttpStatusCode.OK, respuesta);
                }
                else if (!string.IsNullOrEmpty(errores))
                {
                    return request.CreateErrorResponse(HttpStatusCode.Conflict, errores);
                }
                else
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se subieron archivos válidos.");
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error al cargar el archivo: " + ex.Message);
            }
        }

        public HttpResponseMessage ConsultarArchivo(string NombreArchivo)
        {
            try
            {
                string Ruta = HttpContext.Current.Server.MapPath("~/Archivos");
                string Archivo = Path.Combine(Ruta, NombreArchivo);

                if (File.Exists(Archivo))
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StreamContent(new FileStream(Archivo, FileMode.Open, FileAccess.Read))
                    };
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = NombreArchivo
                    };
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    return response;
                }
                else
                {
                    return request.CreateErrorResponse(HttpStatusCode.NotFound, "Archivo no encontrado");
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error al consultar el archivo: " + ex.Message);
            }
        }

        public string EliminarArchivo(string nombreImagen)
        {
            try
            {
                using (var db = new INMOBILIARIAEntities1())
                {
                    var imagen = db.IMAGENES_PROPIEDAD.FirstOrDefault(i => i.NOMBRE == nombreImagen);
                    if (imagen == null)
                        return "No se encontró la imagen en la base de datos.";

                    // Eliminar el archivo físico
                    string rutaBase = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos");
                    string rutaCompleta = System.IO.Path.Combine(rutaBase, nombreImagen);

                    if (System.IO.File.Exists(rutaCompleta))
                        System.IO.File.Delete(rutaCompleta);

                    // Eliminar de la base de datos
                    db.IMAGENES_PROPIEDAD.Remove(imagen);
                    db.SaveChanges();

                    return "OK";
                }
            }
            catch (Exception ex)
            {
                return "Error al eliminar imagen: " + ex.Message;
            }
        }


        private string ProcesarArchivos(List<string> Archivos)
        {
            switch (Proceso.ToUpper())
            {
                case "IMAGEN":
                    var imagenes = new clsIMAGENES_PROPIEDAD
                    {
                        IdPropiedad = Datos,
                        Archivos = Archivos
                    };
                    return imagenes.GrabarImagenes();
                default:
                    return "Proceso no válido";
            }
        }
    }
}