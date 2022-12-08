using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class Conexion
    {
        public static string ConexionString()
        {
            return "Data Source=SRG\\SQL2019;Initial Catalog=BDSeguridadInformatica;Integrated Security=True";
        }
    }
}
