using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EL;
using DAL;

namespace BL
{
    public class BL_Usuario
    {      
        public static Usuario InsertUsuario(Usuario Entidad)
        {
            return DAL_Usuario.InsertUsuario(Entidad);
        }
        public static bool UpdateUsuario(Usuario Entidad, bool UpdatePassword = false)
        {
            return DAL_Usuario.UpdateUsuario(Entidad, UpdatePassword);
        }
        public static bool AnularUsuario(Usuario Entidad)
        {
            return DAL_Usuario.AnularUsuario(Entidad);
        }
        public static bool ExisteUsuario(int IdUsuario, bool Activo = true)
        {
            return DAL_Usuario.ExisteUsuario(IdUsuario);
        }
        public static Usuario BuscarUsuario(int IdUsuario, bool Activo = true)
        {
            return DAL_Usuario.BuscarUsuario(IdUsuario);
        }
       
        public static bool ExisteCorreo(string Correo)
        {
            return DAL_Usuario.ExisteCorreo(Correo);
        }
        public static bool ExisteCorreoUpdate(string Correo, int IdUsuario)
        {
            return DAL_Usuario.ExisteCorreoUpdate(Correo, IdUsuario);
        }
        public static bool ValidarFormatoEmail(string Email)
        {
            return DAL_Usuario.ValidarFormatoEmail(Email);
        }
        public static bool SumarIntentoFallido(int IdUsuario)
        {
            return DAL_Usuario.SumarIntentoFallido(IdUsuario);
        }
        public static bool RestablecerIntentosFallido(int IdUsuario)
        {
            return DAL_Usuario.RestablecerIntentosFallido(IdUsuario);
        }
        public static bool BloquearCuentaUsuario(int IdUsuario, bool bloqueado)
        {
            return DAL_Usuario.BloquearCuentaUsuario(IdUsuario, bloqueado);
        }
        public static bool CuentaBloqueada(string Login)
        {
            return DAL_Usuario.CuentaBloqueada(Login);
        }
        public static bool ExiteLogin(string Login)
        {
            return DAL_Usuario.ExiteLogin(Login);
        }
        public static bool ExiteLoginUpdate(string Login, int IdUsuario)
        {
            return DAL_Usuario.ExiteLoginUpdate(Login, IdUsuario);
        }
        public static Usuario PasswordUpdate(Usuario _usuario)
        {
            return DAL_Usuario.PasswordUpdate(_usuario);
        }
        public static List<vUsuario> VistaUsuarios(bool Activo = true)
        {
            return DAL_Usuario.vUsuarios(Activo);
        }
        public static vUsuario vUsuario(int IdUsuario, bool Activo = true)
        {
            return DAL_Usuario.vUsuario(IdUsuario, Activo);
        }
        public static bool ValidarCredenciales(string Login, byte[] Password)
        {
            return DAL_Usuario.ValidarCredenciales(Login, Password);
        }
        public static Usuario ObtenerUsuario_x_Login(string Login)
        {
            return DAL_Usuario.ObtenerUsuario_x_Login(Login);
        }
      
        public static byte[] Encrypt(string FlatString)
        {
            return DAL_Usuario.Encrypt(FlatString);
        }
    }
}
