﻿using Datos.Seguridad;
using Negocio.Seguridad;
using Sistema_Negocio_Ropa.Modal;
using Sistema_Negocio_Ropa.Modal.Seguridad;
using Sistema_Negocio_Ropa.Principales.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Negocio_Ropa.Principales
{
    public partial class frmMain : Form
    {

        private Form formularioActivo;
        private bool formularioAntiCierre = false;
        private ToolStripButton botonActivo;
        // controladoras
        private Sesion lSesion;
        private UsuarioDA lUsuario;
        private Utilidades uiUtilidades;
        
        public frmMain()
        {
            InitializeComponent();
            lSesion = Sesion.ObtenerInstancia;
            lUsuario = new UsuarioDA();
            uiUtilidades = Utilidades.ObtenerInstancia;
        }

      
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                // poner pantalla completa
                this.WindowState = FormWindowState.Maximized;
                cargarSesion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarSesion()
        {
            lblUsuario.Text = lSesion.UsuarioEnSesion().ObtenerNombreUsuario();
            lblGrupo.Text = lSesion.UsuarioEnSesion().GrupoPerteneciente();
            List<Modulo> modulosPermitidos = lSesion.UsuarioEnSesion().ObtenerModulosPermitidos();

            foreach (ToolStripItem item in tsSuperior.Items)
            {
                if (item is ToolStripButton && ((ToolStripButton)item).Tag != null)
                {
                    // Descripción del módulo del tag del botón
                    string descripcionModulo = ((ToolStripButton)item).Tag.ToString();
                    // Verificar los modulos permitidos, los que no desactivar.
                    bool moduloPermitido = modulosPermitidos.Any(modulo => modulo.Nombre == descripcionModulo);

                    if (moduloPermitido)
                    {
                        ((ToolStripButton)item).Enabled = true;
                        ((ToolStripButton)item).Visible = true;
                    }
                    else
                    {
                        ((ToolStripButton)item).Enabled = false;
                        ((ToolStripButton)item).Visible = false;
                    }
                }
            }
            btnLogOut.Enabled = true;
            btnLogOut.Visible = true;
   
        }

        private void activarBoton(ToolStripButton btnSender)
        {
            if (btnSender != null)
            {
                if (botonActivo != btnSender)
                {
                    // Desactiva el botón que estaba activo
                    if (botonActivo != null)
                    {
                        desactivarBoton(botonActivo);
                    }

                    // Activamos el botón que fue presionado
                    botonActivo = btnSender;
                    // Color azul predeterminado del sistema
                    botonActivo.BackColor = Color.FromArgb(4, 127, 176);
                    // cambiamos el color del borde
                    // Color blanco para el texto
                    botonActivo.ForeColor = Color.White;
                }
            }
        }

        private void desactivarBoton(ToolStripButton btnSender)
        {
            // Color blanco predeterminado del sistema
            btnSender.BackColor = Color.FromArgb(242, 248, 255);
            // cambiamos el color del borde a silver
            btnSender.ForeColor = Color.Black;
        }

        // Abrir Formularios dentro del panel padre
        private void abrirFormularioHijo(Form formularioHijo, ToolStripButton btnSender, bool FormAntiCierre = false)
        {
            if (formularioAntiCierre)
            {
                if (formularioActivo != null)
                {
                    /*
                    if (formularioActivo is frmVentas)
                    {
                        // obtener instancia del formulario venta activa
                        frmVentas formVenta = (frmVentas)formularioActivo;
                        if (formVenta.productoEnGrilla)
                        {
                            DialogResult respuesta = MessageBox.Show("¿Está seguro que desea cerrar el formulario? se perderán todos los cambios realizados.", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (respuesta == DialogResult.No)
                            {
                                return;
                            }
                        
                    }
                        */
                }

            }
            Cursor.Current = Cursors.WaitCursor;
            // Resaltamos el botón activado
            activarBoton(btnSender);

            // Si hay un formulario abierto, lo cerramos
            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }
            // Abrimos el formulario hijo
            formularioActivo = formularioHijo;
            formularioAntiCierre = FormAntiCierre;
            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            formularioHijo.Dock = DockStyle.Fill;
            pnlPadre.Controls.Add(formularioHijo);
            pnlPadre.Tag = formularioHijo;
            // Ponemos al frente el formulario hijo
            formularioHijo.BringToFront();

            // Abrimos el formulario
            formularioHijo.Show();
            Cursor.Current = Cursors.Default;
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            abrirFormularioHijo(new frmVentas(), btnVentas, true);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            abrirFormularioHijo(new frmProductos(), btnProductos);
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            abrirFormularioHijo(new frmInventario(), btnInventario);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            abrirFormularioHijo(new frmReporte(), btnReportes);
        }

        private void btnAjustes_Click(object sender, EventArgs e)
        {
            using(var modal = new mdAjuste())
            {
                modal.ShowDialog();
                switch (modal.OpcionSeleccionada)
                {
                    case "Perfiles":
                        abrirFormularioHijo(new frmPerfiles(), btnAjustes);
                        break;
                    case "Negocio":
                        using (var modalNegocio = new frmNegocio())
                        {
                            modalNegocio.ShowDialog();
                        }
                        break;
                    case "Mis Datos":
                        ModalMisDatos();
                        break;
                    default:
                        break;
                }
            }

        }

        private void ModalMisDatos()
        {
            // Cargar datos del usuario en sesión
            int usuarioID = lSesion.UsuarioEnSesion().UsuarioID;
            // Comprobar si es el usuario admin
            if (lSesion.UsuarioEnSesion().ObtenerNombreUsuario() == "Admin")
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de modificar los datos del usuario admin?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    abrirFormularioMisDatos(usuarioID);
                }
            }
            else if (lSesion.UsuarioEnSesion().ObtenerNombreGrupo() == "Administrador")
            {
                DialogResult resultado = MessageBox.Show("¿Desea modificar su usuario perteneciente al grupo administrador?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    abrirFormularioMisDatos(usuarioID);
                }
            }
            else
            {
                abrirFormularioMisDatos(usuarioID);
            }
        }

        private void abrirFormularioMisDatos(int usuarioID)
        {
            using (var modal = new mdUsuario(true, usuarioID))
            {
                var resultado = modal.ShowDialog();
                if (resultado == DialogResult.OK)
                {
                    MessageBox.Show("Se cerrará la sesión para aplicar los cambios.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Obtenemos la instancia del formulario principal que es el padre de todos y luego cerramos la sesión
                    // haciendo uso de su función encargada de cerrar la sesión.
                    frmMain formMain = (frmMain)Application.OpenForms["frmMain"];
                    formMain.Cerrar_Sesion();
                }
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(respuesta == DialogResult.Yes)
            {
                Cerrar_Sesion();
            }
        }


        public void Cerrar_Sesion(bool cerrarCaja = false)
        {
            try
            {

                Sesion.CerrarSesion(cerrarCaja);
                this.DialogResult = DialogResult.OK;
                this.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        
    }
}
