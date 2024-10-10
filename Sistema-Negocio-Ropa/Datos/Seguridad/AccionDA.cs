using Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Seguridad
{
    public class AccionDA
    {
        private Conexion conexion;

        public AccionDA()
        {
            conexion = new Conexion();
        }

        // OBTENER ACCION POR NOMBRE de MODULO y ACCION
        public Accion ObtenerAccion(string NombreModulo, string NombreAccion)
        {
            Accion oAccion = new Accion();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT A.*");
                    query.AppendLine("FROM Accion A");
                    query.AppendLine("INNER JOIN Modulo M ON A.ModuloID = M.ModuloID");
                    query.AppendLine("WHERE M.Nombre = @NombreModulo AND A.Nombre = @NombreAccion");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@NombreModulo", NombreModulo);
                        cmd.Parameters.AddWithValue("@NombreAccion", NombreAccion);
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oAccion.AccionID = Convert.ToInt32(reader["AccionID"]);
                                oAccion.Nombre = reader["Nombre"].ToString();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Ocurrió un error al intentar obtener la acción. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.");
                }
            }
            return oAccion;
        }

        // OBTENER ACCION POR ID
        public Accion ObtenerAccionIDD(int accionID)
        {
            Accion oAccion = new Accion();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT * FROM Accion WHERE AccionID = @accionID");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@accionID", accionID);
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oAccion.AccionID = Convert.ToInt32(reader["AccionID"]);
                                oAccion.Nombre = reader["Nombre"].ToString();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Ocurrió un error al intentar obtener la acción. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.");
                }
            }
            return oAccion;
        }
    }
}
