using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Final_API.Clases
{
    public class clsSEDE
    {
        private readonly INMOBILIARIAEntities1 db = new INMOBILIARIAEntities1();
        public SEDE entidad { get; set; }

        public string Insertar()
        {
            try
            {
                if (entidad == null || string.IsNullOrWhiteSpace(entidad.NOMBRE))
                    return "Datos inválidos para insertar.";

                if (db.SEDES.Any(s => s.NOMBRE.Trim().ToUpper() == entidad.NOMBRE.Trim().ToUpper()))
                    return "Ya existe una sede con ese nombre.";

                db.SEDES.Add(entidad);
                db.SaveChanges();
                return "Sede insertada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar sede: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                if (entidad == null || entidad.ID_SEDE <= 0)
                    return "Datos inválidos para actualizar.";

                var existente = db.SEDES.Find(entidad.ID_SEDE);
                if (existente == null)
                    return "Sede no encontrada.";

                if (db.SEDES.Any(s => s.NOMBRE.Trim().ToUpper() == entidad.NOMBRE.Trim().ToUpper() &&
                                      s.ID_SEDE != entidad.ID_SEDE))
                    return "Ya existe otra sede con ese nombre.";

                existente.NOMBRE = entidad.NOMBRE;
                existente.DIRECCION = entidad.DIRECCION;
                existente.ID_CIUDAD = entidad.ID_CIUDAD;

                db.SaveChanges();
                return "Sede actualizada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar sede: " + ex.Message;
            }
        }

        public object Consultar(int id)
        {
            try
            {
                var sede = db.SEDES
                    .Where(s => s.ID_SEDE == id)
                    .Select(s => new
                    {
                        s.ID_SEDE,
                        s.NOMBRE,
                        s.DIRECCION,
                        s.TELEFONO,
                        s.ID_CIUDAD
                    })
                    .FirstOrDefault();

                if (sede == null)
                    return "Sede no encontrada, verifique el ID.";

                return sede;
            }
            catch (Exception ex)
            {
                return "Error al consultar sede: " + ex.Message;
            }
        }

        public List<object> ConsultarTodos()
        {
            try
            {
                return db.SEDES
                    .OrderBy(s => s.NOMBRE)
                    .Select(s => new
                    {
                        s.ID_SEDE,
                        s.NOMBRE,
                        s.DIRECCION,
                        s.TELEFONO,
                        s.ID_CIUDAD
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
                if (entidad == null || entidad.ID_SEDE <= 0)
                    return "ID inválido para eliminar.";

                var obj = db.SEDES.Find(entidad.ID_SEDE);
                if (obj == null)
                    return "Sede no encontrada.";

                db.SEDES.Remove(obj);
                db.SaveChanges();
                return "Sede eliminada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar sede: " + ex.Message;
            }
        }
    }
}