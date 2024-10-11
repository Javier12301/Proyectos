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

        // CANCELAR
        public bool CancelarCompra(int compraID)
        {
            bool resultado = false;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("UPDATE Compra SET Estado = @Estado WHERE CompraID = @CompraID");
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@CompraID", compraID);
                        cmd.Parameters.AddWithValue("@Estado", false);
                        oContexto.Open();
                        resultado = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Error al cancelar la compra, contactar con el administrador del sistema si el error persiste.");
                }
            }
            return resultado;
        }

        // Listar Compras
        public DataTable ObtenerComprasFiltradas(string estado, DateTime FechaInicio, DateTime FechaFin)
        {
            // primero, creamos valores default
            estado = estado == "" ? "Todos" : estado;
            // SI FECHA INICIO ES NULL Y FIN HACER QUE INICIO SEA EN -5 AÑOS Y FIN HOY
            FechaInicio = FechaInicio == null ? DateTime.Now.AddYears(-5) : FechaInicio;
            FechaFin = FechaFin == null ? DateTime.Now.AddYears(5) : FechaFin;


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

        // obtener compra por id
        public Compra ObtenerCompraID(int compraID)
        {
            Compra oCompra = new Compra();
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM Compra WHERE CompraID = @CompraID");
                // nos retornara CompraID, UsuarioID, Factura, FechaCompra, Estado
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@CompraID", compraID);
                        oContexto.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Usuario _usuario = new Usuario();
                                oCompra.CompraID = Convert.ToInt32(reader["CompraID"]);
                                _usuario.UsuarioID = Convert.ToInt32(reader["UsuarioID"]);
                                oCompra.oUsuario = _usuario;
                                oCompra.Tipo_Factura = reader["Factura"].ToString();
                                oCompra.FechaCompra = Convert.ToDateTime(reader["FechaCompra"]);
                                oCompra.Estado = Convert.ToBoolean(reader["Estado"]);
                            }
                        }
                    }

                }catch(Exception)
                {
                    throw new Exception("Ocurrió un error al obtener la compra, contactar con el administrador del sistema si el error persiste.");
                }
            }
            return oCompra;
        }



        // Listar detalle compras
        public DataTable ObtenerDetalleCompras(int compraID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerDetalleCompra", oContexto))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CompraID", compraID);

                    try
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Ocurrió un error al cargar el detalle de la compra, contactar con el administrador del sistema si el error persiste.");
                    }
                }
            }
            return dt;
        }

        // Registrar Compra
        public bool RegistrarCompra(Compra oCompra, DataTable DetalleCompra)
        {
            bool resultado = false;
            if(oCompra != null && DetalleCompra != null)
            {
                using(SqlConnection oContexto = conexion.EstablecerConexion())
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("INSERT INTO Compra (UsuarioID, Factura, Estado)");
                    query.AppendLine("OUTPUT INSERTED.CompraID");
                    query.AppendLine("VALUES (@UsuarioID, @Factura, @Estado);");

                    try
                    {
                        using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                        {
                            cmd.Parameters.AddWithValue("@UsuarioID", oCompra.oUsuario.UsuarioID);
                            cmd.Parameters.AddWithValue("@Factura", oCompra.Tipo_Factura);
                            cmd.Parameters.AddWithValue("@Estado", true);
                            oContexto.Open();

                            oCompra.CompraID = Convert.ToInt32(cmd.ExecuteScalar());

                            // Insertar detalle de compra
                            foreach(DataRow fila in DetalleCompra.Rows)
                            {
                                query.Clear();
                                query.AppendLine("INSERT INTO Detalle_Compra (CompraID, ProductoID, PrecioCompra, Cantidad, SubTotal)");
                                query.AppendLine("VALUES (@CompraID, @ProductoID, @PrecioCompra, @Cantidad, @SubTotal);");

                                using(SqlCommand cmdDetalle = new SqlCommand(query.ToString(), oContexto))
                                {
                                    cmdDetalle.Parameters.AddWithValue("@CompraID", oCompra.CompraID);
                                    cmdDetalle.Parameters.AddWithValue("@ProductoID", Convert.ToInt32(fila["dgvcID"]));
                                    cmdDetalle.Parameters.AddWithValue("@PrecioCompra", Convert.ToDecimal(fila["dgvcPrecioCompra"]));
                                    cmdDetalle.Parameters.AddWithValue("@Cantidad", Convert.ToInt32(fila["dgvcCantidad"]));
                                    cmdDetalle.Parameters.AddWithValue("@SubTotal", Convert.ToDecimal(fila["dgvcSubTotal"]));
                                    resultado = cmdDetalle.ExecuteNonQuery() > 0;
                                }
                            }
                        }
                        
                    }catch(Exception ex)
                    {
                        throw new Exception("Ocurrió un error al registrar la compra, contacte con el administrador del sistema si el error persiste.");
                    }
                }
            }
            return resultado;
        }
    }
}
