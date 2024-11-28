using Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Seguridad
{
    public class PermisoDA
    {
        private Conexion conexion;

        public PermisoDA()
        {
            conexion = new Conexion();
        }

        public bool AgregarPermisos(List<Permiso> listaPermisos)
        {
            if (listaPermisos != null && listaPermisos.Count > 0)
            {
                bool resultado = true;
                foreach (var permiso in listaPermisos)
                {
                    resultado &= AltaPermisoBD(permiso);
                }
                return resultado;
            }
            else
            {
                throw new ArgumentNullException("Se ha producido un error: la lista de permisos no puede ser nula o vacía, contacte con el administrador si este error persiste.");
            }
        }


        // ALTA PERMISO
        public bool AltaPermisoBD(Permiso permiso)
        {
            bool alta = false;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("INSERT INTO Permiso (GrupoID, AccionID, Permitido) VALUES (@GrupoID, @AccionID, @Permitido)");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@GrupoID", permiso.oGrupo.GrupoID);
                        cmd.Parameters.AddWithValue("@AccionID", permiso.oAccion.AccionID);
                        cmd.Parameters.AddWithValue("@Permitido", permiso.Estado);
                        oContexto.Open();
                        alta = cmd.ExecuteNonQuery() > 0;
                    }

                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar dar de alta el permiso.");
                }
                return alta;
            }
        }

        public List<Permiso> ObtenerListaPermisosActivosBD(int grupoID)
        {
            List<Permiso> _permisosActivos = new List<Permiso>();
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT P.PermisoID, P.GrupoID, P.AccionID, P.Permitido, A.ModuloID FROM Permiso P");
                query.AppendLine("JOIN Accion A ON P.AccionID = A.AccionID");
                query.AppendLine("WHERE P.GrupoID = @grupoID");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@grupoID", grupoID);
                        oContexto.Open();

                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Permiso permiso = new Permiso();
                                Grupo _grupo = new Grupo();
                                _grupo.GrupoID = Convert.ToInt32(reader["GrupoID"]);
                                permiso.oGrupo = _grupo;
                                Accion _accion = new Accion();
                                _accion.AccionID = Convert.ToInt32(reader["AccionID"]);
                                permiso.oAccion = _accion;
                                permiso.Estado = Convert.ToBoolean(reader["Permitido"]);
                                Modulo _modulo = new Modulo();
                                _modulo.ModuloID = Convert.ToInt32(reader["ModuloID"]);
                                permiso.oModulo = _modulo;
                                _permisosActivos.Add(permiso);
                            }
                        }
                    }
                    
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar obtener la lista de permisos activos.");
                }
            }
            return _permisosActivos;
        }

        public bool DesactivarPermisos(int grupoID)
        {
            bool desactivado = false;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("DELETE FROM Permiso");
                query.AppendLine("WHERE GrupoID = @GrupoID");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@GrupoID", grupoID);
                        oContexto.Open();
                        desactivado = cmd.ExecuteNonQuery() > 0;
                    }
                    
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar desactivar los permisos del grupo.");
                }
            }
            return desactivado;
        }

        public bool GrupoTienePermisosBD(int grupoID)
        {
            bool tienePermisos = false;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT COUNT(*) AS Permisos");
                query.AppendLine("FROM Permiso");
                query.AppendLine("WHERE GrupoID = @GrupoID");
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@GrupoID", grupoID);
                        oContexto.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        tienePermisos = count > 0;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar comprobar si el grupo tiene permisos asignados.");
                }
            }
            return tienePermisos;
        }

        public bool EstadoPermiso(Permiso _permiso)
        {
            bool estado = false;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT COUNT(*) AS Permitido");
                query.AppendLine("FROM Permiso");
                query.AppendLine("WHERE GrupoID = @grupoID AND AccionID = @accionID");
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@grupoID", _permiso.oGrupo.GrupoID);
                        cmd.Parameters.AddWithValue("@accionID", _permiso.oAccion.AccionID);
                        oContexto.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        estado = count > 0;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar obtener el estado del permiso.");
                }
            }
            return estado;
        }

    }
}
