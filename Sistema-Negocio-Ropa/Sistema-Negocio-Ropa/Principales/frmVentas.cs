﻿using Datos.Negocio;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Negocio.Negocio;
using Negocio.Seguridad;
using Sistema_Negocio_Ropa.Modal;
using Sistema_Negocio_Ropa.Modal.Venta;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Negocio_Ropa.Principales
{
    public partial class frmVentas : Form
    {

        private Sesion lSesion = Sesion.ObtenerInstancia;
        private ProductoDA lProducto;
        private Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        private List<Producto> lstProductos { get; set; }
        private VentaDA lVenta;
        private CajaDA lCaja;
        public frmVentas()
        {
            InitializeComponent();
            lProducto = new ProductoDA();
            lstProductos = new List<Producto>();
            lVenta = new VentaDA();
            lCaja = new CajaDA();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDatosFormulario();
                txtProducto.Select();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            try
            {
               if(lSesion.CajaEnSesion() != null)
                {
                    if (dgvVenta.Rows.Count > 0)
                    {
                        // testeamos 
                        VentaM oVenta = new VentaM();
                        // agregar monto total, eliminar $ y espacios si los hubiera
                        oVenta.MontoTotal = Convert.ToDecimal(txtTotal.Text.Replace("$", "").Trim());

                        using (var modal = new mdCobrar(oVenta))
                        {
                            if (modal.ShowDialog() == DialogResult.OK)
                            {
                                // Generamos los productos
                                List<Producto> lstProductos = new List<Producto>();

                                foreach (DataGridViewRow fila in dgvVenta.Rows)
                                {

                                    Producto _producto = new Producto
                                    {

                                        ProductoID = Convert.ToInt32(fila.Cells["dgvcID"].Value),
                                        Nombre = fila.Cells["dgvcProducto"].Value.ToString(),
                                        // el stock no es el stock actual, sino lacantidad que se va a vender
                                        Stock = Convert.ToInt32(fila.Cells["dgvcCantidad"].Value),
                                        PrecioVenta = Convert.ToDecimal(fila.Cells["dgvcPrecio"].Value),
                                    };
                                    lstProductos.Add(_producto);
                                }

                                // datatable de detalle venta
                                DataTable dtDetalleVenta = new DataTable();
                                dtDetalleVenta.Columns.Add("dgvcID", typeof(int));
                                dtDetalleVenta.Columns.Add("dgvcCantidad", typeof(int));
                                dtDetalleVenta.Columns.Add("dgvcPrecioVenta", typeof(decimal));
                                dtDetalleVenta.Columns.Add("dgvcSubTotal", typeof(decimal));

                                foreach (DataGridViewRow fila in dgvVenta.Rows)
                                {
                                    dtDetalleVenta.Rows.Add(
                                        Convert.ToInt32(fila.Cells["dgvcID"].Value),
                                        Convert.ToInt32(fila.Cells["dgvcCantidad"].Value),
                                        Convert.ToDecimal(fila.Cells["dgvcPrecio"].Value),
                                        Convert.ToDecimal(fila.Cells["dgvcSubTotal"].Value)
                                        );
                                }

                                // Crear venta
                                // del modal agararmos

                                oVenta = modal.oVenta;
                                oVenta.oUsuario = lSesion.UsuarioEnSesion();
                                oVenta.oCaja = lSesion.CajaEnSesion();
                                oVenta.TipoComprobante = "Boleta";
                                oVenta.Estado = true;

                                // Agregar venta
                                if (lVenta.RegistrarVenta(oVenta, dtDetalleVenta))
                                {
                                    // actualizar stock
                                    foreach (Producto producto in lstProductos)
                                    {
                                        // si el stock es negativo, se resta
                                        producto.Stock = producto.Stock * -1;
                                        bool resultado = lProducto.ActualizarStock(producto.ProductoID, producto.Stock);
                                        if (!resultado)
                                        {
                                            MessageBox.Show($"Error al actualizar el stock del producto: {producto.Nombre}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    MessageBox.Show("Venta registrada con éxito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.DialogResult = DialogResult.OK;
                                    LimpiarVenta();
                                    CargarDatosFormulario();
                                }
                                else
                                {
                                    MessageBox.Show("Error al registrar la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay productos para cobrar", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Se debe dar apertura a la caja para poder realizar ventas", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosFormulario()
        {
            try
            {
                CajaM oCaja = lSesion.CajaEnSesion();
                if(oCaja != null)
                {
                    lblNumerodeVenta.Text = lCaja.ObtenerNumeroVenta(oCaja).ToString("0000");
                }
                else
                {
                    lblNumerodeVenta.Text = "0000";
                    btnCierreCaja.Enabled = false;
                }
                CargarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscadorProducto_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (var modal = new buscadorProductos())
                {
                    // mouse cambia a cursor de carga
                    var resultado = modal.ShowDialog();
                    if (resultado == DialogResult.OK)
                    {
                        productoSeleccionado = modal._productoSeleccionado;
                        if (productoSeleccionado != null)
                        {
                            txtProducto.BackColor = Color.FromArgb(192, 255, 192);
                            SeleccionarProducto(true);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtBuscarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SeleccionarProducto();
                e.SuppressKeyPress = true; // Evitar el sonido de "ding" al presionar Enter
            }
        }

        private void DiseñoGrilla()
        {
            // Permitir que se pueda modificar descuento escribiendo en la celda
            
        }

        

        // Cargar Producto
        private void CargarProductoEnGrilla(int cantidadElegida, bool remplazar)
        {
            errorProvider.Clear();
            if (productoSeleccionado != null)
            {
                Producto _producto = new Producto();
                _producto = productoSeleccionado;

                if (cantidadElegida <= _producto.Stock)
                {
                    DataGridViewRow existeFila = dgvVenta.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(row => Convert.ToInt32(row.Cells["dgvcID"].Value) == _producto.ProductoID);

                    if (existeFila != null)
                    {
                        // Cantidad en fila
                        int cantidadExistente = Convert.ToInt32(existeFila.Cells["dgvcCantidad"].Value);

                        // Determinar la nueva cantidad, dependiendo si se reemplaza o se suma a la existente
                        int nuevaCantidad = remplazar ? cantidadElegida : cantidadExistente + cantidadElegida;

                        // Verificar si la nueva cantidad supera el stock disponible
                        if (nuevaCantidad > _producto.Stock)
                        {
                            MessageBox.Show("No se puede cargar el producto, ya que supera el stock registrado disponible", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtProducto.BackColor = Color.FromArgb(255, 192, 192); // Opcional
                            return;
                        }

                        // Actualizar cantidad y subtotal
                        existeFila.Cells["dgvcCantidad"].Value = nuevaCantidad;
                        decimal subtotal = nuevaCantidad * _producto.PrecioVenta;
                        existeFila.Cells["dgvcSubTotal"].Value = uiUtilidades.FormatearMonedaString(subtotal);

                        // Si la nueva cantidad es cero, eliminar el producto de la grilla
                        if (nuevaCantidad == 0)
                        {
                            EliminarProducto(existeFila);
                        }

                    }
                    else
                    {
                        decimal subTotal = cantidadElegida * _producto.PrecioVenta;
                        dgvVenta.Rows.Add(_producto.ProductoID, _producto.Nombre, cantidadElegida, _producto.PrecioVenta, subTotal);
                    }
                    CalcularTotal();
                }
                else
                {
                    MessageBox.Show("No hay stock suficiente para cargar el producto", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtProducto.BackColor = Color.FromArgb(255, 192, 192); // Opcional

                }
            }
            else
            {
                errorProvider.SetError(btnLimpiar, "Seleccione un producto");
            }
        }

        Producto productoSeleccionado { get; set; }

        private void SeleccionarProducto(bool buscador = false, int cantidad = 1, bool remplazar = false)
        {
            if (lstProductos.Count > 0)
            {
                if (!buscador)
                {
                    productoSeleccionado = lstProductos.Find(x => string.Equals(x.Nombre, txtProducto.Text, StringComparison.OrdinalIgnoreCase));
                }

                if (productoSeleccionado != null)
                {
                    // Insertar color rgb 192; 255; 192
                    txtProducto.BackColor = Color.FromArgb(192, 255, 192);
                    txtProducto.Text = productoSeleccionado.Nombre;
                    CargarProductoEnGrilla(cantidad, remplazar);
                }
                else
                {
                    // Insertar color rgb 255; 192; 192
                    txtProducto.BackColor = Color.FromArgb(255, 192, 192);
                    MessageBox.Show("No se encontró el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No se encontraron productos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void EliminarProducto(DataGridViewRow fila)
        {
            try
            {
                if(dgvVenta.Rows.Count == 0)
                {
                    MessageBox.Show("No hay productos para eliminar", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int filaIndex = fila.Index;
                dgvVenta.Rows.RemoveAt(filaIndex);
                CalcularTotal();

            }catch(Exception ex)
            {
                throw new Exception("Error al intenar elimina el producto de la grilla de venta: " + ex.Message);
            }
        }


        private void dgvVenta_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                string nombreColumna = dgvVenta.Columns[e.ColumnIndex].Name;

                if (nombreColumna == "btnAumentar")
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    var w = Properties.Resources.Aumentar_15.Width;
                    var h = Properties.Resources.Aumentar_15.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    // dibujar
                    e.Graphics.DrawImage(Properties.Resources.Aumentar_15, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }

                // btnDecrementar
                if (nombreColumna == "btnDecrementar")
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    var w = Properties.Resources.Decrementar_15.Width;
                    var h = Properties.Resources.Decrementar_15.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    // dibujar
                    e.Graphics.DrawImage(Properties.Resources.Decrementar_15, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }

                // btn eliminar
                if (nombreColumna == "btnEliminar")
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    var w = Properties.Resources.eliminar_boton_15.Width;
                    var h = Properties.Resources.eliminar_boton_15.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    // dibujar
                    e.Graphics.DrawImage(Properties.Resources.eliminar_boton_15, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    DataGridViewRow fila = dgvVenta.Rows[indice];
                    Producto producto = lProducto.ObtenerProductoPorIDD(Convert.ToInt32(fila.Cells["dgvcID"].Value));

                    if (dgvVenta.Columns[e.ColumnIndex].Name == "btnAumentar")
                    {
                        productoSeleccionado = producto;
                        SeleccionarProducto(true, 1, false);
                    }
                    else if (dgvVenta.Columns[e.ColumnIndex].Name == "btnDecrementar")
                    {
                        productoSeleccionado = producto;
                        SeleccionarProducto(true, -1, false);
                    }
                    else if (dgvVenta.Columns[e.ColumnIndex].Name == "btnEliminar")
                    {
                        DialogResult dialogResult = MessageBox.Show("¿Desea eliminar el producto de la venta?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if(dialogResult == DialogResult.Yes)
                        {
                            EliminarProducto(fila);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVenta_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Primero verificar que se seleccionó una fila
            if (e.RowIndex >= 0)
            {
                // no cuenta para boton btnAumentar y btnDecrementar
                if(e.ColumnIndex != dgvVenta.Columns["btnAumentar"].Index && e.ColumnIndex != dgvVenta.Columns["btnDecrementar"].Index)
                {
                    int filaIndex = e.RowIndex;
                    Producto _producto = new Producto();
                    _producto.ProductoID = Convert.ToInt32(dgvVenta.Rows[filaIndex].Cells["dgvcID"].Value);
                    int cantidad = Convert.ToInt32(dgvVenta.Rows[filaIndex].Cells["dgvcCantidad"].Value);
                    using (var modal = new mdModificarVenta(_producto.ProductoID, cantidad))
                    {
                        DialogResult resultado = modal.ShowDialog();
                        if (resultado == DialogResult.OK)
                        {
                            // Modificar cantidad
                            _producto = modal._producto;
                            productoSeleccionado = _producto;
                            cantidad = modal.cantidad_elegida;
                            SeleccionarProducto(true, cantidad, true);
                        }
                    }
                }
            }
        }

        private void btnHistorialDeVenta_Click(object sender, EventArgs e)
        {
            CajaM oCaja = lSesion.CajaEnSesion();
            if(oCaja != null)
            {
                using (var modal = new mdVentasRealizadas(oCaja))
                {
                    modal.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Para ver el historial de ventas de la caja de hoy, primero debe abrir una caja", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCierreCaja_Click(object sender, EventArgs e)
        {
            CajaM oCaja = lSesion.CajaEnSesion();
            if(oCaja != null)
            {
                using(var modal = new mdCierreCaja(oCaja))
                {
                    if(modal.ShowDialog() == DialogResult.OK)
                    {
                        // cerrar caja y cerrar sesión
                        frmMain frmMain = (frmMain)Application.OpenForms["frmMain"];
                        frmMain.Cerrar_Sesion();
                    }

                }
            }
            else
            {
                MessageBox.Show("No hay caja abierta para realizar el cierre", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarFiltros()
        {
            lstProductos = lProducto.ObtenerProductosList();
            List<string> listaProductosFiltro = new List<string>();
            foreach (Producto prod in lstProductos)
            {
                // Verificar si el producto ya fue agregado a la lista
                if (!listaProductosFiltro.Contains(prod.Nombre))
                {
                    if (prod.Estado)
                    {
                        listaProductosFiltro.Add(prod.Nombre);
                    }
                }
            }
            ConfigurarAutoCompletarTextBox(txtProducto, listaProductosFiltro);
        }

        private void ConfigurarAutoCompletarTextBox(TextBox textBox, List<string> lista)
        {
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            autoComplete.AddRange(lista.ToArray());

            textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox.AutoCompleteCustomSource = autoComplete;
        }

        private void CalcularTotal()
        {
            if (dgvVenta.Rows.Count == 0)
            {
                txtTotal.Text = "$ 0.00";  // Valor por defecto si no hay productos
                
            }
            else
            {
                decimal total = 0;
                foreach (DataGridViewRow fila in dgvVenta.Rows)
                {
                    // Verifica si el valor es numérico antes de convertirlo
                    if (decimal.TryParse(fila.Cells["dgvcSubTotal"].Value.ToString(), out decimal valor))
                    {
                        total += valor;
                    }
                }

                txtTotal.Text = "$ " + uiUtilidades.FormatearMonedaString(total);  // Formatear el total en moneda argentina
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if(dgvVenta.Rows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("¿Desea cancelar y limpiar la venta?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    LimpiarVenta();
                }
            }
        }

        private void txtProducto_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProducto.Text))
            {
                txtProducto.BackColor = Color.White;
                LimpiarCampos();
            }
        }

        private void LimpiarCampos()
        {
            productoSeleccionado = null;
            txtProducto.Text = string.Empty;
        }

        private void LimpiarVenta()
        {
            dgvVenta.Rows.Clear();
            txtProducto.Text = "";
            txtProducto.BackColor = Color.White;
            CalcularTotal();
        }

        private void txtProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            uiUtilidades.SoloMayusculasYNumeros(e);
        }
    }
}
