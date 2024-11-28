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
    public partial class mdDetalleVenta : Form
    {
        VentaDA lVenta = new VentaDA();
        Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        UsuarioDA lUsuario = new UsuarioDA();
        ProductoDA lProducto = new ProductoDA();

        private VentaM oVenta { get; set; }
        public mdDetalleVenta(VentaM venta)
        {
            InitializeComponent();
            oVenta = venta;
        }

        private void mdDetalleVenta_Load(object sender, EventArgs e)
        {
            try
            {
                CargarLista();
                if(dgvDetalleVenta.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron detalles de venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                CargarFormulario();
                CargarTotal();
            }catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        NegocioDA lNegocio = new NegocioDA();
        private void CargarFormulario()
        {
            if(oVenta.VentaID >= 0)
            {
                txtFecha.Text = oVenta.Fecha.ToString("dd/MM/yyyy");
                txtTipoComprobante.Text = oVenta.TipoComprobante;

                oVenta.oUsuario = lUsuario.ObtenerUsuarioPorID(oVenta.oUsuario.UsuarioID);
                txtNombreyApellido.Text = oVenta.oUsuario.ObtenerNombreCompleto();
                txtUsuario.Text = oVenta.oUsuario.ObtenerNombreUsuario();
                txtFolio.Text = oVenta.VentaID.ToString();
                pbCancelado.Visible = !oVenta.Estado;
                btnCancelar.Visible = oVenta.Estado;
                MetodoPago oMetodoPago = lNegocio.ObtenerMetodoPago(oVenta.oMetodoPago.MetodoPagoID);
                txtMetodoPago.Text = oMetodoPago.Nombre;
                txtRecargo.Text = "% " + oVenta.Recargo.ToString();
            }
        }

        private BindingSource bsDetalleVenta = new BindingSource();
        private DataTable dtDetalleVenta = new DataTable();
        private void CargarLista()
        {
            if (oVenta.VentaID>= 0)
            {
                dtDetalleVenta.Clear();  // Limpiar el DataTable existente
                dtDetalleVenta = lVenta.ObtenerDetalleVenta(oVenta.VentaID);

                // Asigna la fuente de datos
                bsDetalleVenta.DataSource = dtDetalleVenta;
                dgvDetalleVenta.DataSource = bsDetalleVenta;
                // esconde la columna ID
                dgvDetalleVenta.Columns["ID"].Visible = false;
            }
            else
            {
                MessageBox.Show("No se ha seleccionado una compra válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void CargarTotal()
        {
            decimal subtotal = 0;
            // revisar primero si hay filas
            if (dgvDetalleVenta.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
                {
                    subtotal += Convert.ToDecimal(row.Cells["Precio de Venta"].Value) * Convert.ToInt32(row.Cells["Cantidad Vendida"].Value);
                }
                txtSubTotal.Text = $"$ {uiUtilidades.FormatearMonedaString(subtotal)}";
            }
            else
            {
                txtSubTotal.Text = "$ 0.00";
            }

            decimal recargo = oVenta.Recargo / 100;
            decimal total = subtotal * (1 + recargo);
            txtTotal.Text = $"$ {uiUtilidades.FormatearMonedaString(total)}";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(oVenta != null)
            {
                if (oVenta.Estado)
                {
                    try
                    {
                        DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar la venta?", "Cancelar venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if(result == DialogResult.Yes)
                        {
                            int productoID = 0;
                            int cantidadVendida = 0;
                            foreach(DataGridViewRow row in dgvDetalleVenta.Rows)
                            {
                                productoID = Convert.ToInt32(row.Cells["ID"].Value);
                                cantidadVendida = Convert.ToInt32(row.Cells["Cantidad Vendida"].Value);
                                // actualizar stock
                                lProducto.ActualizarStock(productoID, cantidadVendida);
                                pbCancelado.Visible = true;
                            }
                            lVenta.CancelarVenta(oVenta.VentaID);
                            MessageBox.Show("Venta cancelada correctamente.", "Venta cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);   
                            btnCancelar.Enabled = false;
                        }
                    }catch(Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
        }

        private void btnExportarP_Click(object sender, EventArgs e)
        {
            try
            {
                uiUtilidades.ExportarDataGridViewAExcel(dgvDetalleVenta, "Detalle de venta", "Detalle de venta", "Detalle de venta");
            }catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
