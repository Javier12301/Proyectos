using Datos.Negocio;
using DocumentFormat.OpenXml.Presentation;
using Negocio.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Negocio_Ropa.Modal
{
    public partial class mdAjustarPrecioUnidad : Form
    {
        private ProductoDA lProducto;
        private Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        private List<Producto> lstProductos { get; set; }
        public mdAjustarPrecioUnidad()
        {
            InitializeComponent();
            lProducto = new ProductoDA();
            lstProductos = new List<Producto>();
        }

        private void mdAjustarPrecioUnidad_Load(object sender, EventArgs e)
        {
            CargarFiltros();
            txtProducto.Focus();
        }

        private void CargarFiltros()
        {
            lstProductos = lProducto.ObtenerProductosList();
            List<string> listaProductosFiltro = new List<string>();
            foreach(Producto prod in lstProductos)
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

        

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SeleccionarProducto();
                e.SuppressKeyPress = true; // Evitar el sonido de "ding" al presionar Enter
            }
        }


        private void txtProducto_Leave(object sender, EventArgs e)
        {
            SeleccionarProducto();          
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
                    txtPrecio.Text = productoSeleccionado.PrecioVenta.ToString();
                    txtNuevoPrecio.Focus();
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
            txtPrecio.Text = string.Empty;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (productoSeleccionado != null)
            {
                if (string.IsNullOrEmpty(txtNuevoPrecio.Text))
                    txtNuevoPrecio.Text = "0.00";
                txtNuevoPrecio.Text = string.Format(CultureInfo.GetCultureInfo("es-AR"), "{0:N2}", Convert.ToDecimal(txtNuevoPrecio.Text));

                if (MessageBox.Show($"¿Está seguro de actualizar el precio del producto {productoSeleccionado.Nombre}?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {             
                        bool resultado = ActualizarPrecio();
                        if (resultado)
                        {
                            MessageBox.Show("Precio actualizado correctamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // mensaje si el usuario desea actualizar otro producto
                            DialogResult dialogResult = MessageBox.Show("¿Desea actualizar otro producto?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if(dialogResult == DialogResult.Yes)
                            {
                                LimpiarCampos();
                                txtProducto.Focus();
                            }
                            else
                            {
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error al actualizar el precio", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}, contacte con el adminsitrador del sistema", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool ActualizarPrecio()
        {
            if(productoSeleccionado != null)
            {
                productoSeleccionado.PrecioVenta = Convert.ToDecimal(txtNuevoPrecio.Text);
                return lProducto.ActualizarPrecio(productoSeleccionado.ProductoID, productoSeleccionado.PrecioVenta);
            }
            return false;
        }


        // Manejo de interfaz
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

        private void TextboxMoneda_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            uiUtilidades.TextboxMoneda_KeyPress(textbox, e, errorProvider);
        }

        private void TextboxMoneda_Leave(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            uiUtilidades.TextboxMoneda_Leave(textbox, e);
        }

        private void txtNuevoPrecio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        
    }
}
