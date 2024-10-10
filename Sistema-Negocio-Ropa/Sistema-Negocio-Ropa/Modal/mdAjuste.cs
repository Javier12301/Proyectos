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
    public partial class mdAjuste : Form
    {
        public string OpcionSeleccionada { get; set; }
        private Sesion lSesion = Sesion.ObtenerInstancia;
        private Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        private int contador = 0;

        public mdAjuste()
        {
            InitializeComponent();
            OpcionSeleccionada = string.Empty;
        }

        private void mdAjuste_Load(object sender, EventArgs e)
        {
               cargarPermisos();
        }

        private void cargarPermisos()
        {
            List<Modulo> modulosPermitidos = lSesion.UsuarioEnSesion().ObtenerModulosPermitidos();

            bool activarBtnPerfiles = modulosPermitidos.Any(m => m.Nombre == "formGrupos" || m.Nombre == "formUsuarios");
            btnPerfiles.Enabled = activarBtnPerfiles;
            bool activarBtnBackup = modulosPermitidos.Any(m => m.Nombre == "formBackup");
            btnBaseDatos.Enabled = activarBtnBackup;
            bool activarBtnNegocio = modulosPermitidos.Any(m => m.Nombre == "formNegocio");
            btnNegocio.Enabled = activarBtnNegocio;
            btnMisDatos.Enabled = true;
        }

        private void btnPerfiles_Click(object sender, EventArgs e)
        {
            OpcionSeleccionada = "Perfiles";
            this.Close();
        }


        private void btnNegocio_Click(object sender, EventArgs e)
        {
            OpcionSeleccionada = "Negocio";
            this.Close();
        }

        private void btnBaseDatos_Click(object sender, EventArgs e)
        {
            contador++;
            using (var modal = new mdBackup())
            {
                if(contador == 0)
                {
                    MessageBox.Show("Información: La opción 'Generar Backup' permite crear una copia de seguridad de los datos de su negocio. Esto es útil en caso de pérdida de información. La opción 'Cargar Backup' le permite seleccionar la ubicación de una copia previamente guardada para restaurar sus datos.",
                        "Información sobre Backup",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                modal.ShowDialog();
                this.Close();
            }
        }

        private void btnMisDatos_Click(object sender, EventArgs e)
        {
            OpcionSeleccionada = "Mis Datos";
            this.Close();
        }


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
