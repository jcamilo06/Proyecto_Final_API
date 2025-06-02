using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Proyecto_Final_API.Clases
{
    public class clsVENTA
    {
        private readonly INMOBILIARIAEntities1 db = new INMOBILIARIAEntities1();
        public VENTA entidad { get; set; }

        public string Insertar()
        {
            try
            {
                if (entidad == null)
                    return "Entidad no válida.";

                if (!db.PROPIEDADES.Any(p => p.ID_PROPIEDAD == entidad.ID_PROPIEDAD))
                    return "Propiedad no válida.";
                if (!db.CLIENTES.Any(c => c.ID_CLIENTE == entidad.ID_CLIENTE))
                    return "Cliente no válido.";
                if (!db.EMPLEADOS.Any(e => e.ID_EMPLEADO == entidad.ID_EMPLEADO))
                    return "Empleado no válido.";

                db.VENTAS.Add(entidad);
                db.SaveChanges();
                return "Venta insertada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar venta: " + ex.Message;
            }
        }
        public string Actualizar()
        {
            try
            {
                if (entidad == null || entidad.ID_VENTA <= 0)
                    return "Entidad no válida para actualizar.";

                var existente = db.VENTAS.Find(entidad.ID_VENTA);
                if (existente == null)
                    return "Venta no encontrada.";

                existente.ID_PROPIEDAD = entidad.ID_PROPIEDAD;
                existente.ID_CLIENTE = entidad.ID_CLIENTE;
                existente.ID_EMPLEADO = entidad.ID_EMPLEADO;
                existente.FECHA_VENTA = entidad.FECHA_VENTA;
                existente.PRECIO_FINAL = entidad.PRECIO_FINAL;
                existente.COMISION = entidad.COMISION;

                db.SaveChanges();
                return "Venta actualizada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar venta: " + ex.Message;
            }
        }
        public object Consultar(int id)
        {
            try
            {
                var resultado = db.VENTAS
                    .Where(v => v.ID_VENTA == id)
                    .Select(v => new
                    {
                        v.ID_VENTA,
                        v.FECHA_VENTA,
                        v.PRECIO_FINAL,
                        v.COMISION,
                        PROPIEDAD = new { v.PROPIEDADE.ID_PROPIEDAD, v.PROPIEDADE.TITULO },
                        CLIENTE = new { v.CLIENTE.ID_CLIENTE, v.CLIENTE.NOMBRES },
                        EMPLEADO = new { v.EMPLEADO.ID_EMPLEADO, v.EMPLEADO.NOMBRES }
                    })
                    .FirstOrDefault();
                if (resultado == null)
                    return "Venta no encontrada, verifique el ID.";
                return resultado;
            }
            catch (Exception ex)
            {
                return "Error al consultar venta: " + ex.Message;
            }
        }
        public List<object> ConsultarTodos()
        {
            try
            {
                return db.VENTAS
                    .OrderBy(v => v.FECHA_VENTA)
                    .Select(v => new
                    {
                        v.ID_VENTA,
                        v.FECHA_VENTA,
                        v.PRECIO_FINAL,
                        v.COMISION,
                        PROPIEDAD = new { v.PROPIEDADE.ID_PROPIEDAD, v.PROPIEDADE.TITULO },
                        CLIENTE = new { v.CLIENTE.ID_CLIENTE, v.CLIENTE.NOMBRES },
                        EMPLEADO = new { v.EMPLEADO.ID_EMPLEADO, v.EMPLEADO.NOMBRES }
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
                var obj = db.VENTAS.Find(entidad.ID_VENTA);
                if (obj == null)
                    return "Venta no encontrada.";

                db.VENTAS.Remove(obj);
                db.SaveChanges();
                return "Venta eliminada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar venta: " + ex.Message;
            }
        }
    }
}
