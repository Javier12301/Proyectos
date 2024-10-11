using Datos.Negocio;
using Negocio.Negocio;
using Sistema_Negocio_Ropa.Modal.Inventario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Negocio_Ropa.Modal
{
    public partial class mdEntradaInventario : Form
    {

        CompraDA lCompra = new CompraDA();
        Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        public mdEntradaInventario()
        {
            InitializeComponent();
        }

        private void mdEntradaInventario_Load(object sender, EventArgs e)
        {
            try
            {
                CargarLista();
                CargarFiltros();
            }catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarFiltros()
        {
            cmbFiltroEstado.Items.Add("Activo");
            cmbFiltroEstado.Items.Add("Cancelado");
            cmbFiltroEstado.Items.Add("Todos");
            cmbFiltroEstado.SelectedIndex = 0;

            dtpInicio.Value = DateTime.Now.AddYears(-5);
            dtpFin.Value = DateTime.Now.AddYears(5);
        }

        private BindingSource bsCompra = new BindingSource();
        private DataTable dtCompra = new DataTable();
        private void CargarLista()
        { 
            try
            {
                dtCompra.Clear();  // Limpiar el DataTable existente
                dtCompra = lCompra.ObtenerComprasFiltradas(cmbFiltroEstado.Text, dtpInicio.Value, dtpFin.Value);

                // Asigna la fuente de datos
                bsCompra.DataSource = dtCompra;
                dgvCompras.DataSource = bsCompra;
                bNavegadorCompras.BindingSource = bsCompra;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var modal = new mdRegistrarCompras())
            {
                if(modal.ShowDialog() == DialogResult.OK)
                {
                    CargarLista();
                }
            }
        }

        private void btnDetalles_Click(object sender, EventArgs e)
        {
            if (dgvCompras.Rows.Count > 0)
            {
                if (dgvCompras.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar una compra para ver los detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tomar el ID de la compra de la celda seleccionada
                int compraID = Convert.ToInt32(dgvCompras.SelectedCells[0].OwningRow.Cells["Folio"].Value);
                DetalleSeleccionado(compraID);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una compra para ver los detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCompras_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Tomar el ID de la compra de la celda doble clickeada
                int compraID = Convert.ToInt32(dgvCompras.Rows[e.RowIndex].Cells["Folio"].Value);
                DetalleSeleccionado(compraID);
            }
        }

        private void DetalleSeleccionado(int CompraID)
        {
            try
            {
                if (dgvCompras.Rows.Count > 0)
                {
                    Compra _compra = lCompra.ObtenerCompraID(CompraID);
                    using (var modal = new mdDetallesCompra(_compra))
                    {
                        modal.ShowDialog();
                        CargarLista();
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado una compra válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                uiUtilidades.ExportarDataGridViewAExcel(dgvCompras, "Compras realizadas", "Compras", "Compras");
            }catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void dtpFin_ValueChanged(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        
    }
}
