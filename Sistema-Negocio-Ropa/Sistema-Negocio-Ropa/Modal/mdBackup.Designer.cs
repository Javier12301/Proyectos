namespace Sistema_Negocio_Ropa.Modal
{
    partial class mdBackup
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
            this.pnlControl = new System.Windows.Forms.Panel();
            this.gControlCerrar = new Guna.UI.WinForms.GunaControlBox();
            this.lblNombreForm = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscarRestaurar = new FontAwesome.Sharp.IconButton();
            this.txtRutaCargar = new System.Windows.Forms.TextBox();
            this.lblConfirmarContrasena = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnRecuperar = new System.Windows.Forms.Button();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.txtRutaGenerar = new System.Windows.Forms.TextBox();
            this.btnBuscarGenerar = new FontAwesome.Sharp.IconButton();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.pnlControl.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.pnlControl.Size = new System.Drawing.Size(493, 38);
            this.pnlControl.TabIndex = 3;
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
            this.gControlCerrar.Location = new System.Drawing.Point(441, 0);
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
            this.lblNombreForm.Size = new System.Drawing.Size(163, 16);
            this.lblNombreForm.TabIndex = 0;
            this.lblNombreForm.Text = "Backup de Base de datos";
            this.lblNombreForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseDown);
            this.lblNombreForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseMove);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnGenerar);
            this.panel1.Controls.Add(this.btnBuscarGenerar);
            this.panel1.Controls.Add(this.btnRecuperar);
            this.panel1.Controls.Add(this.txtRutaGenerar);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnBuscarRestaurar);
            this.panel1.Controls.Add(this.txtRutaCargar);
            this.panel1.Controls.Add(this.lblConfirmarContrasena);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(493, 165);
            this.panel1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(5, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(333, 23);
            this.label3.TabIndex = 101;
            this.label3.Text = "* Use \'Cargar Backup\' unicamente si perdió información.\r\n";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Moccasin;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(351, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(139, 160);
            this.panel2.TabIndex = 100;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 133);
            this.label2.TabIndex = 100;
            this.label2.Text = "\'Generar Backup\' permite crear una copia de seguridad de los datos del negocio.\r\n" +
    "\r\n\'Cargar Backup\' permite seleccionar la ubicación de una copia guardada, permit" +
    "iendo recuperar sus datos.\r\n";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 99;
            this.label1.Text = "Nota:";
            // 
            // btnBuscarRestaurar
            // 
            this.btnBuscarRestaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarRestaurar.BackColor = System.Drawing.Color.White;
            this.btnBuscarRestaurar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarRestaurar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnBuscarRestaurar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarRestaurar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarRestaurar.ForeColor = System.Drawing.Color.Black;
            this.btnBuscarRestaurar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscarRestaurar.IconColor = System.Drawing.Color.Black;
            this.btnBuscarRestaurar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscarRestaurar.IconSize = 16;
            this.btnBuscarRestaurar.Location = new System.Drawing.Point(309, 103);
            this.btnBuscarRestaurar.Name = "btnBuscarRestaurar";
            this.btnBuscarRestaurar.Size = new System.Drawing.Size(38, 25);
            this.btnBuscarRestaurar.TabIndex = 98;
            this.btnBuscarRestaurar.UseVisualStyleBackColor = false;
            this.btnBuscarRestaurar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtRutaCargar
            // 
            this.txtRutaCargar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRutaCargar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRutaCargar.Location = new System.Drawing.Point(8, 103);
            this.txtRutaCargar.Margin = new System.Windows.Forms.Padding(2);
            this.txtRutaCargar.Name = "txtRutaCargar";
            this.txtRutaCargar.Size = new System.Drawing.Size(296, 25);
            this.txtRutaCargar.TabIndex = 33;
            // 
            // lblConfirmarContrasena
            // 
            this.lblConfirmarContrasena.AutoSize = true;
            this.lblConfirmarContrasena.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmarContrasena.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblConfirmarContrasena.Location = new System.Drawing.Point(4, 81);
            this.lblConfirmarContrasena.Name = "lblConfirmarContrasena";
            this.lblConfirmarContrasena.Size = new System.Drawing.Size(108, 20);
            this.lblConfirmarContrasena.TabIndex = 0;
            this.lblConfirmarContrasena.Text = "Cargar Backup:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(11)))), ((int)(((byte)(53)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancelar.Location = new System.Drawing.Point(395, 209);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(95, 31);
            this.btnCancelar.TabIndex = 10;
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
            this.btnRecuperar.Location = new System.Drawing.Point(8, 131);
            this.btnRecuperar.Name = "btnRecuperar";
            this.btnRecuperar.Size = new System.Drawing.Size(137, 31);
            this.btnRecuperar.TabIndex = 9;
            this.btnRecuperar.Text = "Restaurar";
            this.btnRecuperar.UseVisualStyleBackColor = false;
            this.btnRecuperar.Click += new System.EventHandler(this.btnRecuperar_Click);
            // 
            // openFile
            // 
            this.openFile.FileName = "openFileDialog1";
            // 
            // txtRutaGenerar
            // 
            this.txtRutaGenerar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRutaGenerar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRutaGenerar.Location = new System.Drawing.Point(8, 6);
            this.txtRutaGenerar.Margin = new System.Windows.Forms.Padding(2);
            this.txtRutaGenerar.Name = "txtRutaGenerar";
            this.txtRutaGenerar.Size = new System.Drawing.Size(296, 25);
            this.txtRutaGenerar.TabIndex = 102;
            // 
            // btnBuscarGenerar
            // 
            this.btnBuscarGenerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarGenerar.BackColor = System.Drawing.Color.White;
            this.btnBuscarGenerar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarGenerar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnBuscarGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarGenerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarGenerar.ForeColor = System.Drawing.Color.Black;
            this.btnBuscarGenerar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscarGenerar.IconColor = System.Drawing.Color.Black;
            this.btnBuscarGenerar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscarGenerar.IconSize = 16;
            this.btnBuscarGenerar.Location = new System.Drawing.Point(309, 6);
            this.btnBuscarGenerar.Name = "btnBuscarGenerar";
            this.btnBuscarGenerar.Size = new System.Drawing.Size(38, 25);
            this.btnBuscarGenerar.TabIndex = 103;
            this.btnBuscarGenerar.UseVisualStyleBackColor = false;
            this.btnBuscarGenerar.Click += new System.EventHandler(this.btnBuscarGenerar_Click);
            // 
            // btnGenerar
            // 
            this.btnGenerar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnGenerar.ForeColor = System.Drawing.Color.White;
            this.btnGenerar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGenerar.Location = new System.Drawing.Point(8, 36);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(137, 31);
            this.btnGenerar.TabIndex = 104;
            this.btnGenerar.Text = "Generar Backup";
            this.btnGenerar.UseVisualStyleBackColor = false;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // mdBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(493, 243);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "mdBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mdBackup";
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlControl;
        private Guna.UI.WinForms.GunaControlBox gControlCerrar;
        private System.Windows.Forms.Label lblNombreForm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnRecuperar;
        private System.Windows.Forms.Label lblConfirmarContrasena;
        private FontAwesome.Sharp.IconButton btnBuscarRestaurar;
        private System.Windows.Forms.TextBox txtRutaCargar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SaveFileDialog saveFile;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.TextBox txtRutaGenerar;
        private FontAwesome.Sharp.IconButton btnBuscarGenerar;
        private System.Windows.Forms.Button btnGenerar;
    }
}