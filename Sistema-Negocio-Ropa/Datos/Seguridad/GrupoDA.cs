using Negocio.Negocio;
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

        // VERIFICAR SI GRUPO TIENE USUARIOS
        public bool GrupoTieneUsuarios(int grupoID)
        {
            if(grupoID > 0)
            {
                bool tieneUsuarios = false;
                using(SqlConnection oContexto = conexion.EstablecerConexion())
                {
                    try
                    {
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("SELECT COUNT(U.UsuarioID) AS Cantidad");
                        query.AppendLine("FROM Grupo G");
                        query.AppendLine("LEFT JOIN Usuario U ON G.GrupoID = U.GrupoID");
                        query.AppendLine("WHERE G.GrupoID = @grupoID");
                        using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                        {
                            cmd.Parameters.AddWithValue("@grupoID", grupoID);
                            oContexto.Open();
                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            tieneUsuarios = count > 0;
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("Error al comprobar si el grupo tiene usuarios, contactar con el administrador del sistema.");
                    }
                }
                return tieneUsuarios;
            }
            else
            {
                throw new Exception("Ocurrió un error al intentar verificar si el grupo tiene usuarios, contacte con el administrador del sistema si este error persiste.");
            }
        }

        // Alta Grupo
        public bool AltaGrupo(Grupo oGrupo)
        {
            bool alta = false;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("INSERT INTO Grupo (Nombre, Estado) VALUES (@Nombre, @Estado)");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", oGrupo.Nombre);
                        cmd.Parameters.AddWithValue("@Estado", oGrupo.Estado);
                        oContexto.Open();
                        alta = cmd.ExecuteNonQuery() > 0;
                       
                    }

                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar dar de alta el grupo, contactar con el administrador del sistema.");
                }
            }
            return alta;
        }

        // Modificar Grupo
        public bool ModificarGrupo(Grupo oGrupo)
        {
            bool modificado = false;
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("UPDATE Grupo SET Nombre = @Nombre, Estado = @Estado WHERE GrupoID = @GrupoID");
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", oGrupo.Nombre);
                        cmd.Parameters.AddWithValue("@Estado", oGrupo.Estado);
                        cmd.Parameters.AddWithValue("@GrupoID", oGrupo.GrupoID);
                        oContexto.Open();
                        modificado = cmd.ExecuteNonQuery() > 0;
                        
                    }

                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar modificar el grupo, contactar con el administrador del sistema.");
                }
            }
            return modificado;
        }

        UsuarioDA UsuarioDA = new UsuarioDA();
        // Baja Grupo
        public bool BajaGrupo(Operacion operacion)
        {
            
            bool resultado = true;
            switch (operacion.NombreOperacion)
            {
                case "EliminarGrupo":
                    return EliminarGrupo(operacion);
                case "AsignarUsuariosSinGrupo":
                    resultado &= AsignarUsuarioSinGrupo(operacion);
                    resultado &= EliminarGrupo(operacion);
                    return resultado;
                case "EliminarGrupoYUsuarios":
                    List<int> listaUsuarioID = ListarUsuariosIDEnGrupoD(operacion.ID);
                    foreach (var usuarioID in listaUsuarioID)
                    {
                        resultado &= UsuarioDA.BajaUsuario(usuarioID);
                    }
                    resultado &= EliminarGrupo(operacion);
                    return resultado;

                default:
                    throw new Exception("Ocurrió un error al intentar eliminar el grupo, contacte con el administrador del sistema si este error persiste.");
            }
        }

        public List<int> ListarUsuariosIDEnGrupoD(int grupoID)
        {
            List<int> listaUsuarioID = new List<int>();
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT UsuarioID FROM Usuario");
                query.AppendLine("WHERE GrupoID = @GrupoID");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@GrupoID", grupoID);
                        oContexto.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int usuarioID;
                            while (reader.Read())
                            {
                                usuarioID = Convert.ToInt32(reader["UsuarioID"]);
                                listaUsuarioID.Add(usuarioID);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar obtener los usuarios del grupo, contactar con el administrador del sistema.");
                }
            }
            return listaUsuarioID;
        }

        private bool AsignarUsuarioSinGrupo(Operacion operacion)
        {
            bool resultado = true;
            List<int> listaUsuarioID = ListarUsuariosIDEnGrupoD(operacion.ID);
            foreach (var usuarioID in listaUsuarioID)
            {
                resultado &= AsignarUsuarioSinGrupoBD(usuarioID);
            }
            return resultado;
        }

        private bool AsignarUsuarioSinGrupoBD(int usuarioID)
        {
            bool asignado = false;
            using(var oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("UPDATE Usuario SET GrupoID = 0");
                query.AppendLine("WHERE UsuarioID = @UsuarioID");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                        oContexto.Open();
                        asignado = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar asignar el usuario a un grupo, contactar con el administrador del sistema.");
                }
            }
            return asignado;
        }

        PermisoDA PermisoDA = new PermisoDA();
        private bool EliminarGrupo(Operacion operacion)
        {
            int grupoID = operacion.ID;

            // Comprobar si el grupo tiene permisos asignados
            bool tienePermisos = PermisoDA.GrupoTienePermisosBD(grupoID);

            if (tienePermisos)
            {
                // Desactivar permisos antes de eliminar el grupo
                if(grupoID > 0)
                {
                    PermisoDA.DesactivarPermisos(grupoID);
                }
            }

            // Realizar la eliminación del grupo
            bool eliminacionExitosa = BajaGrupoBD(grupoID);

            return eliminacionExitosa;
        }


        public bool BajaGrupoBD(int grupoID)
        {
            bool baja = false;
            using(var oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("DELETE FROM Grupo WHERE GrupoID = @GrupoID");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        string _nombreGrupo = ObtenerGrupoPorID(grupoID).Nombre;
                        cmd.Parameters.AddWithValue("@GrupoID", grupoID);
                        oContexto.Open();
                        baja = cmd.ExecuteNonQuery() > 0;
                        
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar dar de baja el grupo, contactar con el administrador del sistema.");
                }
            }
            return baja;
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

        public DataTable ObtenerGrupoFiltrados(string filtroBuscar, string buscar, string filtroEstado)
        {       
            filtroBuscar = string.IsNullOrWhiteSpace(filtroBuscar) ? "Todos" : filtroBuscar;
            filtroEstado = string.IsNullOrWhiteSpace(filtroEstado) ? "Todos" : filtroEstado;

            DataTable dt = new DataTable();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerGruposConFiltros", oContexto))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // parámetros
                    cmd.Parameters.AddWithValue("@FiltroBuscar", filtroBuscar);
                    cmd.Parameters.AddWithValue("@Buscar", string.IsNullOrEmpty(buscar) ? "" : buscar);
                    cmd.Parameters.AddWithValue("@FiltroEstado", filtroEstado);

                    try
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    catch (Exception)
                    {
                        // Si ocurre una excepción, lo ideal sería lanzar un error, pero para que no se rompa la tabla, enviar datos vacíos
                        throw new Exception("Ocurrió un error al cargar la grilla de grupos, contactar con el administrador del sistema.");
                    }
                }
            }
            return dt;
        }

        // existe grupo
        public bool ExisteGrupo(string nombreGrupo)
        {
            bool existe = false;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT COUNT(*) FROM Grupo WHERE Nombre COLLATE SQL_Latin1_General_CP1_CS_AS = @nombreGrupo");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@nombreGrupo", nombreGrupo);
                        oContexto.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        existe = count > 0;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Error al verificar si existe el grupo, contactar con el administrador del sistema.");
                }
            }
            return existe;
        }


    }
}
