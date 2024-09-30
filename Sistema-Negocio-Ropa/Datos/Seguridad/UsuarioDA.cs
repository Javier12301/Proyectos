using Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Seguridad
{
    public class UsuarioDA
    {
        private Conexion conexion;

        public UsuarioDA()
        {
            conexion = new Conexion();
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
                    }
                }catch(Exception)
                {
                    throw new Exception("Se ha producido un error al intentar modificar el usuario. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.");
                }
            }
            return modificado;
        }

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
                        cmd.Parameters.AddWithValue("@usuarioID", usuarioID);
                        oContexto.Open();
                        baja = cmd.ExecuteNonQuery() > 0;
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
                using(SqlCommand cmd = new SqlCommand("sp_ObtenerUsuarioPorNombre", oContexto))
                {
                   
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    try
                    {
                        oContexto.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usuario = new Usuario();
                                usuario.UsuarioID = Convert.ToInt32(reader["UsuarioID"]);
                                usuario.NombreUsuario = reader["Usuario"].ToString();
                                usuario.Password = reader["Contrasena"].ToString();
                                usuario.Nombre = reader["Nombre"].ToString();
                                usuario.Apellido = reader["Apellido"].ToString();
                                usuario.Correo = reader["Correo"].ToString();
                                usuario.Estado = Convert.ToBoolean(reader["Estado"]);

                                Grupo grupo = new Grupo();
                                grupo.GrupoID = Convert.ToInt32(reader["GrupoID"]);
                                grupo.Nombre = reader["Gr_Nombre"].ToString();
                                grupo.Estado = Convert.ToBoolean(reader["Gr_Estado"]);
                                usuario.oGrupo = grupo;
                            }
                        }

                    }catch(Exception ex)
                    {
                        throw new Exception("Error al obtener el usuario: " + ex.Message);
                    }
                }
            }
            return usuario;
        }

        // Obtener usuario por ID
        public Usuario ObtenerUsuarioPorIDD(int usuarioID)
        {
            Usuario oUsuario = new Usuario();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT U.UsuarioID, U.NombreUsuario, U.Contraseña AS Password, U.Nombre AS NombreU, U.Apellido, U.Email, U.DNI, U.GrupoID, U.Estado AS EstadoU,");
                    query.AppendLine("G.GrupoID, G.Nombre AS NombreG, G.Estado AS EstadoG");
                    query.AppendLine("FROM Usuario U");
                    query.AppendLine("INNER JOIN Grupo G ON U.GrupoID = G.GrupoID");
                    query.AppendLine("WHERE U.UsuarioID = @usuarioID");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@usuarioID", usuarioID);
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oUsuario.UsuarioID = Convert.ToInt32(reader["UsuarioID"]);
                                oUsuario.NombreUsuario = reader["NombreUsuario"].ToString();
                                oUsuario.Password = reader["Password"].ToString();  // Actualizado para coincidir con el modelo
                                oUsuario.Nombre = reader["NombreU"].ToString();
                                oUsuario.Apellido = reader["Apellido"].ToString();
                                oUsuario.Correo = reader["Email"].ToString();
                                oUsuario.Estado = Convert.ToBoolean(reader["EstadoU"]);

                                Grupo grupo = new Grupo();
                                grupo.GrupoID = Convert.ToInt32(reader["GrupoID"]);
                                grupo.Nombre = reader["NombreG"].ToString();
                                grupo.Estado = Convert.ToBoolean(reader["EstadoG"]);

                                oUsuario.oGrupo = grupo;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Ocurrió un error al obtener el usuario, contactar con el administrador del sistema.");
                }
            }
            return oUsuario;
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
