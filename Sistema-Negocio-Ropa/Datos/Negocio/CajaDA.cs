using Negocio.Negocio;
using Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Negocio
{
    public class CajaDA
    {
        private Conexion conexion;

        public CajaDA()
        {
            conexion = new Conexion();
        }

        public bool VerificarCajaAbierta()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection oContexto = conexion.EstablecerConexion())
                {
                    StringBuilder query = new StringBuilder();
                    query.Append("SELECT COUNT(*) FROM Caja WHERE Estado = 1");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        oContexto.Open();
                        int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                        resultado = cantidad == 1;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error al verificar caja abierta: " + ex.Message);
            }
            return resultado;
        }

        public Caja ObtenerCajaAbierta()
        {
            Caja oCaja = null; // Inicializamos como null para indicar que no se encontró una caja
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT * FROM Caja WHERE Estado = 1"); // Solo se busca la caja activa
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Solo se debe leer una fila, por lo que usamos un if en lugar de un while
                            if (reader.Read())
                            {
                                oCaja = new Caja(); // Creamos la instancia solo si encontramos la caja
                                oCaja.CajaID = Convert.ToInt32(reader["CajaID"]);
                                Usuario oUsuario = new Usuario();
                                oUsuario.UsuarioID = Convert.ToInt32(reader["UsuarioID"]);
                                oCaja.oUsuario = oUsuario;
                                oCaja.MontoInicial = Convert.ToDecimal(reader["MontoInicial"]);
                                oCaja.MontoFinal = reader["MontoFinal"] != DBNull.Value? Convert.ToDecimal(reader["MontoFinal"])
                                                            : (decimal?)null; // Puede ser null si no hay valor                                oCaja.FechaApertura = Convert.ToDateTime(reader["FechaApertura"]);
                                // es posible que la fecha de cierre sea null, por lo que usamos un operador ternario
                                oCaja.FechaCierre = reader["FechaCierre"] != DBNull.Value ? Convert.ToDateTime(reader["FechaCierre"]) : (DateTime?)null; // Manejo de null
                                oCaja.Estado = Convert.ToBoolean(reader["Estado"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener caja abierta: " + ex.Message);
                }
            }
            return oCaja; // Devuelve null si no hay caja abierta
        }

        // Generar CAJA
        public Caja AbrirCaja(Caja oCaja)
        {
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                string query = "INSERT INTO Caja (UsuarioID, MontoInicial, FechaApertura, Estado) VALUES (@UsuarioID, @MontoInicial, GETDATE(), 1);";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, oContexto))
                    {
                        cmd.Parameters.AddWithValue("@UsuarioID", oCaja.oUsuario.UsuarioID);
                        cmd.Parameters.AddWithValue("@MontoInicial", oCaja.MontoInicial);
                        oContexto.Open();

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            // Devuelve la caja recién abierta
                            return ObtenerCajaAbierta();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al abrir caja: " + ex.Message);
                }
            }
            return null; // Devuelve null si no se pudo abrir la caja
        }



    }
}
