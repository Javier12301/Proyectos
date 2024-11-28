using Datos.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Negocio_Ropa.Modal
{
    public partial class mdBackup : Form
    {
        public mdBackup()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
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
        BackupDA lBackup = new BackupDA();
        private void iconButton1_Click(object sender, EventArgs e)
        {
            
            try
            {
                // Mensaje de confirmación
                DialogResult respuesta = MessageBox.Show("¿Desea realizar un backup de la base de datos?", "Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    // Usar SaveFileDialog para seleccionar la ruta y el nombre del archivo
                    saveFile.AddExtension = true;
                    saveFile.Filter = "Archivos de backup (*.bak)|*.bak";
                    saveFile.Title = "Guardar backup";

                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        // Obtener la ruta seleccionada por el usuario
                        string rutaBackup = saveFile.FileName;

                        // Obtener el nombre del archivo (sin la extensión .bak) para usarlo como el nombre descriptivo
                        string nombreBackup = System.IO.Path.GetFileNameWithoutExtension(rutaBackup);

                        // Llamar al método que ejecuta el procedimiento almacenado
                       // MessageBox.Show(lBackup.GenerarBackup(rutaBackup, nombreBackup));
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Error al realizar el backup: " + ex.Message);
            }
        }


        private void btnRecuperar_Click(object sender, EventArgs e)
        {
            if (txtRutaCargar.Text != "")
            {
                // Llamar al método para restaurar el backup y mostrar el resultado
                //MessageBox.Show(lBackup.RestaurarBackup(txtRutaCargar.Text));
            }
            else
            {
                MessageBox.Show("Debe seleccionar un archivo de backup");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                // Eliminar el directorio fijo para permitir al usuario navegar donde desee
                openFile.Title = "Seleccionar archivo de backup";
                openFile.Filter = "Archivos de backup (*.bak)|*.bak"; // Solo archivos .bak
                openFile.FilterIndex = 1;
                openFile.CheckFileExists = true; // Validar que el archivo existe
                openFile.CheckPathExists = true; // Validar que la ruta existe
                openFile.RestoreDirectory = true; // Restaurar el último directorio usado

                // Si el usuario selecciona un archivo y confirma, asignarlo al txtRuta
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    // Verificar si el archivo tiene extensión .bak
                    if (Path.GetExtension(openFile.FileName).ToLower() == ".bak")
                    {
                        txtRutaCargar.Text = openFile.FileName; // Mostrar la ruta en el textbox
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar un archivo de backup válido (.bak)", "Archivo inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnBuscarGenerar_Click(object sender, EventArgs e)
        {
            saveFile.AddExtension = true;
            saveFile.Filter = "Archivos de backup (*.bak)|*.bak";
            saveFile.Title = "Guardar backup";
            saveFile.ShowDialog();
            txtRutaGenerar.Text = saveFile.FileName;
            
        }

        private BackupDA backup = new BackupDA();
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            /*Obtener datos necesarios de app.config*/
            /*<connectionStrings>
    <add name="TiendaDeRopaDB" connectionString="Data Source=localhost;Initial Catalog=NegocioRopa;Integrated Security=True;" providerName="System.Data.SqlClient" />
  </connectionStrings>*/

            //     <add key="NombreBaseDatos" value="NegocioRopa"/>
            /*BACKUP LOG [NegocioRopa] TO  DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Backup\NegocioRopa_LogBackup_2024-11-24_21-59-36.bak' WITH NOFORMAT, NOINIT,  NAME = N'NegocioRopa_LogBackup_2024-11-24_21-59-36', NOSKIP, NOREWIND, NOUNLOAD,  NORECOVERY ,  STATS = 5
RESTORE DATABASE [NegocioRopa] FROM  DISK = N'C:\Proyect\negocio.bak' WITH  FILE = 5,  NOUNLOAD,  STATS = 5*/

            StringBuilder query = new StringBuilder();
            string nombreBaseDatos = ConfigurationManager.AppSettings["NombreBaseDatos"];
            backup.general_query(query.ToString());
            query.AppendLine("BACKUP DATABASE "+ nombreBaseDatos + " TO DISK = '" + txtRutaGenerar.Text + "' WITH NOFORMAT, NOINIT, NAME = 'NegocioRopa_Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10");
            MessageBox.Show("Backup generado con éxito");
        }
    }
}
