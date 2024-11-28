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

namespace Sistema_Negocio_Ropa.Modal.Caja
{
    public partial class mdAperturaCaja : Form
    {

        Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        Sesion sesion = Sesion.ObtenerInstancia;
        public Negocio.Negocio.CajaM _caja { get; set; }
        CajaDA lCaja;
        public mdAperturaCaja()
        {
            InitializeComponent();
            _caja = new Negocio.Negocio.CajaM();
            lCaja = new CajaDA();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // intentar transformar usando parse a txtMontoinicial
            decimal montoInicial;
            if (decimal.TryParse(txtMontoInicial.Text, out montoInicial))
            {
                Usuario usuario = sesion.UsuarioEnSesion();
                _caja.oUsuario = usuario;
                _caja.MontoInicial = montoInicial;
                _caja.Nota = "-";
                _caja = lCaja.AbrirCaja(_caja);

                if (_caja != null)
                {
                    MessageBox.Show("Caja abierta con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo abrir la caja. Inténtalo de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Monto inicial no válido. Por favor ingrese un número válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void mdAperturaCaja_Load(object sender, EventArgs e)
        {

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
