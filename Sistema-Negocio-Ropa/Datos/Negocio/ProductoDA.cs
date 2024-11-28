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
    public class ProductoDA
    {

        private Conexion conexion;

        public ProductoDA()
        {
            conexion = new Conexion();
        }


        // Actualizar Stock puede ser - o +
        public bool ActualizarStock(int productoID, int nuevaCantidad)
        {
            bool actualizado = false;
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("UPDATE Producto");
                query.AppendLine("SET Stock = CASE WHEN Stock + @nuevaCantidad < 0 THEN 0 ELSE Stock + @nuevaCantidad END");
                query.AppendLine("WHERE ProductoID = @productoID");

                try
                {
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@nuevaCantidad", nuevaCantidad);
                        cmd.Parameters.AddWithValue("@productoID", productoID);
                        oContexto.Open();
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        actualizado = filasAfectadas > 0;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar el stock del producto: " + ex.Message);
                }
            }
            return actualizado;
        }




        // actualizar precio, usando ProductoID y NuevoPrecio
        public bool ActualizarPrecio(int productoID, decimal nuevoPrecio)
        {
            bool actualizado = false;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("UPDATE Producto SET PrecioVenta = @nuevoPrecio");
                query.AppendLine("WHERE ProductoID = @productoID");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@nuevoPrecio", nuevoPrecio);
                        cmd.Parameters.AddWithValue("@productoID", productoID);
                        oContexto.Open();
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        actualizado = filasAfectadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar el precio del producto: " + ex.Message);
                }
            }
            return actualizado;
        }

        public List<Producto> ObtenerProductosList()
        {
            List<Producto> lista = new List<Producto>();  
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM Producto");
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Producto oProducto = new Producto();
                                oProducto.ProductoID = Convert.ToInt32(reader["ProductoID"]);
                                oProducto.Nombre = reader["Nombre"].ToString();
                                oProducto.PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"]);
                                oProducto.Talle = reader["Talle"].ToString();
                                oProducto.Equipo = reader["Equipo"].ToString();
                                oProducto.Stock = Convert.ToInt32(reader["Stock"]);
                                oProducto.StockMinimo = Convert.ToInt32(reader["StockMinimo"]);
                                oProducto.Estado = Convert.ToBoolean(reader["Estado"]);

                                // Obtener la categoría del producto
                                CategoriaDA categoriaDA = new CategoriaDA();
                                oProducto.oCategoria = categoriaDA.ObtenerCategoriaID(Convert.ToInt32(reader["CategoriaID"]));
                                lista.Add(oProducto);
                            }
                        }

                    }
                }
                catch(Exception ex)
                {
                    throw new Exception($"Error al obtener los productos existentes: {ex.Message}. Contactar con el administrador si el problema persiste.");
                }
            }
            return lista;
        }

        // OBTENER TABLA PARA AJUSTAR PRECIO POR CATEGORIA
        public DataTable ObtenerProductosenCategoria(string buscar, string filtroCategoria, string strporcentaje = "0")
        {
            // VALORES POR DEFECTO
            buscar = string.IsNullOrWhiteSpace(buscar) ? "" : buscar;
            decimal porcentaje;

            if (!decimal.TryParse(strporcentaje, out porcentaje))
            {
                porcentaje = 0;
            }

            filtroCategoria = string.IsNullOrWhiteSpace(filtroCategoria) ? "Todos" : filtroCategoria;
            DataTable dt = new DataTable();
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                using(SqlCommand cmd = new SqlCommand("sp_ObtenerProductosConNuevoPrecio", oContexto))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Buscar", buscar);
                    cmd.Parameters.AddWithValue("@FiltroCategoria", filtroCategoria);
                    cmd.Parameters.AddWithValue("@Porcentaje", porcentaje);
                    try
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Se ha producido un error al intentar obtener los productos. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.");
                    }
                }
            }
            return dt;
        }

        public DataTable ObtenerProductosFiltrados(string buscar, string filtroBuscar, string filtroTalle, string filtroEquipo, string filtroEstado, string filtroCategoria)
        {
            // VALORES POR DEFECTO
            buscar = string.IsNullOrWhiteSpace(buscar) ? "" : buscar;
            filtroBuscar = string.IsNullOrWhiteSpace(filtroBuscar) ? "Todos" : filtroBuscar;
            filtroTalle = string.IsNullOrWhiteSpace(filtroTalle) ? "Todos" : filtroTalle;
            filtroEquipo = string.IsNullOrWhiteSpace(filtroEquipo) ? "Todos" : filtroEquipo;
            filtroEstado = string.IsNullOrWhiteSpace(filtroEstado) ? "Todos" : filtroEstado;
            filtroCategoria = string.IsNullOrWhiteSpace(filtroCategoria) ? "Todos" : filtroCategoria;

            DataTable dt = new DataTable();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerProductos", oContexto))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Buscar", buscar);
                    cmd.Parameters.AddWithValue("@FiltroBuscar", filtroBuscar);
                    cmd.Parameters.AddWithValue("@FiltroTalle", filtroTalle);
                    cmd.Parameters.AddWithValue("@FiltroEquipo", filtroEquipo);
                    cmd.Parameters.AddWithValue("@FiltroEstado", filtroEstado);
                    cmd.Parameters.AddWithValue("@FiltroCategoria", filtroCategoria);
                    try
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Se ha producido un error al intentar obtener los productos. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.");
                    }
                }
            }
            return dt;
        }


        // Obtener categorías vinculadas a los productos
        public List<Categoria> ListarCategoriasenProducto()
        {
            List<Categoria> lista = new List<Categoria>();
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT DISTINCT C.CategoriaID, C.Nombre");
                query.AppendLine("FROM dbo.Categoria C");
                query.AppendLine("JOIN dbo.Producto P ON C.CategoriaID = P.CategoriaID");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        oContexto.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Categoria _categoria = new Categoria();
                                _categoria.CategoriaID = Convert.ToInt32(reader["CategoriaID"]);
                                _categoria.Nombre = reader["Nombre"].ToString();
                                lista.Add(_categoria);
                            }
                        }
                    }
                   
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar obtener las categorías de los productos. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.");
                }
            }
            return lista;
        }

        // Obtener talles existentes
        public List<string> TallesEnProductos()
        {
            List<string> lista = new List<string>();

            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT DISTINCT Talle");
                query.AppendLine("FROM dbo.Producto");
                query.AppendLine("WHERE Talle IS NOT NULL AND Talle <> ''");
                query.AppendLine("ORDER BY Talle ASC");
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(reader["Talle"].ToString());
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar obtener los talles de los productos. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.");
                }
            }
            return lista;
        }

        public List<string> NombresProductos()
        {
            List<string> lista = new List<string>();
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT DISTINCT Nombre");
                query.AppendLine("FROM dbo.Producto");
                query.AppendLine("WHERE Nombre IS NOT NULL AND Nombre <> ''");
                query.AppendLine("ORDER BY Nombre ASC");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(reader["Nombre"].ToString());
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar obtener los nombres de los productos. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.");
                }
            }
            return lista;
        }

        public List<string> EquiposEnProductos()
        {
            List<string> lista = new List<string>();

            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT Equipo"); // Se elimina DISTINCT
                query.AppendLine("FROM dbo.Producto");
                query.AppendLine("WHERE Equipo IS NOT NULL AND Equipo <> ''");
                query.AppendLine("GROUP BY Equipo"); // Se usa GROUP BY en lugar de DISTINCT
                query.AppendLine("ORDER BY Equipo ASC");

                try
                {
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        oContexto.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(reader["Equipo"].ToString());
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar obtener los equipos de los productos. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.");
                }
            }
            return lista;
        }



        public int ObtenerExistencias(int productoID)
        {
            int existencias = 0;
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    string query = "SELECT Stock FROM Producto WHERE ProductoID = @productoID";
                    using (SqlCommand cmd = new SqlCommand(query, oContexto))
                    {
                        cmd.Parameters.AddWithValue("@productoID", productoID);
                        oContexto.Open();
                        existencias = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar obtener las existencias del producto. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.");
                }
            }
            return existencias;
        }


        public bool AltaProductoD(Producto oProducto)
        {
            bool alta = false;
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("INSERT INTO Producto (Nombre, CategoriaID, PrecioVenta, Talle, Equipo, Stock, StockMinimo, Estado)");
                    query.AppendLine("VALUES (@nombre, @categoriaID, @precioVenta, @talle, @equipo, @stock, @stockMinimo, @estado)");

                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@nombre", oProducto.Nombre);

                        // Verificar si la categoría existe
                        if (oProducto.oCategoria == null)
                        {
                            throw new Exception("La categoría ingresada no existe.");
                        }
                        cmd.Parameters.AddWithValue("@categoriaID", oProducto.oCategoria.CategoriaID);

                        cmd.Parameters.AddWithValue("@precioVenta", oProducto.PrecioVenta);

                        // Si el producto no tiene talle o equipo, se agrega "-"
                        cmd.Parameters.AddWithValue("@talle", string.IsNullOrWhiteSpace(oProducto.Talle) ? "No tiene" : oProducto.Talle.Trim());
                        cmd.Parameters.AddWithValue("@equipo", string.IsNullOrWhiteSpace(oProducto.Equipo) ? "Sin equipo" : oProducto.Equipo.Trim());

                        cmd.Parameters.AddWithValue("@stock", oProducto.Stock);
                        cmd.Parameters.AddWithValue("@stockMinimo", oProducto.StockMinimo);
                        cmd.Parameters.AddWithValue("@estado", oProducto.Estado);

                        oContexto.Open();

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        alta = filasAfectadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return alta;
        }

        // Modificar
        public bool ModificarProductoD(Producto producto)
        {
            bool modificado = false;
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE Producto SET Nombre = @nombre, CategoriaID = @categoriaID, PrecioVenta = @precioVenta, Talle = @talle, Equipo = @equipo, Stock = @stock, StockMinimo = @stockMinimo, Estado = @estado");
                    query.AppendLine("WHERE ProductoID = @productoID");

                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        // Asignación de parámetros
                        cmd.Parameters.AddWithValue("@nombre", producto.Nombre);

                        // Verificar si la categoría existe
                        if (producto.oCategoria == null)
                        {
                            throw new Exception("La categoría ingresada no existe.");
                        }
                        cmd.Parameters.AddWithValue("@categoriaID", producto.oCategoria.CategoriaID);

                        cmd.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);

                        // Si no se especifica un talle o equipo, se usa "-"
                        cmd.Parameters.AddWithValue("@talle", string.IsNullOrWhiteSpace(producto.Talle) ? "No tiene" : producto.Talle.Trim());
                        cmd.Parameters.AddWithValue("@equipo", string.IsNullOrWhiteSpace(producto.Equipo) ? "Sin equipo" : producto.Equipo.Trim());

                        cmd.Parameters.AddWithValue("@stock", producto.Stock);
                        cmd.Parameters.AddWithValue("@stockMinimo", producto.StockMinimo);
                        cmd.Parameters.AddWithValue("@estado", producto.Estado);
                        cmd.Parameters.AddWithValue("@productoID", producto.ProductoID);

                        oContexto.Open();

                        // Ejecutar la consulta y verificar si se modificó algún registro
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        modificado = filasAfectadas > 0;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Se ha producido un error al intentar modificar el producto. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.");
                }
            }
            return modificado;
        }

        // VERIFICAR REFERENCIAS
        public bool VerificarReferencias(int productoID)
        {
            bool tieneReferencias = false;

            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                SqlCommand cmd = new SqlCommand("VerificarReferenciasProducto", oContexto);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetro de entrada
                cmd.Parameters.AddWithValue("@ProductoID", productoID);

                // Parámetro de salida
                SqlParameter param = new SqlParameter("@ReferenciasEncontradas", SqlDbType.Bit);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                try
                {
                    oContexto.Open();
                    cmd.ExecuteNonQuery();
                    tieneReferencias = Convert.ToBoolean(param.Value);
                }
                catch (SqlException sqlEx)
                {
                    // Detalle específico para errores de SQL
                    throw new Exception("Error de base de datos al verificar las referencias del producto: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    // Manejo general de excepciones
                    throw new Exception("Error al verificar las referencias del producto. Contactar con el administrador si el problema persiste: " + ex.Message);
                }
            }

            return tieneReferencias;
        }


        public bool BajaProductoD(int productoID)
        {
            bool baja = false;

            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    // Primero verificamos si el producto tiene referencias
                    bool tieneReferencias = VerificarReferencias(productoID);

                    if (tieneReferencias)
                    {
                        // Si tiene referencias, marcamos el producto como inactivo (Estado = 0)
                        Producto producto = ObtenerProductoPorIDD(productoID);
                        producto.Estado = false;
                        baja = ModificarProductoD(producto);
                    }
                    else
                    {
                        // Si no tiene referencias, procedemos a eliminar el producto
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("DELETE FROM Producto WHERE ProductoID = @productoID");

                        using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                        {
                            cmd.Parameters.AddWithValue("@productoID", productoID);
                            oContexto.Open();
                            int filasAfectadas = cmd.ExecuteNonQuery();
                            baja = filasAfectadas > 0; // Si filas afectadas es mayor a 0, se eliminó
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error de base de datos al intentar dar de baja el producto: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Se ha producido un error al intentar dar de baja el producto. Contactar con el administrador si el problema persiste: " + ex.Message);
                }
            }

            return baja;
        }


        public Producto ObtenerProductoPorIDD(int productoID)
        {
            Producto oProducto = new Producto();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT * FROM Producto");
                    query.AppendLine("WHERE ProductoID = @productoID");

                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@productoID", productoID);
                        oContexto.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oProducto.ProductoID = Convert.ToInt32(reader["ProductoID"]);
                                oProducto.Nombre = reader["Nombre"].ToString();

                                // Verificar la categoría
                                oProducto.oCategoria = new Categoria();
                                if (reader["CategoriaID"] != DBNull.Value)
                                {
                                    oProducto.oCategoria.CategoriaID = Convert.ToInt32(reader["CategoriaID"]);
                                }

                                // Asignar precio de venta
                                oProducto.PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"]);

                                oProducto.Talle = reader["Talle"].ToString();
                                oProducto.Equipo = reader["Equipo"].ToString();

                                // Stock y StockMinimo
                                oProducto.Stock = Convert.ToInt32(reader["Stock"]);
                                oProducto.StockMinimo = Convert.ToInt32(reader["StockMinimo"]);

                                // Estado
                                oProducto.Estado = Convert.ToBoolean(reader["Estado"]);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Ocurrió un error al intentar obtener el producto. Contactar con el administrador si el problema persiste.");
                }
            }
            return oProducto;
        }

        public bool ExisteProductoD(string nombre)
        {
            bool existe = false;

            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    // SELECT COUNT(*) FROM Producto WHERE Nombre = @nombre
                    // comprobar si el nombre del producto ya existe
                    query.AppendLine("SELECT COUNT(*) FROM Producto");
                    query.AppendLine("WHERE Nombre = @nombre");

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
                    throw new Exception("Error al verificar si existe el producto, contactar con el administrador si el problema persiste.");
                }
            }
            return existe;
        }


    }
}
