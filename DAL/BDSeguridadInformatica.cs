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
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DAL
{
    public class BDSeguridadInformatica : DbContext
    {
        public BDSeguridadInformatica() : base(Conexion.ConexionString()) { }

        #region Tablas
        public DbSet<Formulario> Formulario { get; set; }
        public DbSet<Parametros> Parametros { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<RolFormulario> RolFormulario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Historial> Historial { get; set; }
        #endregion

        #region Campos STRING UNICODE
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parametros>().Property(e => e.Descripcion).IsUnicode(false);
            modelBuilder.Entity<Parametros>().Property(e => e.Parametro).IsUnicode(false);
            modelBuilder.Entity<Formulario>().Property(e => e.NombreFormulario).IsUnicode(false);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
