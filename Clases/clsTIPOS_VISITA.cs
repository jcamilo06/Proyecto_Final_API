using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Proyecto_Final_API.Clases
{
    public class clsTIPOS_VISITA
    {
        private readonly INMOBILIARIAEntities1 db = new INMOBILIARIAEntities1();
        public TIPOS_VISITA entidad { get; set; }

        public string Insertar()
        {
            try
            {
                if (entidad == null || string.IsNullOrWhiteSpace(entidad.DESCRIPCION))
                    return "La descripción no puede estar vacía.";

                bool existe = db.TIPOS_VISITA.Any(t => t.DESCRIPCION.Trim().ToUpper() == entidad.DESCRIPCION.Trim().ToUpper());
                if (existe)
                    return "Ya existe un tipo de visita con esa descripción.";

                db.TIPOS_VISITA.Add(entidad);
                db.SaveChanges();
                return "Tipo de visita insertado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar tipo de visita: " + ex.Message;
            }
        }
        public string Actualizar()
        {
            try
            {
                if (entidad == null || entidad.ID_TIPO_VISITA <= 0)
                    return "Datos inválidos para actualizar.";

                bool duplicado = db.TIPOS_VISITA.Any(t =>
                    t.DESCRIPCION.Trim().ToUpper() == entidad.DESCRIPCION.Trim().ToUpper() &&
                    t.ID_TIPO_VISITA != entidad.ID_TIPO_VISITA);

                if (duplicado)
                    return "Ya existe otro tipo de visita con la misma descripción.";

                var existente = db.TIPOS_VISITA.Find(entidad.ID_TIPO_VISITA);
                if (existente == null)
                    return "Tipo de visita no encontrado.";

                existente.DESCRIPCION = entidad.DESCRIPCION;
                db.SaveChanges();
                return "Tipo de visita actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar tipo de visita: " + ex.Message;
            }
        }

        public object Consultar(int id)
        {
            try
            {
                var resultado = db.TIPOS_VISITA
                    .Where(t => t.ID_TIPO_VISITA == id)
                    .Select(t => new
                    {
                        t.ID_TIPO_VISITA,
                        t.DESCRIPCION
                    })
                    .FirstOrDefault();
                if (resultado == null)
                    return "Tipo de visita no encontrado, verifique el ID.";
                return resultado;
            }
            catch (Exception ex)
            {
                return "Error al consultar tipo de visita: " + ex.Message;
            }
        }

        public List<object> ConsultarTodos()
        {
            try
            {
                return db.TIPOS_VISITA
                    .OrderBy(t => t.DESCRIPCION)
                    .Select(t => new
                    {
                        t.ID_TIPO_VISITA,
                        t.DESCRIPCION
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
                if (entidad == null || entidad.ID_TIPO_VISITA <= 0)
                    return "ID inválido para eliminar.";

                var obj = db.TIPOS_VISITA.Find(entidad.ID_TIPO_VISITA);
                if (obj == null)
                    return "Tipo de visita no encontrado.";

                db.TIPOS_VISITA.Remove(obj);
                db.SaveChanges();
                return "Tipo de visita eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar tipo de visita: " + ex.Message;
            }
        }
    }
}
