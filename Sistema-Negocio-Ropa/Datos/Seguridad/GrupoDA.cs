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

        // Obtener grupo por Nombre
        public Grupo ObtenerGrupoPorNombre(string nombre)
        {
            Grupo oGrupo = new Grupo();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                // Usar COLLATE para hacer la comparación sensible a mayúsculas
                query.AppendLine("SELECT * FROM Grupo WHERE Nombre = @Nombre COLLATE SQL_Latin1_General_CP1_CS_AS");

                using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                {
                    try
                    {
                        oContexto.Open();
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                oGrupo.GrupoID = Convert.ToInt32(reader["GrupoID"]);
                                oGrupo.Nombre = reader["Nombre"].ToString();
                                oGrupo.Estado = Convert.ToBoolean(reader["Estado"]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al obtener el grupo por nombre: " + ex.Message);
                    }
                }
            }
            return oGrupo;
        }

        // Obtener grupo por ID
        public Grupo ObtenerGrupoPorID(int GrupoID)
        {
            Grupo oGrupo = new Grupo();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM Grupo WHERE GrupoID = @GrupoID");

                using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                {
                    try
                    {
                        oContexto.Open();
                        cmd.Parameters.AddWithValue("@GrupoID", GrupoID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                oGrupo.GrupoID = Convert.ToInt32(reader["GrupoID"]);
                                oGrupo.Nombre = reader["Nombre"].ToString();
                                oGrupo.Estado = Convert.ToBoolean(reader["Estado"]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al obtener el grupo por ID: " + ex.Message);
                    }
                }
            }
            return oGrupo;
        }


        public List<Grupo> ListarGrupos()
        {
            List<Grupo> oLista = new List<Grupo>();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT * FROM Grupo");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Grupo grupo = new Grupo();
                                grupo.GrupoID = Convert.ToInt32(reader["GrupoID"]);
                                grupo.Nombre = reader["Nombre"].ToString();
                                grupo.Estado = Convert.ToBoolean(reader["Estado"]);
                                oLista.Add(grupo);
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    throw new Exception("Error al listar los grupos, contactar con el administrador del sistema.");
                }
            }
            return oLista;
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
