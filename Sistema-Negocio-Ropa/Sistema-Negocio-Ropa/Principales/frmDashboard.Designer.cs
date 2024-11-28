namespace Sistema_Negocio_Ropa.Principales
{
    partial class frmDashboard
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblSaludo = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.dgvProductosPorVencer = new System.Windows.Forms.DataGridView();
            this.dgvcProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcEquipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcTalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcCantUvendidas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel8 = new System.Windows.Forms.Panel();
            this.pnlSinStock = new System.Windows.Forms.Panel();
            this.dgvProductoSinStock = new System.Windows.Forms.DataGridView();
            this.dgvcProductosnStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcCantidadsnStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.cMedicamentos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cCompras = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductosPorVencer)).BeginInit();
            this.panel8.SuspendLayout();
            this.pnlSinStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductoSinStock)).BeginInit();
            this.toolStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cMedicamentos)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cCompras)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.panel2.Controls.Add(this.lblSaludo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1077, 29);
            this.panel2.TabIndex = 0;
            // 
            // lblSaludo
            // 
            this.lblSaludo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSaludo.AutoSize = true;
            this.lblSaludo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaludo.ForeColor = System.Drawing.Color.White;
            this.lblSaludo.Location = new System.Drawing.Point(498, 2);
            this.lblSaludo.Name = "lblSaludo";
            this.lblSaludo.Size = new System.Drawing.Size(115, 24);
            this.lblSaludo.TabIndex = 0;
            this.lblSaludo.Text = "Bienvenido";
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.Controls.Add(this.toolStrip1);
            this.panel7.Controls.Add(this.dgvProductosPorVencer);
            this.panel7.Location = new System.Drawing.Point(9, 273);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(5, 2, 5, 0);
            this.panel7.Size = new System.Drawing.Size(765, 170);
            this.panel7.TabIndex = 111;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(5, 2);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(755, 25);
            this.toolStrip1.TabIndex = 111;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(87, 22);
            this.toolStripLabel1.Text = "Top Ventas";
            // 
            // dgvProductosPorVencer
            // 
            this.dgvProductosPorVencer.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvProductosPorVencer.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProductosPorVencer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductosPorVencer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductosPorVencer.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductosPorVencer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProductosPorVencer.ColumnHeadersHeight = 40;
            this.dgvProductosPorVencer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProductosPorVencer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcProducto,
            this.dgvcEquipo,
            this.dgvcTalle,
            this.dgvcPrecio,
            this.dgvcCantUvendidas,
            this.dgvcEstado});
            this.dgvProductosPorVencer.GridColor = System.Drawing.Color.White;
            this.dgvProductosPorVencer.Location = new System.Drawing.Point(5, 30);
            this.dgvProductosPorVencer.Name = "dgvProductosPorVencer";
            this.dgvProductosPorVencer.ReadOnly = true;
            this.dgvProductosPorVencer.RowHeadersVisible = false;
            this.dgvProductosPorVencer.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProductosPorVencer.Size = new System.Drawing.Size(757, 136);
            this.dgvProductosPorVencer.TabIndex = 110;
            // 
            // dgvcProducto
            // 
            this.dgvcProducto.HeaderText = "Producto";
            this.dgvcProducto.Name = "dgvcProducto";
            this.dgvcProducto.ReadOnly = true;
            // 
            // dgvcEquipo
            // 
            this.dgvcEquipo.HeaderText = "Equipo";
            this.dgvcEquipo.Name = "dgvcEquipo";
            this.dgvcEquipo.ReadOnly = true;
            // 
            // dgvcTalle
            // 
            this.dgvcTalle.HeaderText = "Talle";
            this.dgvcTalle.Name = "dgvcTalle";
            this.dgvcTalle.ReadOnly = true;
            // 
            // dgvcPrecio
            // 
            this.dgvcPrecio.HeaderText = "Precio";
            this.dgvcPrecio.Name = "dgvcPrecio";
            this.dgvcPrecio.ReadOnly = true;
            // 
            // dgvcCantUvendidas
            // 
            this.dgvcCantUvendidas.HeaderText = "Unidades vendidas";
            this.dgvcCantUvendidas.Name = "dgvcCantUvendidas";
            this.dgvcCantUvendidas.ReadOnly = true;
            // 
            // dgvcEstado
            // 
            this.dgvcEstado.HeaderText = "Estado";
            this.dgvcEstado.Name = "dgvcEstado";
            this.dgvcEstado.ReadOnly = true;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.Controls.Add(this.pnlSinStock);
            this.panel8.Controls.Add(this.cMedicamentos);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(777, 31);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(302, 415);
            this.panel8.TabIndex = 112;
            // 
            // pnlSinStock
            // 
            this.pnlSinStock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSinStock.Controls.Add(this.dgvProductoSinStock);
            this.pnlSinStock.Controls.Add(this.toolStrip3);
            this.pnlSinStock.Location = new System.Drawing.Point(3, 216);
            this.pnlSinStock.Name = "pnlSinStock";
            this.pnlSinStock.Padding = new System.Windows.Forms.Padding(5, 2, 5, 0);
            this.pnlSinStock.Size = new System.Drawing.Size(296, 196);
            this.pnlSinStock.TabIndex = 120;
            // 
            // dgvProductoSinStock
            // 
            this.dgvProductoSinStock.AllowUserToAddRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvProductoSinStock.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvProductoSinStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductoSinStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductoSinStock.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductoSinStock.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvProductoSinStock.ColumnHeadersHeight = 40;
            this.dgvProductoSinStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProductoSinStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcProductosnStock,
            this.dgvcCantidadsnStock});
            this.dgvProductoSinStock.GridColor = System.Drawing.Color.White;
            this.dgvProductoSinStock.Location = new System.Drawing.Point(5, 30);
            this.dgvProductoSinStock.Name = "dgvProductoSinStock";
            this.dgvProductoSinStock.ReadOnly = true;
            this.dgvProductoSinStock.RowHeadersVisible = false;
            this.dgvProductoSinStock.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProductoSinStock.Size = new System.Drawing.Size(286, 163);
            this.dgvProductoSinStock.TabIndex = 112;
            // 
            // dgvcProductosnStock
            // 
            this.dgvcProductosnStock.HeaderText = "Producto";
            this.dgvcProductosnStock.Name = "dgvcProductosnStock";
            this.dgvcProductosnStock.ReadOnly = true;
            // 
            // dgvcCantidadsnStock
            // 
            this.dgvcCantidadsnStock.HeaderText = "Stock";
            this.dgvcCantidadsnStock.Name = "dgvcCantidadsnStock";
            this.dgvcCantidadsnStock.ReadOnly = true;
            // 
            // toolStrip3
            // 
            this.toolStrip3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.toolStripLabel3});
            this.toolStrip3.Location = new System.Drawing.Point(5, 2);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(286, 25);
            this.toolStrip3.TabIndex = 111;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel3.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(222, 22);
            this.toolStripLabel3.Text = "Productos por quedar sin stock";
            // 
            // cMedicamentos
            // 
            this.cMedicamentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.cMedicamentos.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.cMedicamentos.Legends.Add(legend1);
            this.cMedicamentos.Location = new System.Drawing.Point(9, 6);
            this.cMedicamentos.Name = "cMedicamentos";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series1.IsValueShownAsLabel = true;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.YValuesPerPoint = 4;
            this.cMedicamentos.Series.Add(series1);
            this.cMedicamentos.Size = new System.Drawing.Size(283, 204);
            this.cMedicamentos.TabIndex = 119;
            this.cMedicamentos.Text = "chart3";
            title1.Alignment = System.Drawing.ContentAlignment.TopLeft;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "Inventario";
            this.cMedicamentos.Titles.Add(title1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.cCompras);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(1081, 448);
            this.panel1.TabIndex = 3;
            // 
            // cCompras
            // 
            this.cCompras.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.cCompras.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.cCompras.Legends.Add(legend2);
            this.cCompras.Location = new System.Drawing.Point(9, 37);
            this.cCompras.Name = "cCompras";
            series2.ChartArea = "ChartArea1";
            series2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.Legend = "Legend1";
            series2.MarkerBorderWidth = 5;
            series2.Name = "Ventas";
            series2.XValueMember = "Month";
            series2.YValueMembers = "NumeroDeVentas";
            series2.YValuesPerPoint = 3;
            this.cCompras.Series.Add(series2);
            this.cCompras.Size = new System.Drawing.Size(765, 230);
            this.cCompras.TabIndex = 117;
            this.cCompras.Text = "chart1";
            title2.Name = "Title";
            title2.Text = "Ventas de la semana";
            this.cCompras.Titles.Add(title2);
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 448);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDashboard";
            this.Text = "frmDashboard";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductosPorVencer)).EndInit();
            this.panel8.ResumeLayout(false);
            this.pnlSinStock.ResumeLayout(false);
            this.pnlSinStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductoSinStock)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cMedicamentos)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cCompras)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblSaludo;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.DataGridView dgvProductosPorVencer;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart cMedicamentos;
        private System.Windows.Forms.Panel pnlSinStock;
        private System.Windows.Forms.DataGridView dgvProductoSinStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcProductosnStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcCantidadsnStock;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.DataVisualization.Charting.Chart cCompras;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcEquipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcTalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcCantUvendidas;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcEstado;
    }
}