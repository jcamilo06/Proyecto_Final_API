using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Final_API.Clases
{
    public class clsUsuario
    {
        private INMOBILIARIAEntities1 dbInmobiliaria = new INMOBILIARIAEntities1();
        public USUARIO entidad { get; set; }
        public string Insertar(int idPerfil)
        {
            try
            {
                clsCypher cypher = new clsCypher();
                cypher.Password = entidad.CLAVE;
                if (cypher.CifrarClave())
                {
                    //grabar el usuario, se deben leer los datos de la clase cypher con la información encriptada
                    entidad.CLAVE = cypher.PasswordCifrado;
                    entidad.SALT = cypher.Salt;
                    dbInmobiliaria.USUARIOs.Add(entidad);
                    dbInmobiliaria.SaveChanges();
                    //se debe grabar el perfil del usuario
                    USUARIO_PERFIL UsuarioPerfil = new USUARIO_PERFIL();
                    UsuarioPerfil.ID_PERFIL = idPerfil;
                    UsuarioPerfil.ACTIVO = true;
                    UsuarioPerfil.ID_USUARIO = entidad.ID_USUARIO; //El id del usuario queda grabado 
                    dbInmobiliaria.USUARIO_PERFIL.Add(UsuarioPerfil);
                    dbInmobiliaria.SaveChanges();
                    return "Se creó el usuario correctamente";
                }
                {
                    return "No se puede encriptar la clave del usuario";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        //public string Actualizar()
        //{
        //    try
        //    {
        //        if (entidad == null || entidad.ID_USUARIO <= 0)
        //            return "ID inválido para actualizar.";

        //        var existente = dbInmobiliaria.USUARIOs.Find(entidad.ID_USUARIO);
        //        if (existente == null)
        //            return "Usuario no encontrado.";

        //        existente.EMAIL = entidad.EMAIL;
        //        existente.CONTRASENA = entidad.CONTRASENA;
        //        existente.ROL = entidad.ROL;

        //        dbInmobiliaria.SaveChanges();
        //        return "Usuario actualizado correctamente.";
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Error al actualizar usuario: " + ex.Message;
        //    }
        //}

        //public object Consultar(int id)
        //{
        //    try
        //    {
        //        return dbInmobiliaria.USUARIOS
        //            .Where(u => u.ID_USUARIO == id)
        //            .Select(u => new
        //            {
        //                u.ID_USUARIO,
        //                u.EMAIL,
        //                u.ROL,
        //                u.FECHA_REGISTRO
        //            })
        //            .FirstOrDefault();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public object ConsultarPorEmail(string email)
        //{
        //    try
        //    {
        //        return dbInmobiliaria.USUARIOS
        //            .Where(u => u.EMAIL == email)
        //            .Select(u => new
        //            {
        //                u.ID_USUARIO,
        //                u.EMAIL,
        //                u.ROL,
        //                u.FECHA_REGISTRO
        //            })
        //            .FirstOrDefault();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public List<object> ConsultarTodos()
        //{
        //    try
        //    {
        //        return dbInmobiliaria.USUARIOS
        //            .OrderBy(u => u.EMAIL)
        //            .Select(u => new
        //            {
        //                u.ID_USUARIO,
        //                u.EMAIL,
        //                u.ROL,
        //                u.FECHA_REGISTRO
        //            })
        //            .ToList<object>();
        //    }
        //    catch
        //    {
        //        return new List<object>();
        //    }
        //}

        //public string Eliminar()
        //{
        //    try
        //    {
        //        var obj = dbInmobiliaria.USUARIOS.Find(entidad.ID_USUARIO);
        //        if (obj == null)
        //            return "Usuario no encontrado.";

        //        dbInmobiliaria.USUARIOS.Remove(obj);
        //        dbInmobiliaria.SaveChanges();
        //        return "Usuario eliminado correctamente.";
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Error al eliminar usuario: " + ex.Message;
        //    }
        //}
    }
}