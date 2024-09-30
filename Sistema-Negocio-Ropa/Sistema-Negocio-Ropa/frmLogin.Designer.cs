namespace Sistema_Negocio_Ropa
{
    partial class frmLogin
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlPrincipal = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnContraseña = new System.Windows.Forms.Button();
            this.btnOjo = new FontAwesome.Sharp.IconButton();
            this.icoCandado = new FontAwesome.Sharp.IconPictureBox();
            this.icoUsuario = new FontAwesome.Sharp.IconPictureBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtContraseñaG = new Guna.UI.WinForms.GunaLineTextBox();
            this.txtUsuarioG = new Guna.UI.WinForms.GunaLineTextBox();
            this.pbLogoEmpresa = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblNombreEmpresa = new System.Windows.Forms.Label();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.gControlCerrar = new Guna.UI.WinForms.GunaControlBox();
            this.lblNombreForm = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icoCandado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icoUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogoEmpresa)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pnlPrincipal);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 417);
            this.panel1.TabIndex = 0;
            // 
            // pnlPrincipal
            // 
            this.pnlPrincipal.BackColor = System.Drawing.Color.White;
            this.pnlPrincipal.Controls.Add(this.btnSalir);
            this.pnlPrincipal.Controls.Add(this.btnContraseña);
            this.pnlPrincipal.Controls.Add(this.btnOjo);
            this.pnlPrincipal.Controls.Add(this.icoCandado);
            this.pnlPrincipal.Controls.Add(this.icoUsuario);
            this.pnlPrincipal.Controls.Add(this.btnLogin);
            this.pnlPrincipal.Controls.Add(this.txtContraseñaG);
            this.pnlPrincipal.Controls.Add(this.txtUsuarioG);
            this.pnlPrincipal.Controls.Add(this.pbLogoEmpresa);
            this.pnlPrincipal.Controls.Add(this.panel2);
            this.pnlPrincipal.Controls.Add(this.pnlControl);
            this.pnlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrincipal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.pnlPrincipal.Location = new System.Drawing.Point(0, 0);
            this.pnlPrincipal.Name = "pnlPrincipal";
            this.pnlPrincipal.Size = new System.Drawing.Size(302, 417);
            this.pnlPrincipal.TabIndex = 2;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.IndianRed;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSalir.Location = new System.Drawing.Point(43, 374);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(225, 37);
            this.btnSalir.TabIndex = 6;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnContraseña
            // 
            this.btnContraseña.BackColor = System.Drawing.Color.White;
            this.btnContraseña.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContraseña.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnContraseña.FlatAppearance.BorderSize = 0;
            this.btnContraseña.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnContraseña.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnContraseña.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnContraseña.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnContraseña.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnContraseña.Location = new System.Drawing.Point(142, 300);
            this.btnContraseña.Name = "btnContraseña";
            this.btnContraseña.Size = new System.Drawing.Size(156, 25);
            this.btnContraseña.TabIndex = 5;
            this.btnContraseña.Text = "Olvide mi contraseña";
            this.btnContraseña.UseVisualStyleBackColor = false;
            this.btnContraseña.Click += new System.EventHandler(this.btnContraseña_Click);
            // 
            // btnOjo
            // 
            this.btnOjo.BackColor = System.Drawing.Color.White;
            this.btnOjo.FlatAppearance.BorderSize = 0;
            this.btnOjo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnOjo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnOjo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOjo.IconChar = FontAwesome.Sharp.IconChar.Eye;
            this.btnOjo.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.btnOjo.IconFont = FontAwesome.Sharp.IconFont.Regular;
            this.btnOjo.IconSize = 29;
            this.btnOjo.Location = new System.Drawing.Point(269, 267);
            this.btnOjo.Name = "btnOjo";
            this.btnOjo.Size = new System.Drawing.Size(29, 27);
            this.btnOjo.TabIndex = 3;
            this.btnOjo.UseVisualStyleBackColor = false;
            this.btnOjo.Click += new System.EventHandler(this.btnOjo_Click);
            this.btnOjo.MouseEnter += new System.EventHandler(this.btnOjo_MouseEnter);
            this.btnOjo.MouseLeave += new System.EventHandler(this.btnOjo_MouseLeave);
            // 
            // icoCandado
            // 
            this.icoCandado.BackColor = System.Drawing.Color.White;
            this.icoCandado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.icoCandado.IconChar = FontAwesome.Sharp.IconChar.Lock;
            this.icoCandado.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.icoCandado.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.icoCandado.IconSize = 29;
            this.icoCandado.Location = new System.Drawing.Point(8, 265);
            this.icoCandado.Name = "icoCandado";
            this.icoCandado.Size = new System.Drawing.Size(29, 29);
            this.icoCandado.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.icoCandado.TabIndex = 26;
            this.icoCandado.TabStop = false;
            // 
            // icoUsuario
            // 
            this.icoUsuario.BackColor = System.Drawing.Color.White;
            this.icoUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.icoUsuario.IconChar = FontAwesome.Sharp.IconChar.User;
            this.icoUsuario.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.icoUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.icoUsuario.IconSize = 29;
            this.icoUsuario.Location = new System.Drawing.Point(8, 219);
            this.icoUsuario.Name = "icoUsuario";
            this.icoUsuario.Size = new System.Drawing.Size(29, 29);
            this.icoUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.icoUsuario.TabIndex = 25;
            this.icoUsuario.TabStop = false;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLogin.Location = new System.Drawing.Point(43, 331);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(225, 37);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Iniciar Sesión";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtContraseñaG
            // 
            this.txtContraseñaG.BackColor = System.Drawing.Color.White;
            this.txtContraseñaG.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtContraseñaG.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.txtContraseñaG.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContraseñaG.LineColor = System.Drawing.Color.Gainsboro;
            this.txtContraseñaG.Location = new System.Drawing.Point(43, 265);
            this.txtContraseñaG.Name = "txtContraseñaG";
            this.txtContraseñaG.PasswordChar = '*';
            this.txtContraseñaG.Size = new System.Drawing.Size(225, 29);
            this.txtContraseñaG.TabIndex = 2;
            this.txtContraseñaG.Text = "Reloj12301";
            // 
            // txtUsuarioG
            // 
            this.txtUsuarioG.BackColor = System.Drawing.Color.White;
            this.txtUsuarioG.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUsuarioG.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.txtUsuarioG.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuarioG.LineColor = System.Drawing.Color.Gainsboro;
            this.txtUsuarioG.Location = new System.Drawing.Point(43, 219);
            this.txtUsuarioG.Name = "txtUsuarioG";
            this.txtUsuarioG.PasswordChar = '\0';
            this.txtUsuarioG.Size = new System.Drawing.Size(247, 29);
            this.txtUsuarioG.TabIndex = 1;
            this.txtUsuarioG.Text = "Admin";
            // 
            // pbLogoEmpresa
            // 
            this.pbLogoEmpresa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLogoEmpresa.Image = ((System.Drawing.Image)(resources.GetObject("pbLogoEmpresa.Image")));
            this.pbLogoEmpresa.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pbLogoEmpresa.Location = new System.Drawing.Point(76, 44);
            this.pbLogoEmpresa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pbLogoEmpresa.Name = "pbLogoEmpresa";
            this.pbLogoEmpresa.Size = new System.Drawing.Size(151, 109);
            this.pbLogoEmpresa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogoEmpresa.TabIndex = 12;
            this.pbLogoEmpresa.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.lblNombreEmpresa);
            this.panel2.Location = new System.Drawing.Point(12, 159);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(278, 48);
            this.panel2.TabIndex = 0;
            // 
            // lblNombreEmpresa
            // 
            this.lblNombreEmpresa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblNombreEmpresa.AutoEllipsis = true;
            this.lblNombreEmpresa.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreEmpresa.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblNombreEmpresa.Location = new System.Drawing.Point(22, 2);
            this.lblNombreEmpresa.Name = "lblNombreEmpresa";
            this.lblNombreEmpresa.Size = new System.Drawing.Size(234, 41);
            this.lblNombreEmpresa.TabIndex = 0;
            this.lblNombreEmpresa.Text = "Sistema de Gestión de Stock";
            this.lblNombreEmpresa.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlControl
            // 
            this.pnlControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.pnlControl.Controls.Add(this.gControlCerrar);
            this.pnlControl.Controls.Add(this.lblNombreForm);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControl.Location = new System.Drawing.Point(0, 0);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(302, 38);
            this.pnlControl.TabIndex = 0;
            this.pnlControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseDown);
            this.pnlControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseMove);
            // 
            // gControlCerrar
            // 
            this.gControlCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gControlCerrar.AnimationHoverSpeed = 0.07F;
            this.gControlCerrar.AnimationSpeed = 0.03F;
            this.gControlCerrar.IconColor = System.Drawing.Color.White;
            this.gControlCerrar.IconSize = 15F;
            this.gControlCerrar.Location = new System.Drawing.Point(250, 0);
            this.gControlCerrar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gControlCerrar.Name = "gControlCerrar";
            this.gControlCerrar.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.gControlCerrar.OnHoverIconColor = System.Drawing.Color.White;
            this.gControlCerrar.OnPressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.gControlCerrar.Size = new System.Drawing.Size(52, 38);
            this.gControlCerrar.TabIndex = 0;
            // 
            // lblNombreForm
            // 
            this.lblNombreForm.AutoSize = true;
            this.lblNombreForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblNombreForm.ForeColor = System.Drawing.Color.White;
            this.lblNombreForm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblNombreForm.Location = new System.Drawing.Point(5, 12);
            this.lblNombreForm.Name = "lblNombreForm";
            this.lblNombreForm.Size = new System.Drawing.Size(87, 16);
            this.lblNombreForm.TabIndex = 0;
            this.lblNombreForm.Text = "Iniciar Sesión";
            this.lblNombreForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseDown);
            this.lblNombreForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseMove);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 417);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLogin";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.panel1.ResumeLayout(false);
            this.pnlPrincipal.ResumeLayout(false);
            this.pnlPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icoCandado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icoUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogoEmpresa)).EndInit();
            this.panel2.ResumeLayout(false);
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlPrincipal;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnContraseña;
        private FontAwesome.Sharp.IconButton btnOjo;
        private FontAwesome.Sharp.IconPictureBox icoCandado;
        private FontAwesome.Sharp.IconPictureBox icoUsuario;
        private System.Windows.Forms.Button btnLogin;
        private Guna.UI.WinForms.GunaLineTextBox txtContraseñaG;
        private Guna.UI.WinForms.GunaLineTextBox txtUsuarioG;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblNombreEmpresa;
        private System.Windows.Forms.Panel pnlControl;
        private Guna.UI.WinForms.GunaControlBox gControlCerrar;
        private System.Windows.Forms.Label lblNombreForm;
        private System.Windows.Forms.PictureBox pbLogoEmpresa;
    }
}

