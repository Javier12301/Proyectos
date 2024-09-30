using Datos.Seguridad;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio.Seguridad;


namespace Sistema_Negocio_Ropa.Modal
{
    public partial class mdCambiarContraseña : Form
    {
        Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        UsuarioDA usuarioDA;
        AuditoriaDA auditoriaDA;
        private bool contraseñaVisibleNueva { get; set; }
        private bool contraseñaVisibleConfirmar { get; set; }
        public bool contraseñaModificada { get; set; }
        public string nombreUsario { get; set; }

        public mdCambiarContraseña(string nombreUsuario)
        {
            InitializeComponent();
            usuarioDA = new UsuarioDA();
            auditoriaDA = new AuditoriaDA();
            this.nombreUsario = nombreUsuario;
            contraseñaVisibleNueva = false;
            contraseñaVisibleConfirmar = false;
            contraseñaModificada = false;
        }


        private void mdCambiarContraseña_Load(object sender, EventArgs e)
        {
            txtNuevaContrasena.Select();
            alternarVisibilidadContraseña(txtNuevaContrasena, btnOjoNueva);
            alternarVisibilidadContraseña(txtConfirmarContrasena, btnOjoConfirmar);
        }

        private void btnOjo_Click(object sender, EventArgs e)
        {
            IconButton ojoControl = (IconButton)sender;
            if (ojoControl.Name == "btnOjoNueva")
            {
                alternarVisibilidadContraseña(txtNuevaContrasena, ojoControl);
            }
            else
            {
                alternarVisibilidadContraseña(txtConfirmarContrasena, ojoControl);
            }
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

        private void txtCredenciales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                cambiarContraseña();
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            cambiarContraseña();
        }



        private void cambiarContraseña()
        {
            bool contraseñaValida = uiUtilidades.VerificarTextbox(txtNuevaContrasena, errorProvider, lblNuevaContrasena);
            bool confirmarContraseña = uiUtilidades.VerificarTextbox(txtConfirmarContrasena, errorProvider, lblConfirmarContrasena);
            if (contraseñaValida && confirmarContraseña)
            {
                if (txtNuevaContrasena.Text == txtConfirmarContrasena.Text)
                {
                    Usuario oUsuario = usuarioDA.ObtenerUsuarioPorNombre(nombreUsario);
                    oUsuario.Password = uiUtilidades.EncriptarClave(txtNuevaContrasena.Text);
                    try
                    {
                        contraseñaModificada = usuarioDA.ModificarUsuario(oUsuario);
                        auditoriaDA.RegistrarMovimiento("Modificación", "Sistema", "Perfiles/Usuarios", $"Se modificó la contraseña del usuario: {oUsuario.NombreUsuario}");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception)
                    {
                        contraseñaModificada = false;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    errorProvider.SetError(lblConfirmarContrasena, "La contraseñas no coincide.");
                    MessageBox.Show("Las contraseñas ingresada no coinciden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Utilidades de interfaz
        private void alternarVisibilidadContraseña(TextBox textbox, IconButton ojoControl)
        {
            switch (ojoControl.Name)
            {
                case "btnOjoNueva":
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

        // Control de movimiento de la ventana
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
            this.Close();
        }
    }
}
