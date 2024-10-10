using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Negocio
{
    public class CompraDA
    {
        private Conexion conexion;

        public CompraDA()
        {
            conexion = new Conexion();
        }

        public int ObtenerFolio()
        {
            int folio = 1; // Inicializa el folio en 1
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.Append("SELECT MAX(CompraID) FROM Compra");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        oContexto.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value) // Si hay compras existentes
                        {
                            folio = Convert.ToInt32(result) + 1; // Incrementa el folio
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Ocurrió un error al obtener el folio de compra, contacte con el administrador del sistema si este error persiste.");
                }
            }
            return folio;
        }

        // Listar Compras
        public DataTable ObtenerComprasFiltradas(string estado, DateTime FechaInicio, DateTime FechaFin)
        {
            // primero, creamos valores default
            estado = estado == "" ? "Todos" : estado;
            // SI FECHA INICIO ES NULL Y FIN HACER QUE INICIO SEA EN -5 AÑOS Y FIN HOY
            FechaInicio = FechaInicio == null ? DateTime.Now.AddYears(-5) : FechaInicio;
            FechaFin = FechaFin == null ? DateTime.Now : FechaFin;


            DataTable dt = new DataTable();
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                using(SqlCommand cmd = new SqlCommand("sp_ObtenerComprasConTotales", oContexto))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FiltroEstado", estado);
                    cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", FechaFin);

                    try
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Ocurrió un error al cargar la grille de compras, contactar con el administrador del sistema si el error persiste.");
                    }
                }
            }
            return dt;
        }
    }
}
