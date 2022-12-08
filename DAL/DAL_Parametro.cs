using System.Collections.Generic;
using System.Linq;
using EL;

namespace DAL
{
    public class DAL_Parametros
    {

        public static Parametros BuscarParametro(int IdParametro, bool Activo = true)
        {
            using (BDSeguridadInformatica bd = new BDSeguridadInformatica())
            {
                var Consulta = (from tabla in bd.Parametros
                                where tabla.IdParametro == IdParametro
                                && tabla.Activo == Activo
                                select tabla).SingleOrDefault();
                return Consulta;
            }
        }


    }
}
