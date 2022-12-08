using System.Collections.Generic;
using EL;
using DAL;

namespace BL
{
    public class BL_Rol
    {

        public static List<Rol> ListadeRoles(bool Activo = true)
        {
            return DAL_Rol.ListadeRoles(Activo);
        }

    }

}
