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
    }
}