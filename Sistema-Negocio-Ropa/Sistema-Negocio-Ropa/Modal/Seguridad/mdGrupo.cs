using Datos.Seguridad;
using Negocio.Seguridad;
using Negocio.Seguridad.Composite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Negocio_Ropa.Modal.Seguridad
{
    public partial class mdGrupo : Form
    {
        private List<Modulo> listaModulos { get; set; }
        private List<Permiso> listaPermisos { get; set; }
        private List<Permiso> listaPermisosActivados { get; set; }
        private Utilidades lUtiliades = Utilidades.ObtenerInstancia;
        private GrupoDA lGrupo;
        private ModuloDA lModulo;
        private PermisoDA lPermiso;
        private AccionDA lAccion;

        // Modificar Grupo
        private bool modificandoGrupo { get; set; }
        private int grupoID { get; set; }
        private Grupo oGrupo { get; set; }

        public mdGrupo(bool modificar = false, int grupoID = 0)
        {
            InitializeComponent();
            lGrupo = new GrupoDA();
            lModulo = new ModuloDA();
            lPermiso = new PermisoDA();
            lAccion = new AccionDA();


            modificandoGrupo = modificar;
            this.grupoID = grupoID;
            listaPermisos = new List<Permiso>();
            listaPermisosActivados = new List<Permiso>();
        }

        private void mdGrupo_Load(object sender, EventArgs e)
        {
            try
            {
                CargarModulos();
                // seleccionar tab page 0
                if (modificandoGrupo)
                {
                    if (grupoID > 0)
                    {
                        oGrupo = new Grupo();
                        oGrupo = lGrupo.ObtenerGrupoPorID(grupoID);
                        CargarDatosDeGrupo(oGrupo);
                        CargarPermisosActivos();
                    }
                    else
                    {
                        throw new Exception("Ocurrió un error al modificar el grupo, contactar con el administrador si este error persiste.");
                    }
                }
                tcMain.SelectedIndex = 0;
                lbAccionesVentas.SelectedIndex = 0;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkPermisos_Changed(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                string nombreModulo = checkBox.Tag.ToString();
                string nombreAccion = checkBox.Text;
                bool estado = checkBox.Checked;

                // Buscar el permiso en la lista
                Permiso permiso = listaPermisos.FirstOrDefault(p => p.oModulo.Nombre== nombreModulo && p.oAccion.Nombre == nombreAccion);

                if (permiso != null)
                {
                    permiso.Estado = estado;

                    // Actualizar la lista de permisos activados
                    if (estado)
                    {
                        if (!listaPermisosActivados.Contains(permiso))
                        {
                            listaPermisosActivados.Add(permiso);
                        }
                    }
                    else
                    {
                        // Buscar el permiso en la listaPermisosActivados y eliminarlo
                        Permiso eliminarPermiso = listaPermisosActivados.FirstOrDefault(p => p.oModulo.Nombre== permiso.oModulo.Nombre && p.oAccion.Nombre== permiso.oAccion.Nombre);
                        listaPermisosActivados.Remove(eliminarPermiso);
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!modificandoGrupo)
                {
                    AltaGrupo();
                }
                else
                {
                    ModificarGrupo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Manejo de responsabilidades
        private bool ValidarCampos()
        {
            bool camposValidos = true;
            camposValidos &= lUtiliades.VerificarTextbox(txtNombreGrupo, errorProvider, lblNombreGrupo);
            return camposValidos;
        }

        private Grupo CrearGrupo()
        {
            return new Grupo
            {
                Nombre = txtNombreGrupo.Text,
                Estado = chkEstado.Checked
            };
        }

        private Grupo CrearGrupoModificado()
        {
            return new Grupo
            {
                GrupoID = int.Parse(txtID.Text),
                Nombre = txtNombreGrupo.Text,
                Estado = chkEstado.Checked
            };
        }

        // ALTA GRUPO
        private void AltaGrupo()
        {
            if (ValidarCampos())
            {
                Grupo grupo = CrearGrupo();

                if (lGrupo.ExisteGrupo(grupo.Nombre))
                {
                    errorProvider.SetError(lblNombreGrupo, "El Grupo ingresado ya se encuentra en uso.");
                    MessageBox.Show("El grupo ingresado ya se encuentra en uso.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Dar de alta al grupo
                bool resultado = lGrupo.AltaGrupo(grupo);
                if (resultado)
                {
                    grupo = lGrupo.ObtenerGrupoPorNombre(grupo.Nombre);
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al dar de alta el grupo. Si este error persiste, contacte con el administrador del sistema.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Componente grupoRoot = crearComposite(grupo);
                listaPermisos.Clear();
                AgregarPermisos(grupoRoot, grupo);
                AltaPermiso();
            }
        }

        private void AltaPermiso()
        {
            try
            {
                bool resultado = lPermiso.AgregarPermisos(listaPermisos);
                if (resultado)
                {
                    if (modificandoGrupo)
                    {
                        MessageBox.Show("El grupo fue modificado con éxito.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El grupo fue dado de alta con éxito.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un error al insertar los permisos al grupo correspondiente, contactar con el administrador si el error persiste.");
            }
        }

        // Modificar Grupo

        private void ModificarGrupo()
        {
            if (ValidarCampos())
            {
                Grupo grupo = CrearGrupoModificado();
                bool grupoExiste = false;
                if (oGrupo.Nombre != grupo.Nombre)
                {
                    grupoExiste = lGrupo.ExisteGrupo(grupo.Nombre);
                    if (grupoExiste)
                    {
                        errorProvider.SetError(lblNombreGrupo, "El Grupo ingresado ya se encuentra en uso.");
                        MessageBox.Show("El grupo ingresado ya se encuentra en uso.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                bool resultado = lGrupo.ModificarGrupo(grupo);
                if (resultado)
                {
                    grupo = lGrupo.ObtenerGrupoPorNombre(grupo.Nombre);
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al modificar el grupo. Si este error persiste, contacte con el administrador del sistema.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                lPermiso.DesactivarPermisos(grupo.GrupoID);
                Componente grupoRoot = crearComposite(grupo);
                listaPermisos.Clear();
                AgregarPermisos(grupoRoot, grupo);
                AltaPermiso();
            }
        }

        // Agregar Permisos
        private void AgregarPermisos(Componente componente, Grupo grupo, Modulo modulo = null)
        {
            if (componente is Composite composite)
            {
                // Si el componente es un Composite, entonces es un Modulo
                modulo = new Modulo { Nombre = composite.ObtenerNombre() };
                foreach (Componente hijo in composite.ObtenerHijos())
                {
                    AgregarPermisos(hijo, grupo, modulo);
                }
            }
            else if (componente is Hoja hoja)
            {
                if (!string.IsNullOrEmpty(modulo.Nombre))
                {
                    Permiso permiso = new Permiso
                    {
                        oGrupo = grupo,
                        oModulo = lModulo.ObtenerModulo(modulo.Nombre), // Aquí establecemos el Modulo
                        oAccion = lAccion.ObtenerAccion(modulo.Nombre, hoja.ObtenerNombre()),
                        Estado = true
                    };
                    listaPermisos.Add(permiso);
                }
            }
        }


        // Composite
        private Componente crearComposite(Grupo grupo)
        {
            Composite grupoRoot = new Composite(grupo.ObtenerNombre());

            foreach (var modulo in listaModulos)
            {
                if (listaPermisosActivados.Any(p => p.oModulo.Nombre == modulo.Nombre))
                {
                    Composite moduloComposite = new Composite(modulo.Nombre);
                    foreach (var accion in modulo.ListaAcciones)
                    {
                        // Solo agregamos las acciones que esten activadas
                        if (listaPermisosActivados.Any(p => p.oAccion.Nombre== accion.Nombre && p.oModulo.Nombre== modulo.Nombre))
                        {
                            Hoja accionHoja = new Hoja(accion.Nombre);
                            moduloComposite.Agregar(accionHoja);
                        }
                    }
                    grupoRoot.Agregar(moduloComposite);
                }
            }
            return grupoRoot;
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Switch segun el nombre del tab page seleccionado
            switch (tcMain.SelectedTab.Name)
            {
                case "tpVentas":
                    lbAccionesVentas.SelectedIndex = 0;
                    tpVentas_Abierto();
                    break;
                case "tpProductos":
                    lbAccionesProductos.SelectedIndex = 0;
                    tpProductos_Abierto();
                    break;
                case "tpInventario":
                    lbAccionesInventario.SelectedIndex = 0;
                    tpInventario_Abierto();
                    break;
                case "tpReportes":
                    lbAccionesReportes.SelectedIndex = 0;
                    tpReportes_Abierto();
                    break;
                case "tpAjustes":
                    lbAccionesAjustes.SelectedIndex = 0;
                    tpAjustes_Abierto();
                    break;
                default:
                    break;
            }
        }

        private void lbAccionesVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tcMain.SelectedTab.Name)
            {
                case "tpVentas":
                    tpVentas_Abierto();
                    break;
                case "tpProductos":
                    tpProductos_Abierto();
                    break;
                case "tpInventario":
                    tpInventario_Abierto();
                    break;
                case "tpReportes":
                    tpReportes_Abierto();
                    break;
                case "tpAjustes":
                    tpAjustes_Abierto();
                    break;
                default:
                    break;
            }
        }

        private void ActualizarPermisos(string nombreModulo, FlowLayoutPanel flpPermisos)
        {
            try
            {
                Modulo modulo = listaModulos.FirstOrDefault(m => m.Nombre == nombreModulo);
                List<Accion> accionesDisponibles = modulo.ListaAcciones;

                flpPermisos.Controls.Clear();
                listaPermisos.Clear();

                foreach (var accion in accionesDisponibles)
                {
                    CheckBox checkbox = new CheckBox
                    {
                        Text = accion.Nombre,
                        AutoSize = true,
                        Enabled = true,
                        Visible = true,
                        Tag = nombreModulo,
                        Checked = listaPermisosActivados.Any(p => p.oModulo.Nombre== nombreModulo && p.oAccion.Nombre == accion.Nombre)
                    };

                    checkbox.CheckedChanged += chkPermisos_Changed;
                    flpPermisos.Controls.Add(checkbox);

                    Permiso permiso = new Permiso
                    {
                        oModulo = modulo,
                        oAccion = accion,
                        Estado = listaPermisosActivados.Any(p => p.oModulo.Nombre== nombreModulo && p.oAccion.Nombre == accion.Nombre)
                    };
                    listaPermisos.Add(permiso);
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"Error al cargar los permisos del módulo {nombreModulo}. Intente nuevamente y si este error persiste contacte con el administrador del sistema.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tpVentas_Abierto()
        {
            try
            {
                if (lbAccionesVentas.SelectedItem.ToString() == "VENTAS")
                {
                    ActualizarPermisos("formVentas", flpPermisos);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar los permisos del módulo Ventas, Intente nuevamente y si este error persiste contacte con el administrador del sistema.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tpProductos_Abierto()
        {
            try
            {
                if (lbAccionesProductos.SelectedItem.ToString() == "PRODUCTOS")
                {
                    ActualizarPermisos("formProductos", flpPermisosProductos);
                }
                else if (lbAccionesProductos.SelectedItem.ToString() == "CATEGORIAS")
                {
                    ActualizarPermisos("formCategorias", flpPermisosProductos);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar los permisos del módulo Productos, Intente nuevamente y si este error persiste contacte con el administrador del sistema.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tpInventario_Abierto()
        {
            if (lbAccionesInventario.SelectedItem.ToString() == "INVENTARIO")
            {
                ActualizarPermisos("formInventario", flpPermisosInventario);
            }
        }

        private void tpReportes_Abierto()
        {
            if (lbAccionesReportes.SelectedItem.ToString() == "REPORTES")
            {
                ActualizarPermisos("formReportes", flpPermisosReportes);
            }
        }

        private void tpAjustes_Abierto()
        {
            lbSubMenuPerfiles.Items.Clear();
            if (lbAccionesAjustes.SelectedItem.ToString() == "AJUSTES")
            {
                ActualizarPermisos("formAjustes", flpPermisosAjustes);
            }
            else if (lbAccionesAjustes.SelectedItem.ToString() == "PERFILES")
            {
                lbSubMenuPerfiles.Items.Add("USUARIOS");
                lbSubMenuPerfiles.Items.Add("GRUPOS");
            }else if (lbAccionesAjustes.SelectedItem.ToString() == "DATOS DE NEGOCIO")
            {
                ActualizarPermisos("formNegocio", flpPermisosAjustes);
            }
            else if (lbAccionesAjustes.SelectedItem.ToString() == "BACKUP")
            {
                ActualizarPermisos("formBackup", flpPermisosAjustes);
            }
        }

        private void flpSubMenuPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSubMenuPerfiles.SelectedItem.ToString() == "USUARIOS")
            {
                ActualizarPermisos("formUsuarios", flpPermisosAjustes);
            }
            else if (lbSubMenuPerfiles.SelectedItem.ToString() == "GRUPOS")
            {
                ActualizarPermisos("formGrupos", flpPermisosAjustes);
            }
        }

        private void CargarDatosDeGrupo(Grupo oGrupo)
        {
            if (oGrupo != null)
            {
                txtID.Text = oGrupo.GrupoID.ToString();
                txtNombreGrupo.Text = oGrupo.Nombre;
                chkEstado.Checked = oGrupo.Estado;
                if (oGrupo.GrupoID == 1)
                {
                    txtNombreGrupo.Enabled = false;
                    chkEstado.Enabled = false;
                }
            }
        }

        private void CargarPermisosActivos()
        {
            // Cargamos la lista de permisos activados
            listaPermisosActivados = lPermiso.ObtenerListaPermisosActivosBD(oGrupo.GrupoID);
            // Se obtendrá una lista de permisos activos, pero esto solo contendrán las id
            // se deberá hacer un recorrido a toda la lista y obtener con la id el módulo y la acción correspondiente
            foreach (var permiso in listaPermisosActivados)
            {
                permiso.oGrupo = null;
                permiso.oModulo = lModulo.ObtenerModuloID(permiso.oModulo.ModuloID);
                permiso.oAccion= lAccion.ObtenerAccionIDD(permiso.oAccion.AccionID);
            }
        }

        // Carga de Datos
        private void CargarModulos()
        {
            listaModulos = lModulo.ObtenerModulosConAcciones();
        }


        // Movimiento de ventana
        private Point mousePosicion;
        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mousePosicion = e.Location;
            }
        }

        private void pnlTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int deltaX = e.X - mousePosicion.X;
                int deltaY = e.Y - mousePosicion.Y;
                this.Location = new Point(this.Location.X + deltaX, this.Location.Y + deltaY);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro que desea salir del formulario? Los cambios no guardados se perderán.", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
