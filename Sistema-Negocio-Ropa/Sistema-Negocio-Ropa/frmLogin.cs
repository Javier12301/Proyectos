using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_Negocio_Ropa.Modal;
using Negocio.Seguridad;
using Datos.Seguridad;
using Sistema_Negocio_Ropa.Principales;
using Guna.UI.WinForms;
using Sistema_Negocio_Ropa.Modal.Caja;
using Datos.Negocio;
using Negocio.Negocio;
using Sistema_Negocio_Ropa.Modal.Seguridad;

namespace Sistema_Negocio_Ropa
{
    public partial class frmLogin : Form
    {

        private bool contraseñaVisible { get; set; }
        private Utilidades uiUtilidades = Utilidades.ObtenerInstancia;
        private UsuarioDA lUsuario;
        private GrupoDA lGrupo;
        private CajaDA lCaja;
        public frmLogin()
        {
            InitializeComponent();
            lUsuario = new UsuarioDA();
            lGrupo = new GrupoDA();
            lCaja = new CajaDA();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            alternarVisibilidadContraseña();
        }

        private void IniciarSesion()
        {
            try
            {
                // Validaciones de campos
                if (!uiUtilidades.VerificarTextboxG(txtUsuarioG) || !uiUtilidades.VerificarTextboxG(txtContraseñaG))
                {
                    MessageBox.Show("Completar los campos vacíos.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir si hay campos vacíos
                }

                // Obtener el nombre de usuario
                string nombreUsuario = txtUsuarioG.Text;

                // Comprobar si existe el usuario
                if (!lUsuario.ExisteNombreUsuarioD(nombreUsuario))
                {
                    MessageBox.Show("El usuario no existe.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    uiUtilidades.errorTextboxG(txtUsuarioG, true);
                    return; // Salir si el usuario no existe
                }

                Usuario oUsuario = lUsuario.ObtenerUsuarioPorNombre(nombreUsuario);

                // Verificar el estado del grupo
                if (!oUsuario.oGrupo.Estado)
                {
                    MessageBox.Show("El grupo al que pertenece el usuario se encuentra inactivo, consultar al administrador", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    uiUtilidades.errorTextboxG(txtUsuarioG, true);
                    return; // Salir si el grupo está inactivo
                }

                // Verificar el estado del usuario
                if (!oUsuario.Estado)
                {
                    MessageBox.Show("El usuario ingresado se encuentra inactivo, consultar al administrador", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    uiUtilidades.errorTextboxG(txtUsuarioG, true);
                    return; // Salir si el usuario está inactivo
                }

                // Comprobar la contraseña
                string contraseña = uiUtilidades.EncriptarClave(txtContraseñaG.Text);
                if (oUsuario.Password != contraseña)
                {
                    MessageBox.Show("Usuario y/o contraseña incorrecta.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir si la contraseña es incorrecta
                }

                // Iniciar sesión y registrar auditoría
                oUsuario.ModulosPermitidos = lGrupo.ObtenerModulosPermitidos(oUsuario.ObtenerGrupoID());
                Sesion.IniciarSesion(oUsuario);

                bool cajaAbierta = lCaja.VerificarCajaAbierta();
                CajaM oCaja = new CajaM();

                // Si el usuario es administrador (GrupoID = 1)
                if (oUsuario.oGrupo.GrupoID == 1)
                {
                    if (cajaAbierta)
                    {
                        // Si la caja está abierta, se permite ingresar directamente para que la cierre o supervise
                        oCaja = lCaja.ObtenerCajaAbierta();
                        Sesion.IniciarCaja(oCaja);
                        MessageBox.Show("La caja está abierta. Por favor, cierre la caja desde el módulo de ventas.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        abrirFormMain(); // Se abre el formulario principal para que el administrador supervise o cierre la caja.
                    }
                    else
                    {
                        // Si no hay caja abierta, preguntar si desea abrir una nueva caja o supervisar
                        DialogResult adminDecision = MessageBox.Show("¿Desea abrir una nueva caja? Si presiona 'No', se le permitirá supervisar.", "Administrador", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (adminDecision == DialogResult.Yes)
                        {
                            using (var abrirCaja = new mdAperturaCaja())
                            {
                                var resultado = abrirCaja.ShowDialog();
                                if (resultado == DialogResult.OK)
                                {
                                    oCaja = abrirCaja._caja;
                                    Sesion.IniciarCaja(oCaja);
                                    abrirFormMain();
                                }
                                else
                                {
                                    Sesion.CerrarSesion();
                                }
                            }
                        }
                        else
                        {
                            // Si elige no abrir una caja, igual se le permite entrar al sistema para supervisar
                            abrirFormMain();
                        }
                    }
                }
                else
                {
                    // El usuario no es administrador, comportamiento normal
                    if (cajaAbierta)
                    {
                        oCaja = lCaja.ObtenerCajaAbierta();
                        Sesion.IniciarCaja(oCaja);
                        MessageBox.Show("No se hizo el cierre de caja anterior, para abrir una nueva caja, ingrese al módulo de ventas y cierre la caja.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        abrirFormMain();
                    }
                    else
                    {
                        using (var abrirCaja = new mdAperturaCaja())
                        {
                            var resultado = abrirCaja.ShowDialog();
                            if (resultado == DialogResult.OK)
                            {
                                oCaja = abrirCaja._caja;
                                Sesion.IniciarCaja(oCaja);
                                abrirFormMain();
                            }
                            else
                            {
                                Sesion.CerrarSesion();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Sesion.CerrarSesion();
            }
        }


        private void abrirFormMain()
        {
            if (Sesion.ObtenerInstancia == null)
            {
                MessageBox.Show("No se ha iniciado sesión", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Salir si no hay sesión iniciada
            }

            using (var frmMain = new frmMain())
            {
                this.Hide();
                var resultado = frmMain.ShowDialog();

                // Regresar al formulario principal según el resultado
                if (resultado == DialogResult.OK)
                {
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            IniciarSesion();
            Cursor = Cursors.Default;
        }

        private void txtCredenciales_Enter(object sender, EventArgs e)
        {
            GunaLineTextBox textbox = (GunaLineTextBox)sender;
            textbox.LineColor = Color.Gainsboro;
        }

        private void txtCredenciales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                IniciarSesion();
            }
        }

        // Interfaz

        private void alternarVisibilidadContraseña()
        {
            if (contraseñaVisible)
            {
                contraseñaVisible = false;
                uiUtilidades.MostrarContraseñaG(txtContraseñaG, btnOjo);
            }
            else
            {
                contraseñaVisible = true;
                uiUtilidades.OcultarContraseñaG(txtContraseñaG, btnOjo);
            }
        }

        private void btnContraseña_Click(object sender, EventArgs e)
        {
            using (var modal = new mdRecuperarContraseña())
            {
                modal.ShowDialog();
            }
        }

        private void btnOjo_Click(object sender, EventArgs e)
        {
            alternarVisibilidadContraseña();

        }

        private void btnOjo_MouseEnter(object sender, EventArgs e)
        {
            btnOjo.IconFont = IconFont.Solid;

        }

        private void btnOjo_MouseLeave(object sender, EventArgs e)
        {
            btnOjo.IconFont = IconFont.Regular;

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

      

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            // Modificar Correo de negocio, solo administrador
            using (var modal = new mdAdminServCorreo())
            {
                modal.ShowDialog();
            }
        }

    }
}
