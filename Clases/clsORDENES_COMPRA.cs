using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Proyecto_Final_API.Models;

namespace Proyecto_Final_API.Clases
{
    public class clsORDENES_COMPRA
    {
        private readonly INMOBILIARIAEntities1 db = new INMOBILIARIAEntities1();
        public ORDENES_COMPRA entidad { get; set; }

        public string Insertar()
        {
            try
            {
                if (entidad == null || entidad.ID_PROVEEDOR <= 0 || entidad.ID_EMPLEADO <= 0 || entidad.TOTAL <= 0)
                    return "Datos de orden no válidos.";

                entidad.FECHA_ORDEN = entidad.FECHA_ORDEN == default(DateTime)
                    ? DateTime.Now.Date
                    : entidad.FECHA_ORDEN;

                db.ORDENES_COMPRA.Add(entidad);
                db.SaveChanges();
                return "Orden de compra insertada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar orden de compra: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                if (entidad == null || entidad.ID_ORDEN <= 0)
                    return "ID de orden no válido para actualización.";

                var existente = db.ORDENES_COMPRA.Find(entidad.ID_ORDEN);
                if (existente == null)
                    return "Orden no encontrada.";

                existente.ID_PROVEEDOR = entidad.ID_PROVEEDOR;
                existente.ID_EMPLEADO = entidad.ID_EMPLEADO;
                existente.FECHA_ORDEN = entidad.FECHA_ORDEN;
                existente.TOTAL = entidad.TOTAL;

                db.Entry(existente).State = EntityState.Modified;
                db.SaveChanges();
                return "Orden de compra actualizada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar orden de compra: " + ex.Message;
            }
        }

        public object Consultar(int id)
        {
            try
            {
                var orden = db.ORDENES_COMPRA
                    .Where(o => o.ID_ORDEN == id)
                    .Select(o => new
                    {
                        o.ID_ORDEN,
                        o.ID_PROVEEDOR,
                        NOMBRE_PROVEEDOR = o.PROVEEDORE.NOMBRE_COMERCIAL,
                        o.ID_EMPLEADO,
                        NOMBRE_EMPLEADO = o.EMPLEADO.NOMBRES + " " + o.EMPLEADO.APELLIDOS,
                        o.FECHA_ORDEN,
                        o.TOTAL
                    })
                    .FirstOrDefault();

                if (orden == null)
                    return "Orden de compra no encontrada, verifique el ID.";

                return orden;
            }
            catch (Exception ex)
            {
                return "Error al consultar orden de compra: " + ex.Message;
            }
        }


        public List<object> ConsultarTodos()
        {
            try
            {
                return db.ORDENES_COMPRA
                    .OrderByDescending(o => o.FECHA_ORDEN)
                    .Select(o => new
                    {
                        o.ID_ORDEN,
                        o.ID_PROVEEDOR,
                        NOMBRE_PROVEEDOR = o.PROVEEDORE.NOMBRE_COMERCIAL,
                        o.ID_EMPLEADO,
                        NOMBRE_EMPLEADO = o.EMPLEADO.NOMBRES + " " + o.EMPLEADO.APELLIDOS,
                        o.FECHA_ORDEN,
                        o.TOTAL
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
                if (entidad == null || entidad.ID_ORDEN <= 0)
                    return "ID no válido para eliminación.";

                var orden = db.ORDENES_COMPRA.Find(entidad.ID_ORDEN);
                if (orden == null)
                    return "Orden no encontrada.";

                db.ORDENES_COMPRA.Remove(orden);
                db.SaveChanges();
                return "Orden de compra eliminada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar orden de compra: " + ex.Message;
            }
        }
    }
}
