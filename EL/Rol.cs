using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("Rol")]
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        [Required]
        public string NombreRol { get; set; }
        [Required]
        public bool Activo { get; set; }
    }
}
