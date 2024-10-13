using Datos.Negocio;
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
using Sistema_Negocio_Ropa.Modal;

namespace Sistema_Negocio_Ropa.Principales
{
    public partial class frmInventario : Form
    {
        private Sesion lSesion = Sesion.ObtenerInstancia;
        private ProductoDA lProducto;
        private CategoriaDA lCategoria;
        private Utilidades uiUtilidades = Utilidades.ObtenerInstancia;

        private Permiso permisosDeUsuario;

        public frmInventario()
        {
            InitializeComponent();
            lProducto = new ProductoDA();
            lCategoria = new CategoriaDA();
        }

        private void frmInventario_Load(object sender, EventArgs e)
        {
            try
            {
                CargarLista();
                CargarPermisos();
                CargarFiltros();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                string estado = "Activo";
                dtProducto = lProducto.ObtenerProductosFiltrados(txtBuscar.Text, cmbFiltroBuscar.Text, cmbFiltroTalle.Text, cmbFiltroEquipo.Text, estado, cmbFiltroCategoria.Text);

                // Asigna la fuente de datos
                bsProducto.DataSource = dtProducto;
                dgvInventario.DataSource = bsProducto;
                bNavegadorInventario.BindingSource = bsProducto;

                // Cargar menú de columnas
                CargarMenuColumnas();

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

        private bool menuCargado = false; // Variable para asegurarnos de que solo se ejecute una vez

        private void CargarMenuColumnas()
        {
            if (menuCargado) return; // Si ya se cargó, no hacemos nada

            // Agregar items de las columnas de productos
            foreach (DataGridViewColumn col in dgvInventario.Columns)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(col.HeaderText)
                {
                    Name = "tsmi" + col.Name,
                    Checked = col.Visible
                };
                item.Click += ColumMenuItem_Click;
                item.CheckedChanged += ColumnMenuItem_CheckedChanged;
                tsMenuProducto.DropDownItems.Add(item);
                if (item.Name == "tsmiID")
                {
                    item.Checked = false;
                }
            }

            menuCargado = true; // Marcamos que ya se cargó el menú
        }

        private void ColumMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            item.Checked = !item.Checked;
        }

        private void tsdMostrar_DropDownOpened(object sender, EventArgs e)
        {
            tsdMostrar.ForeColor = Color.Black;
            tsdMostrar.Image = Properties.Resources.checkList_Negro;
        }

        private void tsdMostrar_DropDownClosed(object sender, EventArgs e)
        {
            tsdMostrar.ForeColor = Color.White;
            // cargar de recursos la imagen checlist blanca
            tsdMostrar.Image = Properties.Resources.checkList_Blanco;
        }

        private void ColumnMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            string columnName = item.Name.Replace("tsmi", "");
            if (dgvInventario.Columns.Contains(columnName))
            {
                dgvInventario.Columns[columnName].Visible = item.Checked;
            }
        }

        private void ColumnMenuPadre_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem subItem in item.DropDownItems)
            {
                subItem.Checked = item.Checked;
            }
        }

        private void CargarPermisos()
        {
            permisosDeUsuario = new Permiso();
            List<Modulo> modulosPermitidos = lSesion.UsuarioEnSesion().ObtenerModulosPermitidos();
            string nombreFormulario = "formInventario";

            Modulo moduloProductos = modulosPermitidos.FirstOrDefault(m => m.Nombre == nombreFormulario);

            // Si se encuentra el módulo 'formProductos', obtener las acciones permitidas
            List<Accion> accionesPermitidas = null;
            if (moduloProductos != null)
            {
                accionesPermitidas = moduloProductos.ListaAcciones;
            }

            // Buscar el módulo correspondiente al formulario actual
            Modulo moduloActual = modulosPermitidos.FirstOrDefault(m => m.Nombre == nombreFormulario);

            // Si se encuentra el módulo, cargar las opciones
            if (moduloActual != null)
            {
                foreach (Control control in flpContenedorBotones.Controls)
                {
                    if (control is Button && control.Tag != null)
                    {
                        // Obtener el nombre de la acción desde el Tag del botón
                        string nombreAccionBoton = control.Tag.ToString();

                        // Verificar si el nombre de la acción está en la lista de acciones del módulo
                        bool tienePermiso = moduloActual.ListaAcciones.Any(accion => accion.Nombre == nombreAccionBoton);

                        // Activar o desactivar el botón según los permisos
                        control.Enabled = tienePermiso;
                        control.Visible = tienePermiso;
                    }
                }
                // Cargar permisos disponibles
                permisosDeUsuario.Comprar = moduloActual.ListaAcciones.Any(accion => accion.Nombre == "Comprar");
                permisosDeUsuario.Exportar = moduloActual.ListaAcciones.Any(accion => accion.Nombre == "Exportar");
            }

        }

        private void CargarFiltros()
        {
            cmbFiltroBuscar.Items.Clear();
            cmbFiltroCategoria.Items.Clear();
            cmbFiltroTalle.Items.Clear();
            cmbFiltroEquipo.Items.Clear();
            if (cmbFiltroBuscar.Items.Count == 0)
            {
                cmbFiltroBuscar.Items.Add("Todos");
                cmbFiltroBuscar.Items.Add("Nombre");
                cmbFiltroBuscar.Items.Add("Categoría");
                cmbFiltroBuscar.Items.Add("Equipo");
            }
            cmbFiltroBuscar.SelectedIndex = 0;

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

            // filtro talle
            List<string> listaTalles = lProducto.TallesEnProductos();
            cmbFiltroTalle.Items.Add("Todos");
            foreach (var talle in listaTalles)
            {
                if (!cmbFiltroTalle.Items.Contains(talle))
                {
                    cmbFiltroTalle.Items.Add(talle);
                }
            }
            cmbFiltroTalle.SelectedIndex = 0;

            // filtro equipo
            List<string> listaEquipos = lProducto.EquiposEnProductos();
            cmbFiltroEquipo.Items.Add("Todos");
            foreach (var equipo in listaEquipos)
            {
                if (!cmbFiltroEquipo.Items.Contains(equipo))
                {
                    cmbFiltroEquipo.Items.Add(equipo);
                }
            }
            cmbFiltroEquipo.SelectedIndex = 0;
        }



        private void btnEntradaMasiva_Click(object sender, EventArgs e)
        {
            if (permisosDeUsuario.Comprar)
            {
                using (var modal = new mdEntradaInventario())
                {
                    if (modal.ShowDialog() == DialogResult.OK)
                    {
                        CargarLista();
                    }
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos para realizar esta acción.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalidaMasiva_Click(object sender, EventArgs e)
        {
            if (permisosDeUsuario.Comprar)
            {

            }
            else
            {
                MessageBox.Show("No tiene permisos para realizar esta acción.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportarP_Click(object sender, EventArgs e)
        {
            if (permisosDeUsuario.Exportar)
            {
                try
                {
                    uiUtilidades.ExportarDataGridViewAExcel(dgvInventario, "Lista de inventario", "Hoja de Inventario", "Inventario");
                }catch(Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos para realizar esta acción.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void cmbFiltroTalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void cmbFiltroEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void cmbFiltroCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void cmbFiltroBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarLista();
        }


    }
}
