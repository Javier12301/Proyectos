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

namespace Sistema_Negocio_Ropa.Modal.Inventario
{
    public partial class mdDetallesCompra : Form
    {
        private Compra oCompra { get; set; }
        CompraDA lCompra;
        ProductoDA lProducto;
        UsuarioDA lUsuario;
        Utilidades uiUtilidades = Utilidades.ObtenerInstancia;


        public mdDetallesCompra(Compra compra)
        {
            InitializeComponent();
            oCompra = compra;
            lCompra = new CompraDA();
            lUsuario = new UsuarioDA();
            lProducto = new ProductoDA();
        }


        private void mdDetallesCompra_Load(object sender, EventArgs e)
        {
            try
            {
                CargarLista();
                // verificar si se cargo la tabla, sino cerrar
                if (dgvDetallesCompras.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron detalles de compra.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

                CargarFormulario();
                CargarTotal();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarFormulario()
        {
            if(oCompra.CompraID >= 0)
            {
                // Fecha compra
                txtFecha.Text = oCompra.FechaCompra.ToString("dd/MM/yyyy");
                txtTipoComprobante.Text = oCompra.Tipo_Factura;
                // usuario y nombre completo
                oCompra.oUsuario = lUsuario.ObtenerUsuarioPorID(oCompra.oUsuario.UsuarioID);
                txtNombreyApellido.Text = oCompra.oUsuario.ObtenerNombreCompleto();
                txtUsuario.Text = oCompra.oUsuario.ObtenerNombreUsuario();        
                txtFolio.Text = oCompra.CompraID.ToString();
                pbCancelado.Visible = !oCompra.Estado;
                btnCancelar.Enabled = oCompra.Estado;
            }
        }

        private void CargarTotal()
        {
            decimal total = 0;
            // revisar primero si hay filas
            if (dgvDetallesCompras.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDetallesCompras.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["Precio de Compra"].Value) * Convert.ToInt32(row.Cells["Cantidad Comprada"].Value);
                }
                txtTotal.Text = $"$ {uiUtilidades.FormatearMonedaString(total)}";
            }
            else
            {
                txtTotal.Text = "$ 0.00";
            }
        }



        private BindingSource bsDetalleCompra = new BindingSource();
        private DataTable dtDetalleCompra = new DataTable();
        private void CargarLista()
        {
            if (oCompra.CompraID >= 0)
            {
                dtDetalleCompra.Clear();  // Limpiar el DataTable existente
                dtDetalleCompra = lCompra.ObtenerDetalleCompras(oCompra.CompraID);

                // Asigna la fuente de datos
                bsDetalleCompra.DataSource = dtDetalleCompra;
                dgvDetallesCompras.DataSource = bsDetalleCompra;
                // esconde la columna ID
                dgvDetallesCompras.Columns["ID"].Visible = false;

            }
            else
            {
                MessageBox.Show("No se ha seleccionado una compra válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(oCompra != null)
            {
                if (oCompra.Estado)
                {
                    try
                    {
                        DialogResult resultado = MessageBox.Show("¿Está seguro de cancelar la compra?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (resultado == DialogResult.Yes)
                        {
                            // recorrer y leer ID y "Cantidad Comprada" de cada fila
                            int productoID = 0;
                            int cantidadComprada = 0;
                            foreach (DataGridViewRow row in dgvDetallesCompras.Rows)
                            {
                                productoID = Convert.ToInt32(row.Cells["ID"].Value);
                                cantidadComprada = -Convert.ToInt32(row.Cells["Cantidad Comprada"].Value);
                                // actualizar stock
                                lProducto.ActualizarStock(productoID, cantidadComprada);
                                pbCancelado.Visible = true;
                            }
                            // cancelar la compra
                            lCompra.CancelarCompra(oCompra.CompraID);
                            btnCancelar.Enabled = false;
                            MessageBox.Show("Compra cancelada correctamente.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al cancelar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("La compra ya ha sido cancelada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado una compra válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnExportarP_Click(object sender, EventArgs e)
        {
            try
            {
                // eliminar "/"
                string fecha = txtFecha.Text.Replace("/", "-");
                uiUtilidades.ExportarDataGridViewAExcel(dgvDetallesCompras, $"Detalle de compras -Folio {txtFolio.Text} - Fecha {fecha}", "Detalle Compras", "Inventario");
            }catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error al exportar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
