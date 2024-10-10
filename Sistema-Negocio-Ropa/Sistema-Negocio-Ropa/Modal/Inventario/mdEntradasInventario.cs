using Datos.Negocio;
using Negocio.Negocio;
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
    public partial class mdEntradasInventario : Form
    {

        private ProductoDA lProducto;
        private CompraDA lCompra;
        private Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        private List<Producto> lstProductos { get; set; }
        public mdEntradasInventario()
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
                CargarFiltros();
                txtProducto.Focus();
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

        private void CalcularTotal()
        {
            if (dgvProductos.Rows.Count == 0)
            {
                txtTotal.Text = "0.00";  // Valor por defecto si no hay productos
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

                txtTotal.Text = uiUtilidades.FormatearMonedaString(total);  // Formatear el total en moneda argentina
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
                    listaProductosFiltro.Add(prod.Nombre);
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
                    txtTalle.Text = productoSeleccionado.Talle;
                    txtEquipo.Text = productoSeleccionado.Equipo;
                    txtStockActual.Text = productoSeleccionado.Stock.ToString();
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
            txtTalle.Text = string.Empty;
            txtEquipo.Text = string.Empty;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if(productoSeleccionado != null)
            {

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

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
    }
}
