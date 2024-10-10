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
    }
}
