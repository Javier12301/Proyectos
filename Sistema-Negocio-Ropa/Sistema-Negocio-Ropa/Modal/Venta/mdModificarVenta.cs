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

namespace Sistema_Negocio_Ropa.Modal.Venta
{
    public partial class mdModificarVenta : Form
    {
        ProductoDA lProducto;
        Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        public Producto _producto { get; set; }
        public mdModificarVenta(int productoID, int cantidad)
        {
            InitializeComponent();
            lProducto = new ProductoDA();
            _producto = new Producto();
            _producto.ProductoID = productoID;
        }

        private void mdModificarVenta_Load(object sender, EventArgs e)
        {
            try
            {
                // obtener producto
                CargarDatos();
                txtCantidad.Select();                             

            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                this.Close();
            }
        }

        public int cantidad_elegida { get; set; }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            if(_producto != null)
            {
                cantidad_elegida = 0;   
                int existencias = Convert.ToInt32(txtExistencias.Text);
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                
                if(cantidad > existencias)
                {
                    errorProvider.SetError(lblCantidad, "La cantidad no puede ser mayor a la existencias");
                    MessageBox.Show("La cantidad de productos no puede ser mayor a las existencias", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cantidad_elegida = cantidad;
                DialogResult = DialogResult.OK;
                this.Close();
                
            }

        }

        private void CargarDatos()
        {
            _producto = lProducto.ObtenerProductoPorIDD(_producto.ProductoID);
            txtProducto.Text = _producto.Nombre;
            txtTalle.Text = _producto.Talle;
            txtEquipo.Text = _producto.Equipo;
            txtPrecio.Text = _producto.PrecioVenta.ToString();
            txtExistencias.Text = _producto.Stock.ToString();

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

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            uiUtilidades.SoloNumero(e);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
