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

            dtpInicio.Value = DateTime.Now.AddYears(-5);
            dtpFin.Value = DateTime.Now;
        }

        private BindingSource bsCompra = new BindingSource();
        private DataTable dtCompra = new DataTable();
        private DateTime lastMessageTime = DateTime.MinValue;
        private TimeSpan messageInterval = TimeSpan.FromMinutes(1); // Intervalo de 1 minuto
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

                // Verificar si el DataTable está vacío
                if (dtCompra.Rows.Count == 0)
                {
                    DateTime now = DateTime.Now;
                    if (now - lastMessageTime > messageInterval)
                    {
                        MessageBox.Show("No se encontraron resultados para su búsqueda.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lastMessageTime = now;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnDetalles_Click(object sender, EventArgs e)
        {

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
    }
}
