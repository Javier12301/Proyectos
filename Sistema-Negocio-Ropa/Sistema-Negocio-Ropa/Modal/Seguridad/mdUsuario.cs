using Datos.Seguridad;
using FontAwesome.Sharp;
using Negocio.Seguridad;
using Sistema_Negocio_Ropa.Principales;
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
    public partial class mdUsuario : Form
    {

        Utilidades uiUtilidades = Utilidades.ObtenerInstancia;

        private bool contraseñaVisibleNueva { get; set; }
        private bool contraseñaVisibleConfirmar { get; set; }
        private Sesion lSesion = Sesion.ObtenerInstancia;
        private GrupoDA lGrupo = new GrupoDA();
        private UsuarioDA lUsuario = new UsuarioDA();

        // Modificar Usuario
        private bool modificandoUsuario { get; set; }
        private int usuarioID { get; set; }
        private Usuario oUsuario { get; set; }
        public mdUsuario(bool modificar = false, int usuarioID = 0)
        {
            InitializeComponent();
            modificandoUsuario = modificar;
            this.usuarioID = usuarioID;
            contraseñaVisibleNueva = false;
            contraseñaVisibleConfirmar = false;
        }

        private void mdUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                // Configurar la visibilidad y habilitación de los controles de contraseña si se está modificando el usuario
                chkCambiarContraseña.Visible = modificandoUsuario;
                chkCambiarContraseña.Enabled = modificandoUsuario;

                // Cargar los grupos y alternar la visibilidad de los controles de contraseña
                cargarGrupos();
                alternarVisibilidadContraseña(txtContraseña, btnOjo);
                alternarVisibilidadContraseña(txtContraseñaConfirmar, btnOjoConfirmar);

                // Verificar si se está modificando el usuario y si el ID es válido
                if (modificandoUsuario && usuarioID > 0)
                {
                    oUsuario = lUsuario.ObtenerUsuarioPorID(usuarioID);
                    if (oUsuario != null)
                    {
                        cargarDatosDeUsuario(oUsuario);
                        alternarPanelesContraseña(false);
                    }
                    else
                    {
                        throw new Exception("No se encontró el usuario con el ID proporcionado.");
                    }
                }
                else if (modificandoUsuario) // Solo se lanza la excepción si se está modificando pero el ID es inválido
                {
                    throw new Exception("Ocurrió un error al modificar el usuario, contactar con el administrador si este error persiste.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!modificandoUsuario)
                {
                    altaUsuario();
                }
                else
                {
                    modificarUsuario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            bool camposValidos = true;

            camposValidos &= uiUtilidades.VerificarTextbox(txtNombreUsuario, errorProvider, lblUsuario);
            camposValidos &= uiUtilidades.VerificarMail(txtEmail, errorProvider, lblEmail);
            camposValidos &= uiUtilidades.VerificarTextbox(txtNombre, errorProvider, lblNombre);
            camposValidos &= uiUtilidades.VerificarTextbox(txtApellido, errorProvider, lblApellido);
            camposValidos &= uiUtilidades.VerificarCombobox(cmbGrupo, errorProvider, lblGrupo);
            camposValidos &= uiUtilidades.VerificarTextbox(txtContraseña, errorProvider, lblContraseña);
            camposValidos &= uiUtilidades.VerificarTextbox(txtContraseñaConfirmar, errorProvider, lblContraseñaConfirmar);

            return camposValidos;
        }

        private Usuario CrearUsuario()
        {
            return new Usuario
            {
                NombreUsuario = txtNombreUsuario.Text,
                Password = uiUtilidades.EncriptarClave(txtContraseña.Text),
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Correo = txtEmail.Text,
                oGrupo = lGrupo.ObtenerGrupoPorNombre(cmbGrupo.SelectedItem.ToString()),
                Estado = chkEstado.Checked
            };
        }

        private Usuario CrearUsuarioModificado()
        {
            return new Usuario
            {
                UsuarioID = int.Parse(txtID.Text),
                NombreUsuario = txtNombreUsuario.Text,
                Password = txtContraseña.Text,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Correo = txtEmail.Text,
                oGrupo = lGrupo.ObtenerGrupoPorNombre(cmbGrupo.SelectedItem.ToString()),
                Estado = chkEstado.Checked
            };
        }

        private void altaUsuario()
        {
            if (ValidarCampos())
            {
                if (txtContraseña.Text == txtContraseñaConfirmar.Text)
                {
                    Usuario usuario = CrearUsuario();

                    bool usuarioExiste = lUsuario.ExisteNombreUsuarioD(usuario.NombreUsuario);
                    if (usuarioExiste)
                        errorProvider.SetError(lblUsuario, "El Usuario ingresado ya se encuentra en uso.");

                    bool emailExiste = lUsuario.ExisteCorreo(usuario.Correo);
                    if (emailExiste)
                        errorProvider.SetError(lblEmail, "El correo electrónico ingresado ya se encuentra en uso.");

                    if (usuarioExiste || emailExiste)
                    {
                        MessageBox.Show("El usuario o el correo electrónico ingresado ya se encuentra en uso.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bool resultado = lUsuario.AltaUsuario(usuario);

                    if (resultado)
                    {
                        MessageBox.Show("El usuario fue dado de alta con éxito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al dar de alta al usuario. Por favor, inténtelo de nuevo.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Las contraseñas ingresadas no coinciden", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    errorProvider.SetError(lblContraseñaConfirmar, "Las contraseñas no coinciden.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, complete los campos obligatorios correctamente.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarUsuario()
        {
            if (ValidarCampos())
            {
                // Obtener usuario con los datos actualizados
                Usuario usuario = CrearUsuarioModificado();

                if (chkCambiarContraseña.Checked)
                {
                    if (txtContraseña.Text == txtContraseñaConfirmar.Text)
                    {
                        // Encriptar la nueva contraseña si se va a cambiar
                        usuario.Password = uiUtilidades.EncriptarClave(txtContraseña.Text);
                    }
                    else
                    {
                        MessageBox.Show("Las contraseñas ingresadas no coinciden", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        errorProvider.SetError(lblContraseñaConfirmar, "Las contraseñas no coinciden.");
                    }
                }
                bool emailExiste = false;
                bool usuarioExiste = false;

                // Se verifica si el usuario realizo una modificación en el nombre de usuario o en el email
                if (oUsuario.NombreUsuario != usuario.NombreUsuario)
                {
                    // esto significa que si se modifico el nombre de usuario, entonces hay que verificar si ya existe
                    usuarioExiste = lUsuario.ExisteNombreUsuarioD(usuario.NombreUsuario);
                    if (usuarioExiste)
                        errorProvider.SetError(lblUsuario, "El Usuario ingresado ya se encuentra en uso.");
                }

                if (oUsuario.Correo != usuario.Correo)
                {
                    // esto significa que si se modifico el email, entonces hay que verificar si ya existe
                    emailExiste = lUsuario.ExisteCorreo(usuario.Correo);
                    if (emailExiste)
                        errorProvider.SetError(lblEmail, "El correo electrónico ingresado ya se encuentra en uso.");
                }

                if (usuarioExiste || emailExiste)
                {
                    MessageBox.Show("El usuario o el correo electrónico ingresado ya se encuentra en uso.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool resultado = lUsuario.ModificarUsuario(usuario);

                if (resultado)
                {
                    MessageBox.Show("El usuario fue modificado con éxito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al modificar el usuario. Por favor, inténtelo de nuevo.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Por favor, complete los campos obligatorios correctamente.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea limpiar todo los campos?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                uiUtilidades.LimpiarTextbox(txtNombreUsuario, txtEmail, txtNombre, txtApellido, txtContraseña, txtContraseñaConfirmar);
                uiUtilidades.LimpiarCombobox(cmbGrupo);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkCambiarContraseña_CheckedChanged(object sender, EventArgs e)
        {
            if (modificandoUsuario && oUsuario.UsuarioID == lSesion.UsuarioEnSesion().UsuarioID && chkCambiarContraseña.Checked)
            {
                // Deberá confirmar su identidad para poder cambiar la contraseña
                using (var modal = new frmConfirmarContrasena())
                {
                    var resultado = modal.ShowDialog();
                    if (resultado == DialogResult.OK)
                    {
                        bool credencialConfirmada = modal.contraseñaConfirmada;
                        if (credencialConfirmada)
                        {
                            alternarPanelesContraseña(chkCambiarContraseña.Checked);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo confirmar su identidad, por favor, inténtelo de nuevo.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        chkCambiarContraseña.Checked = false;
                    }
                }
            }
            else
            {
                alternarPanelesContraseña(chkCambiarContraseña.Checked);
            }
        }

        private void alternarPanelesContraseña(bool mostrarPanel)
        {
            pnlContraseña.Enabled = mostrarPanel;
            pnlConfirmarContraseña.Visible = mostrarPanel;
            btnOjo.Visible = mostrarPanel;
            btnOjoConfirmar.Visible = mostrarPanel;
            if (mostrarPanel)
            {
                txtContraseña.Text = string.Empty;
                txtContraseñaConfirmar.Text = string.Empty;
            }
            else
            {
                txtContraseña.Text = oUsuario.Password;
            }
        }

        private void cargarDatosDeUsuario(Usuario oUsuario)
        {
            if (oUsuario != null)
            {
                txtID.Text = oUsuario.UsuarioID.ToString();
                txtNombreUsuario.Text = oUsuario.NombreUsuario;
                txtEmail.Text = oUsuario.Correo;
                txtNombre.Text = oUsuario.Nombre;
                txtApellido.Text = oUsuario.Apellido;
                cmbGrupo.SelectedItem = oUsuario.oGrupo.Nombre;
                txtContraseña.Text = oUsuario.Password;
                txtContraseñaConfirmar.Text = oUsuario.Password;
                chkEstado.Checked = oUsuario.Estado;
                if (oUsuario.UsuarioID == lSesion.UsuarioEnSesion().UsuarioID)
                {
                    if (oUsuario.UsuarioID == 1)
                    {
                        txtNombreUsuario.Enabled = false;
                    }
                    cmbGrupo.Enabled = false;
                    // Si se ingreso desde el formulario mis datos, el usuario no puede desactivarse/activarse por si mismo.
                    chkEstado.Enabled = false;
                }
            }
        }

        private void cargarGrupos()
        {
            List<Grupo> listaGrupos = lGrupo.ListarGrupos();
            cmbGrupo.Items.Add("Seleccione una opción...");
            foreach (var grupo in listaGrupos)
            {
                if(grupo.GrupoID != 0)
                {
                    cmbGrupo.Items.Add(grupo.Nombre);
                }
            }
            cmbGrupo.SelectedIndex = 0;
        }

        private void btnOjo_Enter(object sender, EventArgs e)
        {
            IconButton ojoControl = (IconButton)sender;
            ojoControl.IconFont = IconFont.Solid;
        }

        private void btnOjo_MouseLeave(object sender, EventArgs e)
        {
            IconButton ojoControl = (IconButton)sender;
            ojoControl.IconFont = IconFont.Regular;
        }

        private void btnOjo_Click(object sender, EventArgs e)
        {
            IconButton ojoControl = (IconButton)sender;
            if (ojoControl.Name == "btnOjo")
            {
                alternarVisibilidadContraseña(txtContraseña, ojoControl);
            }
            else
            {
                alternarVisibilidadContraseña(txtContraseñaConfirmar, ojoControl);
            }
        }

        private void alternarVisibilidadContraseña(TextBox textbox, IconButton ojoControl)
        {
            switch (ojoControl.Name)
            {
                case "btnOjo":
                    if (contraseñaVisibleNueva)
                    {
                        contraseñaVisibleNueva = false;
                        uiUtilidades.MostrarContraseña(textbox, ojoControl);
                    }
                    else
                    {
                        contraseñaVisibleNueva = true;
                        uiUtilidades.OcultarContraseña(textbox, ojoControl);
                    }
                    break;
                case "btnOjoConfirmar":
                    if (contraseñaVisibleConfirmar)
                    {
                        contraseñaVisibleConfirmar = false;
                        uiUtilidades.MostrarContraseña(textbox, ojoControl);
                    }
                    else
                    {
                        contraseñaVisibleConfirmar = true;
                        uiUtilidades.OcultarContraseña(textbox, ojoControl);
                    }
                    break;
                default:
                    break;
            }
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

        private void cmbGrupo_KeyPress(object sender, KeyPressEventArgs e)
        {           
            ComboBox combobox = (ComboBox)sender;
            uiUtilidades.Combobox_ShortcutSiguienteIndex(combobox, e);
            
        }
    }
}
