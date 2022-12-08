using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("Usuario")]
    public partial class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public int IdRol { get; set; }
        public int Intentos { get; set; }
        public bool Bloqueado { get; set; }
        public bool Baja { get; set; }
        public bool Activo { get; set; }

        public int UsuarioRegistro { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public Nullable<int> UsuarioActualiza { get; set; }
        public Nullable<System.DateTime> FechaActualiza { get; set; }

    }
    public class vUsuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public int IdRol { get; set; }
        public string Rol { get; set; }
        public int Intentos { get; set; }
        public bool Bloqueado { get; set; }
        public bool Baja { get; set; }
        public int IdAtencion { get; set; }
    }


}
