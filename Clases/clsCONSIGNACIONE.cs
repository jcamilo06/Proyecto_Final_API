using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto_Final_API.Clases
{
    public class clsCONSIGNACIONE
    {
        private readonly INMOBILIARIAEntities1 db = new INMOBILIARIAEntities1();
        public CONSIGNACIONE entidad { get; set; }

        public string Insertar()
        {
            try
            {
                if (entidad == null)
                    return "Entidad vacía";

                // Validar llaves foráneas
                bool propiedadExiste = db.PROPIEDADES.Any(p => p.ID_PROPIEDAD == entidad.ID_PROPIEDAD);
                bool propietarioExiste = db.PROPIETARIOS.Any(p => p.ID_PROPIETARIO == entidad.ID_PROPIETARIO);

                if (!propiedadExiste)
                    return "La propiedad asociada no existe.";
                if (!propietarioExiste)
                    return "El propietario asociado no existe.";

                db.CONSIGNACIONES.Add(entidad);
                db.SaveChanges();
                return "Consignación insertada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar consignación: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                if (entidad == null || entidad.ID_CONSIGNACION <= 0)
                    return "Datos inválidos para actualizar.";

                var existente = db.CONSIGNACIONES.Find(entidad.ID_CONSIGNACION);
                if (existente == null)
                    return "Consignación no encontrada.";

                bool propiedadExiste = db.PROPIEDADES.Any(p => p.ID_PROPIEDAD == entidad.ID_PROPIEDAD);
                bool propietarioExiste = db.PROPIETARIOS.Any(p => p.ID_PROPIETARIO == entidad.ID_PROPIETARIO);

                if (!propiedadExiste)
                    return "La propiedad asociada no existe.";
                if (!propietarioExiste)
                    return "El propietario asociado no existe.";

                // Actualizar campos
                existente.ID_PROPIEDAD = entidad.ID_PROPIEDAD;
                existente.ID_PROPIETARIO = entidad.ID_PROPIETARIO;
                existente.FECHA_INICIO = entidad.FECHA_INICIO;
                existente.FECHA_FIN = entidad.FECHA_FIN;
                existente.PORCENTAJE_COMISION = entidad.PORCENTAJE_COMISION;

                db.Entry(existente).State = EntityState.Modified;
                db.SaveChanges();
                return "Consignación actualizada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al actualizar consignación: " + ex.Message;
            }
        }

        public object Consultar(int id)
        {
            try
            {
                var resultado = db.CONSIGNACIONES
                         .Where(c => c.ID_CONSIGNACION == id)
                         .Select(c => new
                         {
                             c.ID_CONSIGNACION,
                             c.FECHA_INICIO,
                             c.FECHA_FIN,
                             c.PORCENTAJE_COMISION,
                             PROPIEDAD = new
                             {
                                 c.PROPIEDADE.ID_PROPIEDAD,
                                 c.PROPIEDADE.TITULO
                             },
                             PROPIETARIO = new
                             {
                                 c.PROPIETARIO.ID_PROPIETARIO,
                                 c.PROPIETARIO.NOMBRES
                             }
                         })
                         .FirstOrDefault();
                if (resultado == null)
                    return "Consignación no encontrada, verifique el ID.";
                return resultado;
            }
            catch (Exception ex)
            {
                return "Error al consultar consignación: " + ex.Message;
            }
        }

        public List<object> ConsultarTodos()
        {
            try
            {
                return db.CONSIGNACIONES
                         .OrderBy(c => c.FECHA_INICIO)
                         .Select(c => new
                         {
                             c.ID_CONSIGNACION,
                             c.FECHA_INICIO,
                             c.FECHA_FIN,
                             c.PORCENTAJE_COMISION,
                             PROPIEDAD = new
                             {
                                 c.PROPIEDADE.ID_PROPIEDAD,
                                 c.PROPIEDADE.TITULO
                             },
                             PROPIETARIO = new
                             {
                                 c.PROPIETARIO.ID_PROPIETARIO,
                                 c.PROPIETARIO.NOMBRES
                             }
                         })
                         .ToList<object>();
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
                if (entidad == null || entidad.ID_CONSIGNACION <= 0)
                    return "ID inválido para eliminar.";

                var obj = db.CONSIGNACIONES.Find(entidad.ID_CONSIGNACION);
                if (obj == null)
                    return "Consignación no encontrada.";

                db.CONSIGNACIONES.Remove(obj);
                db.SaveChanges();
                return "Consignación eliminada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar consignación: " + ex.Message;
            }
        }
    }
}