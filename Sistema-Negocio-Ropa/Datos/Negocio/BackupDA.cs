using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Negocio
{
    public class BackupDA
    {
        public SqlCommand CMD;
        private Conexion conexion;


        public BackupDA()
        {
            conexion = new Conexion();
        }

        //Insert, Update And Delete...
        public void general_query(string query)
        {
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                using(SqlCommand cmd = new SqlCommand(query, oContexto))
                {
                    oContexto.Open();                    
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public DataTable MyTable(string query)
        {
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                using(SqlCommand cmd = new SqlCommand(query, oContexto))
                {
                    oContexto.Open();
                    SqlDataAdapter myAdapter = new SqlDataAdapter(CMD);
                    DataTable mytbl = new DataTable();
                    myAdapter.Fill(mytbl);
                    return mytbl;
                }
            }
        }

    }
}
