using Datos.Negocio;
using Negocio.Negocio;
using Negocio.Seguridad;
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

namespace Sistema_Negocio_Ropa.Modal.Venta
{
    public partial class mdVentasRealizadas : Form
    {
        VentaDA lVenta = new VentaDA();
        Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        Sesion sesion = Sesion.ObtenerInstancia;
        CajaM oCaja = new CajaM();
        bool habilitarFecha { get; set; }
        public mdVentasRealizadas(CajaM caja, bool filtroFecha = false)
        {
            InitializeComponent();
            habilitarFecha = filtroFecha;
            oCaja = caja;
        }

        private void mdVentasRealizadas_Load(object sender, EventArgs e)
        {
            try
            {
                if(oCaja == null)
                {
                    MessageBox.Show("No se ha encontrado la caja, contacte con el administrador del sistema para solucionar este problema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                if (habilitarFecha)
                {
                    pnlFecha.Visible = true;
                    dtpInicio.Value = DateTime.Now.AddYears(-5);
                    dtpFin.Value = DateTime.Now.AddYears(5);
                }
                if(oCaja.Nota == null)
                {
                    txtNota.Text = "-";
                }
                else
                {
                    txtNota.Text = oCaja.Nota;
                }
                CargarLista();
                CargarFiltros();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            if(pnlFecha.Visible)
            {
                CargarLista();
            }
        }

        private void dtpFin_ValueChanged(object sender, EventArgs e)
        {
            if (pnlFecha.Visible)
            {
                CargarLista();
            }
        }

        private BindingSource bsVenta = new BindingSource();
        private DataTable dtVenta = new DataTable();
        private void CargarLista()
        {
            try
            {
                dtVenta.Clear();  // Limpiar el DataTable existente
                DateTime fechaInicio;
                DateTime fechaFin;
                if (habilitarFecha)
                {
                    fechaInicio = dtpInicio.Value;
                    fechaFin = dtpFin.Value;
                }
                else
                {
                    fechaInicio = DateTime.Now.AddDays(-2);
                    fechaFin = DateTime.Now.AddDays(2);
                }

                // esconder Folio
                dtVenta = lVenta.ObtenerVentasFiltradas(oCaja.CajaID.ToString(),cmbFiltroEstado.Text, fechaInicio, fechaFin);

                // Asigna la fuente de datos
                bsVenta.DataSource = dtVenta;
                dgvVentas.DataSource = bsVenta;
                bNavegadorHistorial.BindingSource = bsVenta;
                dgvVentas.Columns["Folio"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargarFiltros()
        {
            cmbFiltroEstado.Items.Add("Activo");
            cmbFiltroEstado.Items.Add("Cancelado");
            cmbFiltroEstado.Items.Add("Todos");
            cmbFiltroEstado.SelectedIndex = 2;
        }

        private void btnDetalles_Click(object sender, EventArgs e)
        {
            if (dgvVentas.Rows.Count > 0)
            {
                if (dgvVentas.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar una venta para ver los detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tomar el ID de la compra de la celda seleccionada
                int ventaID = Convert.ToInt32(dgvVentas.SelectedCells[0].OwningRow.Cells["Folio"].Value);
                DetalleSeleccionado(ventaID);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una venta para ver los detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dgvVentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Tomar el ID de la compra de la celda doble clickeada
                int ventaID = Convert.ToInt32(dgvVentas.Rows[e.RowIndex].Cells["Folio"].Value);
                DetalleSeleccionado(ventaID);
            }
        }

        private void DetalleSeleccionado(int ventaID)
        {
            try
            {
                if (dgvVentas.Rows.Count > 0)
                {
                    VentaM _venta = lVenta.ObtenerVentaID(ventaID);
                    using (var modal = new mdDetalleVenta(_venta))
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
            uiUtilidades.ExportarDataGridViewAExcel(dgvVentas, "Historial Ventas", "Ventas de Hoy", "Ventas");
        }

        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLista();
        }

        
    }
}
