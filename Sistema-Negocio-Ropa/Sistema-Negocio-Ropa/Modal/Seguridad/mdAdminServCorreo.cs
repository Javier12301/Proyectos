using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Negocio_Ropa.Modal.Seguridad
{
    public partial class mdAdminServCorreo : Form
    {
        public mdAdminServCorreo()
        {
            InitializeComponent();
        }

        private void mdAdminServCorreo_Load(object sender, EventArgs e)
        {
            // cargar credenciales actuales 
            lblCorreoGestor.Visible = false;
            lblContrasena.Visible = false;
            btnGuardar.Visible = false;
        }
        Utilidades uiUti = Utilidades.ObtenerInstancia;

        private void txtConfirmarClave_Click(object sender, EventArgs e)
        {
            string clave = uiUti.EncriptarClave(txtConfirmar.Text);
            if (string.IsNullOrEmpty(txtConfirmar.Text))
            {
                MessageBox.Show("Debe ingresar una clave", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string claveActual = ConfigurationManager.AppSettings["ClaveEncriptada"];
            if(string.Equals(clave, claveActual))
            {
                // formulario con tamaño 673; 299
                this.Size = new Size(673, 299);
                lblCorreoGestor.Visible = true;
                txtCorreo.Visible = true;
                lblContrasena.Visible = true;
                txtContrasenaRobot.Visible = true;
                btnGuardar.Visible = true;
                txtCorreo.Text = ConfigurationManager.AppSettings["Email"];
                txtContrasenaRobot.Text = ConfigurationManager.AppSettings["Contrasena"];
                txtCadena.Text = ConfigurationManager.ConnectionStrings["TiendaDeRopaDB"].ConnectionString;
                pnlCadena.Visible = true;
                
                lblGestor.Visible = false;
                txtConfirmar.Visible = false;
                txtConfirmar.Enabled = false;
                txtConfirmarClave.Visible = false;
                txtConfirmarClave.Enabled = false;
            }
            else
            {
                MessageBox.Show("Clave incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCorreo.Text))
            {
                MessageBox.Show("Debe ingresar un correo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtContrasenaRobot.Text))
            {
                MessageBox.Show("Debe ingresar una contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["Email"].Value = txtCorreo.Text;
            config.AppSettings.Settings["Contrasena"].Value = txtContrasenaRobot.Text;

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");


            MessageBox.Show("Credenciales actualizadas exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string nuevaCadenaConexion = txtCadena.Text;

            // Verificar si el TextBox no está vacío
            if (string.IsNullOrEmpty(nuevaCadenaConexion))
            {
                MessageBox.Show("La cadena de conexión no puede estar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Actualizar la cadena de conexión en el App.config
            ActualizarCadenaConexion(nuevaCadenaConexion);

            MessageBox.Show("La cadena de conexión se ha actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void ActualizarCadenaConexion(string nuevaCadenaConexion)
        {
            // Abrir el archivo de configuración
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Actualizar la cadena de conexión
            config.ConnectionStrings.ConnectionStrings["TiendaDeRopaDB"].ConnectionString = nuevaCadenaConexion;

            // Guardar los cambios en el archivo de configuración
            config.Save(ConfigurationSaveMode.Modified);

            // Refrescar la sección de connectionStrings para aplicar los cambios
            ConfigurationManager.RefreshSection("connectionStrings");
        }

    }
}
