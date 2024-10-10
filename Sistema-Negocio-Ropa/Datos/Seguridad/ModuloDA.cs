using Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Seguridad
{
    public class ModuloDA
    {
        private Conexion conexion;

        public ModuloDA()
        {
            conexion = new Conexion();
        }

        public List<Modulo> ObtenerModulosConAcciones()
        {
            List<Modulo> modulos = ObtenerModulosDisponiblesD();
            foreach (var modulo in modulos)
            {
                modulo.ListaAcciones = ObtenerAccionesDeModuloD(modulo.Nombre);
            }
            return modulos;
        }

        public List<Modulo> ObtenerModulosDisponiblesD()
        {
            List<Modulo> modulos = new List<Modulo>();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    // Se debe también tener en cuenta ModuloID
                    query.AppendLine("SELECT * FROM Modulo");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        oContexto.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Modulo modulo = new Modulo();
                                modulo.ModuloID = Convert.ToInt32(reader["ModuloID"]);
                                modulo.Nombre = reader["Nombre"].ToString();
                                modulos.Add(modulo);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    return modulos;
                }
            }
            return modulos;
        }

        public List<Accion> ObtenerAccionesDeModuloD(string moduloDescripcion)
        {
            List<Accion> acciones = new List<Accion>();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT op.* FROM Accion op");
                    query.AppendLine("INNER JOIN Modulo m ON m.ModuloID = op.ModuloID");
                    query.AppendLine("WHERE m.Nombre = @moduloDescripcion");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@moduloDescripcion", moduloDescripcion);
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Accion accion = new Accion();
                                accion.AccionID = Convert.ToInt32(reader["AccionID"]);
                                accion.Nombre = reader["Nombre"].ToString();
                                acciones.Add(accion);
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    throw new Exception("Ocurrió un error al obtener las acciones disponibles del módulo, si este error persiste contacte con el administrador del sistema.");
                }
            }
            return acciones;
        }


        public Modulo ObtenerModulo(string Nombre)
        {
            Modulo _modulo = new Modulo();
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM Modulo");
                query.AppendLine("WHERE Nombre = @nombre");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@nombre", Nombre);
                        oContexto.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _modulo.ModuloID = Convert.ToInt32(reader["ModuloID"]);
                                _modulo.Nombre = reader["Nombre"].ToString();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Ocurrió un error al intentar obtener el módulo. por favor, inténtelo nuevamente.");
                }

            }
            return _modulo;
        }

        // modul por id
        public Modulo ObtenerModuloID(int moduloID)
        {
            Modulo _modulo = new Modulo();
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM Modulo");
                query.AppendLine("WHERE ModuloID = @moduloID");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@moduloID", moduloID);
                        oContexto.Open();

                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _modulo.ModuloID = Convert.ToInt32(reader["ModuloID"]);
                                _modulo.Nombre = reader["Nombre"].ToString();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Ocurrió un error al intentar obtener el módulo. por favor, inténtelo nuevamente.");
                }
            }
            return _modulo;
        }
    }
}
