using Datos.Negocio;
using Negocio.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Negocio_Ropa.Modal.Venta
{
    public partial class mdCobrar : Form
    {

        PictureBox pbActivo;
        NegocioDA lNegocio;
        Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        public VentaM oVenta { get; set; }
        public bool Imprimir { get; set; }

        public mdCobrar(VentaM venta)
        {
            InitializeComponent();
            lNegocio = new NegocioDA();
            Imprimir = false;
            oVenta = venta;
        }

        private void mdCobrar_Load(object sender, EventArgs e)
        {
            try
            {
                CargarVenta();
                CargarMetodosPagos();
                // clickear pbEfectivo
                pbActivo = pEfectivo;
                pEfectivo.BackColor = Color.FromArgb(3, 82, 123);
                AplicarRecargo();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void CargarVenta()
        {
            if(oVenta == null)
            {
                throw new Exception("No se ha encontrado la venta, contacte con el administrador del sistema para solucionar este problema.");
            }

            lblTotal.Text = uiUtilidades.FormatearMonedaString(oVenta.MontoTotal);
        }

        // funcion click picturebox
        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            if(pbActivo != null)
            {
                pbActivo.BackColor = Color.White;
            }
            // aplicarle el color rgb 3; 82; 123
            pb.BackColor = Color.FromArgb(3, 82, 123);
            // establecer pbActivo a pb
            pbActivo = pb;
            AplicarRecargo();
        }

        private void AplicarRecargo()
        {
            if (oVenta != null)
            {
                decimal total = oVenta.MontoTotal; // Precio base (sin recargo)
                decimal recargo = 0;

                if (pbActivo.Name == pEfectivo.Name)
                {
                    // Obtener el recargo según el MétodoPagoID (1 es Efectivo)
                    recargo = lstMetodos.Where(x => x.MetodoPagoID == 1).FirstOrDefault().Recargo;
                    metodoelegido.Text = "1";
                    txtPagoCon.ReadOnly = false;
                }
                else if (pbActivo.Name == pDebito.Name)
                {
                    // Obtener el recargo según el MétodoPagoID (2 es Crédito)
                    recargo = lstMetodos.Where(x => x.MetodoPagoID == 2).FirstOrDefault().Recargo;
                    metodoelegido.Text = "2";
                    txtPagoCon.ReadOnly = true;
                }
                else if (pbActivo.Name == pCredito.Name)
                {
                    // Obtener el recargo según el MétodoPagoID (3 es Débito)
                    recargo = lstMetodos.Where(x => x.MetodoPagoID == 3).FirstOrDefault().Recargo;
                    metodoelegido.Text = "3";
                    txtPagoCon.ReadOnly = true;
                }
                else if (pbActivo.Name == pTransferencia.Name)
                {
                    // Obtener el recargo según el MétodoPagoID (4 es Transferencia)
                    recargo = lstMetodos.Where(x => x.MetodoPagoID == 4).FirstOrDefault().Recargo;
                    metodoelegido.Text = "4";
                    txtPagoCon.ReadOnly = true;
                }

                // Aplicar el recargo
                decimal totalConRecargo = total * (1 + (recargo / 100));

                // Actualizar el monto total con el recargo aplicado
                lblTotal.Text = "$ " + uiUtilidades.FormatearMonedaString(totalConRecargo);
                txtPagoCon.Text = uiUtilidades.FormatearMonedaString(totalConRecargo);    
                txtPagoCon.SelectAll();
                
            }
            else
            {
                throw new Exception("No se ha encontrado la venta, contacte con el administrador del sistema para solucionar este problema.");
            }
        }

        // Obtener metodos pagos
        List<MetodoPago> lstMetodos = new List<MetodoPago>();
        private void CargarMetodosPagos()
        {
            lstMetodos = lNegocio.ObtenerMetodosPagos();
            if(lstMetodos.Count <= 0)
            {
                throw new Exception("No se encontraron metodos de pago, contacte con el adminsitrador del sistema para solucionar este problema.");
            }

        }

        private void txtPagoCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            uiUtilidades.TextboxMoneda_KeyPress(txtPagoCon, e, errorProvider);
        }

        private void txtPagoCon_Leave(object sender, EventArgs e)
        {
            uiUtilidades.TextboxMoneda_Leave(txtPagoCon, e);
            CalcularCambio();
        }
        
        private void CalcularCambio()
        {
            // obtener el monto total, tener en cuenta que puede tener decimales con , o punto busca la forma que no se rompa el programa y elimina "$" y espacios
            decimal total = Convert.ToDecimal(lblTotal.Text.Replace("$", "").Replace(" ", ""));
            if(decimal.TryParse(txtPagoCon.Text, out decimal pagoCon))
            {
                decimal cambio = pagoCon - total;
                txtCambio.Text = uiUtilidades.FormatearMonedaString(cambio);
            }else if (string.IsNullOrEmpty(txtPagoCon.Text))
            {
                decimal cambio = 0 - total;
                txtCambio.Text = uiUtilidades.FormatearMonedaString(cambio);
            }
            else
            {
                MessageBox.Show("El valor ingresado no es un número un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPagoCon_TextChanged(object sender, EventArgs e)
        {
            CalcularCambio();
        }

        private void btnCobrarSinImprimir_Click(object sender, EventArgs e)
        {
            if (ValidarVenta())
            {
                Imprimir = false;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool ValidarVenta()
        {
            bool resultado = false;
            if(decimal.TryParse(txtPagoCon.Text, out decimal pagoCon))
            {
                decimal cambio = Convert.ToDecimal(txtCambio.Text.Replace("$", "").Replace(" ", ""));

                decimal total = Convert.ToDecimal(lblTotal.Text.Replace("$", "").Replace(" ", ""));

                // verificar que no sea negativo cambio
                if(cambio < 0)
                {
                    MessageBox.Show("El monto ingresado es insuficiente para realizar la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return resultado;
                }

                if(pagoCon < total)
                {
                    MessageBox.Show("El monto ingresado es insuficiente para realizar la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return resultado;
                }
                // si todo esta bien
                oVenta.MontoPagado = pagoCon;
                oVenta.MontoCambio = cambio;
                oVenta.MontoTotal = total;
                oVenta.oMetodoPago = lstMetodos.Where(x => x.MetodoPagoID == Convert.ToInt32(metodoelegido.Text)).FirstOrDefault();
                oVenta.Recargo = oVenta.oMetodoPago.Recargo;
                oVenta.Fecha = DateTime.Now;
                return true;
            }
            else
            {
                MessageBox.Show("El valor ingresado no es un número un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return resultado;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar la venta?", "Cancelar venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
