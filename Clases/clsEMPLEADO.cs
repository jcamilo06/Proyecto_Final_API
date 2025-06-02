using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Proyecto_Final_API.Models;

namespace Proyecto_Final_API.Clases
{
    public class clsEMPLEADO
    {
        private readonly INMOBILIARIAEntities db = new INMOBILIARIAEntities();
        public EMPLEADO entidad { get; set; }

        public string Insertar()
        {
            try
            {
                if (entidad == null)
                    return "Datos inválidos para inserción.";

                bool duplicado = db.EMPLEADOS.Any(e => e.IDENTIFICACION == entidad.IDENTIFICACION);
                if (duplicado)
                    return "Ya existe un empleado con esa identificación.";

                db.EMPLEADOS.Add(entidad);
                db.SaveChanges();
                return "Empleado insertado correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al insertar empleado: {ex.Message}";
            }
        }

        public string Actualizar()
        {
            try
            {
                if (entidad == null || entidad.ID_EMPLEADO <= 0)
                    return "Datos inválidos para actualización.";

                var actual = db.EMPLEADOS.Find(entidad.ID_EMPLEADO);
                if (actual == null)
                    return "Empleado no encontrado.";

                bool duplicado = db.EMPLEADOS.Any(e =>
                    e.IDENTIFICACION == entidad.IDENTIFICACION &&
                    e.ID_EMPLEADO != entidad.ID_EMPLEADO);

                if (duplicado)
                    return "Ya existe otro empleado con esa identificación.";

                db.Entry(actual).CurrentValues.SetValues(entidad);
                db.SaveChanges();
                return "Empleado actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al actualizar empleado: {ex.Message}";
            }
        }

        public object Consultar(int id)
        {
            try
            {
                var empleado = db.EMPLEADOS
                    .Where(e => e.ID_EMPLEADO == id)
                    .Select(e => new
                    {
                        e.ID_EMPLEADO,
                        e.IDENTIFICACION,
                        e.NOMBRES,
                        e.APELLIDOS,
                        e.TELEFONO,
                        e.EMAIL,
                        e.DIRECCION,
                        e.CARGO,
                        e.FECHA_INGRESO,
                        e.SALARIO,
                        e.ID_SEDE
                    }).FirstOrDefault();

                if (empleado == null)
                    return "Empleado no encontrado, verifique el ID.";

                return empleado;
            }
            catch (Exception ex)
            {
                return "Error al consultar empleado: " + ex.Message;
            }
        }

        public List<object> ConsultarTodos()
        {
            try
            {
                return db.EMPLEADOS
                    .OrderBy(e => e.NOMBRES)
                    .ThenBy(e => e.APELLIDOS)
                    .Select(e => new
                    {
                        e.ID_EMPLEADO,
                        e.IDENTIFICACION,
                        e.NOMBRES,
                        e.APELLIDOS,
                        e.TELEFONO,
                        e.EMAIL,
                        e.DIRECCION,
                        e.CARGO,
                        e.FECHA_INGRESO,
                        e.SALARIO,
                        e.ID_SEDE
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
                if (entidad == null || entidad.ID_EMPLEADO <= 0)
                    return "ID de empleado no válido.";

                var empleado = db.EMPLEADOS.Find(entidad.ID_EMPLEADO);
                if (empleado == null)
                    return "Empleado no encontrado.";

                db.EMPLEADOS.Remove(empleado);
                db.SaveChanges();
                return "Empleado eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al eliminar empleado: {ex.Message}";
            }
        }
    }
}
