using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Conexion
    {
        private string CadenaConexion = "Data Source=localhost;Initial Catalog=TiendaDeRopa;Integrated Security=True;";

        public SqlConnection EstablecerConexion()
        {
            return new SqlConnection(CadenaConexion);
        }


    }
}
