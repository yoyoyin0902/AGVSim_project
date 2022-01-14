namespace AGVSim
{
    partial class Form2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.aGVSimDataSet = new AGVSim.AGVSimDataSet();
            this.aGVSimDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.orderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.orderTableAdapter = new AGVSim.AGVSimDataSetTableAdapters.OrderTableAdapter();
            this.aGVBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.aGVTableAdapter = new AGVSim.AGVSimDataSetTableAdapters.AGVTableAdapter();
            this.orderBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.orderBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.aGVSimDataSet1 = new AGVSim.AGVSimDataSet1();
            this.orderBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.orderTableAdapter1 = new AGVSim.AGVSimDataSet1TableAdapters.OrderTableAdapter();
            this.orderBindingSource4 = new System.Windows.Forms.BindingSource(this.components);
            this.orderBindingSource5 = new System.Windows.Forms.BindingSource(this.components);
            this.myGroup2 = new AGVSim.myGroup(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Product_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.myGroup1 = new AGVSim.myGroup(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Delect_Order = new System.Windows.Forms.Button();
            this.Add_Order = new System.Windows.Forms.Button();
            this.textBox_Status = new System.Windows.Forms.TextBox();
            this.textBox_Done = new System.Windows.Forms.TextBox();
            this.lab_Done = new System.Windows.Forms.Label();
            this.lab_Status = new System.Windows.Forms.Label();
            this.lab_Priority = new System.Windows.Forms.Label();
            this.lab_Due = new System.Windows.Forms.Label();
            this.lab_Quantity = new System.Windows.Forms.Label();
            this.lab_Product_ID = new System.Windows.Forms.Label();
            this.textBox_Due = new System.Windows.Forms.TextBox();
            this.textBox_Quantity = new System.Windows.Forms.TextBox();
            this.textBox_Product_ID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.aGVSimDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGVSimDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGVBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGVSimDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBindingSource4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBindingSource5)).BeginInit();
            this.myGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.myGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // aGVSimDataSet
            // 
            this.aGVSimDataSet.DataSetName = "AGVSimDataSet";
            this.aGVSimDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // aGVSimDataSetBindingSource
            // 
            this.aGVSimDataSetBindingSource.DataSource = this.aGVSimDataSet;
            this.aGVSimDataSetBindingSource.Position = 0;
            // 
            // orderBindingSource
            // 
            this.orderBindingSource.DataMember = "Order";
            this.orderBindingSource.DataSource = this.aGVSimDataSetBindingSource;
            // 
            // orderTableAdapter
            // 
            this.orderTableAdapter.ClearBeforeFill = true;
            // 
            // aGVBindingSource
            // 
            this.aGVBindingSource.DataMember = "AGV";
            this.aGVBindingSource.DataSource = this.aGVSimDataSetBindingSource;
            // 
            // aGVTableAdapter
            // 
            this.aGVTableAdapter.ClearBeforeFill = true;
            // 
            // orderBindingSource1
            // 
            this.orderBindingSource1.DataMember = "Order";
            this.orderBindingSource1.DataSource = this.aGVSimDataSetBindingSource;
            // 
            // orderBindingSource2
            // 
            this.orderBindingSource2.DataMember = "Order";
            this.orderBindingSource2.DataSource = this.aGVSimDataSetBindingSource;
            // 
            // aGVSimDataSet1
            // 
            this.aGVSimDataSet1.DataSetName = "AGVSimDataSet1";
            this.aGVSimDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // orderBindingSource3
            // 
            this.orderBindingSource3.DataMember = "Order";
            this.orderBindingSource3.DataSource = this.aGVSimDataSet1;
            // 
            // orderTableAdapter1
            // 
            this.orderTableAdapter1.ClearBeforeFill = true;
            // 
            // orderBindingSource4
            // 
            this.orderBindingSource4.DataMember = "Order";
            this.orderBindingSource4.DataSource = this.aGVSimDataSetBindingSource;
            // 
            // orderBindingSource5
            // 
            this.orderBindingSource5.DataMember = "Order";
            this.orderBindingSource5.DataSource = this.aGVSimDataSetBindingSource;
            // 
            // myGroup2
            // 
            this.myGroup2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myGroup2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))));
            this.myGroup2.Controls.Add(this.dataGridView1);
            this.myGroup2.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myGroup2.Location = new System.Drawing.Point(5, 12);
            this.myGroup2.Name = "myGroup2";
            this.myGroup2.Size = new System.Drawing.Size(1002, 219);
            this.myGroup2.TabIndex = 3;
            this.myGroup2.TabStop = false;
            this.myGroup2.Text = "Registered Production Routes";
            this.myGroup2.Enter += new System.EventHandler(this.myGroup2_Enter);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Product_ID,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 23);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 31;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(996, 193);
            this.dataGridView1.TabIndex = 21;
            this.dataGridView1.BorderStyleChanged += new System.EventHandler(this.MessageShow_TextChanged);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Product_ID
            // 
            this.Product_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Product_ID.HeaderText = "Product_ID";
            this.Product_ID.Name = "Product_ID";
            this.Product_ID.ReadOnly = true;
            this.Product_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "Quantity";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "Priority";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.HeaderText = "Due";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Done";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Status";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // myGroup1
            // 
            this.myGroup1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))));
            this.myGroup1.Controls.Add(this.label1);
            this.myGroup1.Controls.Add(this.label2);
            this.myGroup1.Controls.Add(this.button5);
            this.myGroup1.Controls.Add(this.button4);
            this.myGroup1.Controls.Add(this.button3);
            this.myGroup1.Controls.Add(this.comboBox1);
            this.myGroup1.Controls.Add(this.Delect_Order);
            this.myGroup1.Controls.Add(this.Add_Order);
            this.myGroup1.Controls.Add(this.textBox_Status);
            this.myGroup1.Controls.Add(this.textBox_Done);
            this.myGroup1.Controls.Add(this.lab_Done);
            this.myGroup1.Controls.Add(this.lab_Status);
            this.myGroup1.Controls.Add(this.lab_Priority);
            this.myGroup1.Controls.Add(this.lab_Due);
            this.myGroup1.Controls.Add(this.lab_Quantity);
            this.myGroup1.Controls.Add(this.lab_Product_ID);
            this.myGroup1.Controls.Add(this.textBox_Due);
            this.myGroup1.Controls.Add(this.textBox_Quantity);
            this.myGroup1.Controls.Add(this.textBox_Product_ID);
            this.myGroup1.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.myGroup1.Location = new System.Drawing.Point(5, 237);
            this.myGroup1.Name = "myGroup1";
            this.myGroup1.Size = new System.Drawing.Size(1002, 471);
            this.myGroup1.TabIndex = 2;
            this.myGroup1.TabStop = false;
            this.myGroup1.Text = "        ";
            this.myGroup1.Enter += new System.EventHandler(this.myGroup1_Enter);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(313, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(637, 327);
            this.label1.TabIndex = 20;
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(357, -171);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(646, 143);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(317, 390);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(195, 54);
            this.button5.TabIndex = 19;
            this.button5.Text = "load";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(755, 390);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(195, 54);
            this.button4.TabIndex = 17;
            this.button4.Text = "Clear";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(546, 390);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(188, 54);
            this.button3.TabIndex = 16;
            this.button3.Text = "accepte";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "0 :Lowest",
            "1",
            "2",
            "3",
            "4",
            "5 :Highest"});
            this.comboBox1.Location = new System.Drawing.Point(135, 205);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(125, 27);
            this.comboBox1.TabIndex = 14;
            // 
            // Delect_Order
            // 
            this.Delect_Order.Location = new System.Drawing.Point(7, 412);
            this.Delect_Order.Name = "Delect_Order";
            this.Delect_Order.Size = new System.Drawing.Size(271, 43);
            this.Delect_Order.TabIndex = 13;
            this.Delect_Order.Text = "Delet Order";
            this.Delect_Order.UseVisualStyleBackColor = true;
            this.Delect_Order.Click += new System.EventHandler(this.button2_Click);
            // 
            // Add_Order
            // 
            this.Add_Order.Location = new System.Drawing.Point(7, 364);
            this.Add_Order.Name = "Add_Order";
            this.Add_Order.Size = new System.Drawing.Size(271, 42);
            this.Add_Order.TabIndex = 12;
            this.Add_Order.Text = "Add Order";
            this.Add_Order.UseVisualStyleBackColor = true;
            this.Add_Order.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_Status
            // 
            this.textBox_Status.BackColor = System.Drawing.Color.White;
            this.textBox_Status.Location = new System.Drawing.Point(135, 321);
            this.textBox_Status.Name = "textBox_Status";
            this.textBox_Status.Size = new System.Drawing.Size(125, 27);
            this.textBox_Status.TabIndex = 11;
            this.textBox_Status.TextChanged += new System.EventHandler(this.Done_TextChanged);
            // 
            // textBox_Done
            // 
            this.textBox_Done.BackColor = System.Drawing.Color.White;
            this.textBox_Done.Location = new System.Drawing.Point(135, 264);
            this.textBox_Done.Name = "textBox_Done";
            this.textBox_Done.Size = new System.Drawing.Size(125, 27);
            this.textBox_Done.TabIndex = 10;
            // 
            // lab_Done
            // 
            this.lab_Done.AutoSize = true;
            this.lab_Done.Location = new System.Drawing.Point(18, 272);
            this.lab_Done.Name = "lab_Done";
            this.lab_Done.Size = new System.Drawing.Size(43, 19);
            this.lab_Done.TabIndex = 9;
            this.lab_Done.Text = "Done";
            // 
            // lab_Status
            // 
            this.lab_Status.AutoSize = true;
            this.lab_Status.Location = new System.Drawing.Point(18, 321);
            this.lab_Status.Name = "lab_Status";
            this.lab_Status.Size = new System.Drawing.Size(50, 19);
            this.lab_Status.TabIndex = 8;
            this.lab_Status.Text = "Status";
            // 
            // lab_Priority
            // 
            this.lab_Priority.AutoSize = true;
            this.lab_Priority.Location = new System.Drawing.Point(18, 213);
            this.lab_Priority.Name = "lab_Priority";
            this.lab_Priority.Size = new System.Drawing.Size(68, 19);
            this.lab_Priority.TabIndex = 7;
            this.lab_Priority.Text = "Priority :";
            // 
            // lab_Due
            // 
            this.lab_Due.AutoSize = true;
            this.lab_Due.Location = new System.Drawing.Point(18, 157);
            this.lab_Due.Name = "lab_Due";
            this.lab_Due.Size = new System.Drawing.Size(74, 19);
            this.lab_Due.TabIndex = 6;
            this.lab_Due.Text = "Due(sec) :";
            // 
            // lab_Quantity
            // 
            this.lab_Quantity.AutoSize = true;
            this.lab_Quantity.Location = new System.Drawing.Point(18, 99);
            this.lab_Quantity.Name = "lab_Quantity";
            this.lab_Quantity.Size = new System.Drawing.Size(75, 19);
            this.lab_Quantity.TabIndex = 5;
            this.lab_Quantity.Text = "Quantity :";
            // 
            // lab_Product_ID
            // 
            this.lab_Product_ID.AutoSize = true;
            this.lab_Product_ID.Location = new System.Drawing.Point(18, 41);
            this.lab_Product_ID.Name = "lab_Product_ID";
            this.lab_Product_ID.Size = new System.Drawing.Size(87, 19);
            this.lab_Product_ID.TabIndex = 4;
            this.lab_Product_ID.Text = "Product ID :";
            // 
            // textBox_Due
            // 
            this.textBox_Due.BackColor = System.Drawing.Color.White;
            this.textBox_Due.Location = new System.Drawing.Point(135, 149);
            this.textBox_Due.Name = "textBox_Due";
            this.textBox_Due.Size = new System.Drawing.Size(125, 27);
            this.textBox_Due.TabIndex = 2;
            // 
            // textBox_Quantity
            // 
            this.textBox_Quantity.BackColor = System.Drawing.Color.White;
            this.textBox_Quantity.Location = new System.Drawing.Point(135, 96);
            this.textBox_Quantity.Name = "textBox_Quantity";
            this.textBox_Quantity.Size = new System.Drawing.Size(125, 27);
            this.textBox_Quantity.TabIndex = 1;
            // 
            // textBox_Product_ID
            // 
            this.textBox_Product_ID.BackColor = System.Drawing.Color.White;
            this.textBox_Product_ID.Location = new System.Drawing.Point(135, 38);
            this.textBox_Product_ID.Name = "textBox_Product_ID";
            this.textBox_Product_ID.Size = new System.Drawing.Size(125, 27);
            this.textBox_Product_ID.TabIndex = 0;
            this.textBox_Product_ID.TextChanged += new System.EventHandler(this.Product_ID_TextChanged);
            // 
            // Form2
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1019, 720);
            this.Controls.Add(this.myGroup2);
            this.Controls.Add(this.myGroup1);
            this.Font = new System.Drawing.Font("Calibri", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.aGVSimDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGVSimDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGVBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGVSimDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBindingSource4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBindingSource5)).EndInit();
            this.myGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.myGroup1.ResumeLayout(false);
            this.myGroup1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private myGroup myGroup1;
        private myGroup myGroup2;
        private System.Windows.Forms.Label lab_Done;
        private System.Windows.Forms.Label lab_Status;
        private System.Windows.Forms.Label lab_Priority;
        private System.Windows.Forms.Label lab_Due;
        private System.Windows.Forms.Label lab_Quantity;
        private System.Windows.Forms.Label lab_Product_ID;
        private System.Windows.Forms.TextBox textBox_Due;
        private System.Windows.Forms.TextBox textBox_Quantity;
        private System.Windows.Forms.TextBox textBox_Product_ID;
        private System.Windows.Forms.TextBox textBox_Status;
        private System.Windows.Forms.TextBox textBox_Done;
        private System.Windows.Forms.Button Delect_Order;
        private System.Windows.Forms.Button Add_Order;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.BindingSource aGVSimDataSetBindingSource;
        private AGVSimDataSet aGVSimDataSet;
        private System.Windows.Forms.BindingSource orderBindingSource;
        private AGVSimDataSetTableAdapters.OrderTableAdapter orderTableAdapter;
        private System.Windows.Forms.BindingSource aGVBindingSource;
        private AGVSimDataSetTableAdapters.AGVTableAdapter aGVTableAdapter;
        private System.Windows.Forms.BindingSource orderBindingSource1;
        private System.Windows.Forms.BindingSource orderBindingSource2;
        private AGVSimDataSet1 aGVSimDataSet1;
        private System.Windows.Forms.BindingSource orderBindingSource3;
        private AGVSimDataSet1TableAdapters.OrderTableAdapter orderTableAdapter1;
        private System.Windows.Forms.BindingSource orderBindingSource4;
        private System.Windows.Forms.BindingSource orderBindingSource5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Product_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}