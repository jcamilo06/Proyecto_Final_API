using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Borrador.Models;

namespace Borrador.Clases
{
    public class clsESTADOS_PROPIEDAD
    {
        private readonly INMOBILIARIAEntities db = new INMOBILIARIAEntities();
        public ESTADOS_PROPIEDAD entidad { get; set; }

        public string Insertar()
        {
            try
            {
                if (entidad == null || string.IsNullOrWhiteSpace(entidad.DESCRIPCION))
                    return "Descripción requerida para insertar estado.";

                bool existe = db.ESTADOS_PROPIEDAD.Any(e => e.DESCRIPCION == entidad.DESCRIPCION);
                if (existe)
                    return "Ya existe un estado de propiedad con esa descripción.";

                db.ESTADOS_PROPIEDAD.Add(entidad);
                db.SaveChanges();
                return "Estado de propiedad insertado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar estado de propiedad: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                if (entidad == null || entidad.ID_ESTADO_PROPIEDAD <= 0)
                    return "Datos inválidos para actualización.";

                var actual = db.ESTADOS_PROPIEDAD.Find(entidad.ID_ESTADO_PROPIEDAD);
                if (actual == null)
                    return "Estado de propiedad no encontrado.";

                bool duplicado = db.ESTADOS_PROPIEDAD.Any(e =>
                    e.DESCRIPCION == entidad.DESCRIPCION &&
                    e.ID_ESTADO_PROPIEDAD != entidad.ID_ESTADO_PROPIEDAD);

                if (duplicado)
                    return "Ya existe otro estado de propiedad con esa descripción.";

                db.Entry(actual).CurrentValues.SetValues(entidad);
                db.SaveChanges();
                return "Estado de propiedad actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar estado de propiedad: " + ex.Message;
            }
        }

        public object Consultar(int id)
        {
            try
            {
                var resultado = db.ESTADOS_PROPIEDAD
                         .Where(e => e.ID_ESTADO_PROPIEDAD == id)
                         .Select(e => new
                         {
                             e.ID_ESTADO_PROPIEDAD,
                             e.DESCRIPCION
                         })
                         .FirstOrDefault();
                if (resultado == null)
                    return "Estado de propiedad no encontrado, verifique el ID.";
                return resultado;
            }
            catch (Exception ex)
            {
                return "Error al consultar estado de propiedad: " + ex.Message;
            }
        }

        public List<object> ConsultarTodos()
        {
            try
            {
                return db.ESTADOS_PROPIEDAD
                         .OrderBy(e => e.DESCRIPCION)
                         .Select(e => new
                         {
                             e.ID_ESTADO_PROPIEDAD,
                             e.DESCRIPCION
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
                if (entidad == null || entidad.ID_ESTADO_PROPIEDAD <= 0)
                    return "ID de estado inválido.";

                var estado = db.ESTADOS_PROPIEDAD.Find(entidad.ID_ESTADO_PROPIEDAD);
                if (estado == null)
                    return "Estado de propiedad no encontrado.";

                db.ESTADOS_PROPIEDAD.Remove(estado);
                db.SaveChanges();
                return "Estado de propiedad eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar estado de propiedad: " + ex.Message;
            }
        }
    }
}
