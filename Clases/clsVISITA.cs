using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Proyecto_Final_API.Clases
{
    public class clsVISITA
    {
        private readonly INMOBILIARIAEntities1 db = new INMOBILIARIAEntities1();
        public VISITA entidad { get; set; }

        public string Insertar()
        {
            try
            {
                if (entidad == null)
                    return "Entidad no válida para inserción.";

                db.VISITAS.Add(entidad);
                db.SaveChanges();
                return "VISITA insertada correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al insertar VISITA: {ex.Message}";
            }
        }
        public string Actualizar()
        {
            try
            {
                if (entidad == null || entidad.ID_VISITA <= 0)
                    return "Entidad no válida para actualización.";

                var existente = db.VISITAS.Find(entidad.ID_VISITA);
                if (existente == null)
                    return "VISITA no encontrada para actualizar.";

                db.Entry(existente).CurrentValues.SetValues(entidad);
                db.SaveChanges();
                return "VISITA actualizada correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al actualizar VISITA: {ex.Message}";
            }
        }
        public object Consultar(int id)
        {
            try
            {
                var resultado = db.VISITAS
                    .Where(v => v.ID_VISITA == id)
                    .Select(v => new
                    {
                        v.ID_VISITA,
                        PROPIEDAD = new { v.PROPIEDADE.ID_PROPIEDAD, v.PROPIEDADE.TITULO },
                        CLIENTE = new { v.CLIENTE.ID_CLIENTE, v.CLIENTE.NOMBRES, v.CLIENTE.APELLIDOS },
                        EMPLEADO = new { v.EMPLEADO.ID_EMPLEADO, v.EMPLEADO.NOMBRES, v.EMPLEADO.APELLIDOS },
                        TIPO_VISITA = new { v.TIPOS_VISITA.ID_TIPO_VISITA, v.TIPOS_VISITA.DESCRIPCION },
                        v.FECHA_HORA,
                        v.COMENTARIOS
                    }).FirstOrDefault();

                if (resultado == null)
                    return "Visita no encontrada,verifique el ID.";

                return resultado;

            }
            catch (Exception ex)
            {
                return "Error al consultar visita: " + ex.Message;
            }
        }
        public List<object> ConsultarTodos()
        {
            try
            {
                return db.VISITAS
                    .OrderBy(v => v.ID_VISITA)
                    .Select(v => new
                    {
                        v.ID_VISITA,
                        PROPIEDAD = new { v.PROPIEDADE.ID_PROPIEDAD, v.PROPIEDADE.TITULO },
                        CLIENTE = new { v.CLIENTE.ID_CLIENTE, v.CLIENTE.NOMBRES, v.CLIENTE.APELLIDOS },
                        EMPLEADO = new { v.EMPLEADO.ID_EMPLEADO, v.EMPLEADO.NOMBRES, v.EMPLEADO.APELLIDOS },
                        TIPO_VISITA = new { v.TIPOS_VISITA.ID_TIPO_VISITA, v.TIPOS_VISITA.DESCRIPCION },
                        v.FECHA_HORA,
                        v.COMENTARIOS
                    }).ToList<object>();
            }
            catch
            {
                return new List<object>();
            }
        }
        public string Eliminar()
        {
            try
            {
                if (entidad == null || entidad.ID_VISITA <= 0)
                    return "Entidad no válida para eliminación.";

                var obj = db.VISITAS.Find(entidad.ID_VISITA);
                if (obj == null)
                    return "VISITA no encontrada.";

                db.VISITAS.Remove(obj);
                db.SaveChanges();
                return "VISITA eliminada correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al eliminar VISITA: {ex.Message}";
            }
        }
    }
}
