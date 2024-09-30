using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Sistema_Negocio_Ropa.Modal
{
    public partial class mdVerificarCorreo : Form
    {
        Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        private string codigoGenerado { get; set; }
        public bool codigoValido { get; set; }
        private string nombreUsuario { get; set; }
        private string correo { get; set; }
        public mdVerificarCorreo(string nombreUsuario, string correo, string codigoGenerado)
        {
            InitializeComponent();
            this.codigoGenerado = codigoGenerado;
            codigoValido = false;
            this.nombreUsuario = nombreUsuario;
            this.correo = correo;
        }

        private void mdVerificarCorreo_Load(object sender, EventArgs e)
        {
            lblMail.Text += $" {correo}";
            lblUsuario.Text += $" {nombreUsuario}";
            uiUtilidades.LimpiarTextbox(txt1, txt2, txt3, txt4, txt5);
            txt1.Select();
        }

        private void txtN_Changed(object sender, EventArgs e)
        {
            verificarCodigo();
        }

        private void txtN_KeyPress(object sender, KeyPressEventArgs e)
        {
            // solo mayusculas
            uiUtilidades.SoloMayusculasYNumeros(e);
            if (e.KeyChar == (char)Keys.Enter)
            {
                verificarCodigo();
            }
            siguienteTxtN(sender, e);
        }


        private void verificarCodigo()
        {
            string codigo = txt1.Text + txt2.Text + txt3.Text + txt4.Text + txt5.Text;
            if (codigo == codigoGenerado)
            {
                codigoValido = true;
                // cerrar con un ok
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // Manejo de interfaz

        // Cuando se presiona una tecla en los textbox, se tabulará
        private void siguienteTxtN(object sender, KeyPressEventArgs e)
        {
            TextBox txtN = (TextBox)sender;
            if (e.KeyChar != (char)Keys.Back)
            {
                if (txtN.TabIndex < 4)
                {
                    SendKeys.Send("{TAB}");
                }
            }
            else if (txtN.TabIndex > 0)
            {
                SendKeys.Send("+{TAB}");
            }
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

        private void gControlCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                this.Close();
            }
            else
            {
                return;
            }
        }
    }
}
