using Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Seguridad
{
    public class GrupoDA
    {
        private Conexion conexion;

        public GrupoDA()
        {
            conexion = new Conexion();
        }

        // Obtener modulo permitidos
        public List<Modulo> ObtenerModulosPermitidos(int GrupoID)
        {
            List<Modulo> listaModulos = new List<Modulo>();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerModulosPermitidos", oContexto))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@GrupoID", GrupoID);
                        oContexto.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Modulo modulo = new Modulo();
                                if (!reader.IsDBNull(reader.GetOrdinal("Nombre")))
                                {
                                    modulo.Nombre = reader["Nombre"].ToString();
                                }
                                modulo.ListaAcciones = ObtenerAccionesPermitidas(GrupoID, modulo.Nombre);
                                listaModulos.Add(modulo);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Registro del error
                    throw new Exception("Error al obtener los modulos permitidos: " + ex.Message);
                }
            }
            return listaModulos;
        }

        public List<Accion> ObtenerAccionesPermitidas(int GrupoID, string moduloDescripcion)
        {
            List<Accion> listaAcciones = new List<Accion>();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerAccionesPermitidasPorModulo", oContexto))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@GrupoID", GrupoID);
                        cmd.Parameters.AddWithValue("@moduloDescripcion", moduloDescripcion);
                        oContexto.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Accion accion = new Accion();
                                accion.AccionID = Convert.ToInt32(reader["AccionID"]);
                                if (!reader.IsDBNull(reader.GetOrdinal("Nombre")))
                                {
                                    accion.Nombre = reader["Nombre"].ToString();
                                }
                                listaAcciones.Add(accion);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Registro del error
                    throw new Exception("Error al obtener las acciones permitidas: " + ex.Message);
                }
            }
            return listaAcciones;
        }

    }
}
