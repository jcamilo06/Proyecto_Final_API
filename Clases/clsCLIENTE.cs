using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Proyecto_Final_API.Clases
{
    public class clsCLIENTE
    {
        private INMOBILIARIAEntities1 db = new INMOBILIARIAEntities1();
        public CLIENTE entidad { get; set; }

        public string Insertar()
        {
            try
            {
                if (entidad == null)
                    return "Datos del cliente no válidos.";

                bool existe = db.CLIENTES.Any(c => c.IDENTIFICACION == entidad.IDENTIFICACION);
                if (existe)
                    return "Ya existe un cliente con la misma identificación.";

                if (entidad.FECHA_REGISTRO == default(DateTime))
                    entidad.FECHA_REGISTRO = DateTime.Now;

                db.CLIENTES.Add(entidad);
                db.SaveChanges();
                return "cliente insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar cliente: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                if (entidad == null || entidad.ID_CLIENTE <= 0)
                    return "Datos del cliente no válidos para actualizar.";

                var existente = db.CLIENTES.Find(entidad.ID_CLIENTE);
                if (existente == null)
                    return "Cliente no encontrado.";

                bool duplicado = db.CLIENTES.Any(c =>
                    c.IDENTIFICACION == entidad.IDENTIFICACION &&
                    c.ID_CLIENTE != entidad.ID_CLIENTE);

                if (duplicado)
                    return "Ya existe otro cliente con la misma identificación.";

                existente.IDENTIFICACION = entidad.IDENTIFICACION;
                existente.NOMBRES = entidad.NOMBRES;
                existente.APELLIDOS = entidad.APELLIDOS;
                existente.TELEFONO = entidad.TELEFONO;
                existente.EMAIL = entidad.EMAIL;
                existente.DIRECCION = entidad.DIRECCION;

                db.Entry(existente).State = EntityState.Modified;
                db.SaveChanges();
                return "cliente actualizado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al actualizar cliente: " + ex.Message;
            }
        }

        public object Consultar(int id)
        {
            try
            {
                var resultado = db.CLIENTES
                         .Where(c => c.ID_CLIENTE == id)
                         .Select(c => new
                         {
                             c.ID_CLIENTE,
                             c.IDENTIFICACION,
                             c.NOMBRES,
                             c.APELLIDOS,
                             c.TELEFONO,
                             c.EMAIL,
                             c.DIRECCION,
                             c.FECHA_REGISTRO
                         }).FirstOrDefault();
                if (resultado == null)
                    return "Cliente no encontrado, verifique el ID.";
                return resultado;
            }
            catch (Exception ex)
            {
                return "Error al consultar cliente: " + ex.Message;
            }
        }

        public List<object> ConsultarTodos()
        {
            try
            {
                return db.CLIENTES
                         .OrderBy(c => c.NOMBRES)
                         .Select(c => new
                         {
                             c.ID_CLIENTE,
                             c.IDENTIFICACION,
                             c.NOMBRES,
                             c.APELLIDOS,
                             c.TELEFONO,
                             c.EMAIL,
                             c.DIRECCION,
                             c.FECHA_REGISTRO
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
                if (entidad == null || entidad.ID_CLIENTE <= 0)
                    return "ID de cliente no válido.";

                var cliente = db.CLIENTES.Find(entidad.ID_CLIENTE);
                if (cliente == null)
                    return "No se encontró el cliente";

                db.CLIENTES.Remove(cliente);
                db.SaveChanges();
                return "cliente eliminado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar cliente: " + ex.Message;
            }
        }
    }
}
