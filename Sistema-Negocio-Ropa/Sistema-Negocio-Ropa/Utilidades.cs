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
using System.Globalization;

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

        public bool VerificarTextboxPrecio(TextBoxBase textbox, ErrorProvider mensajeError)
        {
            if (!string.IsNullOrEmpty(textbox.Text))
            {
                // No está vacío
                mensajeError.SetError(textbox, "");
                return true;
            }
            else
            {
                // Está vacío
                mensajeError.SetError(textbox, "Este campo no puede estar vacío.");
                return false;
            }
        }

        public void TextboxMoneda_KeyPress(TextBox textbox, KeyPressEventArgs e, ErrorProvider error)
        {
            error.SetError(textbox, "");
            // Formato regional de Argentina
            if (e.KeyChar == '.')
            {
                e.Handled = true;
                SendKeys.Send(",");
            }

            // Solo puede haber una coma en el textbox
            if (e.KeyChar == ',')
            {
                if (textbox.Text.Contains(","))
                {
                    error.SetError(textbox, "Solo puede haber una coma en el campo.");
                    e.Handled = true;
                }
            }

            // Solo permite números y control keys (como backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar == '.' || e.KeyChar == ','))
            {
                error.SetError(textbox, "Solo se permiten números en este campo.");
                e.Handled = true;
            }
        }


        public void TextboxMoneda_Leave(TextBox textbox, EventArgs e)
        {
            // Formato regional de Argentina
            // 1.000,00
            if (string.IsNullOrEmpty(textbox.Text))
                textbox.Text = "0.00";
            textbox.Text = string.Format(CultureInfo.GetCultureInfo("es-AR"), "{0:N2}", Convert.ToDecimal(textbox.Text));

        }

        public bool VerificarFormatoMoneda(TextBoxBase textbox, ErrorProvider mensajeError)
        {
            if (decimal.TryParse(textbox.Text, out decimal valor))
            {
                if (valor == 0.00m)  // Verifica si el valor es igual a 0.00
                {
                    mensajeError.SetError(textbox, "El valor ingresado debe ser mayor a 0.");
                    return false;
                }
                else
                {
                    mensajeError.SetError(textbox, "");
                    return true;
                }
            }
            else
            {
                mensajeError.SetError(textbox, "El valor ingresado no cumple con el formato moneda, si desea agregar centavos, utilice el formato 0.00");
                return false;
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

        public void SoloNumero(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        public void CargarPermisos(string nombreFormulario, FlowLayoutPanel flpContenedorBotones, Permiso permisoDeUsuario = null)
        {
            List<Modulo> modulosPermitidos = Sesion.ObtenerInstancia.UsuarioEnSesion().ObtenerModulosPermitidos();

            Modulo modulosActivos = modulosPermitidos.FirstOrDefault(m => m.Nombre == nombreFormulario);

            List<Accion> accionesPermitidas = null;
            if (modulosActivos != null)
            {
                accionesPermitidas = modulosActivos.ListaAcciones;
            }

            // Buscar el módulo correspondiente al formulario actual
            Modulo moduloActual = modulosPermitidos.FirstOrDefault(m => m.Nombre == nombreFormulario);

            // Si se encuentra el módulo, cargar las opciones
            if (moduloActual != null)
            {
                foreach (Control control in flpContenedorBotones.Controls)
                {
                    if (control is Button && control.Tag != null)
                    {
                        // Obtener el nombre de la acción desde el Tag del botón
                        string nombreAccionBoton = control.Tag.ToString();

                        // Verificar si el nombre de la acción está en la lista de acciones del módulo
                        bool tienePermiso = moduloActual.ListaAcciones.Any(accion => accion.Nombre == nombreAccionBoton);

                        // Activar o desactivar el botón según los permisos
                        control.Enabled = tienePermiso;
                        control.Visible = tienePermiso;
                    }
                }
                if (permisoDeUsuario != null)
                {
                    permisoDeUsuario.Alta = moduloActual.ListaAcciones.Any(accion => accion.Nombre == "Alta");
                    permisoDeUsuario.Modificar = moduloActual.ListaAcciones.Any(accion => accion.Nombre == "Modificar");
                    permisoDeUsuario.Baja = moduloActual.ListaAcciones.Any(accion => accion.Nombre == "Baja");
                    permisoDeUsuario.Comprar = moduloActual.ListaAcciones.Any(accion => accion.Nombre == "Comprar");
                    permisoDeUsuario.Vender = moduloActual.ListaAcciones.Any(accion => accion.Nombre == "Vender");
                    permisoDeUsuario.Exportar = moduloActual.ListaAcciones.Any(accion => accion.Nombre == "Exportar");

                    permisoDeUsuario.Permitir_Acceso = moduloActual.ListaAcciones.Any(accion => accion.Nombre == "Permitir_Acceso");
                }
            }
        }
    }
}
