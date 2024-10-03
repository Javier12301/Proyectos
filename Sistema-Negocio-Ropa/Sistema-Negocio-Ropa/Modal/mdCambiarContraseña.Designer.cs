namespace Sistema_Negocio_Ropa.Modal
{
    partial class mdCambiarContraseña
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.gControlCerrar = new Guna.UI.WinForms.GunaControlBox();
            this.lblNombreForm = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOjoConfirmar = new FontAwesome.Sharp.IconButton();
            this.btnOjoNueva = new FontAwesome.Sharp.IconButton();
            this.txtConfirmarContrasena = new System.Windows.Forms.TextBox();
            this.lblConfirmarContrasena = new System.Windows.Forms.Label();
            this.txtNuevaContrasena = new System.Windows.Forms.TextBox();
            this.lblNuevaContrasena = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnRecuperar = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlControl.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.pnlControl.Controls.Add(this.gControlCerrar);
            this.pnlControl.Controls.Add(this.lblNombreForm);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControl.Location = new System.Drawing.Point(0, 0);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(350, 38);
            this.pnlControl.TabIndex = 2;
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
            this.gControlCerrar.Location = new System.Drawing.Point(298, 0);
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
            this.lblNombreForm.Size = new System.Drawing.Size(128, 16);
            this.lblNombreForm.TabIndex = 0;
            this.lblNombreForm.Text = "Cambiar contraseña";
            this.lblNombreForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseDown);
            this.lblNombreForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseMove);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnOjoConfirmar);
            this.panel1.Controls.Add(this.btnOjoNueva);
            this.panel1.Controls.Add(this.txtConfirmarContrasena);
            this.panel1.Controls.Add(this.lblConfirmarContrasena);
            this.panel1.Controls.Add(this.txtNuevaContrasena);
            this.panel1.Controls.Add(this.lblNuevaContrasena);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 133);
            this.panel1.TabIndex = 3;
            // 
            // btnOjoConfirmar
            // 
            this.btnOjoConfirmar.BackColor = System.Drawing.Color.White;
            this.btnOjoConfirmar.FlatAppearance.BorderSize = 0;
            this.btnOjoConfirmar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnOjoConfirmar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnOjoConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOjoConfirmar.IconChar = FontAwesome.Sharp.IconChar.Eye;
            this.btnOjoConfirmar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.btnOjoConfirmar.IconFont = FontAwesome.Sharp.IconFont.Regular;
            this.btnOjoConfirmar.IconSize = 29;
            this.btnOjoConfirmar.Location = new System.Drawing.Point(318, 91);
            this.btnOjoConfirmar.Name = "btnOjoConfirmar";
            this.btnOjoConfirmar.Size = new System.Drawing.Size(29, 27);
            this.btnOjoConfirmar.TabIndex = 5;
            this.btnOjoConfirmar.UseVisualStyleBackColor = false;
            this.btnOjoConfirmar.Click += new System.EventHandler(this.btnOjo_Click);
            this.btnOjoConfirmar.MouseEnter += new System.EventHandler(this.btnOjo_Enter);
            this.btnOjoConfirmar.MouseLeave += new System.EventHandler(this.btnOjo_MouseLeave);
            // 
            // btnOjoNueva
            // 
            this.btnOjoNueva.BackColor = System.Drawing.Color.White;
            this.btnOjoNueva.FlatAppearance.BorderSize = 0;
            this.btnOjoNueva.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnOjoNueva.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnOjoNueva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOjoNueva.IconChar = FontAwesome.Sharp.IconChar.Eye;
            this.btnOjoNueva.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.btnOjoNueva.IconFont = FontAwesome.Sharp.IconFont.Regular;
            this.btnOjoNueva.IconSize = 29;
            this.btnOjoNueva.Location = new System.Drawing.Point(318, 31);
            this.btnOjoNueva.Name = "btnOjoNueva";
            this.btnOjoNueva.Size = new System.Drawing.Size(29, 27);
            this.btnOjoNueva.TabIndex = 4;
            this.btnOjoNueva.UseVisualStyleBackColor = false;
            this.btnOjoNueva.Click += new System.EventHandler(this.btnOjo_Click);
            this.btnOjoNueva.MouseEnter += new System.EventHandler(this.btnOjo_Enter);
            this.btnOjoNueva.MouseLeave += new System.EventHandler(this.btnOjo_MouseLeave);
            // 
            // txtConfirmarContrasena
            // 
            this.txtConfirmarContrasena.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfirmarContrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmarContrasena.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmarContrasena.Location = new System.Drawing.Point(21, 92);
            this.txtConfirmarContrasena.Name = "txtConfirmarContrasena";
            this.txtConfirmarContrasena.Size = new System.Drawing.Size(295, 25);
            this.txtConfirmarContrasena.TabIndex = 2;
            this.txtConfirmarContrasena.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCredenciales_KeyPress);
            // 
            // lblConfirmarContrasena
            // 
            this.lblConfirmarContrasena.AutoSize = true;
            this.lblConfirmarContrasena.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmarContrasena.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblConfirmarContrasena.Location = new System.Drawing.Point(8, 71);
            this.lblConfirmarContrasena.Name = "lblConfirmarContrasena";
            this.lblConfirmarContrasena.Size = new System.Drawing.Size(151, 20);
            this.lblConfirmarContrasena.TabIndex = 0;
            this.lblConfirmarContrasena.Text = "Confirmar contraseña";
            // 
            // txtNuevaContrasena
            // 
            this.txtNuevaContrasena.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNuevaContrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNuevaContrasena.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNuevaContrasena.Location = new System.Drawing.Point(21, 32);
            this.txtNuevaContrasena.Name = "txtNuevaContrasena";
            this.txtNuevaContrasena.Size = new System.Drawing.Size(295, 25);
            this.txtNuevaContrasena.TabIndex = 1;
            this.txtNuevaContrasena.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCredenciales_KeyPress);
            // 
            // lblNuevaContrasena
            // 
            this.lblNuevaContrasena.AutoSize = true;
            this.lblNuevaContrasena.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNuevaContrasena.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblNuevaContrasena.Location = new System.Drawing.Point(8, 11);
            this.lblNuevaContrasena.Name = "lblNuevaContrasena";
            this.lblNuevaContrasena.Size = new System.Drawing.Size(127, 20);
            this.lblNuevaContrasena.TabIndex = 0;
            this.lblNuevaContrasena.Text = "Nueva contraseña";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(11)))), ((int)(((byte)(53)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancelar.Location = new System.Drawing.Point(151, 177);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(95, 31);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnRecuperar
            // 
            this.btnRecuperar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.btnRecuperar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecuperar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnRecuperar.ForeColor = System.Drawing.Color.White;
            this.btnRecuperar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRecuperar.Location = new System.Drawing.Point(252, 177);
            this.btnRecuperar.Name = "btnRecuperar";
            this.btnRecuperar.Size = new System.Drawing.Size(95, 31);
            this.btnRecuperar.TabIndex = 7;
            this.btnRecuperar.Text = "Confirmar";
            this.btnRecuperar.UseVisualStyleBackColor = false;
            this.btnRecuperar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkRate = 300;
            this.errorProvider.ContainerControl = this;
            // 
            // mdCambiarContraseña
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 215);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnRecuperar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "mdCambiarContraseña";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mdCambiarContraseña";
            this.Load += new System.EventHandler(this.mdCambiarContraseña_Load);
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlControl;
        private Guna.UI.WinForms.GunaControlBox gControlCerrar;
        private System.Windows.Forms.Label lblNombreForm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtConfirmarContrasena;
        private System.Windows.Forms.Label lblConfirmarContrasena;
        private System.Windows.Forms.TextBox txtNuevaContrasena;
        private System.Windows.Forms.Label lblNuevaContrasena;
        private FontAwesome.Sharp.IconButton btnOjoConfirmar;
        private FontAwesome.Sharp.IconButton btnOjoNueva;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnRecuperar;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}