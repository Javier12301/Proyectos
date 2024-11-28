namespace Sistema_Negocio_Ropa.Modal
{
    partial class mdAjustePrecio
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
            this.flpContenedorBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnUnidad = new FontAwesome.Sharp.IconButton();
            this.btnCategoriaP = new FontAwesome.Sharp.IconButton();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.gControlCerrar = new Guna.UI.WinForms.GunaControlBox();
            this.lblNombreForm = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.flpContenedorBotones.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpContenedorBotones
            // 
            this.flpContenedorBotones.Controls.Add(this.btnUnidad);
            this.flpContenedorBotones.Controls.Add(this.btnCategoriaP);
            this.flpContenedorBotones.Location = new System.Drawing.Point(0, 40);
            this.flpContenedorBotones.Name = "flpContenedorBotones";
            this.flpContenedorBotones.Size = new System.Drawing.Size(372, 120);
            this.flpContenedorBotones.TabIndex = 5;
            // 
            // btnUnidad
            // 
            this.btnUnidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnidad.IconChar = FontAwesome.Sharp.IconChar.Box;
            this.btnUnidad.IconColor = System.Drawing.Color.Black;
            this.btnUnidad.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnUnidad.IconSize = 45;
            this.btnUnidad.Location = new System.Drawing.Point(3, 3);
            this.btnUnidad.Name = "btnUnidad";
            this.btnUnidad.Size = new System.Drawing.Size(180, 108);
            this.btnUnidad.TabIndex = 0;
            this.btnUnidad.Tag = "Perfiles";
            this.btnUnidad.Text = "Por unidad";
            this.btnUnidad.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUnidad.UseVisualStyleBackColor = true;
            this.btnUnidad.Click += new System.EventHandler(this.btnUnidad_Click);
            // 
            // btnCategoriaP
            // 
            this.btnCategoriaP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCategoriaP.IconChar = FontAwesome.Sharp.IconChar.Tag;
            this.btnCategoriaP.IconColor = System.Drawing.Color.Black;
            this.btnCategoriaP.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCategoriaP.IconSize = 45;
            this.btnCategoriaP.Location = new System.Drawing.Point(189, 3);
            this.btnCategoriaP.Name = "btnCategoriaP";
            this.btnCategoriaP.Size = new System.Drawing.Size(180, 108);
            this.btnCategoriaP.TabIndex = 1;
            this.btnCategoriaP.Tag = "formNegocio";
            this.btnCategoriaP.Text = "Por categoría";
            this.btnCategoriaP.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCategoriaP.UseVisualStyleBackColor = true;
            this.btnCategoriaP.Click += new System.EventHandler(this.btnCategoriaP_Click);
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
            this.pnlControl.TabIndex = 6;
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
            this.lblNombreForm.Size = new System.Drawing.Size(94, 17);
            this.lblNombreForm.TabIndex = 0;
            this.lblNombreForm.Tag = "";
            this.lblNombreForm.Text = "Ajustar precio";
            this.lblNombreForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseDown);
            this.lblNombreForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseMove);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(11)))), ((int)(((byte)(53)))));
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(137)))), ((int)(((byte)(180)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSalir.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalir.Location = new System.Drawing.Point(265, 166);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(104, 33);
            this.btnSalir.TabIndex = 94;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // mdAjustePrecio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(372, 208);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.pnlControl);
            this.Controls.Add(this.flpContenedorBotones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "mdAjustePrecio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mdAjustePrecio";
            this.flpContenedorBotones.ResumeLayout(false);
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpContenedorBotones;
        private FontAwesome.Sharp.IconButton btnUnidad;
        private FontAwesome.Sharp.IconButton btnCategoriaP;
        private System.Windows.Forms.Panel pnlControl;
        private Guna.UI.WinForms.GunaControlBox gControlCerrar;
        private System.Windows.Forms.Label lblNombreForm;
        private System.Windows.Forms.Button btnSalir;
    }
}