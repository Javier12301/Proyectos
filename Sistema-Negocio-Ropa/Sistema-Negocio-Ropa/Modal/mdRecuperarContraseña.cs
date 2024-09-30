using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos.Seguridad;
using Sistema_Negocio_Ropa.Recursos;

namespace Sistema_Negocio_Ropa.Modal
{
    public partial class mdRecuperarContraseña : Form
    {
        private Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        private UsuarioDA usuarioDA;
        
        public mdRecuperarContraseña()
        {
            InitializeComponent();
            usuarioDA = new UsuarioDA();
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

        private void btnRecuperar_Click(object sender, EventArgs e)
        {
            recuperarContraseña();
        }

        private void txtCredenciales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                recuperarContraseña();
            }
        }


        private void recuperarContraseña()
        {
            try
            {
                // Validar los campos de usuario y correo
                bool usuarioValido = uiUtilidades.VerificarTextbox(txtUsuario, errorProvider, lblUsuario);
                bool emailValido = uiUtilidades.VerificarTextbox(txtCorreo, errorProvider, lblEmail);

                if (!usuarioValido || !emailValido)
                {
                    MessageBox.Show("Por favor, complete los campos correctamente.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar si el correo es válido
                if (!uiUtilidades.VerificarMail(txtCorreo, errorProvider, lblEmail))
                {
                    MessageBox.Show("El correo ingresado no es válido.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Comprobar si el usuario ingresado y el correo pertenecen a ese usuario
                if (!usuarioDA.ExisteUsuarioConCorreo(txtUsuario.Text, txtCorreo.Text))
                {
                    MessageBox.Show("Usuario y/o correo incorrecto.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Generar código y enviar el correo
                string codigoGenerado = uiUtilidades.GenerarCodigo();
                ServicioCorreo servCorreo = new ServicioCorreo();
                bool correoEnviado = servCorreo.EnviarCorreo(txtUsuario.Text, txtCorreo.Text, codigoGenerado);

                if (!correoEnviado)
                {
                    MessageBox.Show("Ocurrió un error al enviar el correo, por favor intente nuevamente.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Se envió un correo con un código de verificación, por favor revise su bandeja de entrada.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Abrir modal para verificar el código enviado
                using (var modal = new mdVerificarCorreo(txtUsuario.Text, txtCorreo.Text, codigoGenerado))
                {
                    if (modal.ShowDialog() == DialogResult.OK && modal.codigoValido)
                    {
                        // Si el código es válido, abrir el formulario de cambio de contraseña
                        using (var modalCambiar = new mdCambiarContraseña(txtUsuario.Text))
                        {
                            if (modalCambiar.ShowDialog() == DialogResult.OK && modalCambiar.contraseñaModificada)
                            {
                                MessageBox.Show("Se modificó la contraseña.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo modificar la contraseña, consultar con el administrador.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Operación cancelada.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar el correo: " + ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


}
