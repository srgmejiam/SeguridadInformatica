using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("Parametros")]
    public class Parametros
    {
        [Key]
        public int IdParametro { get; set; }
        [MaxLength(50), Required]
        public string Descripcion { get; set; }
        [MaxLength(50), Required]
        public string Parametro { get; set; }
        [Required]
        public string TipoDato { get; set; }
        [Required]
        public bool Activo { get; set; }
        [Required]
        public int Usuarioregistro { get; set; }
        [Required]
        public DateTime Fecharegistro { get; set; }
        public int? Usuarioactualiza { get; set; }
        public DateTime? Fechaactualiza { get; set; }

    }
}
