using ClosedXML.Excel;
using Datos.Seguridad;
using FontAwesome.Sharp;
using Guna.UI.WinForms;
using Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

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

        public void ExportarDataGridViewAExcel(DataGridView dgv, string nombreArchivo, string nombreHoja, string modulo)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dgv.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn columna in dgv.Columns)
                {
                    dt.Columns.Add(columna.HeaderText, columna.ValueType);
                }

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Visible)
                    {
                        dt.Rows.Add(row.Cells.Cast<DataGridViewCell>().Select(c => c.Value).ToArray());
                    }
                }

                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format($"{nombreArchivo}.xlsx");
                savefile.Filter = "Excel Files | *.xlsx";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var hoja = wb.Worksheets.Add(dt, nombreHoja);
                            hoja.ColumnsUsed().AdjustToContents();

                            // La ultima columna siempre ponerle tamaño 15
                            int columnasTotales = hoja.ColumnsUsed().Count();
                            hoja.Column(columnasTotales).Width = 15;

                            wb.SaveAs(savefile.FileName);
                            AuditoriaDA auditoriaDA = new AuditoriaDA();
                            auditoriaDA.RegistrarMovimiento("Exportar", Sesion.ObtenerInstancia.UsuarioEnSesion().ObtenerNombreUsuario(), modulo, $"Archivo excel con nombre ({nombreArchivo}) fue exportado con éxito.");
                            MessageBox.Show("Datos exportados correctamente.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    // Catch cuando el archivo está abierto
                    catch (System.IO.IOException)
                    {
                        throw new Exception("El archivo está abierto, por favor cierre el archivo y vuelva a intentarlo.");
                    }
                    catch (Exception)
                    {
                        throw new Exception("Ocurrió un error al exportar los datos, por favor contacte al administrador para solucionar este error.");
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            Cursor.Current = Cursors.Default;
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
