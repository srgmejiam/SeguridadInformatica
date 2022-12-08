using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("RolFormulario")]
    public partial class RolFormulario
    {
        [Key]
        public int IdRolFormulario { get; set; }
        public int IdRol { get; set; }
        public int IdFormulario { get; set; }
        public bool Escribir { get; set; }
        public bool Anular { get; set; }
        public bool Activo { get; set; }
        public int UsuarioRegistra { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public Nullable<int> UsuarioActualiza { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
    }
}
