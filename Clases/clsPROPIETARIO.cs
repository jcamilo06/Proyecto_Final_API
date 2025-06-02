using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto_Final_API.Clases
{
    public class clsPROPIETARIO
    {
        private readonly INMOBILIARIAEntities1 db = new INMOBILIARIAEntities1();
        public PROPIETARIO entidad { get; set; }

        public string Insertar()
        {
            try
            {
                if (entidad == null || string.IsNullOrWhiteSpace(entidad.IDENTIFICACION))
                    return "Datos inválidos para insertar.";

                if (db.PROPIETARIOS.Any(p => p.IDENTIFICACION == entidad.IDENTIFICACION))
                    return "Ya existe un propietario con esa identificación.";

                db.PROPIETARIOS.Add(entidad);
                db.SaveChanges();
                return "Propietario insertado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar propietario: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                if (entidad == null || entidad.ID_PROPIETARIO <= 0)
                    return "Datos inválidos para actualizar.";

                var existente = db.PROPIETARIOS.Find(entidad.ID_PROPIETARIO);
                if (existente == null)
                    return "Propietario no encontrado.";

                bool duplicado = db.PROPIETARIOS.Any(p =>
                    p.IDENTIFICACION == entidad.IDENTIFICACION &&
                    p.ID_PROPIETARIO != entidad.ID_PROPIETARIO);

                if (duplicado)
                    return "Ya existe otro propietario con esa identificación.";

                existente.IDENTIFICACION = entidad.IDENTIFICACION;
                existente.NOMBRES = entidad.NOMBRES;
                existente.APELLIDOS = entidad.APELLIDOS;
                existente.TELEFONO = entidad.TELEFONO;
                existente.EMAIL = entidad.EMAIL;
                existente.DIRECCION = entidad.DIRECCION;

                db.Entry(existente).State = EntityState.Modified;
                db.SaveChanges();
                return "Propietario actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar propietario: " + ex.Message;
            }
        }

        public object Consultar(int id)
        {
            try
            {
                var resultado = db.PROPIETARIOS
                    .Where(p => p.ID_PROPIETARIO == id)
                    .Select(p => new
                    {
                        p.ID_PROPIETARIO,
                        p.IDENTIFICACION,
                        p.NOMBRES,
                        p.APELLIDOS,
                        p.TELEFONO,
                        p.EMAIL,
                        p.DIRECCION
                    }).FirstOrDefault();
                if (resultado == null)
                    return "Propietario no encontrado, verifique el ID.";
                return resultado;
            }
            catch (Exception ex)
            {
                return "Error al consultar propietario: " + ex.Message;
            }
        }

        public List<object> ConsultarTodos()
        {
            try
            {
                return db.PROPIETARIOS
                    .OrderBy(p => p.NOMBRES)
                    .Select(p => new
                    {
                        p.ID_PROPIETARIO,
                        p.IDENTIFICACION,
                        p.NOMBRES,
                        p.APELLIDOS,
                        p.TELEFONO,
                        p.EMAIL,
                        p.DIRECCION
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
                if (entidad == null || entidad.ID_PROPIETARIO <= 0)
                    return "ID inválido para eliminación.";

                var obj = db.PROPIETARIOS.Find(entidad.ID_PROPIETARIO);
                if (obj == null)
                    return "Propietario no encontrado.";

                db.PROPIETARIOS.Remove(obj);
                db.SaveChanges();
                return "Propietario eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar propietario: " + ex.Message;
            }
        }
    }
}