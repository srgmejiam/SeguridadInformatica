using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EL;

namespace DAL
{
    public static class DAL_Rol
    {

        public static List<Rol> ListadeRoles(bool Activo = true)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                return (from table in bd.Rol
                        where table.Activo == Activo
                        select table).ToList();

            }
        }

    }
}
