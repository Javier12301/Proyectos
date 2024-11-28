using Datos.Negocio;
using Negocio.Negocio;
using Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Negocio_Ropa.Modal.Inventario
{
    public partial class mdRegistrarCompras : Form
    {

        private Sesion lSesion = Sesion.ObtenerInstancia;
        private ProductoDA lProducto;
        private CompraDA lCompra;
        private Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        private List<Producto> lstProductos { get; set; }
        public mdRegistrarCompras()
        {
            InitializeComponent();
            lProducto = new ProductoDA();
            lCompra = new CompraDA();
            lstProductos = new List<Producto>();
        }

        private void mdEntradasInventario_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDatosFormulario();
                CargarFiltros();
                txtProducto.Select();

            } catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void CargarDatosFormulario()
        {
            // obtener folio
            txtFolio.Text = lCompra.ObtenerFolio().ToString();
            dtpFecha.Value = DateTime.Now;
            // combobox de boletas
            cmbTipoDeDocumento.Items.Add("Factura");
            cmbTipoDeDocumento.Items.Add("Boleta");
            cmbTipoDeDocumento.SelectedIndex = 0;
            CalcularTotal();
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

        private void txtProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SeleccionarProducto();
                e.SuppressKeyPress = true; // Evitar el sonido de "ding" al presionar Enter
            }
        }
   

        Producto productoSeleccionado { get; set; }

        private void SeleccionarProducto(bool buscador = false)
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
                    txtStockActual.Text = productoSeleccionado.Stock.ToString();
                    txtNuevaCantidad.Focus();
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

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (var modal = new buscadorProductos())
                {
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
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
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
            txtStockActual.Text = string.Empty;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // apagar errores
            errorProvider.Clear();
            if(productoSeleccionado != null)
            {
                // Validar que la cantidad a cargar sea mayor a 0
                if(int.TryParse(txtNuevaCantidad.Text, out int cantidad) && cantidad > 0)
                {
                    if(decimal.TryParse(txtPrecioCompra.Text, out decimal precioCompra) && precioCompra > 0)
                    {
                        // cargar datos al datagridview
                        Producto _producto = new Producto();
                        _producto = productoSeleccionado;

                        //Verificar si el producto ya fue agregado al datagridview
                        DataGridViewRow existeFila = dgvProductos.Rows
                            .Cast<DataGridViewRow>()
                            .FirstOrDefault(row => Convert.ToInt32(row.Cells["dgvcID"].Value) == _producto.ProductoID);

                        if (existeFila != null)
                        {
                            // El producto ya fue agregado, se actualiza la cantidad
                            int cantidadExistente = Convert.ToInt32(existeFila.Cells["dgvcCantidad"].Value);
                            // remplazamos el precio de compra
                            decimal precioExistente = Convert.ToDecimal(existeFila.Cells["dgvcPrecioCompra"].Value);

                            if(precioExistente != precioCompra)
                            {
                                existeFila.Cells["dgvcPrecioCompra"].Value = precioCompra;
                            }
                            int stockNuevo = cantidadExistente + cantidad;
                            existeFila.Cells["dgvcCantidad"].Value = stockNuevo;
                            existeFila.Cells["dgvcSubTotal"].Value = stockNuevo * precioCompra;
                        }
                        else
                        {
                            decimal subTotal = cantidad * precioCompra;
                            dgvProductos.Rows.Add(_producto.ProductoID, _producto.Nombre, cantidad, precioCompra, subTotal);
                        }
                        CalcularTotal();
                    }
                    else
                    {
                        MessageBox.Show("El precio de compra debe ser mayor a 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        errorProvider.SetError(lblPrecioCompra, "El precio de compra debe ser mayor a 0");
                    }
                }
                else
                {
                    MessageBox.Show("La cantidad a cargar debe ser mayor a 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    errorProvider.SetError(lblNuevaCant, "La cantidad a cargar debe ser mayor a 0");
                }
            }
        }

        private void CalcularTotal()
        {
            if (dgvProductos.Rows.Count == 0)
            {
                txtTotal.Text = "$ 0.00";  // Valor por defecto si no hay productos
            }
            else
            {
                decimal total = 0;
                foreach (DataGridViewRow fila in dgvProductos.Rows)
                {
                    // Verifica si el valor es numérico antes de convertirlo
                    if (decimal.TryParse(fila.Cells["dgvcSubTotal"].Value.ToString(), out decimal valor))
                    {
                        total += valor;
                    }
                }

                txtTotal.Text = "$ "+ uiUtilidades.FormatearMonedaString(total);  // Formatear el total en moneda argentina
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvProductos.SelectedCells.Count > 0)
                {
                    DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar el producto seleccionado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(resultado == DialogResult.Yes)
                    {
                        foreach(DataGridViewCell celda in dgvProductos.SelectedCells)
                        {
                            int filaIndex = celda.RowIndex;
                            dgvProductos.Rows.RemoveAt(filaIndex);
                            CalcularTotal();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un producto para eliminar de la lista.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // limpiar todo
            DialogResult resultado = MessageBox.Show("¿Está seguro de limpiar la lista de productos?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(resultado == DialogResult.Yes)
            {
                dgvProductos.Rows.Clear();
                CalcularTotal();
            }
        }

        private Point mousePosicion;
        private void pnlControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int deltaX = e.X - mousePosicion.X;
                int deltaY = e.Y - mousePosicion.Y;
                this.Location = new Point(this.Location.X + deltaX, this.Location.Y + deltaY);
            }
        }

        private void pnlControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mousePosicion = e.Location;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtStockActual.Text))
            {
                txtNuevaCantidad.Text = "1";
            }
        }

        private void txtNuevaCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            uiUtilidades.SoloNumero(e);
        }

        private void textBox1_Leave_1(object sender, EventArgs e)
        {
            uiUtilidades.TextboxMoneda_Leave(txtPrecioCompra, e);
        }

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            uiUtilidades.TextboxMoneda_KeyPress(txtPrecioCompra, e, errorProvider);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProductos.Rows.Count > 0)
                {
                    // Cargar productos del DataGridView
                    List<Producto> lstProductos = new List<Producto>();

                    foreach (DataGridViewRow fila in dgvProductos.Rows)
                    {
                        Producto _producto = new Producto
                        {
                            ProductoID = Convert.ToInt32(fila.Cells["dgvcID"].Value),
                            Nombre = fila.Cells["dgvcNombre"].Value.ToString(),
                            Stock = Convert.ToInt32(fila.Cells["dgvcCantidad"].Value), // Cantidad a incrementar
                            PrecioCompra = Convert.ToDecimal(fila.Cells["dgvcPrecioCompra"].Value)
                        };

                        lstProductos.Add(_producto);
                    }

                    // Crear DataTable para Detalle de Compra
                    DataTable dtDetalleCompra = new DataTable();
                    dtDetalleCompra.Columns.Add("dgvcID", typeof(int));
                    dtDetalleCompra.Columns.Add("dgvcCantidad", typeof(int));
                    dtDetalleCompra.Columns.Add("dgvcPrecioCompra", typeof(decimal));
                    dtDetalleCompra.Columns.Add("dgvcSubTotal", typeof(decimal));

                    foreach (DataGridViewRow fila in dgvProductos.Rows)
                    {
                        dtDetalleCompra.Rows.Add(
                            Convert.ToInt32(fila.Cells["dgvcID"].Value),
                            Convert.ToInt32(fila.Cells["dgvcCantidad"].Value),
                            Convert.ToDecimal(fila.Cells["dgvcPrecioCompra"].Value),
                            Convert.ToDecimal(fila.Cells["dgvcSubTotal"].Value)
                        );
                    }

                    // Crear Compra
                    Compra compra = new Compra
                    {
                        oUsuario = lSesion.UsuarioEnSesion(),
                        Tipo_Factura = cmbTipoDeDocumento.Text,
                        FechaCompra = dtpFecha.Value
                    };

                    // Guardar Compra
                    if (lCompra.RegistrarCompra(compra, dtDetalleCompra))
                    {
                        // Actualizar stock
                        foreach (Producto producto in lstProductos)
                        {
                            bool resultado = lProducto.ActualizarStock(producto.ProductoID, producto.Stock);
                            if (!resultado)
                            {
                                MessageBox.Show($"Error al actualizar el stock del producto: {producto.Nombre}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        MessageBox.Show("Compra registrada correctamente.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No hay productos en la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No hay productos en la lista.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // dialog cancelar
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            uiUtilidades.SoloMayusculasYNumeros(e);
        }
    }
}
