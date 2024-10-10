using Datos.Seguridad;
using Negocio.Seguridad;
using Sistema_Negocio_Ropa.Modal.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Sistema_Negocio_Ropa.Principales.Seguridad
{
    public partial class frmUsuario : Form
    {
        private GrupoDA grupoDA;
        private UsuarioDA usuarioDA;
        private Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        private Sesion lSesion = Sesion.ObtenerInstancia;

        private Permiso _permisousuario;

        public frmUsuario()
        {
            InitializeComponent();
            grupoDA = new GrupoDA();
            usuarioDA = new UsuarioDA();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                cargarLista();
                cargarFiltros();
                CargarPermisos();

            }catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar la ventana de usuario: " + ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPermisos()
        {
            _permisousuario = new Permiso();
            uiUtilidades.CargarPermisos("formUsuarios", flpContenedorBotones, _permisousuario);
            btnExportarP.Enabled = true;
            btnExportarP.Visible = true;
        }

        private void btnNuevoP_Click(object sender, EventArgs e)
        {
            using (var modal = new mdUsuario())
            {
                var resultado = modal.ShowDialog();
                if (resultado == DialogResult.OK)
                {
                    cargarLista();
                }
            }
        }

        private void btnModificarP_Click(object sender, EventArgs e)
        {
            AbrirModalModificar();
        }

        private void dgvUsuario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                AbrirModalModificar();
            }
        }

        

        private void AbrirModalModificar()
        {
            if (_permisousuario.Modificar)
            {
                try
                {
                    if (dgvUsuario.CurrentCell != null)
                    {
                        int filaIndex = dgvUsuario.CurrentCell.RowIndex;
                        int usuarioID = Convert.ToInt32(dgvUsuario.Rows[filaIndex].Cells["ID"].Value);
                        if (usuarioID > 0)
                        {

                            // No se puede modificar su propio usuario en esta ventana, utilices el módulo de Mis Datos.
                            if (usuarioID == lSesion.UsuarioEnSesion().UsuarioID)
                            {
                                MessageBox.Show("No puede modificar su propio usuario en esta ventana, utilice el módulo de \"Mis datos\".", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (usuarioID == 1)
                            {
                                MessageBox.Show("No puede modificar el usuario Admin.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            using (var modal = new mdUsuario(true, usuarioID))
                            {
                                var resultado = modal.ShowDialog();
                                if (resultado == DialogResult.OK)
                                {
                                    cargarLista();
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Seleccione un usuario para modificar.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos para modificar usuarios.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminarP_Click(object sender, EventArgs e)
        {
            bajaUsuario();
        }

        private void dgvUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                bajaUsuario();
            }
        }

        private void bajaUsuario()
        {
            if (_permisousuario.Baja)
            {
                try
                {
                    // Verificar si hay celdas seleccionadas
                    if (dgvUsuario.SelectedCells.Count == 0)
                    {
                        MessageBox.Show("Seleccione por lo menos un usuario para eliminar.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    List<int> usuariosAEliminar = new List<int>();

                    DialogResult resultado = MessageBox.Show("¿Estás seguro que desea eliminar el/los usuario seleccionados?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado != DialogResult.Yes)
                        return;

                    // Limpiar lista de usuarios.
                    usuariosAEliminar.Clear();

                    // Recorrer las celdas seleccionadas
                    foreach (DataGridViewCell celda in dgvUsuario.SelectedCells)
                    {
                        // Obtener el ID del usuario
                        int usuarioID = Convert.ToInt32(dgvUsuario.Rows[celda.RowIndex].Cells["ID"].Value);

                        // Verificar si el usuario es el mismo que el logueado o si es el administrador
                        if (usuarioID == lSesion.UsuarioEnSesion().UsuarioID)
                        {
                            MessageBox.Show("No puede eliminar su propio usuario.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else if (usuarioID == 1)
                        {
                            MessageBox.Show("No puede eliminar el usuario Admin.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Agregar el ID del usuario a la lista de usuarios a eliminar
                        usuariosAEliminar.Add(usuarioID);
                    }
                    // Eliminar los usuarios después de recorrer todas las celdas
                    int usuariosEliminados = 0;

                    foreach (int usuarioID in usuariosAEliminar)
                    {
                        if (usuarioDA.BajaUsuario(usuarioID))
                            usuariosEliminados++;
                        else
                            MessageBox.Show($"No se pudo eliminar el usuario con ID: {usuarioID}", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // Mostrar mensaje de éxito
                    if (usuariosEliminados > 0)
                    {
                        MessageBox.Show(usuariosEliminados > 1 ? "Usuarios eliminados correctamente." : "Usuario eliminado correctamente.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cargarLista();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos para eliminar usuarios.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnExportarP_Click(object sender, EventArgs e)
        {
            try
            {
                uiUtilidades.ExportarDataGridViewAExcel(dgvUsuario, "Lista de Usuarios", "Informe de Usuarios", "Usuarios");                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Desea limpiar el campo de búsqueda?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                limpiarBusqueda();
                cargarLista();
            }
        }

        private void limpiarBusqueda()
        {
            txtBuscar.Text = string.Empty;
        }


        private BindingSource bindingSource = new BindingSource();
        private DataTable dt = new DataTable(); // Reutilizar instancia de DataTable

        private void cargarLista()
        {
            try
            {
                dt.Clear();  // Limpiar el DataTable existente

                dt = usuarioDA.ObtenerUsuarioFiltrados(
                    cmbFiltroBuscar.Text,
                    txtBuscar.Text,
                    cmbFiltroGrupo.Text,
                    cmbFiltroEstado.Text
                );

                // Asigna la fuente de datos
                bindingSource.DataSource = dt;
                dgvUsuario.DataSource = bindingSource;
                bNavegadorUsuario.BindingSource = bindingSource;

                // Verificar si el DataTable está vacío
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron resultados para su búsqueda.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void cargarFiltros()
        {
            // Filtro de buscar por
            cmbFiltroBuscar.Items.Add("Todos");
            cmbFiltroBuscar.Items.Add("Usuario");
            cmbFiltroBuscar.Items.Add("Nombre");
            cmbFiltroBuscar.Items.Add("Apellido");
            cmbFiltroBuscar.Items.Add("Correo");
            cmbFiltroBuscar.SelectedIndex = 0;

            // Filtro de Grupos
            List<Grupo> listaGrupos = grupoDA.ListarGrupos();
            cmbFiltroGrupo.Items.Add("Todos");
            foreach (var grupo in listaGrupos)
            {
                cmbFiltroGrupo.Items.Add(grupo.Nombre);
            }
            cmbFiltroGrupo.SelectedIndex = 0;

            // Filtro de estados
            cmbFiltroEstado.Items.Add("Todos");
            cmbFiltroEstado.Items.Add("Activo");
            cmbFiltroEstado.Items.Add("Inactivo");
            cmbFiltroEstado.SelectedIndex = 0;
        }

        private void dgvUsuario_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Comprobar si la celda es mayor a 0
            if (e.RowIndex > 0)
            {
                if (dgvUsuario.Rows[e.RowIndex].Cells["Estado"].Value.ToString() == "True")
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            cargarLista();
        }

        private void cmbFiltroBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarLista();
        }

        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarLista();
        }

        private void cmbFiltroGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarLista();
        }

       
    }
}
