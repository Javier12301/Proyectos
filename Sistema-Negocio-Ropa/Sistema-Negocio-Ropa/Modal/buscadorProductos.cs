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
    public partial class buscadorProductos : Form
    {
        ProductoDA lProducto;

        public Producto _productoSeleccionado { get; set; }
        public buscadorProductos()
        {
            InitializeComponent();
            lProducto = new ProductoDA();
            _productoSeleccionado = new Producto();
        }

        private void buscadorProductos_Load(object sender, EventArgs e)
        {
            CargarLista();
            txtBuscar.Focus();
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            int filaIndex = dgvProductos.CurrentCell.RowIndex;
            if (filaIndex >= 0)
            {
                SeleccionarProducto(filaIndex);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto de la lista", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SeleccionarProducto(e.RowIndex);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto de la lista", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SeleccionarProducto(int filaIndex)
        {
            if (filaIndex >= 0)
            {
                int productoID = Convert.ToInt32(dgvProductos.Rows[filaIndex].Cells["ID"].Value);
                _productoSeleccionado = lProducto.ObtenerProductoPorIDD(productoID);
                if (_productoSeleccionado != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo seleccionar el producto.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void DiseñoTabla()
        {

            dgvProductos.Columns["ID"].Visible = false;
            dgvProductos.Columns["Stock"].Visible = false;
            dgvProductos.Columns["Cant. Minima"].Visible = false;
            dgvProductos.Columns["Estado"].Visible = false;

            dgvProductos.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvProductos.Columns["Categoria"].Width = 100;
            dgvProductos.Columns["Talle"].Width = 100;
            dgvProductos.Columns["Equipo"].Width = 100;
            dgvProductos.Columns["Precio"].Width = 50;

        }

        private BindingSource bsProducto = new BindingSource();
        private DataTable dtProducto = new DataTable();
        private DateTime lastMessageTime = DateTime.MinValue;
        private TimeSpan messageInterval = TimeSpan.FromMinutes(1); // Intervalo de 1 minuto
        private void CargarLista()
        {
            try
            {
                dtProducto.Clear();  // Limpiar el DataTable existente

                dtProducto = lProducto.ObtenerProductosFiltrados(txtBuscar.Text, string.Empty, string.Empty, string.Empty, "Activo", string.Empty);
                // Asigna la fuente de datos
                bsProducto.DataSource = dtProducto;
                dgvProductos.DataSource = bsProducto;
                DiseñoTabla();
                // Cargar menú de columnas

                // Verificar si el DataTable está vacío
                if (dtProducto.Rows.Count == 0)
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

       
        // Manejo de interfaz
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
