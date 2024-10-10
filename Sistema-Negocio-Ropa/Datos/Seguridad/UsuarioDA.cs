using Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Seguridad
{
    public class UsuarioDA
    {
        private Conexion conexion;
        private AuditoriaDA auditoriaDA;

        public UsuarioDA()
        {
            conexion = new Conexion();
            auditoriaDA = new AuditoriaDA();
        }

        // Comprobar si existe un usuario en el sistema
        public bool ExisteNombreUsuarioD(string nombreUsuario)
        {
            bool existe = false;

            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    // Query sensible a mayúsculas y minúsculas
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT COUNT(*) FROM Usuario WHERE Usuario COLLATE SQL_Latin1_General_CP1_CS_AS = @nombreUsuario");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        // Abrimos la conexión
                        cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                        oContexto.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        // si retorna mayor a 0 es porque existe
                        existe = count > 0;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Error al verificar si existe el usuario, contactar con el administrador del sistema.");
                }
            }

            return existe;
        }

        public bool ExisteCorreo(string correo)
        {
            bool existe = false;
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT COUNT(*) FROM Usuario WHERE Correo COLLATE SQL_Latin1_General_CP1_CS_AS = @Correo");
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        oContexto.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        existe = count > 0;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Error al verificar si existe el correo, contactar con el administrador del sistema.");
                }
            }
            return existe;
        }



        // Alta de Usuario
        public bool AltaUsuario(Usuario usuario)
        {
            bool alta = false;
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("INSERT INTO Usuario (Usuario, Contrasena, Nombre, Apellido, Correo, GrupoID, Estado)");
                    query.AppendLine("VALUES (@usuario, @contrasena, @nombre, @apellido, @correo, @grupoID, @estado)");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario.NombreUsuario);
                        cmd.Parameters.AddWithValue("@contrasena", usuario.Password);
                        cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@apellido", usuario.Apellido);
                        cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                        cmd.Parameters.AddWithValue("@grupoID", usuario.oGrupo.GrupoID);
                        cmd.Parameters.AddWithValue("@estado", usuario.Estado);
                        oContexto.Open();
                        alta = cmd.ExecuteNonQuery() > 0;
                        if (alta)
                            auditoriaDA.RegistrarMovimiento("Alta", Sesion.ObtenerInstancia.UsuarioEnSesion().ObtenerNombreUsuario(), "Usuario", $"Se ha dado de alta un nuevo usuario: {usuario.NombreUsuario}.");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar registrar el nuevo usuario. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.");
                }
            }
            return alta;
        }

        // Modificar Usuario
        public bool ModificarUsuario(Usuario usuario)
        {
            bool modificado = false;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE Usuario SET Usuario = @usuario, Contrasena = @contrasena, Nombre = @nombre, Apellido = @apellido, Correo = @correo, GrupoID = @grupoID, Estado = @estado");
                    query.AppendLine("WHERE UsuarioID = @usuarioID");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario.NombreUsuario);
                        cmd.Parameters.AddWithValue("@contrasena", usuario.Password);
                        cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@apellido", usuario.Apellido);
                        cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                        cmd.Parameters.AddWithValue("@grupoID", usuario.oGrupo.GrupoID);
                        cmd.Parameters.AddWithValue("@estado", usuario.Estado);
                        cmd.Parameters.AddWithValue("@usuarioID", usuario.UsuarioID);
                        oContexto.Open();
                        modificado = cmd.ExecuteNonQuery() > 0;
                        if (modificado)
                            auditoriaDA.RegistrarMovimiento("Modificación", Sesion.ObtenerInstancia.UsuarioEnSesion().ObtenerNombreUsuario(), "Usuario", $"Se ha modificado al usuario {usuario.NombreUsuario}");
                    }
                }catch(Exception)
                {
                    throw new Exception("Se ha producido un error al intentar modificar el usuario. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.");
                }
            }
            return modificado;
        }

        // DISEÑAR CONTROL DE REFERENCIAS

        // Baja de Usuario
        public bool BajaUsuario(int usuarioID)
        {
            bool baja = false;
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("DELETE FROM Usuario");
                    query.AppendLine("WHERE UsuarioID = @usuarioID");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        string _nombreUsuario = ObtenerUsuarioPorID(usuarioID).NombreUsuario;
                        cmd.Parameters.AddWithValue("@usuarioID", usuarioID);
                        oContexto.Open();
                        baja = cmd.ExecuteNonQuery() > 0;
                        if (baja)
                            auditoriaDA.RegistrarMovimiento("Baja", Sesion.ObtenerInstancia.UsuarioEnSesion().ObtenerNombreUsuario(), "Usuario", $"Se ha dado de baja al usuario: {_nombreUsuario}");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar eliminar el usuario. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.");
                }
            }
            return baja;
        }


        // Query para verificar si el usuario ingresado tiene el email ingresado
        public bool ExisteUsuarioConCorreo(string nombreUsuario, string correo)
        {
            bool existe = false;
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT COUNT(*) FROM Usuario");
                    query.AppendLine(" WHERE Usuario COLLATE SQL_Latin1_General_CP1_CS_AS = @usuario"); // Añadido espacio antes de WHERE
                    query.AppendLine(" AND Correo COLLATE SQL_Latin1_General_CP1_CS_AS = @correo"); // Añadido espacio antes de AND

                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@usuario", nombreUsuario);
                        cmd.Parameters.AddWithValue("@correo", correo);
                        oContexto.Open();
                        int count = (int)cmd.ExecuteScalar();
                        existe = count > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return existe;
        }


        public Usuario ObtenerUsuarioPorNombre(string nombreUsuario)
        {
            Usuario usuario = null;

            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerUsuarioPorNombre", oContexto))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                    try
                    {
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Cambiar a if para solo obtener un usuario
                            {
                                usuario = new Usuario
                                {
                                    UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                                    NombreUsuario = reader["Usuario"].ToString(),
                                    Password = reader["Contrasena"].ToString(),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    Correo = reader["Correo"].ToString(),
                                    Estado = Convert.ToBoolean(reader["Estado"]),
                                    oGrupo = new Grupo
                                    {
                                        GrupoID = Convert.ToInt32(reader["GrupoID"]),
                                        Nombre = reader["Gr_Nombre"].ToString(),
                                        Estado = Convert.ToBoolean(reader["Gr_Estado"])
                                    }
                                };
                            }
                            else
                            {
                                // Si no se encuentra el usuario, puedes lanzar una excepción o devolver null
                                throw new Exception("Usuario no encontrado.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al obtener el usuario: " + ex.Message);
                    }
                }
            }
            return usuario;
        }


        // Obtener usuario por ID
        public Usuario ObtenerUsuarioPorID(int usuarioID)
        {
            Usuario usuario = new Usuario();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerUsuarioPorID", oContexto))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuarioID", usuarioID);

                    try
                    {
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Cambiar a if para que solo obtenga un usuario
                            {
                                usuario.UsuarioID = Convert.ToInt32(reader["UsuarioID"]);
                                usuario.NombreUsuario = reader["Usuario"].ToString();
                                usuario.Password = reader["Contrasena"].ToString();
                                usuario.Nombre = reader["Nombre"].ToString();
                                usuario.Apellido = reader["Apellido"].ToString();
                                usuario.Correo = reader["Correo"].ToString();
                                usuario.Estado = Convert.ToBoolean(reader["Estado"]);

                                // Asignar grupo si existe
                                Grupo grupo = new Grupo
                                {
                                    GrupoID = Convert.ToInt32(reader["GrupoID"]),
                                    Nombre = reader["Gr_Nombre"].ToString(),
                                    Estado = Convert.ToBoolean(reader["Gr_Estado"])
                                };
                                usuario.oGrupo = grupo;
                            }
                            else
                            {
                                // Si no se encuentra el usuario, puedes lanzar una excepción o devolver null
                                throw new Exception("Usuario no encontrado.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al obtener el usuario: " + ex.Message);
                    }
                }
            }
            return usuario;
        }


        // Filtro dinamico para el datagridview de modulo usuario
        public DataTable ObtenerUsuarioFiltrados(string filtroBuscar, string buscar, string filtroGrupo, string filtroEstado)
        {
            // Asignar valores predeterminados si los filtros están vacíos o nulos
            filtroBuscar = string.IsNullOrWhiteSpace(filtroBuscar) ? "Todos" : filtroBuscar;
            filtroGrupo = string.IsNullOrWhiteSpace(filtroGrupo) ? "Todos" : filtroGrupo;
            filtroEstado = string.IsNullOrWhiteSpace(filtroEstado) ? "Todos" : filtroEstado;

            DataTable dt = new DataTable();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerUsuariosConFiltros", oContexto))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // parámetros
                    cmd.Parameters.AddWithValue("@FiltroBuscar", filtroBuscar);
                    cmd.Parameters.AddWithValue("@Buscar", string.IsNullOrEmpty(buscar) ? "" : buscar);
                    cmd.Parameters.AddWithValue("@FiltroGrupo", filtroGrupo);
                    cmd.Parameters.AddWithValue("@FiltroEstado", filtroEstado);

                    try
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    catch (Exception)
                    {
                        // Si ocurre una excepción, lo ideal sería lanzar un error, pero para que no se rompa la tabla, enviar datos vacíos
                        throw new Exception("Ocurrió un error al cargar la grilla de usuarios, contactar con el administrador del sistema.");
                    }
                }
            }
            return dt;
        }

        // ejemplo para conectarse

        /* public bool AltaDepartamento(DepartamentoBLL oDepartamento)
        {
            bool alta = false;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("INSERT INTO Departamento (departamento)");
                    query.AppendLine("VALUES (@departamento)");
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@departamento", oDepartamento.Departamento);
                        oContexto.Open();
                        // comando para verificar si se insertó correctamente
                        alta = cmd.ExecuteNonQuery() > 0;
                    }

                }catch(Exception ex)
                {
                    throw new Exception("Ocurrió un error al intentar dar de alta el departamento", ex);
                }
            }
            return alta;
        }*/
    }
}
