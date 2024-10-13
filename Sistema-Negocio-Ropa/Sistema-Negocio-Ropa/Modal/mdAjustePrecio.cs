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
    public partial class mdAjustePrecio : Form
    {
        public mdAjustePrecio()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // hacer un enum para seleccionar mdAjustePrecioUnidad o mdAjustePrecioCategoria

        public string TipoSeleccionado { get; private set; }
        private void btnUnidad_Click(object sender, EventArgs e)
        {
            TipoSeleccionado = "Unidad"; // Usamos un string para representar la opción
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCategoriaP_Click(object sender, EventArgs e)
        {
            TipoSeleccionado = "Categoria"; // Usamos un string para representar la opción
            this.DialogResult = DialogResult.OK;
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
    }
}
