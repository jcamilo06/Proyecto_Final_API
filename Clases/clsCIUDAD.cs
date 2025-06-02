using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Proyecto_Final_API.Clases
{
    public class clsCIUDAD
    {
        private INMOBILIARIAEntities1 db = new INMOBILIARIAEntities1();
        public CIUDADE entidad { get; set; }

        public string Insertar()
        {
            try
            {
                bool duplicado = db.CIUDADES.Any(c =>
                    c.NOMBRE.Trim().ToUpper() == entidad.NOMBRE.Trim().ToUpper() &&
                    c.DEPARTAMENTO.Trim().ToUpper() == entidad.DEPARTAMENTO.Trim().ToUpper());

                if (duplicado)
                    return "Ya existe una ciudad con ese nombre y departamento.";

                db.CIUDADES.Add(entidad);
                db.SaveChanges();
                return "Ciudad insertada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar ciudad: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                if (entidad == null || entidad.ID_CIUDAD <= 0)
                    return "Datos inválidos para actualizar.";

                bool duplicado = db.CIUDADES.Any(c =>
                    c.NOMBRE.Trim().ToUpper() == entidad.NOMBRE.Trim().ToUpper() &&
                    c.DEPARTAMENTO.Trim().ToUpper() == entidad.DEPARTAMENTO.Trim().ToUpper() &&
                    c.ID_CIUDAD != entidad.ID_CIUDAD);

                if (duplicado)
                    return "Ya existe otra ciudad con el mismo nombre y departamento.";

                db.CIUDADES.AddOrUpdate(entidad);
                db.SaveChanges();
                return "Ciudad actualizada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al actualizar ciudad: " + ex.Message;
            }
        }

        public object Consultar(int id)
        {
            try
            {
                var resultado = db.CIUDADES
                         .Where(c => c.ID_CIUDAD == id)
                         .Select(c => new
                         {
                             c.ID_CIUDAD,
                             c.NOMBRE,
                             c.DEPARTAMENTO
                         }).FirstOrDefault();

                if (resultado == null)
                    return "Ciudad no encontrada, verifique el ID.";
                return resultado;
            }
            catch (Exception ex)
            {
                return "Error al ciudad: " + ex.Message;
            }
        }

        public List<object> ConsultarTodos()
        {
            try
            {
                return db.CIUDADES
                         .OrderBy(c => c.NOMBRE)
                         .Select(c => new
                         {
                             c.ID_CIUDAD,
                             c.NOMBRE,
                             c.DEPARTAMENTO
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
                var obj = db.CIUDADES.Find(entidad.ID_CIUDAD);
                if (obj == null)
                    return "Ciudad no encontrada.";

                db.CIUDADES.Remove(obj);
                db.SaveChanges();
                return "Ciudad eliminada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar ciudad: " + ex.Message;
            }
        }
    }

}
