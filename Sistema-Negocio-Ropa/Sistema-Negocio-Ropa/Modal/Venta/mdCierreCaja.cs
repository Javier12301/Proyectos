using Datos.Negocio;
using Datos.Seguridad;
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

namespace Sistema_Negocio_Ropa.Modal.Venta
{
    public partial class mdCierreCaja : Form
    {
        private CajaM oCaja { get; set; }
        private VentaDA lVenta = new VentaDA();
        private CajaDA lCaja = new CajaDA();
        public mdCierreCaja(CajaM oCaja)
        {
            InitializeComponent();
            this.oCaja = oCaja;
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

        private void mdCierreCaja_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCaja();
                txtRecuento.Select();

            }catch(Exception ex)
            {
                throw new Exception("Ocurrió un error al cargar la ventana de cierre de caja: " + ex.Message);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cerrar la caja?", "Cerrar caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Cerrar la caja
                if (decimal.TryParse(txtRecuento.Text, out decimal montoFinal))
                {
                    oCaja.MontoFinal = montoFinal;
                    oCaja.FechaCierre = DateTime.Now;
                    oCaja.Nota = txtNota.Text;
                    if (string.IsNullOrEmpty(txtNota.Text))
                    {
                        oCaja.Nota = "-";
                    }
                }
                else
                {
                    MessageBox.Show("El monto final ingresado no es un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir del método si el valor no es válido
                }

                bool cerrado = lCaja.CerrarCaja(oCaja);
                if (cerrado)
                {
                    MessageBox.Show("La caja se ha cerrado correctamente, se cerrará sesión.", "Caja cerrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al cerrar la caja. Por favor, inténtelo de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        CajaM totales { get; set; }
        private Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        private UsuarioDA lUsuario = new UsuarioDA();
        private void CargarCaja()
        {

            if(oCaja != null)
            {
                txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Usuario oUsuario = Sesion.ObtenerInstancia.UsuarioEnSesion();
                txtUsuario.Text = oUsuario.Nombre + " " + oUsuario.Apellido;

                // cargar totales
                totales = lCaja.ObtenerTotales(oCaja.CajaID);
                txtMontoInicial.Text = oCaja.MontoInicial.ToString();
                txtEfectivoCobrado.Text = totales.TotalEfectivo.ToString();
                txtCredito.Text = totales.TotalCredito.ToString();
                txtDebito.Text = totales.TotalDebito.ToString();
                txtTransferencia.Text = totales.TotalTransferencia.ToString();
                // sumar todo
                decimal total = totales.TotalEfectivo + totales.TotalCredito + totales.TotalDebito + totales.TotalTransferencia;
                // redondear 2 cifras total
                total = Math.Round(total, 2);
                txtTotal.Text = uiUtilidades.FormatearMonedaString(total);
                oCaja.oUsuario = oUsuario;

                decimal CajaDebeHaberEfectivo = totales.TotalEfectivo + oCaja.MontoInicial;
                CajaDebeHaberEfectivo = Math.Round(CajaDebeHaberEfectivo, 2);
                txtCajaDebeHaber.Text = uiUtilidades.FormatearMonedaString(CajaDebeHaberEfectivo);
                decimal debehaber = Convert.ToDecimal(txtCajaDebeHaber.Text);
                
                if (decimal.TryParse(txtRecuento.Text, out decimal recuento))
                {
                    decimal diferencia = recuento - debehaber;
                    txtDescuadre.Text = uiUtilidades.FormatearMonedaString(diferencia);
                }
                else if (string.IsNullOrEmpty(txtRecuento.Text))
                {
                    decimal diferencia = 0 - debehaber;
                    txtDescuadre.Text = uiUtilidades.FormatearMonedaString(diferencia);
                }
                else
                {
                    MessageBox.Show("El valor ingresado no es un número un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                throw new Exception("No se ha encontrado la caja, contacte con el administrador del sistema para solucionar este problema.");
            }

        }

        private void txtPagoCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            uiUtilidades.TextboxMoneda_KeyPress(txtRecuento, e, errorProvider);
        }

        private void txtPagoCon_Leave(object sender, EventArgs e)
        {
            uiUtilidades.TextboxMoneda_Leave(txtRecuento, e);
        }

        private void txtRecuento_TextChanged(object sender, EventArgs e)
        {
            decimal debehaber = Convert.ToDecimal(txtCajaDebeHaber.Text);
            if (decimal.TryParse(txtRecuento.Text, out decimal recuento))
            {
                decimal diferencia = recuento - debehaber;
                txtDescuadre.Text = uiUtilidades.FormatearMonedaString(diferencia);
            }
            else if (string.IsNullOrEmpty(txtRecuento.Text))
            {
                decimal diferencia = 0 - debehaber;
                txtDescuadre.Text = uiUtilidades.FormatearMonedaString(diferencia);
            }
            else
            {
                MessageBox.Show("El valor ingresado no es un número un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
