using Negocio.Negocio;
using Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Negocio
{
    public class VentaDA
    {
        private Conexion conexion;

        public VentaDA()
        {
            conexion = new Conexion();
        }

        public VentaM ObtenerVentaID(int ventaID)
        {
            /*CREATE TABLE Venta (
    VentaID INT IDENTITY PRIMARY KEY,
    UsuarioID INT REFERENCES Usuario(UsuarioID) ON DELETE SET NULL,
	CajaID INT REFERENCES Caja(CajaID) ON DELETE SET NULL,
    TipoComprobante NVARCHAR(50),
    MontoPagado DECIMAL(10,2),
    MontoCambio DECIMAL(10,2),
    Recargo DECIMAL(10,2),
    MetodoPagoID INT REFERENCES MetodoPago(MetodoPagoID),
    MontoTotal DECIMAL(10,2) NOT NULL,
    FechaVenta DATETIME DEFAULT GETDATE(),
    Estado BIT DEFAULT 1
);*/
            VentaM oVenta = new VentaM();
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM Venta WHERE VentaID = @VentaID");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@VentaID", ventaID);
                        oContexto.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Usuario _usuario = new Usuario();
                                oVenta.VentaID = Convert.ToInt32(reader["VentaID"]);
                                _usuario.UsuarioID = Convert.ToInt32(reader["UsuarioID"]);
                                oVenta.oUsuario = _usuario;
                                oVenta.TipoComprobante = reader["TipoComprobante"].ToString();
                                oVenta.MontoPagado = Convert.ToDecimal(reader["MontoPagado"]);
                                oVenta.MontoCambio = Convert.ToDecimal(reader["MontoCambio"]);
                                oVenta.Recargo = Convert.ToDecimal(reader["Recargo"]);
                                MetodoPago _metodoPago = new MetodoPago();
                                _metodoPago.MetodoPagoID = Convert.ToInt32(reader["MetodoPagoID"]);
                                oVenta.oMetodoPago = _metodoPago;
                                oVenta.MontoTotal = Convert.ToDecimal(reader["MontoTotal"]);
                                oVenta.Fecha = Convert.ToDateTime(reader["FechaVenta"]);
                                oVenta.Estado = Convert.ToBoolean(reader["Estado"]);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Error al obtener la venta. Contacte con el administrador del sistema.");
                }
            }
            return oVenta;
        }

        public bool RegistrarVenta(VentaM oVenta, DataTable DetalleVenta)
        {
            bool resultado = false;
            if (oVenta != null && DetalleVenta != null)
            {
                using (SqlConnection oContexto = conexion.EstablecerConexion())
                {
                    SqlTransaction transaction = null;
                    try
                    {
                        oContexto.Open();
                        transaction = oContexto.BeginTransaction();

                        StringBuilder query = new StringBuilder();
                        query.Append("INSERT INTO Venta (UsuarioID, CajaID, TipoComprobante, MontoPagado, MontoCambio, Recargo, MetodoPagoID, MontoTotal, FechaVenta, Estado) ");
                        query.Append("OUTPUT INSERTED.VentaID ");
                        query.Append("VALUES (@UsuarioID, @CajaID, @TipoComprobante, @MontoPagado, @MontoCambio, @Recargo, @MetodoPagoID, @MontoTotal, @FechaVenta, @Estado)");

                        using (SqlCommand cmd = new SqlCommand(query.ToString(), oContexto, transaction))
                        {
                            cmd.Parameters.AddWithValue("UsuarioID", oVenta.oUsuario.UsuarioID);
                            cmd.Parameters.AddWithValue("CajaID", oVenta.oCaja.CajaID);
                            cmd.Parameters.AddWithValue("TipoComprobante", oVenta.TipoComprobante);
                            cmd.Parameters.AddWithValue("MontoPagado", oVenta.MontoPagado);
                            cmd.Parameters.AddWithValue("MontoCambio", oVenta.MontoCambio);
                            cmd.Parameters.AddWithValue("Recargo", oVenta.Recargo);
                            cmd.Parameters.AddWithValue("MetodoPagoID", oVenta.oMetodoPago.MetodoPagoID);
                            cmd.Parameters.AddWithValue("MontoTotal", oVenta.MontoTotal);
                            cmd.Parameters.AddWithValue("FechaVenta", oVenta.Fecha);
                            cmd.Parameters.AddWithValue("Estado", oVenta.Estado);

                            oVenta.VentaID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Registrar detalle de venta
                        foreach (DataRow fila in DetalleVenta.Rows)
                        {
                            query.Clear();
                            query.AppendLine("INSERT INTO Detalle_Venta (VentaID, ProductoID, PrecioVenta, Cantidad, SubTotal) ");
                            query.AppendLine("VALUES (@VentaID, @ProductoID, @PrecioVenta, @Cantidad, @SubTotal)");
                            using (SqlCommand cmdDetalle = new SqlCommand(query.ToString(), oContexto, transaction))
                            {
                                cmdDetalle.Parameters.AddWithValue("@VentaID", oVenta.VentaID);
                                /*Las filas se llaman
                                 dtDetalleVenta.Columns.Add("dgvcID", typeof(int));
                            dtDetalleVenta.Columns.Add("dgvcCantidad", typeof(int));
                            dtDetalleVenta.Columns.Add("dgvcPrecioVenta", typeof(decimal));
                            dtDetalleVenta.Columns.Add("dgvcSubTotal", typeof(decimal));
*/
                                cmdDetalle.Parameters.AddWithValue("@ProductoID", fila["dgvcID"]);
                                cmdDetalle.Parameters.AddWithValue("@PrecioVenta", fila["dgvcPrecioVenta"]);
                                cmdDetalle.Parameters.AddWithValue("@Cantidad", fila["dgvcCantidad"]);
                                cmdDetalle.Parameters.AddWithValue("@SubTotal", fila["dgvcSubTotal"]);
                                cmdDetalle.ExecuteNonQuery();
                            }
                        }

                        // Si todo es exitoso, confirmar transacción
                        transaction.Commit();
                        resultado = true;
                    }
                    catch (SqlException ex)
                    {
                        if (transaction != null) transaction.Rollback();
                        throw new Exception("Error en la base de datos: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        if (transaction != null) transaction.Rollback();
                        throw new Exception("Error general: " + ex.Message);
                    }
                }
            }
            return resultado;
        }


        /*
-- CAJA
CREATE PROCEDURE sp_ObtenerCajasConTotales
    @FiltroEstado NVARCHAR(50),
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    -- Selección de cajas con totales por método de pago
    SELECT 
        C.CajaID,
        U.Usuario AS 'Apertura de Caja',
        C.MontoInicial AS 'Monto Inicial',
        C.MontoFinal AS 'Monto Final',
        C.FechaApertura AS 'Fecha de Apertura',
        C.FechaCierre AS 'Fecha de Cierre',
        SUM(CASE WHEN MP.Nombre = 'Efectivo' THEN V.MontoTotal ELSE 0 END) AS 'Total Efectivo',
        SUM(CASE WHEN MP.Nombre = 'Tarjeta Débito' THEN V.MontoTotal ELSE 0 END) AS 'Total Débito',
        SUM(CASE WHEN MP.Nombre = 'Tarjeta Crédito' THEN V.MontoTotal ELSE 0 END) AS 'Total Crédito',
        SUM(CASE WHEN MP.Nombre = 'Depósito/Transferencia' THEN V.MontoTotal ELSE 0 END) AS 'Total Transferencia'
    FROM 
        Caja C
        INNER JOIN Usuario U ON C.UsuarioID = U.UsuarioID
        LEFT JOIN Venta V ON C.CajaID = V.CajaID AND V.Estado = 1 -- Solo ventas activas
        LEFT JOIN MetodoPago MP ON V.MetodoPagoID = MP.MetodoPagoID
    WHERE 
        (
            @FiltroEstado = 'Todos' OR 
            (C.Estado = 1 AND @FiltroEstado = 'Abierta') OR 
            (C.Estado = 0 AND @FiltroEstado = 'Cerrada')
        )
        AND C.FechaApertura BETWEEN @FechaInicio AND @FechaFin
    GROUP BY 
        C.CajaID, 
        U.Usuario, 
        C.MontoInicial, 
        C.MontoFinal, 
        C.FechaApertura, 
        C.FechaCierre;
END;


CREATE PROCEDURE sp_ObtenerVentasConTotales
    @CajaFiltro NVARCHAR(50), -- 'Todas' o número de Caja
    @FiltroEstado NVARCHAR(50), -- 'Todos', 'Activo' o 'Cancelado'
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    SELECT 
        V.VentaID AS Folio,
        U.Usuario AS 'Vendedor',
        MP.Nombre AS 'Método de Pago',
        SUM(DV.Cantidad) AS 'Cantidad Vendida',
        V.MontoPagado AS 'Monto Pagado',
        V.MontoCambio AS 'Monto Cambio',
        V.Recargo AS 'Recargo Aplicado',
        V.MontoTotal AS 'Monto Total'
    FROM 
        Venta V
        INNER JOIN Usuario U ON V.UsuarioID = U.UsuarioID
        INNER JOIN MetodoPago MP ON V.MetodoPagoID = MP.MetodoPagoID
        INNER JOIN Detalle_Venta DV ON V.VentaID = DV.VentaID
    WHERE 
        (@CajaFiltro = 'Todas' OR V.CajaID = @CajaFiltro) -- Filtro por Caja
        AND (
            @FiltroEstado = 'Todos' OR 
            (@FiltroEstado = 'Activo' AND V.Estado = 1) OR 
            (@FiltroEstado = 'Cancelado' AND V.Estado = 0)
        ) -- Filtro por Estado
        AND V.FechaVenta BETWEEN @FechaInicio AND @FechaFin -- Filtro por Fecha
    GROUP BY 
        V.VentaID, 
        U.Usuario, 
        MP.Nombre, 
        V.MontoPagado, 
        V.MontoCambio, 
        V.Recargo, 
        V.MontoTotal;
END;


EXEC sp_ObtenerVentasConTotales 
    @CajaFiltro = 1, 
    @FiltroEstado = 'Todos', 
    @FechaInicio = '2024-01-01', 
    @FechaFin = '2024-12-31';


CREATE PROCEDURE sp_ObtenerDetalleVenta
    @VentaID INT
AS
BEGIN
    SELECT 
        P.ProductoID AS 'ID',
        P.Nombre AS 'Nombre de Producto',
        DV.Cantidad AS 'Cantidad Vendida',
        DV.PrecioVenta AS 'Precio de Venta',
        DV.Cantidad * DV.PrecioVenta AS 'Sub Total'
    FROM 
        Detalle_Venta DV
        INNER JOIN Producto P ON DV.ProductoID = P.ProductoID
    WHERE 
        DV.VentaID = @VentaID;
END;

*/

        // Obtener Historial de Ventas
        public DataTable ObtenerVentasFiltradas(string CajaFiltro, string FiltroEstado, DateTime? FechaInicio, DateTime? FechaFin)
        {
            // VALORES DEFAULT
            if (string.IsNullOrEmpty(CajaFiltro)) CajaFiltro = "Todas";
            if (string.IsNullOrEmpty(FiltroEstado)) FiltroEstado = "Todos";

            // Establecer fechas por defecto si son null
            FechaInicio = FechaInicio ?? DateTime.Now.AddYears(-5);
            FechaFin = FechaFin ?? DateTime.Now.AddYears(5);

            DataTable dtVentas = new DataTable();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerVentasConTotales", oContexto))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CajaFiltro", CajaFiltro);
                        cmd.Parameters.AddWithValue("@FiltroEstado", FiltroEstado);
                        cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", FechaFin);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtVentas);
                    }
                }
                catch (Exception ex)
                {
                    // Aquí podrías agregar logging
                    throw new Exception("Error en la base de datos: " + ex.Message);
                }
            }
            return dtVentas;
        }

        // Obtener Detalle de Venta
        public DataTable ObtenerDetalleVenta(int VentaID)
        {
            DataTable dtDetalleVenta = new DataTable();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerDetalleVenta", oContexto))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@VentaID", VentaID);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtDetalleVenta);
                    }
                }
                catch (Exception ex)
                {
                    // Aquí podrías agregar logging
                    throw new Exception("Error en la base de datos: " + ex.Message);
                }
            }
            return dtDetalleVenta;
        }

        public DataTable ObtenerCajasFiltradas(int? CajaFiltro, string FiltroEstado, DateTime FechaInicio, DateTime FechaFin)
        {
            // VALORES DEFAULT
            if (string.IsNullOrEmpty(FiltroEstado)) FiltroEstado = "Todos";
            // fechas se pondrá 5 años antes y 5 años después
            FechaInicio = FechaInicio == default ? DateTime.Now.AddYears(-5) : FechaInicio;
            FechaFin = FechaFin == default ? DateTime.Now.AddYears(5) : FechaFin;

            DataTable dtCajas = new DataTable();
            using (SqlConnection oContexto = conexion.EstablecerConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerCajasConTotales", oContexto))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Si CajaFiltro es null, se le asigna el valor DBNull
                        cmd.Parameters.AddWithValue("@CajaFiltro", (object)CajaFiltro ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@FiltroEstado", FiltroEstado);
                        cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", FechaFin);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtCajas);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en la base de datos: " + ex.Message);
                }
            }
            return dtCajas;
        }

        public bool CancelarVenta(int ventaID)
        {
            bool resultado = false;
            using(SqlConnection oContexto = conexion.EstablecerConexion())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("UPDATE Venta SET Estado = 0 WHERE VentaID = @VentaID");
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query.ToString(), oContexto))
                    {
                        cmd.Parameters.AddWithValue("@VentaID", ventaID);
                        oContexto.Open();
                        resultado = cmd.ExecuteNonQuery() > 0;
                    }

                }
                catch (Exception)
                {
                    throw new Exception("Error al cancelar la venta. Contacte con el administrador del sistema.");
                }
            }
            return resultado;
        }




    }
}
