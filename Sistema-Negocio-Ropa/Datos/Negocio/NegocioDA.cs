using Negocio.Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Negocio
{
    public class NegocioDA
    {
        private Conexion conexion;

        public NegocioDA()
        {
            conexion = new Conexion();
        }

        // obtener metodo pago segun id
        public MetodoPago ObtenerMetodoPago(int id)
        {
            MetodoPago metodoPago = new MetodoPago();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT * FROM MetodoPago WHERE MetodoPagoID = @MetodoPagoID");
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@MetodoPagoID", id);
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                metodoPago.MetodoPagoID = Convert.ToInt32(reader["MetodoPagoID"]);
                                metodoPago.Nombre = reader["Nombre"].ToString();
                                metodoPago.Recargo = Convert.ToDecimal(reader["Recargo"]);
                            }
                        }
                    }
                }catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return metodoPago;
        }


        public NegocioM CargarDatosNegocio()
        {
            NegocioM negocio = null;
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT * FROM Negocio");
                try
                {
                    SqlCommand cmd = new SqlCommand(query.ToString(), oContexto);
                    oContexto.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            negocio = new NegocioM();
                            negocio.NegocioID = Convert.ToInt32(reader["NegocioID"]);
                            negocio.Nombre = reader["Nombre"].ToString();
                            negocio.Direccion = reader["Direccion"].ToString();
                            negocio.Ciudad = reader["Ciudad"].ToString();
                            negocio.CodigoPostal = reader["CodigoPostal"].ToString();
                            negocio.NombreCompletoPropietario = reader["NombreCompletoPropietario"].ToString();
                            negocio.Telefono = reader["Telefono"].ToString();
                            negocio.TipoDocumento = reader["TipoDocumento"].ToString();
                            negocio.Documento = reader["Documento"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return negocio;
        }


        public bool ActualizarDatosNegocio(NegocioM negocio)
        {
            bool actualizado = false;
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.Append("UPDATE Negocio SET Nombre = @Nombre, Direccion = @Direccion, Ciudad = @Ciudad, CodigoPostal = @CodigoPostal, NombreCompletoPropietario = @NombreCompletoPropietario, Telefono = @Telefono, TipoDocumento = @TipoDocumento, Documento = @Documento WHERE NegocioID = 1");
                try
                {
                    SqlCommand cmd = new SqlCommand(query.ToString(), oContexto);
                    cmd.Parameters.AddWithValue("@Nombre", negocio.Nombre);
                    cmd.Parameters.AddWithValue("@Direccion", negocio.Direccion);
                    cmd.Parameters.AddWithValue("@Ciudad", negocio.Ciudad);
                    cmd.Parameters.AddWithValue("@CodigoPostal", negocio.CodigoPostal);
                    cmd.Parameters.AddWithValue("@NombreCompletoPropietario", negocio.NombreCompletoPropietario);
                    cmd.Parameters.AddWithValue("@Telefono", negocio.Telefono);
                    cmd.Parameters.AddWithValue("@TipoDocumento", negocio.TipoDocumento);
                    cmd.Parameters.AddWithValue("@Documento", negocio.Documento);

                    oContexto.Open();
                    actualizado = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return actualizado;
        }



        public bool ActualizarMetodosPago(MetodoPago metodopago)
        {
            bool actualizado = false;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.Append("UPDATE MetodoPago SET Recargo = @Recargo WHERE MetodoPagoID = @MetodoPagoID");
                try
                {

                    SqlCommand cmd = new SqlCommand(query.ToString(), oContexto);
                    cmd.Parameters.AddWithValue("@Recargo", metodopago.Recargo);
                    cmd.Parameters.AddWithValue("@MetodoPagoID", metodopago.MetodoPagoID);

                    oContexto.Open();
                    actualizado = cmd.ExecuteNonQuery() > 0;


                }catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return actualizado;

        }

        public List<MetodoPago> ObtenerMetodosPagos()
        {
            List<MetodoPago> lstMetodosPagos = new List<MetodoPago>();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT * FROM MetodoPago");
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MetodoPago metodoPago = new MetodoPago();
                                metodoPago.MetodoPagoID = Convert.ToInt32(reader["MetodoPagoID"]);
                                metodoPago.Nombre = reader["Nombre"].ToString();
                                metodoPago.Recargo = Convert.ToDecimal(reader["Recargo"]);
                                lstMetodosPagos.Add(metodoPago);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
                return lstMetodosPagos;
        }



    }
}
