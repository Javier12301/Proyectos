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
    public class AuditoriaDA
    {
        private Conexion conexion;

        public AuditoriaDA()
        {
            conexion = new Conexion();
        }

        public void RegistrarMovimiento(string movimiento, string nombreUsuario, string modulo, string descripcion)
        {
            // Registramos la auditoria
            Auditoria auditoria = new Auditoria(movimiento, nombreUsuario, modulo, descripcion);
            bool registroRealizado = RegistrarAuditoria(auditoria.Movimiento, auditoria.Modulo, auditoria.Usuario, auditoria.Descripcion);
            if (!registroRealizado)
            {
                throw new Exception("Ocurrió un error al registrar el movimiento, por favor contacte al administrador para solucionar este error.");
            }
        }

        public bool RegistrarAuditoria(string movimiento, string modulo, string nombreUsuario, string descripcion)
        {
            bool registroRealizado = false; // Inicializamos como false
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("INSERT INTO Auditoria (Movimiento, Modulo, NombreUsuario, Descripcion)");
                    query.AppendLine("VALUES (@Movimiento, @Modulo, @NombreUsuario, @Descripcion)");

                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@Movimiento", movimiento);
                        cmd.Parameters.AddWithValue("@Modulo", modulo);
                        cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                        cmd.Parameters.AddWithValue("@Descripcion", descripcion);

                        oContexto.Open();
                        // filas afectadas
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        // Verificamos si se afectaron filas
                        registroRealizado = filasAfectadas > 0; // Actualizamos el estado de registro
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar la auditoría: " + ex.Message);
                }
            }
            return registroRealizado; // Devolvemos el estado del registro
        }




    }
}
