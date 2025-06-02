using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Proyecto_Final_API.Models;

namespace Proyecto_Final_API.Clases
{
    public class clsDETALLES_ORDEN
    {
        private readonly INMOBILIARIAEntities1 db = new INMOBILIARIAEntities1();
        public DETALLES_ORDEN entidad { get; set; }

        public string Insertar()
        {
            try
            {
                if (entidad == null)
                    return "Datos inválidos para inserción.";

                // Validar que la orden exista
                if (!db.ORDENES_COMPRA.Any(o => o.ID_ORDEN == entidad.ID_ORDEN))
                    return "La orden de compra asociada no existe.";

                db.DETALLES_ORDEN.Add(entidad);
                db.SaveChanges();
                return "Detalle de orden insertado correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al insertar detalle de orden: {ex.Message}";
            }
        }

        public string Actualizar()
        {
            try
            {
                if (entidad == null || entidad.ID_DETALLE <= 0)
                    return "Datos inválidos para actualización.";

                var existente = db.DETALLES_ORDEN.Find(entidad.ID_DETALLE);
                if (existente == null)
                    return "No se encontró el detalle de orden.";

                // Validación si se cambia la orden
                if (!db.ORDENES_COMPRA.Any(o => o.ID_ORDEN == entidad.ID_ORDEN))
                    return "La orden de compra asociada no existe.";

                // Actualizar campos
                existente.ID_ORDEN = entidad.ID_ORDEN;
                existente.DESCRIPCION = entidad.DESCRIPCION;
                existente.CANTIDAD = entidad.CANTIDAD;
                existente.PRECIO_UNITARIO = entidad.PRECIO_UNITARIO;

                db.Entry(existente).State = EntityState.Modified;
                db.SaveChanges();
                return "Detalle de orden actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al actualizar detalle de orden: {ex.Message}";
            }
        }

        public object Consultar(int id)
        {
            try
            {
                var detalle = db.DETALLES_ORDEN
                                .Where(d => d.ID_DETALLE == id)
                                .Select(d => new
                                {
                                    d.ID_DETALLE,
                                    d.DESCRIPCION,
                                    d.CANTIDAD,
                                    d.PRECIO_UNITARIO,
                                    d.ID_ORDEN,
                                    ORDEN = new
                                    {
                                        d.ORDENES_COMPRA.ID_ORDEN,
                                        d.ORDENES_COMPRA.FECHA_ORDEN
                                    }
                                })
                                .FirstOrDefault();

                if (detalle == null)
                    return "Detalles de orden no encontrados, verifique el ID.";

                return detalle;
            }
            catch (Exception ex)
            {
                return $"Error al consultar detalles de orden: {ex.Message}";
            }
        }


        public List<object> ConsultarTodos()
        {
            try
            {
                return db.DETALLES_ORDEN
                         .OrderBy(d => d.ID_DETALLE)
                         .Select(d => new
                         {
                             d.ID_DETALLE,
                             d.DESCRIPCION,
                             d.CANTIDAD,
                             d.PRECIO_UNITARIO,
                             d.ID_ORDEN,
                             ORDEN = new
                             {
                                 d.ORDENES_COMPRA.ID_ORDEN,
                                 d.ORDENES_COMPRA.FECHA_ORDEN
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
                if (entidad == null || entidad.ID_DETALLE <= 0)
                    return "ID inválido para eliminación.";

                var detalle = db.DETALLES_ORDEN.Find(entidad.ID_DETALLE);
                if (detalle == null)
                    return "No se encontró el detalle de orden.";

                db.DETALLES_ORDEN.Remove(detalle);
                db.SaveChanges();
                return "Detalle de orden eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al eliminar detalle de orden: {ex.Message}";
            }
        }
    }
}
