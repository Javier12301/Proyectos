namespace Sistema_Negocio_Ropa.Modal.Seguridad
{
    partial class mdAdminServCorreo
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblGestor = new System.Windows.Forms.Label();
            this.txtConfirmarClave = new System.Windows.Forms.Button();
            this.txtConfirmar = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasenaRobot = new System.Windows.Forms.TextBox();
            this.lblCorreoGestor = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.pnlCadena = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCadena = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.pnlCadena.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblGestor);
            this.panel1.Controls.Add(this.txtConfirmarClave);
            this.panel1.Controls.Add(this.txtConfirmar);
            this.panel1.Controls.Add(this.btnGuardar);
            this.panel1.Controls.Add(this.lblContrasena);
            this.panel1.Controls.Add(this.txtContrasenaRobot);
            this.panel1.Controls.Add(this.lblCorreoGestor);
            this.panel1.Controls.Add(this.txtCorreo);
            this.panel1.Location = new System.Drawing.Point(11, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(308, 233);
            this.panel1.TabIndex = 6;
            // 
            // lblGestor
            // 
            this.lblGestor.AutoSize = true;
            this.lblGestor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGestor.Location = new System.Drawing.Point(20, 28);
            this.lblGestor.Name = "lblGestor";
            this.lblGestor.Size = new System.Drawing.Size(172, 21);
            this.lblGestor.TabIndex = 7;
            this.lblGestor.Text = "Ingrese Clave de gestor";
            this.lblGestor.Visible = false;
            // 
            // txtConfirmarClave
            // 
            this.txtConfirmarClave.Location = new System.Drawing.Point(104, 78);
            this.txtConfirmarClave.Name = "txtConfirmarClave";
            this.txtConfirmarClave.Size = new System.Drawing.Size(105, 23);
            this.txtConfirmarClave.TabIndex = 6;
            this.txtConfirmarClave.Text = "Confirmar";
            this.txtConfirmarClave.UseVisualStyleBackColor = true;
            this.txtConfirmarClave.Click += new System.EventHandler(this.txtConfirmarClave_Click);
            // 
            // txtConfirmar
            // 
            this.txtConfirmar.Location = new System.Drawing.Point(24, 52);
            this.txtConfirmar.Name = "txtConfirmar";
            this.txtConfirmar.PasswordChar = '*';
            this.txtConfirmar.Size = new System.Drawing.Size(250, 20);
            this.txtConfirmar.TabIndex = 5;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(62, 161);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(177, 37);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Gardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Visible = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblContrasena
            // 
            this.lblContrasena.AutoSize = true;
            this.lblContrasena.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContrasena.Location = new System.Drawing.Point(5, 75);
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.Size = new System.Drawing.Size(169, 21);
            this.lblContrasena.TabIndex = 3;
            this.lblContrasena.Text = "Contraseña de 2-pasos";
            this.lblContrasena.Visible = false;
            // 
            // txtContrasenaRobot
            // 
            this.txtContrasenaRobot.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContrasenaRobot.Location = new System.Drawing.Point(9, 99);
            this.txtContrasenaRobot.Name = "txtContrasenaRobot";
            this.txtContrasenaRobot.PasswordChar = '*';
            this.txtContrasenaRobot.Size = new System.Drawing.Size(277, 29);
            this.txtContrasenaRobot.TabIndex = 2;
            this.txtContrasenaRobot.Visible = false;
            // 
            // lblCorreoGestor
            // 
            this.lblCorreoGestor.AutoSize = true;
            this.lblCorreoGestor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorreoGestor.Location = new System.Drawing.Point(5, 10);
            this.lblCorreoGestor.Name = "lblCorreoGestor";
            this.lblCorreoGestor.Size = new System.Drawing.Size(132, 21);
            this.lblCorreoGestor.TabIndex = 1;
            this.lblCorreoGestor.Text = "Correo de Gestor:";
            this.lblCorreoGestor.Visible = false;
            // 
            // txtCorreo
            // 
            this.txtCorreo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorreo.Location = new System.Drawing.Point(8, 34);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(278, 29);
            this.txtCorreo.TabIndex = 0;
            this.txtCorreo.Visible = false;
            // 
            // pnlCadena
            // 
            this.pnlCadena.Controls.Add(this.button2);
            this.pnlCadena.Controls.Add(this.label3);
            this.pnlCadena.Controls.Add(this.txtCadena);
            this.pnlCadena.Location = new System.Drawing.Point(337, 14);
            this.pnlCadena.Name = "pnlCadena";
            this.pnlCadena.Size = new System.Drawing.Size(308, 233);
            this.pnlCadena.TabIndex = 7;
            this.pnlCadena.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(68, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 37);
            this.button2.TabIndex = 4;
            this.button2.Text = "Gardar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "CadenaBD";
            // 
            // txtCadena
            // 
            this.txtCadena.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCadena.Location = new System.Drawing.Point(8, 34);
            this.txtCadena.Name = "txtCadena";
            this.txtCadena.Size = new System.Drawing.Size(278, 29);
            this.txtCadena.TabIndex = 0;
            // 
            // mdAdminServCorreo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 260);
            this.Controls.Add(this.pnlCadena);
            this.Controls.Add(this.panel1);
            this.Name = "mdAdminServCorreo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurador";
            this.Load += new System.EventHandler(this.mdAdminServCorreo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlCadena.ResumeLayout(false);
            this.pnlCadena.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblGestor;
        private System.Windows.Forms.Button txtConfirmarClave;
        private System.Windows.Forms.TextBox txtConfirmar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasenaRobot;
        private System.Windows.Forms.Label lblCorreoGestor;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Panel pnlCadena;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCadena;
    }
}