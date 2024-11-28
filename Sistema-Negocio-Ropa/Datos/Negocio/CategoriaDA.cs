using Negocio.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Negocio
{
    public class CategoriaDA
    {
        private Conexion conexion;

        public CategoriaDA()
        {
            conexion = new Conexion();
        }

        public List<Categoria> ObtenerCategoriasConProductos()
        {
            List<Categoria> listaCategorias = new List<Categoria>();

            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("ObtenerCategoriasConProductos", oContexto))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        oContexto.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Verificar si hay filas en el lector
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Categoria oCategoria = new Categoria
                                    {
                                        CategoriaID = Convert.ToInt32(reader["CategoriaID"]),
                                        Nombre = reader["Nombre"].ToString()
                                    };
                                    listaCategorias.Add(oCategoria);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Proporcionar más información en la excepción
                    throw new Exception($"Ocurrió un error al intentar obtener las categorías con productos: {ex.Message}", ex);
                }
            }
            return listaCategorias;
        }


        // Filtro dinamico para datagridview de modulo categorias
        public DataTable ObtenerCategoriasFiltrados(string filtroEstado, string buscar)
        {
            // valores predterminados
            filtroEstado = string.IsNullOrEmpty(filtroEstado) ? "Todos" : filtroEstado;
            buscar = string.IsNullOrEmpty(buscar) ? "" : buscar;

            DataTable dt = new DataTable();
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                using(SqlCommand cmd = new SqlCommand("FiltrarCategorias", oContexto))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FiltroEstado", filtroEstado);
                    cmd.Parameters.AddWithValue("@Buscar", buscar);

                    try
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Ocurrió un error al cargar la grille de categorías, contactar con el administrador del sistema si el error persiste.");
                    }
                }
            }
            return dt;
        }


        public int ConteoCategoria()
        {
            int conteo = 0;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT COUNT(*) FROM Categoria");
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        oContexto.Open();
                        conteo = Convert.ToInt32(cmd.ExecuteScalar());
                        conteo--;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar obtener el conteo de categorias.");
                }
            }
            return conteo;
        }

        public int ConteoProductosEnCategoriaD(int categoriaID)
        {
            int conteo = 0;
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT COUNT(*) FROM Producto");
                    query.AppendLine("WHERE CategoriaID = @categoriaID");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@categoriaID", categoriaID);
                        oContexto.Open();
                        conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar obtener el conteo de productos en categoría, póngase en contacto con el administrador del sistema para solucionar el error.");
                }
            }
            return conteo;
        }

        // ALTA
        public bool AltaCategoria(Categoria oCategoria)
        {
            bool alta = false;
            using(var oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("INSERT INTO Categoria (Nombre, Estado)");
                query.AppendLine("VALUES (@nombre, @estado)");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@nombre", oCategoria.Nombre);
                        cmd.Parameters.AddWithValue("@estado", oCategoria.Estado);
                        oContexto.Open();
                        alta = cmd.ExecuteNonQuery() > 0;
                    }

                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar registrar la nueva categoría, póngase en contacto con el administrador del sistema para solucionar el error.");
                }
            }
                return alta;
        }

        // MODIFICAR
        public bool ModificarCategoria(Categoria oCategoria)
        {
            bool modificado = false;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("UPDATE Categoria");
                query.AppendLine("SET Nombre = @nombre, Estado = @estado");
                query.AppendLine("WHERE CategoriaID = @categoriaID");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@nombre", oCategoria.Nombre);
                        cmd.Parameters.AddWithValue("@estado", oCategoria.Estado);
                        cmd.Parameters.AddWithValue("@categoriaID", oCategoria.CategoriaID);
                        oContexto.Open();
                        modificado = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar modificar la categoría, póngase en contacto con el administrador del sistema para solucionar el error.");
                }
            }
            return modificado;
        }


        ProductoDA productoDA = new ProductoDA();
        public bool BajaCategoria(Operacion operacion)
        {
            bool resultado = true;
            switch (operacion.NombreOperacion)
            {
                case "EliminarCategoria":
                    return BajaCategoriaBD(operacion.ID);
                case "AsignarProductosSinCategoria":
                    resultado &= AsignarProductosSinCategoria(operacion);
                    resultado &= BajaCategoriaBD(operacion.ID);
                    return resultado;
                case "EliminarCategoriaYProductos":
                    List<int> listaProductosID = ListarProductosIDEnCategoria(operacion.ID);
                    foreach (var productoID in listaProductosID)
                    {
                        resultado &= productoDA.BajaProductoD(productoID);
                    }
                    resultado &= BajaCategoriaBD(operacion.ID);
                    return resultado;
                default:
                    throw new Exception("Ocurrió un error inesperado al intentar eliminar la categoría, contactar con el administrador del sistema si el error persiste.");
            }
        }

        private List<int> ListarProductosIDEnCategoria(int categoriaID)
        {
            List<int> listaProductosID = ListarProductosEnCategoriaID(categoriaID);
            return listaProductosID;
        }

        private bool AsignarProductosSinCategoria(Operacion operacion)
        {
            bool resultado = true;
            List<int> listaProductoID = ListarProductosIDEnCategoria(operacion.ID);
            foreach (var productoID in listaProductoID)
            {
                resultado &= AsignarProductosSinCategoria(productoID);
            }
            return resultado;
        }

        // BAJA
        public bool BajaCategoriaBD(int categoriaID)
        {
            bool baja = false;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("DELETE FROM Categoria");
                query.AppendLine("WHERE CategoriaID = @categoriaID");
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@categoriaID", categoriaID);
                        oContexto.Open();
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        baja = filasAfectadas > 0;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar eliminar la categoría, póngase en contacto con el administrador del sistema para solucionar el error.");
                }
            }
            return baja;
        }

        // CONTROL DE REFERENCIAS
        public bool AsignarProductosSinCategoria(int productoID)
        {
            bool asignado = false;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("UPDATE Producto");
                query.AppendLine("SET CategoriaID = 0");
                query.AppendLine("WHERE ProductoID = @productoID");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@productoID", productoID);
                        oContexto.Open();
                        asignado = cmd.ExecuteNonQuery() > 0;
                    }

                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar asignar productos sin categoría, póngase en contacto con el administrador del sistema para solucionar el error.");
                }
            }
            return asignado;
        }

        // Obtener ID de todo los productos de una categoría
        public List<int> ListarProductosEnCategoriaID(int categoriaID)
        {
            List<int> listaProductosID = new List<int>();
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT ProductoID FROM Producto");
                    query.AppendLine("WHERE CategoriaID = @categoriaID");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@categoriaID", categoriaID);
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int productoID;
                            while (reader.Read())
                            {
                                productoID = Convert.ToInt32(reader["ProductoID"]);
                                listaProductosID.Add(productoID);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar obtener los productos de la categoría. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.");
                }
            }
            return listaProductosID;
        }

        // HABILITAR TODO LOS PRODUCTOS DE LA CATEGORIA
        public int HabilitarProductos(int categoriaID)
        {
            int productosHabilitados = -1;
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE Producto SET Estado = 1");
                    query.AppendLine("WHERE CategoriaID = @categoriaID");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@categoriaID", categoriaID);
                        oContexto.Open();
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        productosHabilitados = filasAfectadas;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Error al habilitar los productos de la categoría, contactar con el administrador del sistema si el error persiste.");
                }
            }
            return productosHabilitados;
        }

        // Deshabilitar todo los productos de la categoría
        public int DeshabilitarProductos(int categoriaID)
        {
            int productosDeshabilitado = -1;
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE Producto SET Estado = 0");
                    query.AppendLine("WHERE CategoriaID = @categoriaID");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@categoriaID", categoriaID);
                        oContexto.Open();
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        productosDeshabilitado = filasAfectadas;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Error al deshabilitar los productos de la categoría, contactar con el administrador del sistema si el error persiste.");
                }
            }
            return productosDeshabilitado;
        }


        // Comprobar si la categoría tiene productos asignados
        public bool CategoriaTieneProductosD(int categoriaID)
        {
            bool tieneProductos = false;
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT COUNT(P.ProductoID) AS Cantidad");
                    query.AppendLine("FROM Categoria C");
                    query.AppendLine("LEFT JOIN Producto P ON C.CategoriaID = P.CategoriaID");
                    query.AppendLine("WHERE C.CategoriaID = @categoriaID");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@categoriaID", categoriaID);
                        oContexto.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        tieneProductos = count > 0;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Error al comprobar si la categoría tiene productos asignados, contactar con el administrador del sistema si el error persiste.");
                }
            }
            return tieneProductos;
        }

        public bool ExisteCategoriaD(string nombre)
        {
            bool existe = false;

            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT COUNT(*) FROM Categoria WHERE Nombre = @nombre");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        oContexto.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        existe = count > 0;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Error al verificar si existe la categoría, contactar con el administrador del sistema si el error persiste.");
                }
            }
            return existe;
        }

        // Listar Categorias
        public List<Categoria> ListarCategoriasD()
        {
            List<Categoria> listaCategorias = new List<Categoria>();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT * FROM Categoria");
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Categoria oCategoria = new Categoria();
                                oCategoria.CategoriaID = Convert.ToInt32(reader["CategoriaID"]);
                                oCategoria.Nombre = reader["Nombre"].ToString();

                                oCategoria.Estado = Convert.ToBoolean(reader["Estado"]);
                                listaCategorias.Add(oCategoria);
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    throw new Exception("Ocurrió un error al intentar obtener la lista de categorías, contactar con el administrador del sistema si el error persiste.");
                }
            }
            return listaCategorias;
        }

        // Obtener Categoria por ID
        public Categoria ObtenerCategoriaID(int categoriaID)
        {
            Categoria oCategoria = new Categoria();
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM Categoria");
                query.AppendLine("WHERE CategoriaID = @categoriaID");
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@categoriaID", categoriaID);
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                oCategoria.CategoriaID = Convert.ToInt32(reader["CategoriaID"]);
                                oCategoria.Nombre = reader["Nombre"].ToString();
                                oCategoria.Estado = Convert.ToBoolean(reader["Estado"]);
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    throw new Exception("Error al obtener la categoría por ID, contactar con el administrador del sistema si el error persiste.");
                }
            }
            return oCategoria;
        }

        
    }
}
