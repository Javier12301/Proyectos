using Datos.Seguridad;
using FontAwesome.Sharp;
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

namespace Sistema_Negocio_Ropa.Principales
{
    public partial class frmConfirmarContrasena : Form
    {
        private Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        private Sesion lSesion = Sesion.ObtenerInstancia;
        private UsuarioDA lUsuario = new UsuarioDA();

        private bool contraseñaVisible { get; set; }
        public bool contraseñaConfirmada { get; set; }
        public frmConfirmarContrasena()
        {
            InitializeComponent();
            contraseñaVisible = false;
            contraseñaConfirmada = false;
        }

        private void frmConfirmarContrasena_Load(object sender, EventArgs e)
        {
            alternarVisibilidadContraseña();
            MessageBox.Show("Necesitamos que ingrese su contraseña actual para confirmar su identidad", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRecuperar_Click(object sender, EventArgs e)
        {
            // Primero, se validará que se haya ingresado una contraseña
            bool contraseñaValida = uiUtilidades.VerificarTextbox(txtContraseñaActual, errorProvider, lblContraseñaActual);
            if (contraseñaValida)
            {
                // Se encriptará la contraseña, y se comparará con la contraseña actual del usuario, si coinciden, se procederá a cambiar la contraseña
                string contraseñaEncriptada = uiUtilidades.EncriptarClave(txtContraseñaActual.Text);
                // Contraseña encriptada del usuario en sesión
                string contraseñaUsuario = lSesion.UsuarioEnSesion().ObtenerContraseña();
                if (contraseñaEncriptada == contraseñaUsuario)
                {
                    contraseñaConfirmada = true;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("La contraseña ingresada no coincide con la contraseña actual", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    errorProvider.SetError(lblContraseñaActual, "La contraseña ingresada no coincide con la contraseña actual.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese su contraseña actual", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro que desea cancelar el proceso de cambio de contraseña?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnOjoNueva_Click(object sender, EventArgs e)
        {
            alternarVisibilidadContraseña();
        }

        private void btnOjoNueva_MouseEnter(object sender, EventArgs e)
        {
            btnOjo.IconFont = IconFont.Solid;
        }

        private void btnOjoNueva_MouseLeave(object sender, EventArgs e)
        {
            btnOjo.IconFont = IconFont.Regular;
        }

        private void alternarVisibilidadContraseña()
        {
            if (contraseñaVisible)
            {
                contraseñaVisible = false;
                uiUtilidades.MostrarContraseña(txtContraseñaActual, btnOjo);
            }
            else
            {
                contraseñaVisible = true;
                uiUtilidades.OcultarContraseña(txtContraseñaActual, btnOjo);
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

    }
}
