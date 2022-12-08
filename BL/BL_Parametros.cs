using EL;
using DAL;

namespace BL
{
    public static class BL_Parametros
    {

        public static Parametros BuscarParametro(int IdParametro, bool Activo = true)
        {
            return DAL_Parametros.BuscarParametro(IdParametro, Activo);
        }

    }
}
