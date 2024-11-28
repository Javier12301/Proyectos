namespace Sistema_Negocio_Ropa.Principales
{
    partial class frmReporte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReporte));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlReportesPadre = new System.Windows.Forms.Panel();
            this.pnlPadre = new System.Windows.Forms.Panel();
            this.flpContenedorBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCierresCajas = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnlReportesPadre.SuspendLayout();
            this.flpContenedorBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlReportesPadre);
            this.panel1.Controls.Add(this.flpContenedorBotones);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1081, 448);
            this.panel1.TabIndex = 3;
            // 
            // pnlReportesPadre
            // 
            this.pnlReportesPadre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlReportesPadre.Controls.Add(this.pnlPadre);
            this.pnlReportesPadre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlReportesPadre.Location = new System.Drawing.Point(0, 70);
            this.pnlReportesPadre.Name = "pnlReportesPadre";
            this.pnlReportesPadre.Size = new System.Drawing.Size(1081, 378);
            this.pnlReportesPadre.TabIndex = 210;
            // 
            // pnlPadre
            // 
            this.pnlPadre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPadre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPadre.Location = new System.Drawing.Point(0, 3);
            this.pnlPadre.Name = "pnlPadre";
            this.pnlPadre.Size = new System.Drawing.Size(1081, 375);
            this.pnlPadre.TabIndex = 211;
            // 
            // flpContenedorBotones
            // 
            this.flpContenedorBotones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpContenedorBotones.BackColor = System.Drawing.Color.LightGray;
            this.flpContenedorBotones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpContenedorBotones.Controls.Add(this.btnCierresCajas);
            this.flpContenedorBotones.Location = new System.Drawing.Point(0, 6);
            this.flpContenedorBotones.Margin = new System.Windows.Forms.Padding(4);
            this.flpContenedorBotones.Name = "flpContenedorBotones";
            this.flpContenedorBotones.Size = new System.Drawing.Size(857, 62);
            this.flpContenedorBotones.TabIndex = 106;
            // 
            // btnCierresCajas
            // 
            this.btnCierresCajas.BackColor = System.Drawing.Color.White;
            this.btnCierresCajas.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnCierresCajas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCierresCajas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCierresCajas.ForeColor = System.Drawing.Color.Black;
            this.btnCierresCajas.Image = ((System.Drawing.Image)(resources.GetObject("btnCierresCajas.Image")));
            this.btnCierresCajas.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCierresCajas.Location = new System.Drawing.Point(3, 3);
            this.btnCierresCajas.Name = "btnCierresCajas";
            this.btnCierresCajas.Size = new System.Drawing.Size(118, 54);
            this.btnCierresCajas.TabIndex = 19;
            this.btnCierresCajas.Tag = "Resumen total";
            this.btnCierresCajas.Text = "Cierre de cajas";
            this.btnCierresCajas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCierresCajas.UseVisualStyleBackColor = false;
            this.btnCierresCajas.Click += new System.EventHandler(this.btnCierresCajas_Click);
            // 
            // frmReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 448);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReporte";
            this.Text = "frmReporte";
            this.panel1.ResumeLayout(false);
            this.pnlReportesPadre.ResumeLayout(false);
            this.flpContenedorBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlReportesPadre;
        private System.Windows.Forms.FlowLayoutPanel flpContenedorBotones;
        private System.Windows.Forms.Panel pnlPadre;
        private System.Windows.Forms.Button btnCierresCajas;
    }
}