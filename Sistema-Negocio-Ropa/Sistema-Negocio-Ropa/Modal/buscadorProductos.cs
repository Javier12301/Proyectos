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

        // Variables estáticas para almacenar los datos en memoria
        private static List<string> equiposCache = null;

        private Timer delayTimer;


        public Producto _productoSeleccionado { get; set; }

        public buscadorProductos()
        {
            InitializeComponent();
            lProducto = new ProductoDA();
            _productoSeleccionado = new Producto();

            // Inicializar el Timer
            delayTimer = new Timer();
            delayTimer.Interval = 50;  // Establece el retraso en milisegundos (500ms = 0.5 segundos)
            delayTimer.Tick += DelayTimer_Tick;
        }

        private void buscadorProductos_Load(object sender, EventArgs e)
        {
            // Crear y configurar el temporizador
            Timer delaytimerLoad = new Timer();
            delaytimerLoad.Interval = 10; // 500 milisegundos = 0.5 segundos
            delaytimerLoad.Tick += (sender2, e2) =>
            {
                Cursor.Current = Cursors.WaitCursor;
                delaytimerLoad.Stop(); // Detener el temporizador para evitar ejecuciones repetidas
                try
                {
                    // Puntero load
                    CargarLista();
                    CargarFiltros();  // Carga desde cache o base de datos
                    InicializarDiseñoTabla();
                    txtBuscar.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            };

            delaytimerLoad.Start(); // Iniciar el temporizador
        }

        private bool isUserSelection = false;

        private void CargarFiltros()
        {
            isUserSelection = false;  // Desactivar eventos durante la carga de los filtros

            List<Categoria> listaCategorias = lProducto.ListarCategoriasenProducto();
            cmbFiltroCategoria.Items.Add("Todos");
            foreach (var categoria in listaCategorias)
            {
                if (!cmbFiltroCategoria.Items.Contains(categoria.Nombre))
                {
                    cmbFiltroCategoria.Items.Add(categoria.Nombre);
                }
            }
            cmbFiltroCategoria.SelectedIndex = 0;

            equiposCache = lProducto.EquiposEnProductos();  // Obtener de la base de datos si es la primera vez
            
            cmbEquipo.Items.Clear();
            cmbEquipo.Items.Add("Todos");
            foreach (string equipo in equiposCache)
            {
                cmbEquipo.Items.Add(equipo);
            }
            cmbEquipo.SelectedIndex = 0;

            isUserSelection = true;  // Reactivar eventos una vez cargados los filtros
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
           CargarLista();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Reiniciar el Timer cada vez que el texto cambia
            delayTimer.Stop();  // Detener cualquier ejecución previa del Timer
            delayTimer.Start();  // Reiniciar el Timer
        }

        // Este evento se ejecuta cuando el Timer completa el intervalo
        private void DelayTimer_Tick(object sender, EventArgs e)
        {
            delayTimer.Stop();  // Detener el Timer para que no se ejecute repetidamente
            CargarLista();  // Cargar la lista después del retraso
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

        private void InicializarDiseñoTabla()
        {
            // Evita errores si no hay columnas
            if (dgvProductos.Columns.Count == 0) return;
            dgvProductos.Columns["ID"].Visible = false;
            dgvProductos.Columns["Stock"].Visible = true;
            dgvProductos.Columns["Cant. Minima"].Visible = false;
            dgvProductos.Columns["Estado"].Visible = false;
            // Configuración de columnas
            dgvProductos.Columns["Producto"].Width = 350; // Columna grande para productos
            dgvProductos.Columns["Categoria"].Width = 150; // Columna mediana para categorías
            dgvProductos.Columns["Talle"].Width = 50; // Columna pequeña para talles
            dgvProductos.Columns["Equipo"].Width = 150; // Columna mediana para equipos
            dgvProductos.Columns["Precio"].Width = 80; // Columna pequeña para precios
            dgvProductos.Columns["Stock"].Width = 80; // Columna pequeña para stock

            // Configurar estilos predeterminados
            dgvProductos.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 9), // Fuente estándar
                Alignment = DataGridViewContentAlignment.MiddleLeft, // Alineación por defecto
                Padding = new Padding(2) // Espaciado interno
            };

            // Encabezados de columna
            dgvProductos.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                Alignment = DataGridViewContentAlignment.MiddleCenter // Centrar encabezados
            };

            // Altura fija para todas las filas
            dgvProductos.RowTemplate.Height = 25;

            // Desactivar ajustes dinámicos para mejorar rendimiento
            dgvProductos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.AllowUserToResizeColumns = false; // Opcional: fijar columnas
        }



        private BindingSource bsProducto = new BindingSource();
        private DataTable dtProducto = new DataTable();

        private void CargarLista()
        {
            dtProducto.Clear();
            dtProducto = lProducto.ObtenerProductosFiltrados(txtBuscar.Text, string.Empty, string.Empty, cmbEquipo.Text, "Activo", cmbFiltroCategoria.Text);
            // Asigna la fuente de datos
            bsProducto.DataSource = dtProducto;
            dgvProductos.DataSource = bsProducto;
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

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUserSelection)  // Solo cargar la lista si el usuario cambia la selección
            {
                CargarLista();
            }
        }

        private void cmbEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUserSelection)  // Solo cargar la lista si el usuario cambia la selección
            {
                CargarLista();
            }
        }

        private void cmbFiltroCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUserSelection)
            {
                CargarLista();
            }
        }
    }
}
