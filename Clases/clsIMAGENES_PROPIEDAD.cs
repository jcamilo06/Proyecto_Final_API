using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Final_API.Clases
{
    public class clsIMAGENES_PROPIEDAD
    {
        private INMOBILIARIAEntities1 db = new INMOBILIARIAEntities1();
        public string IdPropiedad { get; set; }
        public List<string> Archivos { get; set; }

        public string GrabarImagenes()
        {
            try
            {
                if (Archivos != null && Archivos.Count > 0)
                {
                    foreach (var archivo in Archivos)
                    {
                        IMAGENES_PROPIEDAD img = new IMAGENES_PROPIEDAD
                        {
                            ID_PROPIEDAD = Convert.ToInt32(IdPropiedad),
                            NOMBRE = archivo
                        };
                        db.IMAGENES_PROPIEDAD.Add(img);
                    }
                    db.SaveChanges();
                    return "Imágenes guardadas correctamente.";
                }
                return "No se enviaron archivos.";
            }
            catch (Exception ex)
            {
                return $"Error al guardar imágenes: {ex.Message}";
            }
        }
    }
}