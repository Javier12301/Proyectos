using Datos.Negocio;
using Datos.Seguridad;
using Negocio.Negocio;
using Negocio.Seguridad;
using Sistema_Negocio_Ropa.Modal;
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
    public partial class frmProductos : Form
    {

        private Sesion lSesion = Sesion.ObtenerInstancia;
        private ProductoDA lProducto;
        private CategoriaDA lCategoria;
        private Utilidades uiUtilidades = Utilidades.ObtenerInstancia;

        private Timer delayTimer;


        private Permiso permisosDeUsuario;
        public frmProductos()
        {
            InitializeComponent();
            lProducto = new ProductoDA();
            lCategoria = new CategoriaDA();

            // Inicializar el Timer
            delayTimer = new Timer();
            delayTimer.Interval = 50;  // Establece el retraso en milisegundos (500ms = 0.5 segundos)
            delayTimer.Tick += DelayTimer_Tick;
        }

        private void frmProductos_Load(object sender, EventArgs e)
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
                    CargarPermisos();
                    CargarFiltros();
                    AjustarTamaño();
                    CargarMenuColumnas();
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


        private void AjustarTamaño()
        {
            // datagridview con wrap
            dgvProductos.Columns["Producto"].Width = 250;


        }

        private BindingSource bsProducto = new BindingSource();
        private DataTable dtProducto = new DataTable();
        private void CargarLista()
        {
            
                dtProducto.Clear();  // Limpiar el DataTable existente
                string estado = tsProductosInactivos.Checked ? "Todos" : "Activo";
                dtProducto = lProducto.ObtenerProductosFiltrados(txtBuscar.Text, cmbFiltroBuscar.Text, cmbFiltroTalle.Text, cmbFiltroEquipo.Text, estado ,cmbFiltroCategoria.Text);

                // Asigna la fuente de datos
                bsProducto.DataSource = dtProducto;
                dgvProductos.DataSource = bsProducto;
                bNavegadorProductos.BindingSource = bsProducto;

                // Cargar menú de columnas
                // ocultar Cant. Minima
                dgvProductos.Columns["Cant. Minima"].Visible = false;

            

        }

        private bool menuCargado = false; // Variable para asegurarnos de que solo se ejecute una vez

        private void CargarMenuColumnas()
        {
            if (menuCargado) return; // Si ya se cargó, no hacemos nada

            // Agregar items de las columnas de productos
            foreach (DataGridViewColumn col in dgvProductos.Columns)
            {
                if(col.Name == "Cant. Minima"){
                    continue;
                }
                ToolStripMenuItem item = new ToolStripMenuItem(col.HeaderText)
                {
                    Name = "tsmi" + col.Name,
                    Checked = col.Visible
                };
                item.Click += ColumMenuItem_Click;
                item.CheckedChanged += ColumnMenuItem_CheckedChanged;
                tsMenuProducto.DropDownItems.Add(item);
                if(item.Name == "tsmiID")
                {
                    item.Checked = false;
                }
                if(item.Name == "tsmiEstado")
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

        private void ColumnMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            string columnName = item.Name.Replace("tsmi", "");
            if (dgvProductos.Columns.Contains(columnName))
            {
                dgvProductos.Columns[columnName].Visible = item.Checked;
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
            string nombreFormulario = "formProductos";

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
                permisosDeUsuario.Alta = moduloActual.ListaAcciones.Any(accion => accion.Nombre == "Alta");
                permisosDeUsuario.Modificar = moduloActual.ListaAcciones.Any(accion => accion.Nombre== "Modificar");
                permisosDeUsuario.Baja = moduloActual.ListaAcciones.Any(accion => accion.Nombre == "Baja");
                permisosDeUsuario.Exportar = moduloActual.ListaAcciones.Any(accion => accion.Nombre == "Exportar");
            }

            // Verificar si el módulo 'formCategorias' está permitido
            Modulo moduloCategoria = modulosPermitidos.FirstOrDefault(m => m.Nombre == "formCategorias");
            if (moduloCategoria != null)
            {
                btnCategorias.Enabled = true;
                btnCategorias.Visible = true;
            }
        }

        private void CargarFiltros()
        {
            cmbFiltroBuscar.Items.Clear();
            cmbFiltroCategoria.Items.Clear();
            cmbFiltroTalle.Items.Clear();
            cmbFiltroEquipo.Items.Clear();
            if(cmbFiltroBuscar.Items.Count == 0)
            {
                cmbFiltroBuscar.Items.Add("Todos");
                cmbFiltroBuscar.Items.Add("Nombre");
                cmbFiltroBuscar.Items.Add("Categoría");
                cmbFiltroBuscar.Items.Add("Equipo");
            }
            cmbFiltroBuscar.SelectedIndex = 0;

            List<Categoria> listaCategorias = lProducto.ListarCategoriasenProducto();
            cmbFiltroCategoria.Items.Add("Todos");
            foreach(var categoria in listaCategorias)
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

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (btnCategorias.Enabled)
            {
                using (var modal = new mdCategoria())
                {
                    Cursor.Current = Cursors.Default;
                    var resultado = modal.ShowDialog();
                    if (resultado == DialogResult.OK)
                    {
                        CargarLista();
                        CargarFiltros();
                    }
                }
            }
        }

        
        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            // activar o desactivar
            tsProductosInactivos.Checked = !tsProductosInactivos.Checked;
            CargarLista();
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

        private void cmbFiltroBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            delayTimer.Stop();  // Detener cualquier ejecución previa del Timer
            delayTimer.Start();  // Reiniciar el Timer
        }

        private void DelayTimer_Tick(object sender, EventArgs e)
        {
            delayTimer.Stop();  // Detener el Timer para que no se ejecute repetidamente
            CargarLista();  // Cargar la lista después del retraso
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarLista();
        }


        private void dgvProductos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Comprobar si la celda es mayor a 0
            if (e.RowIndex >= 0)
            {
                if (dgvProductos.Rows[e.RowIndex].Cells["Estado"].Value.ToString() == "True")
                {
                    lblEstado.Text = "Activo";
                    lblEstado.ForeColor = Color.Blue;
                }
                else
                {
                    lblEstado.Text = "Inactivo";
                    lblEstado.ForeColor = Color.Red;
                }
            }
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

        private void toolStripDropDownButton1_DropDownClosed(object sender, EventArgs e)
        {
            tsdVer.ForeColor = Color.White;

        }

        private void toolStripDropDownButton1_DropDownOpened(object sender, EventArgs e)
        {
            tsdVer.ForeColor= Color.Black;
        }

        private void btnNuevoP_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (permisosDeUsuario.Alta)
            {
                using (var modal = new mdProducto())
                {
                    Cursor.Current = Cursors.Default;
                    var resultado = modal.ShowDialog();
                    if (resultado == DialogResult.OK)
                    {
                        CargarLista();
                        CargarFiltros();
                    }
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos para realizar esta acción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnModificarP_Click(object sender, EventArgs e)
        {
            AbrirModalModificar();
        }

        private void dgvProducto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Se debe ignorar la celda de cabecera
            if (e.RowIndex >= 0)
            {
                AbrirModalModificar();
            }
        }

        private void AbrirModalModificar()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (permisosDeUsuario.Modificar)
                {
                    if (dgvProductos.CurrentCell != null)
                    {
                        int filaIndex = dgvProductos.CurrentCell.RowIndex;
                        int productoID = Convert.ToInt32(dgvProductos.Rows[filaIndex].Cells["ID"].Value.ToString());
                        if (productoID > 0)
                        {
                            using (var modal = new mdProducto(true, productoID))
                            {
                                var resultado = modal.ShowDialog();
                                if (resultado == DialogResult.OK)
                                {
                                    CargarLista();
                                    CargarFiltros();

                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se ha seleccionado ningún producto. Por favor, seleccione un producto de la lista e inténtelo de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No tiene permiso para realizar esta acción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnEliminarP_Click(object sender, EventArgs e)
        {
            BajaProducto();

        }

        private void dgvProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                BajaProducto();
            }
        }

        private void BajaProducto()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (!permisosDeUsuario.Baja)
                {
                    MessageBox.Show("No tiene permiso para realizar esta acción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dgvProductos.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Seleccione por lo menos un producto de la lista antes de intentar darlo de baja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult resultado = MessageBox.Show("¿Estás seguro que deseas eliminar los productos seleccionados?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado != DialogResult.Yes)
                    return;

                List<int> productosAEliminar = new List<int>();

                foreach (DataGridViewCell celda in dgvProductos.SelectedCells)
                {
                    int productoID = Convert.ToInt32(dgvProductos.Rows[celda.RowIndex].Cells["ID"].Value);

                    if (productoID > 0)
                    {
                        productosAEliminar.Add(productoID);
                    }
                    else
                    {
                        MessageBox.Show("Seleccione un producto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                }

                int productosEliminados = 0;
                StringBuilder errores = new StringBuilder();

                foreach (int productoID in productosAEliminar)
                {
                    try
                    {
                        if (lProducto.BajaProductoD(productoID))
                        {
                            productosEliminados++;
                        }
                    }
                    catch (Exception ex)
                    {
                        errores.AppendLine($"Error al intentar eliminar el producto con ID {productoID}: {ex.Message}");
                        continue;
                    }
                }

                if (productosEliminados > 0)
                {
                    MessageBox.Show(productosEliminados > 1 ? $"Se eliminaron {productosEliminados} productos con éxito." : "Producto eliminado con éxito.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (errores.Length > 0)
                {
                    MessageBox.Show(errores.ToString(), "Errores durante la eliminación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                CargarLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error general: {ex.Message}", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        private void btnExportarP_Click(object sender, EventArgs e)
        {
            if (permisosDeUsuario.Exportar)
            {
                uiUtilidades.ExportarDataGridViewAExcel(dgvProductos, "Lista de Productos", "Productos", "Productos");
            }
            else
            {
                MessageBox.Show("No tiene permisos para realizar esta acción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAjustarPrecio_Click(object sender, EventArgs e)
        {
            if (permisosDeUsuario.Modificar)
            {
                using (var modal = new mdAjustePrecio())
                {
                    // Mostrar el modal y verificar si el resultado es OK
                    if (modal.ShowDialog() == DialogResult.OK)
                    {
                        string tipoAjusteSeleccionado = modal.TipoSeleccionado; // Leer el tipo seleccionado

                        switch (tipoAjusteSeleccionado)
                        {
                            case "Unidad":
                                using (var modalU = new mdAjustarPrecioUnidad())
                                {
                                    modalU.ShowDialog(); // Abrir el modal para ajustar precio por unidad
                                }
                                break;
                            case "Categoria":
                                // Lógica para ajustar precio por categoría
                                try
                                {
                                    using (var modalC = new mdAjustarPrecioCategoria())
                                    {
                                        modalC.ShowDialog(); // Abrir el modal para ajustar precio por categoría
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error al intentar ajustar precios por categoría: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                finally
                                {
                                    Cursor.Current = Cursors.Default;
                                }
                                break;
                            default:
                                break;
                        }

                        CargarLista(); // Actualizar la lista o realizar cualquier otra acción necesaria
                    }
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos para realizar esta acción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
