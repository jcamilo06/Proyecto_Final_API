using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto_Final_API.Clases
{
    public class clsPROYECTO
    {
        private readonly INMOBILIARIAEntities1 db = new INMOBILIARIAEntities1();
        public PROYECTO entidad { get; set; }

        public string Insertar()
        {
            try
            {
                if (entidad == null || string.IsNullOrWhiteSpace(entidad.NOMBRE))
                    return "Datos inválidos para insertar.";

                if (db.PROYECTOS.Any(p => p.NOMBRE.Trim().ToUpper() == entidad.NOMBRE.Trim().ToUpper()))
                    return "Ya existe un proyecto con ese nombre.";

                db.PROYECTOS.Add(entidad);
                db.SaveChanges();
                return "Proyecto insertado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar proyecto: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                if (entidad == null || entidad.ID_PROYECTO <= 0)
                    return "Datos inválidos para actualizar.";

                var existente = db.PROYECTOS.Find(entidad.ID_PROYECTO);
                if (existente == null)
                    return "Proyecto no encontrado.";

                if (db.PROYECTOS.Any(p => p.NOMBRE.Trim().ToUpper() == entidad.NOMBRE.Trim().ToUpper()
                    && p.ID_PROYECTO != entidad.ID_PROYECTO))
                    return "Ya existe otro proyecto con ese nombre.";

                existente.NOMBRE = entidad.NOMBRE;
                existente.ID_CIUDAD = entidad.ID_CIUDAD;
                existente.DIRECCION = entidad.DIRECCION;
                existente.FECHA_LANZAMIENTO = entidad.FECHA_LANZAMIENTO;
                existente.FECHA_ENTREGA_ESTIMADA = entidad.FECHA_ENTREGA_ESTIMADA;
                existente.DESARROLLADOR = entidad.DESARROLLADOR;

                db.Entry(existente).State = EntityState.Modified;
                db.SaveChanges();
                return "Proyecto actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar proyecto: " + ex.Message;
            }
        }

        public object Consultar(int id)
        {
            try
            {
                var resultado = db.PROYECTOS
                    .Where(p => p.ID_PROYECTO == id)
                    .Select(p => new
                    {
                        p.ID_PROYECTO,
                        p.NOMBRE,
                        p.DIRECCION,
                        p.FECHA_LANZAMIENTO,
                        p.FECHA_ENTREGA_ESTIMADA,
                        p.DESARROLLADOR,
                        CIUDAD = new
                        {
                            p.CIUDADE.ID_CIUDAD,
                            p.CIUDADE.NOMBRE,
                            p.CIUDADE.DEPARTAMENTO
                        }
                    }).FirstOrDefault();
                if (resultado == null)
                    return "Proyecto no encontrado, verifique el ID.";
                return resultado;
            }
            catch (Exception ex)
            {
                return "Error al consultar proyecto: " + ex.Message;
            }
        }

        public List<object> ConsultarTodos()
        {
            try
            {
                return db.PROYECTOS
                    .OrderBy(p => p.NOMBRE)
                    .Select(p => new
                    {
                        p.ID_PROYECTO,
                        p.NOMBRE,
                        p.DIRECCION,
                        p.FECHA_LANZAMIENTO,
                        p.FECHA_ENTREGA_ESTIMADA,
                        p.DESARROLLADOR,
                        CIUDAD = new
                        {
                            p.CIUDADE.ID_CIUDAD,
                            p.CIUDADE.NOMBRE,
                            p.CIUDADE.DEPARTAMENTO
                        }
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
                if (entidad == null || entidad.ID_PROYECTO <= 0)
                    return "ID inv\u00e1lido para eliminaci\u00f3n.";

                var obj = db.PROYECTOS.Find(entidad.ID_PROYECTO);
                if (obj == null)
                    return "Proyecto no encontrado.";

                db.PROYECTOS.Remove(obj);
                db.SaveChanges();
                return "Proyecto eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar proyecto: " + ex.Message;
            }
        }
    }
}