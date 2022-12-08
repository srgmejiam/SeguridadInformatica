using System;
using EL;

namespace DAL
{
    public class DAL_Historial
    {
        public static Historial InsertarHistorial(Historial Entidad)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                Entidad.Activo = true;
                Entidad.FechaRegistro = DateTime.Now;
                bd.Historial.Add(Entidad);
                bd.SaveChanges();
                return Entidad;
            }
        }
    }
}
