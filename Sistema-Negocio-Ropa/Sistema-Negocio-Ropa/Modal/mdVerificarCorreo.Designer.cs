namespace Sistema_Negocio_Ropa.Modal
{
    partial class mdVerificarCorreo
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
            this.lblInformacion = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblMail = new System.Windows.Forms.Label();
            this.txt4 = new System.Windows.Forms.TextBox();
            this.txt2 = new System.Windows.Forms.TextBox();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.txt5 = new System.Windows.Forms.TextBox();
            this.txt3 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlControl.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
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
            this.pnlControl.Size = new System.Drawing.Size(368, 38);
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
            this.gControlCerrar.Location = new System.Drawing.Point(316, 0);
            this.gControlCerrar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gControlCerrar.Name = "gControlCerrar";
            this.gControlCerrar.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.gControlCerrar.OnHoverIconColor = System.Drawing.Color.White;
            this.gControlCerrar.OnPressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.gControlCerrar.Size = new System.Drawing.Size(52, 38);
            this.gControlCerrar.TabIndex = 0;
            this.gControlCerrar.Click += new System.EventHandler(this.gControlCerrar_Click);
            // 
            // lblNombreForm
            // 
            this.lblNombreForm.AutoSize = true;
            this.lblNombreForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblNombreForm.ForeColor = System.Drawing.Color.White;
            this.lblNombreForm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblNombreForm.Location = new System.Drawing.Point(5, 12);
            this.lblNombreForm.Name = "lblNombreForm";
            this.lblNombreForm.Size = new System.Drawing.Size(141, 16);
            this.lblNombreForm.TabIndex = 0;
            this.lblNombreForm.Text = "Recuperar contraseña";
            this.lblNombreForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseDown);
            this.lblNombreForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseMove);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblInformacion);
            this.panel1.Controls.Add(this.lblUsuario);
            this.panel1.Controls.Add(this.lblMail);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(368, 92);
            this.panel1.TabIndex = 0;
            // 
            // lblInformacion
            // 
            this.lblInformacion.AutoSize = true;
            this.lblInformacion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformacion.ForeColor = System.Drawing.Color.Black;
            this.lblInformacion.Location = new System.Drawing.Point(8, 7);
            this.lblInformacion.Name = "lblInformacion";
            this.lblInformacion.Size = new System.Drawing.Size(203, 21);
            this.lblInformacion.TabIndex = 0;
            this.lblInformacion.Text = "Información de tu cuenta";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.Black;
            this.lblUsuario.Location = new System.Drawing.Point(12, 62);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(56, 17);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Usuario:";
            // 
            // lblMail
            // 
            this.lblMail.AutoSize = true;
            this.lblMail.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMail.ForeColor = System.Drawing.Color.Black;
            this.lblMail.Location = new System.Drawing.Point(12, 34);
            this.lblMail.Name = "lblMail";
            this.lblMail.Size = new System.Drawing.Size(120, 17);
            this.lblMail.TabIndex = 0;
            this.lblMail.Text = "Correo electronico:";
            // 
            // txt4
            // 
            this.txt4.BackColor = System.Drawing.Color.White;
            this.txt4.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt4.Location = new System.Drawing.Point(224, 3);
            this.txt4.MaxLength = 1;
            this.txt4.Name = "txt4";
            this.txt4.Size = new System.Drawing.Size(66, 48);
            this.txt4.TabIndex = 3;
            this.txt4.Text = "1";
            this.txt4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt4.TextChanged += new System.EventHandler(this.txtN_Changed);
            this.txt4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtN_KeyPress);
            // 
            // txt2
            // 
            this.txt2.BackColor = System.Drawing.Color.White;
            this.txt2.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt2.Location = new System.Drawing.Point(80, 3);
            this.txt2.MaxLength = 1;
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(66, 48);
            this.txt2.TabIndex = 1;
            this.txt2.Text = "1";
            this.txt2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt2.TextChanged += new System.EventHandler(this.txtN_Changed);
            this.txt2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtN_KeyPress);
            // 
            // txt1
            // 
            this.txt1.BackColor = System.Drawing.Color.White;
            this.txt1.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt1.Location = new System.Drawing.Point(8, 3);
            this.txt1.MaxLength = 1;
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(66, 48);
            this.txt1.TabIndex = 0;
            this.txt1.Text = "1";
            this.txt1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt1.TextChanged += new System.EventHandler(this.txtN_Changed);
            this.txt1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtN_KeyPress);
            // 
            // txt5
            // 
            this.txt5.BackColor = System.Drawing.Color.White;
            this.txt5.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt5.Location = new System.Drawing.Point(296, 3);
            this.txt5.MaxLength = 1;
            this.txt5.Name = "txt5";
            this.txt5.Size = new System.Drawing.Size(66, 48);
            this.txt5.TabIndex = 4;
            this.txt5.Text = "1";
            this.txt5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt5.TextChanged += new System.EventHandler(this.txtN_Changed);
            this.txt5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtN_KeyPress);
            // 
            // txt3
            // 
            this.txt3.BackColor = System.Drawing.Color.White;
            this.txt3.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt3.Location = new System.Drawing.Point(152, 3);
            this.txt3.MaxLength = 1;
            this.txt3.Name = "txt3";
            this.txt3.Size = new System.Drawing.Size(66, 48);
            this.txt3.TabIndex = 2;
            this.txt3.Text = "1";
            this.txt3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt3.TextChanged += new System.EventHandler(this.txtN_Changed);
            this.txt3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtN_KeyPress);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.txt1);
            this.flowLayoutPanel1.Controls.Add(this.txt2);
            this.flowLayoutPanel1.Controls.Add(this.txt3);
            this.flowLayoutPanel1.Controls.Add(this.txt4);
            this.flowLayoutPanel1.Controls.Add(this.txt5);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 133);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(368, 61);
            this.flowLayoutPanel1.TabIndex = 8;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // mdVerificarCorreo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 194);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "mdVerificarCorreo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mdVerificarCorreo";
            this.Load += new System.EventHandler(this.mdVerificarCorreo_Load);
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlControl;
        private Guna.UI.WinForms.GunaControlBox gControlCerrar;
        private System.Windows.Forms.Label lblNombreForm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblInformacion;
        private System.Windows.Forms.Label lblMail;
        private System.Windows.Forms.TextBox txt4;
        private System.Windows.Forms.TextBox txt2;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.TextBox txt5;
        private System.Windows.Forms.TextBox txt3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}