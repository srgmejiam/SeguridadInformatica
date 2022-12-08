
using EL;
using DAL;

namespace BL
{
    public class BL_Historial
    {
        public static Historial InsertarHistorial(Historial Entidad)
        {
            return DAL_Historial.InsertarHistorial(Entidad);
        }
    }
}
