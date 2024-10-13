namespace Sistema_Negocio_Ropa.Principales
{
    partial class frmVentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVentas));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.lblNumerodeVenta = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.lblCliente = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblNombreEmpresa = new System.Windows.Forms.Label();
            this.pbLogoEmpresa = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscadorProducto = new FontAwesome.Sharp.IconButton();
            this.txtProducto = new System.Windows.Forms.TextBox();
            this.icoCandado = new FontAwesome.Sharp.IconPictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnCobrar = new System.Windows.Forms.Button();
            this.txtTotal = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnImprimirUltimoTicket = new System.Windows.Forms.Button();
            this.btnHistorialDeVenta = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.dgvVenta = new System.Windows.Forms.DataGridView();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.dgvcID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcSubTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAumentar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnDecrementar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.toolStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogoEmpresa)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icoCandado)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.toolStrip2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.lblNumerodeVenta,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.lblCliente,
            this.toolStripSeparator3});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1081, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(124, 22);
            this.toolStripLabel1.Text = "Número de venta: ";
            // 
            // lblNumerodeVenta
            // 
            this.lblNumerodeVenta.ForeColor = System.Drawing.Color.White;
            this.lblNumerodeVenta.Name = "lblNumerodeVenta";
            this.lblNumerodeVenta.Size = new System.Drawing.Size(39, 22);
            this.lblNumerodeVenta.Text = "0001";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel2.Text = "Cliente:";
            // 
            // lblCliente
            // 
            this.lblCliente.ForeColor = System.Drawing.Color.White;
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(120, 22);
            this.lblCliente.Text = "Consumidor Final";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.pbLogoEmpresa);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1081, 72);
            this.panel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.lblNombreEmpresa);
            this.panel3.Location = new System.Drawing.Point(99, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(978, 46);
            this.panel3.TabIndex = 0;
            // 
            // lblNombreEmpresa
            // 
            this.lblNombreEmpresa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombreEmpresa.AutoEllipsis = true;
            this.lblNombreEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreEmpresa.Location = new System.Drawing.Point(30, 9);
            this.lblNombreEmpresa.Name = "lblNombreEmpresa";
            this.lblNombreEmpresa.Size = new System.Drawing.Size(888, 34);
            this.lblNombreEmpresa.TabIndex = 0;
            this.lblNombreEmpresa.Text = "Punto de Venta";
            this.lblNombreEmpresa.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbLogoEmpresa
            // 
            this.pbLogoEmpresa.Image = ((System.Drawing.Image)(resources.GetObject("pbLogoEmpresa.Image")));
            this.pbLogoEmpresa.Location = new System.Drawing.Point(5, 3);
            this.pbLogoEmpresa.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pbLogoEmpresa.Name = "pbLogoEmpresa";
            this.pbLogoEmpresa.Size = new System.Drawing.Size(89, 66);
            this.pbLogoEmpresa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogoEmpresa.TabIndex = 0;
            this.pbLogoEmpresa.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(82)))), ((int)(((byte)(123)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 97);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1081, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel4.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(212, 22);
            this.toolStripLabel4.Text = "VENTA DE PRODUCTOS";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnLimpiar);
            this.panel6.Controls.Add(this.btnBuscadorProducto);
            this.panel6.Controls.Add(this.txtProducto);
            this.panel6.Controls.Add(this.icoCandado);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 122);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1081, 35);
            this.panel6.TabIndex = 4;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.White;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiar.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.Image")));
            this.btnLimpiar.Location = new System.Drawing.Point(309, 3);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(29, 27);
            this.btnLimpiar.TabIndex = 218;
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscadorProducto
            // 
            this.btnBuscadorProducto.BackColor = System.Drawing.Color.White;
            this.btnBuscadorProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscadorProducto.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscadorProducto.IconColor = System.Drawing.Color.Black;
            this.btnBuscadorProducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscadorProducto.IconSize = 24;
            this.btnBuscadorProducto.Location = new System.Drawing.Point(274, 3);
            this.btnBuscadorProducto.Name = "btnBuscadorProducto";
            this.btnBuscadorProducto.Size = new System.Drawing.Size(29, 27);
            this.btnBuscadorProducto.TabIndex = 4;
            this.btnBuscadorProducto.UseVisualStyleBackColor = false;
            this.btnBuscadorProducto.Click += new System.EventHandler(this.btnBuscadorProducto_Click);
            // 
            // txtProducto
            // 
            this.txtProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(196)))));
            this.txtProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProducto.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProducto.Location = new System.Drawing.Point(38, 3);
            this.txtProducto.Name = "txtProducto";
            this.txtProducto.Size = new System.Drawing.Size(231, 27);
            this.txtProducto.TabIndex = 3;
            this.txtProducto.TextChanged += new System.EventHandler(this.txtProducto_TextChanged);
            this.txtProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscarProducto_KeyDown);
            // 
            // icoCandado
            // 
            this.icoCandado.BackColor = System.Drawing.Color.White;
            this.icoCandado.ForeColor = System.Drawing.Color.Black;
            this.icoCandado.IconChar = FontAwesome.Sharp.IconChar.Barcode;
            this.icoCandado.IconColor = System.Drawing.Color.Black;
            this.icoCandado.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.icoCandado.IconSize = 29;
            this.icoCandado.Location = new System.Drawing.Point(4, 2);
            this.icoCandado.Name = "icoCandado";
            this.icoCandado.Size = new System.Drawing.Size(29, 29);
            this.icoCandado.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.icoCandado.TabIndex = 0;
            this.icoCandado.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 367);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1081, 81);
            this.panel2.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.btnCobrar);
            this.panel5.Controls.Add(this.txtTotal);
            this.panel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(127)))), ((int)(((byte)(176)))));
            this.panel5.Location = new System.Drawing.Point(743, 3);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(334, 73);
            this.panel5.TabIndex = 0;
            // 
            // btnCobrar
            // 
            this.btnCobrar.BackColor = System.Drawing.Color.White;
            this.btnCobrar.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnCobrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCobrar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCobrar.ForeColor = System.Drawing.Color.Black;
            this.btnCobrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCobrar.Image")));
            this.btnCobrar.Location = new System.Drawing.Point(3, 3);
            this.btnCobrar.Name = "btnCobrar";
            this.btnCobrar.Size = new System.Drawing.Size(111, 67);
            this.btnCobrar.TabIndex = 12;
            this.btnCobrar.Text = "Cobrar";
            this.btnCobrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCobrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCobrar.UseVisualStyleBackColor = false;
            this.btnCobrar.Click += new System.EventHandler(this.btnCobrar_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.AutoEllipsis = true;
            this.txtTotal.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(110)))), ((int)(((byte)(142)))));
            this.txtTotal.Location = new System.Drawing.Point(124, 3);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTotal.Size = new System.Drawing.Size(204, 67);
            this.txtTotal.TabIndex = 0;
            this.txtTotal.Text = "$0.00";
            this.txtTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.flowLayoutPanel1);
            this.panel4.Location = new System.Drawing.Point(4, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(386, 73);
            this.panel4.TabIndex = 6;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnImprimirUltimoTicket);
            this.flowLayoutPanel1.Controls.Add(this.btnHistorialDeVenta);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(386, 73);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnImprimirUltimoTicket
            // 
            this.btnImprimirUltimoTicket.BackColor = System.Drawing.Color.White;
            this.btnImprimirUltimoTicket.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnImprimirUltimoTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimirUltimoTicket.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirUltimoTicket.ForeColor = System.Drawing.Color.Black;
            this.btnImprimirUltimoTicket.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimirUltimoTicket.Image")));
            this.btnImprimirUltimoTicket.Location = new System.Drawing.Point(3, 3);
            this.btnImprimirUltimoTicket.Name = "btnImprimirUltimoTicket";
            this.btnImprimirUltimoTicket.Size = new System.Drawing.Size(177, 67);
            this.btnImprimirUltimoTicket.TabIndex = 10;
            this.btnImprimirUltimoTicket.Tag = "Detalles";
            this.btnImprimirUltimoTicket.Text = "Cierre de caja";
            this.btnImprimirUltimoTicket.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnImprimirUltimoTicket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImprimirUltimoTicket.UseVisualStyleBackColor = false;
            this.btnImprimirUltimoTicket.Click += new System.EventHandler(this.btnImprimirUltimoTicket_Click);
            // 
            // btnHistorialDeVenta
            // 
            this.btnHistorialDeVenta.BackColor = System.Drawing.Color.White;
            this.btnHistorialDeVenta.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnHistorialDeVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialDeVenta.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorialDeVenta.ForeColor = System.Drawing.Color.Black;
            this.btnHistorialDeVenta.Image = ((System.Drawing.Image)(resources.GetObject("btnHistorialDeVenta.Image")));
            this.btnHistorialDeVenta.Location = new System.Drawing.Point(186, 3);
            this.btnHistorialDeVenta.Name = "btnHistorialDeVenta";
            this.btnHistorialDeVenta.Size = new System.Drawing.Size(197, 67);
            this.btnHistorialDeVenta.TabIndex = 9;
            this.btnHistorialDeVenta.Tag = "Historial";
            this.btnHistorialDeVenta.Text = "Historial de Venta";
            this.btnHistorialDeVenta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHistorialDeVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHistorialDeVenta.UseVisualStyleBackColor = false;
            this.btnHistorialDeVenta.Click += new System.EventHandler(this.btnHistorialDeVenta_Click);
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.Controls.Add(this.dgvVenta);
            this.panel7.Location = new System.Drawing.Point(4, 159);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1073, 202);
            this.panel7.TabIndex = 6;
            // 
            // dgvVenta
            // 
            this.dgvVenta.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvVenta.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVenta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVenta.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvVenta.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVenta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVenta.ColumnHeadersHeight = 27;
            this.dgvVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvVenta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcID,
            this.dgvcProducto,
            this.dgvcCantidad,
            this.dgvcPrecio,
            this.dgvcSubTotal,
            this.btnAumentar,
            this.btnDecrementar,
            this.btnEliminar});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVenta.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvVenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVenta.GridColor = System.Drawing.Color.White;
            this.dgvVenta.Location = new System.Drawing.Point(0, 0);
            this.dgvVenta.Name = "dgvVenta";
            this.dgvVenta.ReadOnly = true;
            this.dgvVenta.RowHeadersVisible = false;
            this.dgvVenta.Size = new System.Drawing.Size(1073, 202);
            this.dgvVenta.TabIndex = 2;
            this.dgvVenta.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVenta_CellDoubleClick);
            this.dgvVenta.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvVenta_CellPainting);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkRate = 300;
            this.errorProvider.ContainerControl = this;
            // 
            // dgvcID
            // 
            this.dgvcID.HeaderText = "ID";
            this.dgvcID.Name = "dgvcID";
            this.dgvcID.ReadOnly = true;
            this.dgvcID.Visible = false;
            // 
            // dgvcProducto
            // 
            this.dgvcProducto.FillWeight = 180F;
            this.dgvcProducto.HeaderText = "Producto";
            this.dgvcProducto.Name = "dgvcProducto";
            this.dgvcProducto.ReadOnly = true;
            // 
            // dgvcCantidad
            // 
            this.dgvcCantidad.FillWeight = 78.08739F;
            this.dgvcCantidad.HeaderText = "Cantidad";
            this.dgvcCantidad.Name = "dgvcCantidad";
            this.dgvcCantidad.ReadOnly = true;
            // 
            // dgvcPrecio
            // 
            this.dgvcPrecio.FillWeight = 51.2655F;
            this.dgvcPrecio.HeaderText = "Precio unitario";
            this.dgvcPrecio.Name = "dgvcPrecio";
            this.dgvcPrecio.ReadOnly = true;
            // 
            // dgvcSubTotal
            // 
            this.dgvcSubTotal.FillWeight = 51.2655F;
            this.dgvcSubTotal.HeaderText = "Subtotal";
            this.dgvcSubTotal.Name = "dgvcSubTotal";
            this.dgvcSubTotal.ReadOnly = true;
            // 
            // btnAumentar
            // 
            this.btnAumentar.FillWeight = 20F;
            this.btnAumentar.HeaderText = "";
            this.btnAumentar.Name = "btnAumentar";
            this.btnAumentar.ReadOnly = true;
            // 
            // btnDecrementar
            // 
            this.btnDecrementar.FillWeight = 20F;
            this.btnDecrementar.HeaderText = "";
            this.btnDecrementar.Name = "btnDecrementar";
            this.btnDecrementar.ReadOnly = true;
            // 
            // btnEliminar
            // 
            this.btnEliminar.FillWeight = 20F;
            this.btnEliminar.HeaderText = "";
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.ReadOnly = true;
            // 
            // frmVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 448);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmVentas";
            this.Text = "frmVentas";
            this.Load += new System.EventHandler(this.frmVentas_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogoEmpresa)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icoCandado)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel lblNumerodeVenta;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel lblCliente;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblNombreEmpresa;
        private System.Windows.Forms.PictureBox pbLogoEmpresa;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.Panel panel6;
        private FontAwesome.Sharp.IconButton btnBuscadorProducto;
        private System.Windows.Forms.TextBox txtProducto;
        private FontAwesome.Sharp.IconPictureBox icoCandado;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label txtTotal;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnCobrar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnImprimirUltimoTicket;
        private System.Windows.Forms.Button btnHistorialDeVenta;
        private System.Windows.Forms.DataGridView dgvVenta;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcSubTotal;
        private System.Windows.Forms.DataGridViewButtonColumn btnAumentar;
        private System.Windows.Forms.DataGridViewButtonColumn btnDecrementar;
        private System.Windows.Forms.DataGridViewButtonColumn btnEliminar;
    }
}