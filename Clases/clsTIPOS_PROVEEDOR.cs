using System;
using System.Collections.Generic;
using System.Linq;
using Proyecto_Final_API.Models;

namespace Proyecto_Final_API.Clases
{ 
    public class clsTIPOS_PROVEEDOR
    {
        private readonly INMOBILIARIAEntities1 db = new INMOBILIARIAEntities1();
        public TIPOS_PROVEEDOR entidad { get; set; }

        public string Insertar()
        {
            try
            {
                if (entidad == null || string.IsNullOrWhiteSpace(entidad.DESCRIPCION))
                    return "La descripción no puede estar vacía.";

                bool existe = db.TIPOS_PROVEEDOR.Any(tp =>
                    tp.DESCRIPCION.Trim().ToUpper() == entidad.DESCRIPCION.Trim().ToUpper());

                if (existe)
                    return "Ya existe un tipo de proveedor con esta descripción.";

                db.TIPOS_PROVEEDOR.Add(entidad);
                db.SaveChanges();
                return "Tipo de proveedor insertado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar tipo de proveedor: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                if (entidad == null || entidad.ID_TIPO_PROVEEDOR <= 0)
                    return "ID inválido para actualizar.";

                var actual = db.TIPOS_PROVEEDOR.Find(entidad.ID_TIPO_PROVEEDOR);
                if (actual == null)
                    return "Tipo de proveedor no encontrado.";

                bool duplicado = db.TIPOS_PROVEEDOR.Any(tp =>
                    tp.DESCRIPCION.Trim().ToUpper() == entidad.DESCRIPCION.Trim().ToUpper() &&
                    tp.ID_TIPO_PROVEEDOR != entidad.ID_TIPO_PROVEEDOR);

                if (duplicado)
                    return "Ya existe otro tipo de proveedor con la misma descripción.";

                actual.DESCRIPCION = entidad.DESCRIPCION;
                db.SaveChanges();
                return "Tipo de proveedor actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar tipo de proveedor: " + ex.Message;
            }
        }

        public object Consultar(int id)
        {
            try
            {
                var tipoProveedor = db.TIPOS_PROVEEDOR
                    .Where(tp => tp.ID_TIPO_PROVEEDOR == id)
                    .Select(tp => new
                    {
                        tp.ID_TIPO_PROVEEDOR,
                        tp.DESCRIPCION
                    })
                    .FirstOrDefault();

                if (tipoProveedor == null)
                    return "Tipo de proveedor no encontrado, verifique el ID.";

                return tipoProveedor;
            }
            catch (Exception ex)
            {
                return "Error al consultar tipo de proveedor: " + ex.Message;
            }
        }


        public List<object> ConsultarTodos()
        {
            try
            {
                return db.TIPOS_PROVEEDOR
                    .OrderBy(tp => tp.DESCRIPCION)
                    .Select(tp => new
                    {
                        tp.ID_TIPO_PROVEEDOR,
                        tp.DESCRIPCION
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
                if (entidad == null || entidad.ID_TIPO_PROVEEDOR <= 0)
                    return "ID inválido para eliminar.";

                var obj = db.TIPOS_PROVEEDOR.Find(entidad.ID_TIPO_PROVEEDOR);
                if (obj == null)
                    return "Tipo de proveedor no encontrado.";

                db.TIPOS_PROVEEDOR.Remove(obj);
                db.SaveChanges();
                return "Tipo de proveedor eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar tipo de proveedor: " + ex.Message;
            }
        }
    }
}
