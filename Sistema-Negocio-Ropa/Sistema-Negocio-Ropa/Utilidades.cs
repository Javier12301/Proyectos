using FontAwesome.Sharp;
using Guna.UI.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Negocio_Ropa
{
    public class Utilidades
    {

        // Singleton
        private static Utilidades _instancia = null;
        private Utilidades()
        {
        }
        public static Utilidades ObtenerInstancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new Utilidades();
                }
                return _instancia;
            }
        }

        public void MostrarContraseña(TextBox textbox, IconButton btnOjo)
        {
            textbox.PasswordChar = (char)0;
            btnOjo.IconFont = IconFont.Solid;
        }

        public void OcultarContraseña(TextBox textbox, IconButton btnOjo)
        {
            textbox.PasswordChar = '*';
            btnOjo.IconFont = IconFont.Regular;
        }

        public void LimpiarTextbox(params TextBoxBase[] textboxes)
        {
            foreach (TextBoxBase txbox in textboxes)
            {
                txbox.Text = "";
            }
        }

        // Setear a index 0 a varios combobox
        public void LimpiarCombobox(params ComboBox[] comboboxes)
        {
            foreach (ComboBox combobox in comboboxes)
            {
                combobox.SelectedIndex = 0;
            }
        }

        public void Combobox_ShortcutSiguienteIndex(ComboBox combobox, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (combobox.SelectedIndex == combobox.Items.Count - 1)
                {
                    combobox.SelectedIndex = 0;
                }
                else
                {
                    combobox.SelectedIndex++;
                }
            }
        }

        public void SoloMayusculasYNumeros(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
        }

        public void MostrarContraseñaG(GunaLineTextBox textbox, IconButton btnOjo)
        {
            textbox.PasswordChar = (char)0;
            btnOjo.IconFont = IconFont.Solid;
        }

        public void OcultarContraseñaG(GunaLineTextBox textbox, IconButton btnOjo)
        {
            textbox.PasswordChar = '*';
            btnOjo.IconFont = IconFont.Regular;
        }

        public bool errorTextboxG(GunaLineTextBox textbox, bool error)
        {
            if (error)
            {
                textbox.LineColor = ColorTranslator.FromHtml("#FF4136");
                return true;
            }
            else
            {
                textbox.LineColor = ColorTranslator.FromHtml("#5AA8E1");
                return false;
            }
        }

        public bool VerificarTextbox(TextBoxBase textbox, ErrorProvider mensajeError, Label lbl)
        {
            if (!string.IsNullOrEmpty(textbox.Text))
            {
                // No está vacío
                mensajeError.SetError(lbl, "");
                return true;
            }
            else
            {
                // Está vacío
                mensajeError.SetError(lbl, "Este campo no puede estar vacío.");
                return false;
            }
        }

        // Estos tipos de textbox poseen lineas inferiores, lo que indica visualmente al usuario donde ocurrio un error
        public bool VerificarTextboxG(GunaLineTextBox textbox)
        {
            if (!string.IsNullOrEmpty(textbox.Text))
            {
                errorTextboxG(textbox, false);
                return true;
            }
            else
            {
                errorTextboxG(textbox, true);
                return false;
            }
        }


        // Verificar mail correcto
        public bool VerificarMail(TextBoxBase textbox, ErrorProvider mensajeError, Label lbl)
        {
            if (textbox.Text.Contains("@"))
            {
                mensajeError.SetError(lbl, "");
                return true;
            }
            else
            {
                mensajeError.SetError(lbl, "El mail ingresado no es válido.");
                return false;
            }
        }

        public bool VerificarMailG(GunaLineTextBox textbox)
        {
            if (textbox.Text.Contains("@"))
            {
                textbox.LineColor = ColorTranslator.FromHtml("#5AA8E1");
                return true;
            }
            else
            {
                textbox.LineColor = ColorTranslator.FromHtml("#FF4136");
                return false;
            }
        }

        // verificar combobox que no esté en selectindex 0
        public bool VerificarCombobox(ComboBox combobox, ErrorProvider mensajeError, Label lbl)
        {
            if (combobox.SelectedIndex != 0)
            {
                mensajeError.SetError(lbl, "");
                return true;
            }
            else
            {
                mensajeError.SetError(lbl, "Debe seleccionar una opción.");
                return false;
            }
        }

        public string EncriptarClave(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] hashValue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashValue = sha256.ComputeHash(objUtf8.GetBytes(str));
            string hashString = BitConverter.ToString(hashValue).Replace("-", "");
            return hashString;
        }

        public string GenerarCodigo()
        {
            var caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[5];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = caracteres[random.Next(caracteres.Length)];
            }
            return new String(stringChars);
        }
    }
}
