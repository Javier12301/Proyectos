using Negocio.Negocio;
using Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
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

        public CajaM ObtenerCajaID(int cajaID)
        {
            CajaM oCaja = new CajaM();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT * FROM Caja WHERE CajaID = @CajaID");
                using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                {
                    cmd.Parameters.AddWithValue("@CajaID", cajaID);
                    oContexto.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            oCaja.CajaID = Convert.ToInt32(reader["CajaID"]);
                            Usuario oUsuario = new Usuario();
                            oUsuario.UsuarioID = Convert.ToInt32(reader["UsuarioID"]);
                            oCaja.oUsuario = oUsuario;
                            oCaja.MontoInicial = Convert.ToDecimal(reader["MontoInicial"]);
                            oCaja.MontoFinal = reader["MontoFinal"] != DBNull.Value ? Convert.ToDecimal(reader["MontoFinal"]) : (decimal?)null;
                            oCaja.FechaApertura = Convert.ToDateTime(reader["FechaApertura"]);
                            oCaja.FechaCierre = reader["FechaCierre"] != DBNull.Value ? Convert.ToDateTime(reader["FechaCierre"]) : (DateTime?)null;
                            oCaja.Estado = Convert.ToBoolean(reader["Estado"]);
                            oCaja.Nota = reader["Nota"] != DBNull.Value ? reader["Nota"].ToString() : null;  // Leer la nota
                        }
                    }
                }
            }
            return oCaja;
        }


        public bool CerrarCaja(CajaM caja)
        {
            bool resultado = false;

            // Verificar que la caja no sea nula y que esté abierta
            if (caja != null && caja.Estado)
            {
                using (SqlConnection oContexto = conexion.EstablecerConexion())
                {
                    // Crear el comando para ejecutar la consulta
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = oContexto;
                        cmd.CommandText = "UPDATE Caja SET UsuarioID = @UsuarioID, MontoFinal = @MontoFinal, FechaCierre = @FechaCierre, Estado = 0, Nota = @Nota WHERE CajaID = @CajaID";

                        // Agregar los parámetros necesarios
                        cmd.Parameters.AddWithValue("@UsuarioID", caja.oUsuario.UsuarioID);
                        cmd.Parameters.AddWithValue("@MontoFinal", caja.MontoFinal);
                        cmd.Parameters.AddWithValue("@FechaCierre", caja.FechaCierre);
                        cmd.Parameters.AddWithValue("@Nota", caja.Nota); // Agregar la nota
                        cmd.Parameters.AddWithValue("@CajaID", caja.CajaID);

                        // Abrir la conexión
                        oContexto.Open();

                        // Ejecutar la consulta y verificar el resultado
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        resultado = filasAfectadas > 0; // Retornar true si se actualizó la caja
                    }
                }
            }

            return resultado; // Retornar el resultado de la operación
        }





        // obtener numero de venta, la logica de está será hacer un count de ventas referenciada a CajaID
        // vamos a obtener el numero de ventas que se han hecho en una caja, si se realizo la primera venta, al hacer count nos daría 0
        // ya que todavio no se registro la venta, entonces suamamos 1, y noso daría 1, si se realiza otra venta, el count nos daría 1
        public int ObtenerNumeroVenta(CajaM oCaja)
        {
            int numeroVenta = 0;
            if(oCaja != null)
            {
                using (SqlConnection oContexto = conexion.EstablecerConexion())
                {
                    StringBuilder query = new StringBuilder();
                    query.Append("SELECT COUNT(*) FROM Venta WHERE CajaID = @CajaID");
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                        {
                            cmd.Parameters.AddWithValue("@CajaID", oCaja.CajaID);
                            oContexto.Open();
                            numeroVenta = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al obtener número de venta: " + ex.Message);
                    }
                }
            }
                return numeroVenta;
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

        public CajaM ObtenerTotales(int cajaid)
        {
            CajaM oCaja = new CajaM();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerTotalesPorMetodoPago", oContexto))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CajaID", cajaid);
                    oContexto.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Leer la primera fila de resultados
                        {
                            oCaja.TotalEfectivo = reader.IsDBNull(0) ? 0 : reader.GetDecimal(0);
                            oCaja.TotalDebito = reader.IsDBNull(1) ? 0 : reader.GetDecimal(1);
                            oCaja.TotalCredito = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2);
                            oCaja.TotalTransferencia = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3);
                        }
                    }
                }
            }
            return oCaja;
        }


        public CajaM ObtenerCajaAbierta()
        {
            CajaM oCaja = null; // Inicializamos como null para indicar que no se encontró una caja
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
                                oCaja = new CajaM(); // Creamos la instancia solo si encontramos la caja
                                oCaja.CajaID = Convert.ToInt32(reader["CajaID"]);
                                Usuario oUsuario = new Usuario();
                                oUsuario.UsuarioID = Convert.ToInt32(reader["UsuarioID"]);
                                oCaja.oUsuario = oUsuario;
                                oCaja.MontoInicial = Convert.ToDecimal(reader["MontoInicial"]);
                                oCaja.MontoFinal = reader["MontoFinal"] != DBNull.Value ? Convert.ToDecimal(reader["MontoFinal"]) : (decimal?)null; // Puede ser null si no hay valor
                                oCaja.FechaApertura = Convert.ToDateTime(reader["FechaApertura"]);
                                // es posible que la fecha de cierre sea null, por lo que usamos un operador ternario
                                oCaja.FechaCierre = reader["FechaCierre"] != DBNull.Value ? Convert.ToDateTime(reader["FechaCierre"]) : (DateTime?)null; // Manejo de null
                                oCaja.Estado = Convert.ToBoolean(reader["Estado"]);
                                oCaja.Nota = reader["Nota"] != DBNull.Value ? reader["Nota"].ToString() : null; // Leer la nota
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
        public CajaM AbrirCaja(CajaM oCaja)
        {
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                string query = "INSERT INTO Caja (UsuarioID, MontoInicial, FechaApertura, Estado, Nota) VALUES (@UsuarioID, @MontoInicial, GETDATE(), 1, @Nota);"; // Añadida la nota
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, oContexto))
                    {
                        cmd.Parameters.AddWithValue("@UsuarioID", oCaja.oUsuario.UsuarioID);
                        cmd.Parameters.AddWithValue("@MontoInicial", oCaja.MontoInicial);
                        cmd.Parameters.AddWithValue("@Nota", oCaja.Nota); // Añadida la nota
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


        public DataTable ObtenerCajaFiltrada(string FiltroEstado, DateTime FechaInicio, DateTime FechaFin)
        {
            // default
            FiltroEstado = FiltroEstado == "" ? "Todos" : FiltroEstado;
            // SI FECHA INICIO ES NULL Y FIN HACER QUE INICIO SEA EN -5 AÑOS Y FIN HOY
            FechaInicio = FechaInicio == null ? DateTime.Now.AddYears(-5) : FechaInicio;
            FechaFin = FechaFin == null ? DateTime.Now.AddYears(5) : FechaFin;
            DataTable dtCajas = new DataTable();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {

                using (SqlCommand cmd = new SqlCommand("sp_ObtenerCajasConTotales", oContexto))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FiltroEstado", FiltroEstado);
                    cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                    oContexto.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtCajas);
                    }
                }
            }
            return dtCajas;
        }
    }
}
