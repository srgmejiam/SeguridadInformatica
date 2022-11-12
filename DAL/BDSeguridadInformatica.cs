using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using EL;

namespace DAL
{
    public class BDSeguridadInformatica : DbContext
    {
        public BDSeguridadInformatica()
       : base(Conexion.ConexionString())
        {
        }

        public virtual DbSet<Rol> Rol { get; set; }

    }
}
