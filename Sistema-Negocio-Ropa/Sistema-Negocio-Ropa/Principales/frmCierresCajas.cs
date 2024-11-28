using Datos.Negocio;
using Negocio.Negocio;
using Sistema_Negocio_Ropa.Modal.Venta;
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
    public partial class frmCierresCajas : Form
    {
        public frmCierresCajas()
        {
            InitializeComponent();
        }

        private void frmCierresCajas_Load(object sender, EventArgs e)
        {
            try
            {
                CargarLista();
                CargarFechas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        CajaDA lCaja = new CajaDA();
        BindingSource bsCierre = new BindingSource();
        DataTable dtCierre = new DataTable();
        private void CargarLista()
        {
            try
            {
                // esconder "CajaID"
                dtCierre.Clear();  // Limpiar el DataTable existente
                dtCierre = lCaja.ObtenerCajaFiltrada("Todos", dtpInicio.Value, dtpFin.Value);
                bsCierre.DataSource = dtCierre;
                dgvCierreCajas.DataSource = bsCierre;
                dgvCierreCajas.Columns[0].Visible = false;
            }catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CargarFechas()
        {
            dtpInicio.Value = DateTime.Now.AddYears(-5);
            dtpFin.Value = DateTime.Now.AddYears(5);
        }

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            CargarLista();

        }

        private void dtpFin_ValueChanged(object sender, EventArgs e)
        {
            CargarLista();

        }

        private void dgvCierreCajas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                int cajaID = Convert.ToInt32(dgvCierreCajas.Rows[e.RowIndex].Cells["CajaID"].Value);
                DetalleSeleccionado(cajaID);
            }
        }

        private void btnDetalles_Click(object sender, EventArgs e)
        {
            if (dgvCierreCajas.Rows.Count > 0)
            {
                if (dgvCierreCajas.SelectedCells.Count == 0)
                {
                    MessageBox.Show("No se ha seleccionado un cierre de caja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int cajaID = Convert.ToInt32(dgvCierreCajas.SelectedCells[0].OwningRow.Cells["CajaID"].Value);
                DetalleSeleccionado(cajaID);
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un cierre de caja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DetalleSeleccionado(int cajaID)
        {
            try
            {
                if(dgvCierreCajas.Rows.Count > 0)
                {
                    CajaM oCaja = lCaja.ObtenerCajaID(cajaID);
                    if (oCaja.Estado)
                    {
                        MessageBox.Show("La caja que está por ingresar todavia no se cerró.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    using (var modal = new mdVentasRealizadas(oCaja))
                    {
                        modal.ShowDialog();
                    }

                }
            }catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
