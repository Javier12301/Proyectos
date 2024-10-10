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

namespace Sistema_Negocio_Ropa.Modal
{
    public partial class mdCategoria : Form
    {


        Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        CategoriaDA lCategoria;
        Sesion lSesion = Sesion.ObtenerInstancia;

        private bool permisoAgregar { get; set; }
        private bool permisoModificar { get; set; }
        private bool permisoEliminar { get; set; }

        private int cantidadAntes { get; set; }
        private int cantidadDespues { get; set; }

        private Categoria oCategoriaPorModificar { get; set; }
        private bool modificandoCategoria { get; set; }

        public mdCategoria()
        {
            InitializeComponent();
            modificandoCategoria = false;
            lCategoria = new CategoriaDA();
        }

        private void mdCategoria_Load(object sender, EventArgs e)
        {
            txtNombre.Select();
            Cursor.Current = Cursors.WaitCursor;
            CargarLista();;
            CargarFiltros();
            CargarPermiso();
            Cursor.Current = Cursors.Default;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (!modificandoCategoria)
                {
                    AltaCategoria();
                }
                else
                {
                    modificarCategoria();
                }
                txtNombre.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool ValidarCampos()
        {
            bool CamposValidos = true;
            CamposValidos &= uiUtilidades.VerificarTextbox(txtNombre,errorProvider, lblNombre);
            return CamposValidos;
        }

        private Categoria CrearCategoria()
        {
            return new Categoria
            {
                Nombre = txtNombre.Text,
                Estado = chkEstado.Checked
            };
        }

        private Categoria CrearCategoriaModificada()
        {
            return new Categoria
            {
                CategoriaID = Convert.ToInt32(txtID.Text),
                Nombre = txtNombre.Text,
                Estado = chkEstado.Checked
            };
        }

        private void AltaCategoria()
        {
            if (ValidarCampos())
            {
                Categoria _categoria = CrearCategoria();
                bool categoriaExiste = lCategoria.ExisteCategoriaD(_categoria.Nombre);
                if (categoriaExiste)
                {
                    errorProvider.SetError(lblNombre, "Ya existe una categoría con ese nombre.");
                    MessageBox.Show("La categoría ingresada ya existe. Por favor, ingrese una categoría diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool resultado = lCategoria.AltaCategoria(_categoria);
                if (resultado)
                {
                    MessageBox.Show("La categoría se ha dado de alta correctamente.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult respuesta = MessageBox.Show("¿Desea seguir agregando categorías?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.No)
                    {
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        CargarLista();
                        AlternarModoEdicion();
                    }
                }
                else
                {
                    MessageBox.Show("No se ha podido dar de alta la categoría.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                }
            }
        }

        private void modificarCategoria()
        {
            if (ValidarCampos())
            {
                Categoria categoria = CrearCategoriaModificada();

                bool categoriaExiste = false;
                if (oCategoriaPorModificar.Nombre != categoria.Nombre)
                {
                    categoriaExiste = lCategoria.ExisteCategoriaD(categoria.Nombre);
                    if (categoriaExiste)
                    {
                        errorProvider.SetError(lblNombre, "Ya existe una categoría con ese nombre.");
                        MessageBox.Show("La categoría ingresada ya existe. Por favor, ingrese una categoría diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                bool resultado = lCategoria.ModificarCategoria(categoria);

                if (resultado)
                {
                    if (lCategoria.CategoriaTieneProductosD(categoria.CategoriaID))
                    {
                        if (categoria.Estado == true)
                        {
                            DialogResult respuesta = MessageBox.Show("La categoría tiene productos asignados. ¿Desea activar todos los productos de la categoría?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta == DialogResult.Yes)
                            {
                                int cantidadProductosHabilitados = lCategoria.HabilitarProductos(categoria.CategoriaID);
                            }
                        }
                        else
                        {
                            DialogResult respuesta = MessageBox.Show("La categoría tiene productos asignados. ¿Desea desactivar todos los productos de la categoría?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta == DialogResult.Yes)
                            {
                                int cantidadProductosDeshabilitados = lCategoria.DeshabilitarProductos(categoria.CategoriaID);
                            }
                        }
                    }
                    MessageBox.Show("La categoría fue modificada con éxito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult respuestaUsuario = MessageBox.Show("¿Desea seguir modificando categorías?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuestaUsuario == DialogResult.No)
                    {
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        modificandoCategoria = false;
                        AlternarModoEdicion();
                        CargarLista();
                    }
                }
                else
                {
                    MessageBox.Show("Error al modificar la categoría. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Baja
        private void bajaCategoria(int categoriaID, string nombre)
        {
            try
            {
                string nombreCategoria = nombre;
                string operacion = string.Empty;
                DialogResult respuesta;
                Operacion categoriaAEliminar;
                // Comprobar si la categoría tiene productos asignados
                if (lCategoria.CategoriaTieneProductosD(categoriaID))
                {
                    respuesta = MessageBox.Show("La categoría seleccionada tiene productos asignados, ¿desea asignar a los productos como sin categoría?", "Sistema", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (DialogResult.Yes == respuesta)
                    {
                        operacion = "AsignarProductosSinCategoria";
                    }
                    else if (DialogResult.No == respuesta)
                    {
                        respuesta = MessageBox.Show("¿Desea eliminar la categoría y los productos asignados a esta?", "Sistema", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (DialogResult.Yes == respuesta)
                        {
                            operacion = "EliminarCategoriaYProductos";
                        }
                        else
                        {
                            MessageBox.Show("Operación cancelada.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Operación cancelada.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                else
                {
                    operacion = "EliminarCategoria";
                }

                categoriaAEliminar = new Operacion
                {
                    ID = categoriaID,
                    Nombre = nombreCategoria,
                    NombreOperacion = operacion
                };

                bool resultado = lCategoria.BajaCategoria(categoriaAEliminar);
                if (resultado)
                {
                    MessageBox.Show("La categoría fue eliminada con éxito.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarLista();
                }
                else
                {
                    MessageBox.Show("Error al eliminar la categoría. Por favor, vuelva a intentarlo y, si el problema persiste, póngase en contacto con el administrador del sistema.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPermiso()
        {
            string nombreFormulario = "formCategorias";
            List<Modulo> modulosPermitidos = lSesion.UsuarioEnSesion().ObtenerModulosPermitidos();

            // Buscar modulo correspondiente del formulario actual
            Modulo moduloActual = modulosPermitidos.FirstOrDefault(m => m.Nombre == nombreFormulario);

            // Si el modulo se encuentra cargar permisos
            if (moduloActual != null)
            {
                permisoAgregar = moduloActual.ListaAcciones.Any(accion => accion.Nombre == "Alta");
                permisoModificar = moduloActual.ListaAcciones.Any(accion => accion.Nombre== "Modificar");
                permisoEliminar = moduloActual.ListaAcciones.Any(accion => accion.Nombre== "Baja");

                if (permisoAgregar)
                {
                    HabilitarCampos();
                }
                else
                {
                    DeshabilitarCampos();
                }

                dgvcModificar.Visible = permisoModificar;
                dgvcEliminar.Visible = permisoEliminar;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este módulo. Por favor, póngase en contacto con el administrador del sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void HabilitarCampos()
        {
            txtNombre.Enabled = true;
            chkEstado.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void DeshabilitarCampos()
        {
            txtNombre.Enabled = false;
            chkEstado.Enabled = false;
            btnGuardar.Enabled = false;
        }


        private void AjustarColumnasDataGridView()
        {
            // Ajustar el ancho de las columnas
            dgvCategorias.Columns["ID"].Width = 10; // Ajusta según sea necesario
            // centrar id
            dgvCategorias.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCategorias.Columns["Nombre"].Width = 100; // Ajusta según sea necesario
            dgvCategorias.Columns["Estado"].Width = 15; // Ajusta según sea necesario

            // Ajustar el ancho de los botones
            dgvCategorias.Columns["dgvcModificar"].Width = 40; // Ajusta según sea necesario
            dgvCategorias.Columns["dgvcEliminar"].Width = 40; // Ajusta según sea necesario

            // Ajustar el modo de ajuste automático de las columnas

            dgvCategorias.Columns["ID"].DisplayIndex = 0;
            dgvCategorias.Columns["Nombre"].DisplayIndex = 1;
            dgvCategorias.Columns["Estado"].DisplayIndex = 2;
            dgvCategorias.Columns["dgvcModificar"].DisplayIndex = 3;
            dgvCategorias.Columns["dgvcEliminar"].DisplayIndex = 4;
        
            dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private BindingSource bs = new BindingSource();
        private DataTable dt = new DataTable();
        private DateTime lastMessageTime = DateTime.MinValue;
        private TimeSpan messageInterval = TimeSpan.FromMinutes(1); // Intervalo de 1 minuto

        private void CargarLista()
        {
            try
            {
                dt.Clear();  // Limpiar el DataTable existente

                dt = lCategoria.ObtenerCategoriasFiltrados(
                    cmbFiltroEstado.Text,
                    txtBusqueda.Text
                );

                // Asigna la fuente de datos
                bs.DataSource = dt;
                dgvCategorias.DataSource = bs;
                bNavegadorCategoria.BindingSource = bs;

                // Ajustar columnas
                AjustarColumnasDataGridView();

                // Verificar si el DataTable está vacío
                if (dt.Rows.Count == 0)
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



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!modificandoCategoria)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro que desea salir?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == resultado)
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                DialogResult resultado = MessageBox.Show("¿Desea cancelar la modificación de la categoría?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == resultado)
                {
                    modificandoCategoria = false;
                    AlternarModoEdicion();
                    MessageBox.Show("Operación cancelada.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void CargarFiltros()
        {
            cmbFiltroEstado.Items.Add("Todos");
            cmbFiltroEstado.Items.Add("Activo");
            cmbFiltroEstado.Items.Add("Inactivo");
            cmbFiltroEstado.SelectedIndex = 0;
        }

        private void AlternarModoEdicion(Categoria oCategoria = null)
        {
            if (oCategoria != null && modificandoCategoria && permisoModificar)
            {
                HabilitarCampos();
                oCategoriaPorModificar = oCategoria;
                txtID.Text = oCategoria.CategoriaID.ToString();
                txtNombre.Text = oCategoria.Nombre;
                chkEstado.Checked = oCategoria.Estado;
                // Boton modificar
                btnGuardar.BackColor = Color.DarkOrange;
                btnGuardar.Text = "Modificar";
                btnCancelar.Text = "Cancelar";
            }
            else if (!permisoModificar)
            {
                MessageBox.Show("No tiene permisos para realizar esta acción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                oCategoriaPorModificar = null;
                // limpiar campos
                uiUtilidades.LimpiarTextbox(txtID, txtNombre);
                chkEstado.Checked = true;
                // cambair el color del botón
                btnGuardar.BackColor = Color.FromArgb(38, 125, 166);
                btnGuardar.Text = "Guardar";
                btnCancelar.Text = "Salir";
                if (permisoAgregar)
                {
                    HabilitarCampos();
                }
                else
                {
                    DeshabilitarCampos();
                }
            }
        }


        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void dgvCategorias_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                // Nombre de la columna
                string nombreColumna = dgvCategorias.Columns[e.ColumnIndex].Name;

                // Botón modificar
                if (nombreColumna == "dgvcModificar" && permisoModificar)
                {
                    // Se crearan los botones, pero su estado de enabled será verificado utilizando los permisos del usuario
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    // Propiedades de la imagen
                    var w = Properties.Resources.modificar_boton_15.Width;
                    var h = Properties.Resources.modificar_boton_15.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    // Dibujar imagen
                    e.Graphics.DrawImage(Properties.Resources.modificar_boton_15, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }

                // Botón eliminar
                if (nombreColumna == "dgvcEliminar" && permisoEliminar)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    // Propiedades de la imagen
                    var w = Properties.Resources.eliminar_boton_15.Width;
                    var h = Properties.Resources.eliminar_boton_15.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;
                    // Dibujar imagen
                    e.Graphics.DrawImage(Properties.Resources.eliminar_boton_15, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + " Contacte con el administrador del sistema.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvCategorias.Columns[e.ColumnIndex].Name == "dgvcModificar" && permisoModificar)
                {
                    // Modificar categoría seleccionada
                    int indice = e.RowIndex;
                    if (indice >= 0)
                    {
                        // Obtener id de la categoría
                        int categoriaID = Convert.ToInt32(dgvCategorias.Rows[indice].Cells["ID"].Value);
                        // Pasar datos de la categoría al formulario
                        Categoria oCategoria = lCategoria.ObtenerCategoriaID(categoriaID);
                        modificandoCategoria = true;
                        AlternarModoEdicion(oCategoria);
                    }
                }
                else if (!permisoModificar)
                {
                    MessageBox.Show("Error: No tiene permisos para realizar esta acción.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (dgvCategorias.Columns[e.ColumnIndex].Name == "dgvcEliminar" && permisoEliminar)
                {
                    int indice = e.RowIndex;
                    if (indice >= 0)
                    {
                        string nombreCategoria = dgvCategorias.Rows[indice].Cells["Nombre"].Value.ToString();
                        int categoriaID = Convert.ToInt32(dgvCategorias.Rows[indice].Cells["ID"].Value);
                        DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar la categoría " + nombreCategoria + "?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (resultado == DialogResult.Yes)
                        {
                            // Verificar si la categoría que se está eliminando está en modo de modificación
                            if (modificandoCategoria && oCategoriaPorModificar != null && oCategoriaPorModificar.CategoriaID == categoriaID)
                            {
                                // Salir del modo de edición
                                AlternarModoEdicion(null);
                            }

                            bajaCategoria(categoriaID, nombreCategoria);
                            CargarLista();
                        }
                    }
                }
                else if (!permisoEliminar)
                {
                    MessageBox.Show("Error: No tiene permisos para realizar esta acción.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + " Contacte con el administrador del sistema.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


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


    }
}
