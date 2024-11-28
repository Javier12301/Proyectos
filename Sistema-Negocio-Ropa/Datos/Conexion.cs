using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Datos
{
    public class Conexion
    {
        private string CadenaConexion = ConfigurationManager.ConnectionStrings["TiendaDeRopaDB"].ConnectionString;

        public SqlConnection EstablecerConexion()
        {
            return new SqlConnection(CadenaConexion);
        }


    }
}
