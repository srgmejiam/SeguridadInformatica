using EL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using static EL.Enum;
using System.Security.Cryptography;
using Utility;

namespace DAL
{
    public static class DAL_Usuario
    {
        private static byte[] Key = Encoding.UTF8.GetBytes("S3Gur1d4d1nf0rm4t1c42o22");
        private static byte[] IV = Encoding.UTF8.GetBytes("Pr0y3ct03J3mpl00");
        public static Usuario InsertUsuario(Usuario Entidad)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                Entidad.Activo = true;
                Entidad.FechaRegistro = DateTime.Now;
                bd.Usuario.Add(Entidad);
                bd.SaveChanges();
                return Entidad;
            }

        }
        public static bool UpdateUsuario(Usuario Entidad, bool UpdatePassword = false)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                var registro = (from tabla in bd.Usuario where tabla.IdUsuario == Entidad.IdUsuario && tabla.Activo == true select tabla).SingleOrDefault();
                registro.Nombre = Entidad.Nombre;
                registro.Login = Entidad.Login;
                if (UpdatePassword) registro.Password = Entidad.Password;
                registro.Email = Entidad.Email;
                registro.IdRol = Entidad.IdRol;
                registro.Cargo = Entidad.Cargo;
                registro.Baja = Entidad.Baja;
                registro.UsuarioActualiza = Entidad.UsuarioActualiza;
                registro.FechaActualiza = DateTime.Now;
                return bd.SaveChanges() > 0;
            }
        }
        public static Usuario PasswordUpdate(Usuario _usuario)
        {
            try
            {
                using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
                {
                    var query = (from user in bd.Usuario where user.IdUsuario == _usuario.IdUsuario select user).SingleOrDefault();
                    query.Password = _usuario.Password;
                    query.FechaActualiza = DateTime.Now;
                    query.UsuarioActualiza = _usuario.UsuarioActualiza;

                    bd.SaveChanges();
                }
                return _usuario;
            }
            catch { return new Usuario(); }

        }
        public static bool AnularUsuario(Usuario Entidad)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                var query = (from tabla in bd.Usuario where tabla.IdUsuario == Entidad.IdUsuario && tabla.Activo == true select tabla).SingleOrDefault();
                query.Activo = false;
                query.FechaActualiza = DateTime.Now;
                return bd.SaveChanges() > 0;
            }
        }
        public static bool ExisteUsuario(int IdUsuario, bool Activo = true)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                var Consulta = (from tabla in bd.Usuario
                                where tabla.IdUsuario == IdUsuario
                                && tabla.Activo == Activo
                                select tabla).Count();
                return Consulta > 0;
            }
        }
        public static Usuario BuscarUsuario(int IdUsuario, bool Activo = true)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                var Consulta = (from tabla in bd.Usuario
                                where tabla.IdUsuario == IdUsuario
                                && tabla.Activo == Activo
                                select tabla).SingleOrDefault();
                return Consulta;
            }
        }
        public static List<vUsuario> vUsuarios(bool Activo = true)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                var Consulta = (from Usuarios in bd.Usuario
                                where Usuarios.Activo == Activo
                                join Roles in bd.Rol on Usuarios.IdRol equals Roles.IdRol

                                select new vUsuario
                                {
                                    Nombre = Usuarios.Nombre,
                                    Login = Usuarios.Login,
                                    Email = Usuarios.Email,
                                    Cargo = Usuarios.Cargo,
                                    Rol = Roles.NombreRol,
                                    Intentos = Usuarios.Intentos,
                                    Bloqueado = Usuarios.Bloqueado,
                                    Baja = Usuarios.Baja,
                                    IdUsuario = Usuarios.IdUsuario
                                }).ToList().OrderByDescending(x => x.IdUsuario).ToList();
                return Consulta;
            }
        }
        public static vUsuario vUsuario(int IdUsuario, bool Activo = true)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                var Consulta = (from Usuarios in bd.Usuario
                                where Usuarios.Activo == Activo
                                join Roles in bd.Rol on Usuarios.IdRol equals Roles.IdRol
                                where Usuarios.IdUsuario == IdUsuario
                                select new vUsuario
                                {
                                    Nombre = Usuarios.Nombre,
                                    Login = Usuarios.Login,
                                    Email = Usuarios.Email,
                                    Cargo = Usuarios.Cargo,
                                    IdRol = Usuarios.IdRol,
                                    Rol = Roles.NombreRol,
                                    Intentos = Usuarios.Intentos,
                                    Bloqueado = Usuarios.Bloqueado,
                                    Baja = Usuarios.Baja,
                                    IdUsuario = Usuarios.IdUsuario,
                                }).SingleOrDefault();

                return Consulta;
            }
        }
        public static bool ExisteCorreo(string Correo)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                var query = (from user in bd.Usuario where user.Email.Trim().ToUpper() == Correo.Trim().ToUpper() select user).Count();
                return (query > 0);
            }
        }
        public static bool ExisteCorreoUpdate(string Correo, int IdUsuario)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                var query = (from user in bd.Usuario where user.Email.Trim().ToUpper() == Correo.Trim().ToUpper() && user.IdUsuario != IdUsuario select user).Count();
                return (query > 0);
            }
        }
        public static bool ValidarFormatoEmail(string Email)
        {
            return Regex.IsMatch(Email, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$");
        }
        public static bool SumarIntentoFallido(int IdUsuario)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                var Usuario = (from tabla in bd.Usuario where tabla.IdUsuario == IdUsuario && tabla.Activo == true select tabla).Single();
                Usuario.Intentos = Convert.ToByte(Usuario.Intentos + 1);
                return bd.SaveChanges() > 0;
            }
        }
        public static bool RestablecerIntentosFallido(int IdUsuario)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                var Usuario = (from tabla in bd.Usuario where tabla.IdUsuario == IdUsuario && tabla.Activo == true select tabla).Single();
                Usuario.Intentos = 0;
                Usuario.FechaActualiza = DateTime.Now;
                return bd.SaveChanges() > 0;
            }
        }
        public static bool BloquearCuentaUsuario(int IdUsuario, bool bloqueado)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                var Usuario = (from tabla in bd.Usuario where tabla.IdUsuario == IdUsuario && tabla.Activo == true select tabla).Single();
                Usuario.Bloqueado = bloqueado;
                Usuario.FechaActualiza = DateTime.Now;
                Usuario.UsuarioActualiza = IdUsuario;
                return bd.SaveChanges() > 0;
            }
        }
        public static bool CuentaBloqueada(string Login)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                return (from tabla in bd.Usuario where tabla.Activo == true && tabla.Login.Trim().ToUpper() == Login.Trim().ToUpper() && tabla.Bloqueado == true select tabla).Count() > 0;
            }
        }
        public static bool ExiteLogin(string Login)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                var query = (from user in bd.Usuario where user.Login.Trim().ToUpper() == Login.Trim().ToUpper() && user.Activo == true select user).Count();
                return (query > 0);
            }
        }
        public static bool ExiteLoginUpdate(string Login, int IdUsuario)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                var query = (from user in bd.Usuario where user.Login.Trim().ToUpper() == Login.Trim().ToUpper() && user.IdUsuario != IdUsuario && user.Activo == true select user).Count();
                return (query > 0);
            }
        }
        public static bool ValidarCredenciales(string Login, byte[] Password)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                return (from tabla in bd.Usuario where tabla.Activo == true && tabla.Login.Trim().ToUpper() == Login.Trim().ToUpper() && tabla.Password == Password select tabla).Count() > 0;
            }
        }
        public static Usuario ObtenerUsuario_x_Login(string Login)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                return (from tabla in bd.Usuario where tabla.Activo == true && tabla.Login.Trim().ToUpper() == Login.Trim().ToUpper() select tabla).Single();
            }
        }
        public static byte[] Encrypt(string FlatString)
        {
            return Encripty.Encrypt(FlatString, Key, IV);
        }
    }


}


