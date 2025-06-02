using Inmobiliaria.Models;
using Proyecto_Final_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Final_API.Clases
{
    public class clsLogin
    {
        public clsLogin()
        {
            loginRespuesta = new LoginRespuesta();
        }
        private INMOBILIARIAEntities1 dbInmobiliaria = new INMOBILIARIAEntities1();
        public Login login { get; set; }
        public LoginRespuesta loginRespuesta { get; set; }
        public bool ValidarUsuario()
        {
            try
            {
                clsCypher cifrar = new clsCypher();
                USUARIO usuario = dbInmobiliaria.USUARIOs.FirstOrDefault(u => u.USERNAME == login.Usuario);
                if (usuario == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Usuario no existe";
                    return false;
                }
                byte[] arrBytesSalt = Convert.FromBase64String(usuario.SALT);
                string ClaveCifrada = cifrar.HashPassword(login.Clave, arrBytesSalt);
                login.Clave = ClaveCifrada;
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        private bool ValidarClave()
        {
            try
            {
                USUARIO usuario = dbInmobiliaria.USUARIOs.FirstOrDefault(u => u.USERNAME == login.Usuario && u.CLAVE == login.Clave);
                if (usuario == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "La clave no coincide";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        public IQueryable<LoginRespuesta> Ingresar()
        {
            // if (login.Usuario == "admin")
            if (ValidarUsuario() && ValidarClave())
            {
                //Se genera el token
                string Token = TokenGenerator.GenerateTokenJwt(login.Usuario);
                string token = TokenGenerator.GenerateTokenJwt(login.Usuario);
                return from U in dbInmobiliaria.Set<USUARIO>()
                       join UP in dbInmobiliaria.Set<USUARIO_PERFIL>()
                       on U.ID_USUARIO equals UP.ID_USUARIO
                       join P in dbInmobiliaria.Set<PERFIL>()
                       on UP.ID_PERFIL equals P.ID_PERFIL
                       where U.USERNAME == login.Usuario &&
                               U.CLAVE == login.Clave
                       select new LoginRespuesta
                       {
                           Usuario = U.USERNAME,
                           Autenticado = true,
                           Perfil = P.NOMBRE,
                           PaginaInicio = P.PAGINA_NAVEGAR,
                           Token = token,
                           Mensaje = ""
                       };
            }
            else
            {
                List<LoginRespuesta> listRpta = new List<LoginRespuesta>();
                listRpta.Add(loginRespuesta);
                return listRpta.AsQueryable();
            }
        }
    }
}