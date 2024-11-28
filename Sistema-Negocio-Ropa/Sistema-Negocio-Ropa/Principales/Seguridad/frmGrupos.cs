using Datos.Seguridad;
using Negocio.Negocio;
using Negocio.Seguridad;
using Sistema_Negocio_Ropa.Modal.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Negocio_Ropa.Principales.Seguridad
{
    public partial class frmGrupos : Form
    {

        private GrupoDA lGrupo;
        private UsuarioDA lUsuario;
        private Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        private Sesion lSesion = Sesion.ObtenerInstancia;
        private Permiso _permisousuario;

        public frmGrupos()
        {
            InitializeComponent();
            lGrupo = new GrupoDA();
            lUsuario = new UsuarioDA();
        }

        private void frmGrupos_Load(object sender, EventArgs e)
        {
            try
            {
                CargarLista();
                CargarFiltros();
                CargarPermisos();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPermisos()
        {
            _permisousuario = new Permiso();
            uiUtilidades.CargarPermisos("formGrupos", flpContenedorBotones, _permisousuario);
            btnExportarP.Enabled = true;
            btnExportarP.Visible = true;
        }

        private void CargarFiltros()
        {
            cmbFiltroBuscar.Items.Add("Nombre");
            cmbFiltroBuscar.SelectedIndex = 0;

            cmbFiltroEstado.Items.Add("Todos");
            cmbFiltroEstado.Items.Add("Activo");
            cmbFiltroEstado.Items.Add("Inactivo");
            cmbFiltroEstado.SelectedIndex = 0;
        }

        private BindingSource bsGrupo = new BindingSource();
        private DataTable dt = new DataTable();  // Reutilizar la instancia

        private void CargarLista()
        {
            try
            {
                dt.Clear();  // Limpiar el DataTable existente
                dt = lGrupo.ObtenerGrupoFiltrados(cmbFiltroBuscar.Text, txtBuscar.Text, cmbFiltroEstado.Text);

                bsGrupo.DataSource = dt;
                dgvGrupos.DataSource = bsGrupo;
                bnGrupo.BindingSource = bsGrupo;

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

        private void btnNuevoP_Click(object sender, EventArgs e)
        {
            if (_permisousuario.Alta)
            {
                using (var modal = new mdGrupo())
                {
                    var resultado = modal.ShowDialog();
                    if (resultado == DialogResult.OK)
                    {
                        CargarLista();
                    }
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos para realizar esta acción.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnModificarP_Click(object sender, EventArgs e)
        {
            AbrirModalModificar();
        }

        private void dgvGrupos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AbrirModalModificar();
            }
        }

        private void AbrirModalModificar()
        {
            try
            {
                if (_permisousuario.Modificar)
                {
                    if (dgvGrupos.CurrentCell != null)
                    {
                        int filaIndex = dgvGrupos.CurrentCell.RowIndex;
                        int grupoID = Convert.ToInt32(dgvGrupos.Rows[filaIndex].Cells["ID"].Value);
                        if (grupoID > 0)
                        {
                            if (grupoID == lSesion.UsuarioEnSesion().ObtenerGrupoID() && grupoID != 1)
                            {
                                MessageBox.Show("No puede modificar el grupo al que pertenece.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // El unico que puede modificar el grupo Administradores es el usuario Admin
                            if (grupoID == 1 && lSesion.UsuarioEnSesion().UsuarioID != 1)
                            {
                                MessageBox.Show("No puede modificar el grupo de Administradores", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            using (var modal = new mdGrupo(true, grupoID))
                            {
                                var resultado = modal.ShowDialog();
                                if (resultado == DialogResult.OK)
                                {
                                    if (grupoID == 1)
                                    {
                                        MessageBox.Show("Se cerrará la sesión para aplicar los cambios.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        // Obtenemos la instancia del formulario principal que es el padre de todos y luego cerramos la sesión
                                        // haciendo uso de su función encargada de cerrar la sesión.
                                        frmMain frmMain = (frmMain)Application.OpenForms["frmMain"];
                                        frmMain.Cerrar_Sesion();
                                    }
                                    else
                                    {
                                        CargarLista();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Seleccione un grupo para modificar.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No tiene permisos para realizar esta acción.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void btnEliminarP_Click(object sender, EventArgs e)
        {
            BajaGrupo();
        }

        private void dgvGrupos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                BajaGrupo();
            }
        }

        private List<Operacion> gruposAEliminar = new List<Operacion>();
        private void BajaGrupo()
        {
            try
            {
                if (_permisousuario.Baja)
                {
                    // Verificar si hay celdas seleccionadas
                    if (dgvGrupos.SelectedCells.Count == 0)
                    {
                        MessageBox.Show("Seleccione por lo menos un grupo para eliminar.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar los grupos seleccionados?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado != DialogResult.Yes)
                        return;

                    gruposAEliminar.Clear();

                    // Recorrer celdas seleccionadas
                    foreach (DataGridViewCell celda in dgvGrupos.SelectedCells)
                    {
                        int grupoID = Convert.ToInt32(dgvGrupos.Rows[celda.RowIndex].Cells["ID"].Value);
                        string nombreGrupo = dgvGrupos.Rows[celda.RowIndex].Cells["Nombre"].Value.ToString();
                        string operacion = string.Empty;
                        // Comprobar si no se está por eliminar el grupo al que pertenece el usuario en sesión
                        if (grupoID == lSesion.UsuarioEnSesion().ObtenerGrupoID())
                        {
                            MessageBox.Show("No puede eliminar el grupo al que pertenece.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else if (grupoID == 1)
                        {
                            MessageBox.Show("No puede eliminar al grupo Admin.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Comprobar si el grupo tiene usuarios asignados
                        if (lGrupo.GrupoTieneUsuarios(grupoID))
                        {
                            DialogResult respuesta = MessageBox.Show($"El grupo seleccionado: ( {dgvGrupos.Rows[celda.RowIndex].Cells["Nombre"].Value} ) tiene usuarios asignados, ¿desea asignar a los usuarios de este grupo como sin grupo?", "Sistema", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (DialogResult.Yes == respuesta)
                            {
                                operacion = "AsignarUsuariosSinGrupo";
                            }
                            else if (DialogResult.No == respuesta)
                            {
                                respuesta = MessageBox.Show($"¿Desea eliminar el grupo: ( {dgvGrupos.Rows[celda.RowIndex].Cells["Nombre"].Value} ) y los usuarios asignados a este?", "Sistema", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                if (DialogResult.Yes == respuesta)
                                {
                                    operacion = "EliminarGrupoYUsuarios";
                                }
                                else
                                {
                                    MessageBox.Show($"Operación cancelada para el grupo: ( {dgvGrupos.Rows[celda.RowIndex].Cells["Nombre"].Value} )", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    continue;
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Operación cancelada para el grupo: ( {dgvGrupos.Rows[celda.RowIndex].Cells["Nombre"].Value} )", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                continue;
                            }
                        }
                        else
                        {
                            operacion = "EliminarGrupo";
                        }
                        gruposAEliminar.Add(new Operacion { ID = grupoID, Nombre = nombreGrupo, NombreOperacion = operacion });
                    }
                    int gruposEliminados = 0;

                    foreach (var grupo in gruposAEliminar)
                    {
                        if (lGrupo.BajaGrupo(grupo))
                            gruposEliminados++;
                        else
                        {
                            MessageBox.Show($"No se pudo eliminar el grupo con ID: ( {grupo.ID} )", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }
                    }

                    if (gruposEliminados > 0)
                    {
                        MessageBox.Show(gruposEliminados > 1 ? "Grupos eliminados con éxito." : "Grupo eliminado con éxito.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarLista();
                    }
                }
                else
                {
                    MessageBox.Show("No tiene permisos para realizar esta acción.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportarP_Click(object sender, EventArgs e)
        {
            uiUtilidades.ExportarDataGridViewAExcel(dgvGrupos, "Lista de grupos", "Hoja grupos", "Grupos");
        }

        private void cmbFiltroBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLista();

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarLista();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarLista();

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;   
        }

        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLista();

        }

        
    }
}
