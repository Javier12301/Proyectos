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

namespace Sistema_Negocio_Ropa.Principales
{
    public partial class frmNegocio : Form
    {
        NegocioDA lNegocio;
        Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        public frmNegocio()
        {
            InitializeComponent();
            lNegocio = new NegocioDA();
        }

        private void frmNegocio_Load(object sender, EventArgs e)
        {
            try
            {
                cmbTipoDocumento.SelectedIndex = 0;
                CargarDatosNegocio();
                CargarMetodosPago();


            }catch(Exception ex)
            {
                MessageBox.Show("Error al cargar el formulario de negocio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    NegocioM negocio = new NegocioM();
                    negocio.Nombre = txtNombreNegocio.Text;
                    negocio.Direccion = txtDireccion.Text;
                    negocio.Ciudad = txtCiudad.Text;
                    negocio.CodigoPostal = txtCodigoPostal.Text;
                    negocio.NombreCompletoPropietario = txtNombreyApellido.Text;
                    negocio.Telefono = txtTelefonoCelular.Text;
                    negocio.TipoDocumento = cmbTipoDocumento.Text;
                    negocio.Documento = txtDocumento.Text;
                   bool resultado = lNegocio.ActualizarDatosNegocio(negocio);
                    foreach (MetodoPago metodo in listMetodopago)
                    {
                        if (metodo.MetodoPagoID == 1)
                        {
                            metodo.Recargo = Convert.ToDecimal(txtEfectivo.Text);
                        }
                        if (metodo.MetodoPagoID == 2)
                        {
                            metodo.Recargo = Convert.ToDecimal(txtDebito.Text);
                        }
                        if (metodo.MetodoPagoID == 3)
                        {
                            metodo.Recargo = Convert.ToDecimal(txtCredito.Text);
                        }
                        if (metodo.MetodoPagoID == 4)
                        {
                            metodo.Recargo = Convert.ToDecimal(txtTransferencia.Text);
                        }
                       resultado &= lNegocio.ActualizarMetodosPago(metodo);
                    }
                    if (resultado)
                    {
                        MessageBox.Show("Datos del negocio actualizados con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar los datos del negocio. Inténtalo de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Complete los campos obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al guardar los datos del negocio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            bool camposValidos = true;
            camposValidos = uiUtilidades.VerificarTextbox(txtNombreNegocio, errorProvider, lblNombreNegocio);
            camposValidos = uiUtilidades.VerificarTextbox(txtDireccion, errorProvider, lblDireccion);
            camposValidos = uiUtilidades.VerificarTextbox(txtCiudad, errorProvider, lblCiudad);
            camposValidos = uiUtilidades.VerificarTextbox(txtCodigoPostal, errorProvider, lblCodigoPostal);
            camposValidos = uiUtilidades.VerificarTextbox(txtNombreyApellido, errorProvider, lblNombreCompleto);
            camposValidos = uiUtilidades.VerificarTextbox(txtTelefonoCelular, errorProvider, lblCelular);
            camposValidos = uiUtilidades.VerificarTextbox(txtDocumento, errorProvider, lblDocumento);
            // recargos, que sean números enteros, positivos y mayores a 0
            camposValidos = uiUtilidades.VerificarTextbox(txtEfectivo, errorProvider, lblEfectivo);
            camposValidos = uiUtilidades.VerificarTextbox(txtDebito, errorProvider, lblTarjetaDebito);
            camposValidos = uiUtilidades.VerificarTextbox(txtCredito, errorProvider, lblCredito);
            camposValidos = uiUtilidades.VerificarTextbox(txtTransferencia, errorProvider, lblTransferencia);

            return camposValidos;
        }

        private void CargarDatosNegocio()
        {
            NegocioM negocio = lNegocio.CargarDatosNegocio();
            if(negocio != null)
            {
                txtNombreNegocio.Text = negocio.Nombre;
                txtDireccion.Text = negocio.Direccion;
                txtCiudad.Text = negocio.Ciudad;
                txtCodigoPostal.Text = negocio.CodigoPostal;
                // datos del propietario
                txtNombreyApellido.Text = negocio.NombreCompletoPropietario;
                txtTelefonoCelular.Text = negocio.Telefono;
                // hacer un if si no existe el tipo documento al hacer selected item, poner por defecto select index 0
                if (cmbTipoDocumento.Items.Contains(negocio.TipoDocumento))
                {
                    cmbTipoDocumento.SelectedItem = negocio.TipoDocumento;
                }
                else
                {
                    cmbTipoDocumento.SelectedIndex = 0;
                }
                txtDocumento.Text = negocio.Documento;
            }
            else
            {
                MessageBox.Show("No se pudo cargar los datos del negocio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private List<MetodoPago> listMetodopago { get; set; }
        private void CargarMetodosPago()
        {
            listMetodopago = lNegocio.ObtenerMetodosPagos();
            if(listMetodopago != null)
            {
                foreach (MetodoPago metodo in listMetodopago)
                {
                    if (metodo.MetodoPagoID == 1)
                    {
                        txtEfectivo.Text = metodo.Recargo.ToString("0");
                    }
                    if (metodo.MetodoPagoID == 2)
                    {
                        txtDebito.Text = metodo.Recargo.ToString("0");
                    }
                    if (metodo.MetodoPagoID == 3)
                    {
                        txtCredito.Text = metodo.Recargo.ToString("0");
                    }
                    if (metodo.MetodoPagoID == 4)
                    {
                        txtTransferencia.Text = metodo.Recargo.ToString("0");
                    }
                }
            }
            else
            {
                MessageBox.Show("No se pudo cargar los métodos de pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private Point mousePosicion;
        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mousePosicion = e.Location;
            }
        }

        private void pnlTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int deltaX = e.X - mousePosicion.X;
                int deltaY = e.Y - mousePosicion.Y;
                this.Location = new Point(this.Location.X + deltaX, this.Location.Y + deltaY);
            }
        }

        private void txtEfectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            uiUtilidades.SoloNumero(e);
        }

        private void txtEfectivo_Leave(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (string.IsNullOrEmpty(textbox.Text))
            {
                textbox.Text = "0";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
