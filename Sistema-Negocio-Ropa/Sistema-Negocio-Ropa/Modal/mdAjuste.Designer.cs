namespace Sistema_Negocio_Ropa.Modal
{
    partial class mdAjuste
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
            this.flpContenedorBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPerfiles = new FontAwesome.Sharp.IconButton();
            this.btnNegocio = new FontAwesome.Sharp.IconButton();
            this.btnBaseDatos = new FontAwesome.Sharp.IconButton();
            this.btnMisDatos = new FontAwesome.Sharp.IconButton();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pnlControl.SuspendLayout();
            this.flpContenedorBotones.SuspendLayout();
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
            this.pnlControl.Size = new System.Drawing.Size(372, 32);
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
            this.gControlCerrar.Location = new System.Drawing.Point(327, 0);
            this.gControlCerrar.Name = "gControlCerrar";
            this.gControlCerrar.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.gControlCerrar.OnHoverIconColor = System.Drawing.Color.White;
            this.gControlCerrar.OnPressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.gControlCerrar.Size = new System.Drawing.Size(45, 32);
            this.gControlCerrar.TabIndex = 0;
            // 
            // lblNombreForm
            // 
            this.lblNombreForm.AutoEllipsis = true;
            this.lblNombreForm.AutoSize = true;
            this.lblNombreForm.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreForm.ForeColor = System.Drawing.SystemColors.Control;
            this.lblNombreForm.Location = new System.Drawing.Point(7, 8);
            this.lblNombreForm.Name = "lblNombreForm";
            this.lblNombreForm.Size = new System.Drawing.Size(53, 17);
            this.lblNombreForm.TabIndex = 0;
            this.lblNombreForm.Tag = "";
            this.lblNombreForm.Text = "Ajustes";
            this.lblNombreForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseDown);
            this.lblNombreForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseMove);
            // 
            // flpContenedorBotones
            // 
            this.flpContenedorBotones.Controls.Add(this.btnPerfiles);
            this.flpContenedorBotones.Controls.Add(this.btnNegocio);
            this.flpContenedorBotones.Controls.Add(this.btnBaseDatos);
            this.flpContenedorBotones.Controls.Add(this.btnMisDatos);
            this.flpContenedorBotones.Location = new System.Drawing.Point(0, 38);
            this.flpContenedorBotones.Name = "flpContenedorBotones";
            this.flpContenedorBotones.Size = new System.Drawing.Size(372, 176);
            this.flpContenedorBotones.TabIndex = 4;
            // 
            // btnPerfiles
            // 
            this.btnPerfiles.Enabled = false;
            this.btnPerfiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPerfiles.IconChar = FontAwesome.Sharp.IconChar.UserGroup;
            this.btnPerfiles.IconColor = System.Drawing.Color.Black;
            this.btnPerfiles.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPerfiles.IconSize = 45;
            this.btnPerfiles.Location = new System.Drawing.Point(3, 3);
            this.btnPerfiles.Name = "btnPerfiles";
            this.btnPerfiles.Size = new System.Drawing.Size(180, 82);
            this.btnPerfiles.TabIndex = 0;
            this.btnPerfiles.Tag = "Perfiles";
            this.btnPerfiles.Text = "Perfiles";
            this.btnPerfiles.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPerfiles.UseVisualStyleBackColor = true;
            this.btnPerfiles.Click += new System.EventHandler(this.btnPerfiles_Click);
            // 
            // btnNegocio
            // 
            this.btnNegocio.Enabled = false;
            this.btnNegocio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNegocio.IconChar = FontAwesome.Sharp.IconChar.StoreAlt;
            this.btnNegocio.IconColor = System.Drawing.Color.Black;
            this.btnNegocio.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNegocio.IconSize = 45;
            this.btnNegocio.Location = new System.Drawing.Point(189, 3);
            this.btnNegocio.Name = "btnNegocio";
            this.btnNegocio.Size = new System.Drawing.Size(180, 82);
            this.btnNegocio.TabIndex = 1;
            this.btnNegocio.Tag = "formNegocio";
            this.btnNegocio.Text = "Datos de negocio";
            this.btnNegocio.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNegocio.UseVisualStyleBackColor = true;
            this.btnNegocio.Click += new System.EventHandler(this.btnNegocio_Click);
            // 
            // btnBaseDatos
            // 
            this.btnBaseDatos.Enabled = false;
            this.btnBaseDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaseDatos.IconChar = FontAwesome.Sharp.IconChar.Database;
            this.btnBaseDatos.IconColor = System.Drawing.Color.Black;
            this.btnBaseDatos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBaseDatos.IconSize = 45;
            this.btnBaseDatos.Location = new System.Drawing.Point(3, 91);
            this.btnBaseDatos.Name = "btnBaseDatos";
            this.btnBaseDatos.Size = new System.Drawing.Size(180, 82);
            this.btnBaseDatos.TabIndex = 2;
            this.btnBaseDatos.Tag = "formBackup";
            this.btnBaseDatos.Text = "Backup";
            this.btnBaseDatos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBaseDatos.UseVisualStyleBackColor = true;
            this.btnBaseDatos.Click += new System.EventHandler(this.btnBaseDatos_Click);
            // 
            // btnMisDatos
            // 
            this.btnMisDatos.Enabled = false;
            this.btnMisDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMisDatos.IconChar = FontAwesome.Sharp.IconChar.UserGear;
            this.btnMisDatos.IconColor = System.Drawing.Color.Black;
            this.btnMisDatos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMisDatos.IconSize = 45;
            this.btnMisDatos.Location = new System.Drawing.Point(189, 91);
            this.btnMisDatos.Name = "btnMisDatos";
            this.btnMisDatos.Size = new System.Drawing.Size(180, 82);
            this.btnMisDatos.TabIndex = 3;
            this.btnMisDatos.Tag = "formMisDatos";
            this.btnMisDatos.Text = "Mis datos";
            this.btnMisDatos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMisDatos.UseVisualStyleBackColor = true;
            this.btnMisDatos.Click += new System.EventHandler(this.btnMisDatos_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(11)))), ((int)(((byte)(53)))));
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(137)))), ((int)(((byte)(180)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelar.Location = new System.Drawing.Point(265, 220);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(104, 33);
            this.btnCancelar.TabIndex = 93;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // mdAjuste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(372, 257);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.flpContenedorBotones);
            this.Controls.Add(this.pnlControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "mdAjuste";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mdAjuste";
            this.Load += new System.EventHandler(this.mdAjuste_Load);
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.flpContenedorBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlControl;
        private Guna.UI.WinForms.GunaControlBox gControlCerrar;
        private System.Windows.Forms.Label lblNombreForm;
        private System.Windows.Forms.FlowLayoutPanel flpContenedorBotones;
        private FontAwesome.Sharp.IconButton btnPerfiles;
        private FontAwesome.Sharp.IconButton btnNegocio;
        private FontAwesome.Sharp.IconButton btnBaseDatos;
        private System.Windows.Forms.Button btnCancelar;
        private FontAwesome.Sharp.IconButton btnMisDatos;
    }
}