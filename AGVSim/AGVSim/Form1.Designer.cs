namespace AGVSim
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.FuntoolStrip = new System.Windows.Forms.ToolStrip();
            this.onButton_Station = new System.Windows.Forms.ToolStripButton();
            this.onButton_Connection = new System.Windows.Forms.ToolStripButton();
            this.onButton_Traffic = new System.Windows.Forms.ToolStripButton();
            this.onButton_Floor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.onButton_Line = new System.Windows.Forms.ToolStripButton();
            this.onButton_90Line = new System.Windows.Forms.ToolStripButton();
            this.onButton_Arc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.onButton_Select = new System.Windows.Forms.ToolStripButton();
            this.onButton_Grid = new System.Windows.Forms.ToolStripButton();
            this.onButton_FitGrid = new System.Windows.Forms.ToolStripButton();
            this.onButtonLChange = new System.Windows.Forms.ToolStripButton();
            this.onButton_NChange = new System.Windows.Forms.ToolStripButton();
            this.OnButton_DisplayPathInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.onButton_Car = new System.Windows.Forms.ToolStripButton();
            this.onButton_SetPath = new System.Windows.Forms.ToolStripButton();
            this.onButton_AStart = new System.Windows.Forms.ToolStripButton();
            this.onButton_AutoPath = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.onButton_Play = new System.Windows.Forms.ToolStripButton();
            this.onButton_Pause = new System.Windows.Forms.ToolStripButton();
            this.onButton_Stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.onButton_Save = new System.Windows.Forms.ToolStripButton();
            this.onButton_Load = new System.Windows.Forms.ToolStripButton();
            this.onButton_Order = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.pictureBoxMap = new System.Windows.Forms.PictureBox();
            this.FuntoolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).BeginInit();
            this.SuspendLayout();
            // 
            // FuntoolStrip
            // 
            this.FuntoolStrip.AutoSize = false;
            this.FuntoolStrip.BackColor = System.Drawing.SystemColors.MenuBar;
            this.FuntoolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FuntoolStrip.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FuntoolStrip.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.FuntoolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onButton_Station,
            this.onButton_Connection,
            this.onButton_Traffic,
            this.onButton_Floor,
            this.toolStripSeparator1,
            this.onButton_Line,
            this.onButton_90Line,
            this.onButton_Arc,
            this.toolStripSeparator2,
            this.onButton_Select,
            this.onButton_Grid,
            this.onButton_FitGrid,
            this.onButtonLChange,
            this.onButton_NChange,
            this.OnButton_DisplayPathInfo,
            this.toolStripSeparator3,
            this.onButton_Car,
            this.onButton_SetPath,
            this.onButton_AStart,
            this.onButton_AutoPath,
            this.toolStripSeparator4,
            this.onButton_Play,
            this.onButton_Pause,
            this.onButton_Stop,
            this.toolStripSeparator5,
            this.onButton_Save,
            this.onButton_Load,
            this.onButton_Order,
            this.toolStripSeparator6,
            this.toolStripButton1});
            this.FuntoolStrip.Location = new System.Drawing.Point(0, 0);
            this.FuntoolStrip.Name = "FuntoolStrip";
            this.FuntoolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.FuntoolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.FuntoolStrip.Size = new System.Drawing.Size(1150, 65);
            this.FuntoolStrip.TabIndex = 0;
            this.FuntoolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.FuntoolStrip_ItemClicked);
            // 
            // onButton_Station
            // 
            this.onButton_Station.AutoSize = false;
            this.onButton_Station.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_Station.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onButton_Station.Image = ((System.Drawing.Image)(resources.GetObject("onButton_Station.Image")));
            this.onButton_Station.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.onButton_Station.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_Station.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_Station.Name = "onButton_Station";
            this.onButton_Station.Size = new System.Drawing.Size(37, 55);
            this.onButton_Station.Text = "Station";
            this.onButton_Station.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.onButton_Station.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_Station.Click += new System.EventHandler(this.onButton_Station_Click);
            // 
            // onButton_Connection
            // 
            this.onButton_Connection.AutoSize = false;
            this.onButton_Connection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_Connection.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.onButton_Connection.Image = ((System.Drawing.Image)(resources.GetObject("onButton_Connection.Image")));
            this.onButton_Connection.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.onButton_Connection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_Connection.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_Connection.Name = "onButton_Connection";
            this.onButton_Connection.Size = new System.Drawing.Size(45, 55);
            this.onButton_Connection.Text = "Connection";
            this.onButton_Connection.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.onButton_Connection.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_Connection.Click += new System.EventHandler(this.onButton_Connection_Click);
            // 
            // onButton_Traffic
            // 
            this.onButton_Traffic.AutoSize = false;
            this.onButton_Traffic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_Traffic.Image = ((System.Drawing.Image)(resources.GetObject("onButton_Traffic.Image")));
            this.onButton_Traffic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_Traffic.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_Traffic.Name = "onButton_Traffic";
            this.onButton_Traffic.Size = new System.Drawing.Size(37, 55);
            this.onButton_Traffic.Text = "Traffic";
            this.onButton_Traffic.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_Traffic.ToolTipText = "Traffic";
            this.onButton_Traffic.Click += new System.EventHandler(this.onButton_Traffic_Click);
            // 
            // onButton_Floor
            // 
            this.onButton_Floor.AutoSize = false;
            this.onButton_Floor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_Floor.Image = ((System.Drawing.Image)(resources.GetObject("onButton_Floor.Image")));
            this.onButton_Floor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_Floor.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_Floor.Name = "onButton_Floor";
            this.onButton_Floor.Size = new System.Drawing.Size(37, 55);
            this.onButton_Floor.Text = "Floor";
            this.onButton_Floor.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_Floor.ToolTipText = "Floor";
            this.onButton_Floor.Click += new System.EventHandler(this.onButton_Floor_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 65);
            // 
            // onButton_Line
            // 
            this.onButton_Line.AutoSize = false;
            this.onButton_Line.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_Line.Image = ((System.Drawing.Image)(resources.GetObject("onButton_Line.Image")));
            this.onButton_Line.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_Line.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_Line.Name = "onButton_Line";
            this.onButton_Line.Size = new System.Drawing.Size(30, 55);
            this.onButton_Line.Text = "Line";
            this.onButton_Line.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_Line.Click += new System.EventHandler(this.onButton_Line_Click);
            // 
            // onButton_90Line
            // 
            this.onButton_90Line.AutoSize = false;
            this.onButton_90Line.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_90Line.Image = ((System.Drawing.Image)(resources.GetObject("onButton_90Line.Image")));
            this.onButton_90Line.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_90Line.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_90Line.Name = "onButton_90Line";
            this.onButton_90Line.Size = new System.Drawing.Size(35, 55);
            this.onButton_90Line.Text = "90Line";
            this.onButton_90Line.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_90Line.Click += new System.EventHandler(this.onButton_90Line_Click);
            // 
            // onButton_Arc
            // 
            this.onButton_Arc.AutoSize = false;
            this.onButton_Arc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_Arc.Image = ((System.Drawing.Image)(resources.GetObject("onButton_Arc.Image")));
            this.onButton_Arc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_Arc.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_Arc.Name = "onButton_Arc";
            this.onButton_Arc.Size = new System.Drawing.Size(35, 55);
            this.onButton_Arc.Text = "Arc";
            this.onButton_Arc.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_Arc.Click += new System.EventHandler(this.onButton_Arc_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 65);
            // 
            // onButton_Select
            // 
            this.onButton_Select.AutoSize = false;
            this.onButton_Select.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_Select.Image = ((System.Drawing.Image)(resources.GetObject("onButton_Select.Image")));
            this.onButton_Select.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_Select.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_Select.Name = "onButton_Select";
            this.onButton_Select.Size = new System.Drawing.Size(38, 55);
            this.onButton_Select.Text = "Select";
            this.onButton_Select.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_Select.Click += new System.EventHandler(this.onButton_Select_Click);
            // 
            // onButton_Grid
            // 
            this.onButton_Grid.AutoSize = false;
            this.onButton_Grid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_Grid.Image = ((System.Drawing.Image)(resources.GetObject("onButton_Grid.Image")));
            this.onButton_Grid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_Grid.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_Grid.Name = "onButton_Grid";
            this.onButton_Grid.Size = new System.Drawing.Size(38, 55);
            this.onButton_Grid.Text = "Grid";
            this.onButton_Grid.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_Grid.ToolTipText = "Grid";
            this.onButton_Grid.Click += new System.EventHandler(this.onButton_Grid_Click);
            // 
            // onButton_FitGrid
            // 
            this.onButton_FitGrid.AutoSize = false;
            this.onButton_FitGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_FitGrid.Image = ((System.Drawing.Image)(resources.GetObject("onButton_FitGrid.Image")));
            this.onButton_FitGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_FitGrid.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_FitGrid.Name = "onButton_FitGrid";
            this.onButton_FitGrid.Size = new System.Drawing.Size(38, 55);
            this.onButton_FitGrid.Text = "Fit Grid";
            this.onButton_FitGrid.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_FitGrid.Click += new System.EventHandler(this.onButton_FitGrid_Click);
            // 
            // onButtonLChange
            // 
            this.onButtonLChange.AutoSize = false;
            this.onButtonLChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButtonLChange.Image = ((System.Drawing.Image)(resources.GetObject("onButtonLChange.Image")));
            this.onButtonLChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButtonLChange.Margin = new System.Windows.Forms.Padding(5);
            this.onButtonLChange.Name = "onButtonLChange";
            this.onButtonLChange.Size = new System.Drawing.Size(38, 55);
            this.onButtonLChange.Text = "Line Change";
            this.onButtonLChange.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButtonLChange.Click += new System.EventHandler(this.onButtonLChange_Click);
            // 
            // onButton_NChange
            // 
            this.onButton_NChange.AutoSize = false;
            this.onButton_NChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_NChange.Image = ((System.Drawing.Image)(resources.GetObject("onButton_NChange.Image")));
            this.onButton_NChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_NChange.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_NChange.Name = "onButton_NChange";
            this.onButton_NChange.Size = new System.Drawing.Size(45, 55);
            this.onButton_NChange.Text = "Node Change";
            this.onButton_NChange.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_NChange.Click += new System.EventHandler(this.onButton_NChange_Click);
            // 
            // OnButton_DisplayPathInfo
            // 
            this.OnButton_DisplayPathInfo.AutoSize = false;
            this.OnButton_DisplayPathInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OnButton_DisplayPathInfo.Image = ((System.Drawing.Image)(resources.GetObject("OnButton_DisplayPathInfo.Image")));
            this.OnButton_DisplayPathInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OnButton_DisplayPathInfo.Name = "OnButton_DisplayPathInfo";
            this.OnButton_DisplayPathInfo.Size = new System.Drawing.Size(54, 62);
            this.OnButton_DisplayPathInfo.Text = "DisplayPathInfo";
            this.OnButton_DisplayPathInfo.ToolTipText = "DisplayPathInfo";
            this.OnButton_DisplayPathInfo.Click += new System.EventHandler(this.OnButton_DisplayPathInfo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 65);
            // 
            // onButton_Car
            // 
            this.onButton_Car.AutoSize = false;
            this.onButton_Car.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_Car.Image = ((System.Drawing.Image)(resources.GetObject("onButton_Car.Image")));
            this.onButton_Car.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_Car.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_Car.Name = "onButton_Car";
            this.onButton_Car.Size = new System.Drawing.Size(40, 55);
            this.onButton_Car.Text = "Car";
            this.onButton_Car.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_Car.ToolTipText = "Car";
            this.onButton_Car.Click += new System.EventHandler(this.onButton_Car_Click);
            // 
            // onButton_SetPath
            // 
            this.onButton_SetPath.AutoSize = false;
            this.onButton_SetPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_SetPath.Image = ((System.Drawing.Image)(resources.GetObject("onButton_SetPath.Image")));
            this.onButton_SetPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_SetPath.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_SetPath.Name = "onButton_SetPath";
            this.onButton_SetPath.Size = new System.Drawing.Size(40, 55);
            this.onButton_SetPath.Text = "SetPath";
            this.onButton_SetPath.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_SetPath.ToolTipText = "SetPath";
            this.onButton_SetPath.Click += new System.EventHandler(this.onButton_SetPath_Click);
            // 
            // onButton_AStart
            // 
            this.onButton_AStart.AutoSize = false;
            this.onButton_AStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_AStart.Image = ((System.Drawing.Image)(resources.GetObject("onButton_AStart.Image")));
            this.onButton_AStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_AStart.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_AStart.Name = "onButton_AStart";
            this.onButton_AStart.Size = new System.Drawing.Size(50, 55);
            this.onButton_AStart.Text = "AStart";
            this.onButton_AStart.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_AStart.ToolTipText = "AStart";
            this.onButton_AStart.Click += new System.EventHandler(this.onButton_AStart_Click);
            // 
            // onButton_AutoPath
            // 
            this.onButton_AutoPath.AutoSize = false;
            this.onButton_AutoPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_AutoPath.Image = ((System.Drawing.Image)(resources.GetObject("onButton_AutoPath.Image")));
            this.onButton_AutoPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_AutoPath.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_AutoPath.Name = "onButton_AutoPath";
            this.onButton_AutoPath.Size = new System.Drawing.Size(50, 55);
            this.onButton_AutoPath.Text = "AutoPath";
            this.onButton_AutoPath.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_AutoPath.Click += new System.EventHandler(this.onButton_AutoPath_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 65);
            // 
            // onButton_Play
            // 
            this.onButton_Play.AutoSize = false;
            this.onButton_Play.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_Play.Image = ((System.Drawing.Image)(resources.GetObject("onButton_Play.Image")));
            this.onButton_Play.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_Play.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_Play.Name = "onButton_Play";
            this.onButton_Play.Size = new System.Drawing.Size(38, 55);
            this.onButton_Play.Text = "Play";
            this.onButton_Play.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_Play.ToolTipText = "Play";
            this.onButton_Play.Click += new System.EventHandler(this.onButton_Play_Click);
            // 
            // onButton_Pause
            // 
            this.onButton_Pause.AutoSize = false;
            this.onButton_Pause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_Pause.Image = ((System.Drawing.Image)(resources.GetObject("onButton_Pause.Image")));
            this.onButton_Pause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_Pause.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_Pause.Name = "onButton_Pause";
            this.onButton_Pause.Size = new System.Drawing.Size(38, 55);
            this.onButton_Pause.Text = "Pause";
            this.onButton_Pause.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_Pause.ToolTipText = "Pause";
            this.onButton_Pause.Click += new System.EventHandler(this.onButton_Pause_Click);
            // 
            // onButton_Stop
            // 
            this.onButton_Stop.AutoSize = false;
            this.onButton_Stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_Stop.Image = ((System.Drawing.Image)(resources.GetObject("onButton_Stop.Image")));
            this.onButton_Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_Stop.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_Stop.Name = "onButton_Stop";
            this.onButton_Stop.Size = new System.Drawing.Size(38, 55);
            this.onButton_Stop.Text = "Stop";
            this.onButton_Stop.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_Stop.Click += new System.EventHandler(this.onButton_Stop_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 65);
            // 
            // onButton_Save
            // 
            this.onButton_Save.AutoSize = false;
            this.onButton_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_Save.Image = ((System.Drawing.Image)(resources.GetObject("onButton_Save.Image")));
            this.onButton_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_Save.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_Save.Name = "onButton_Save";
            this.onButton_Save.Size = new System.Drawing.Size(38, 55);
            this.onButton_Save.Text = "Save";
            this.onButton_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_Save.Click += new System.EventHandler(this.onButton_Save_Click);
            // 
            // onButton_Load
            // 
            this.onButton_Load.AutoSize = false;
            this.onButton_Load.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_Load.Image = ((System.Drawing.Image)(resources.GetObject("onButton_Load.Image")));
            this.onButton_Load.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_Load.Margin = new System.Windows.Forms.Padding(5);
            this.onButton_Load.Name = "onButton_Load";
            this.onButton_Load.Size = new System.Drawing.Size(38, 55);
            this.onButton_Load.Text = "Load";
            this.onButton_Load.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_Load.ToolTipText = "Load";
            this.onButton_Load.Click += new System.EventHandler(this.onButton_Load_Click);
            // 
            // onButton_Order
            // 
            this.onButton_Order.AutoSize = false;
            this.onButton_Order.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.onButton_Order.Image = global::AGVSim.Properties.Resources.order_history_80px;
            this.onButton_Order.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.onButton_Order.Margin = new System.Windows.Forms.Padding(-9, 0, 5, 5);
            this.onButton_Order.Name = "onButton_Order";
            this.onButton_Order.Size = new System.Drawing.Size(50, 60);
            this.onButton_Order.Text = "Order";
            this.onButton_Order.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.onButton_Order.ToolTipText = "Order";
            this.onButton_Order.Click += new System.EventHandler(this.onButton_Order_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 81);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(54, 54);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_2);
            // 
            // pictureBoxMap
            // 
            this.pictureBoxMap.BackColor = System.Drawing.Color.White;
            this.pictureBoxMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxMap.Location = new System.Drawing.Point(0, 65);
            this.pictureBoxMap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxMap.Name = "pictureBoxMap";
            this.pictureBoxMap.Size = new System.Drawing.Size(1150, 617);
            this.pictureBoxMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxMap.TabIndex = 1;
            this.pictureBoxMap.TabStop = false;
            this.pictureBoxMap.Click += new System.EventHandler(this.pictureBoxMap_Click);
            this.pictureBoxMap.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxMap_Paint);
            this.pictureBoxMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMap_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1150, 682);
            this.Controls.Add(this.pictureBoxMap);
            this.Controls.Add(this.FuntoolStrip);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.FuntoolStrip.ResumeLayout(false);
            this.FuntoolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip FuntoolStrip;
        private System.Windows.Forms.ToolStripButton onButton_Station;
        private System.Windows.Forms.ToolStripButton onButton_Connection;
        private System.Windows.Forms.ToolStripButton onButton_Traffic;
        private System.Windows.Forms.ToolStripButton onButton_Floor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton onButton_Line;
        private System.Windows.Forms.ToolStripButton onButton_90Line;
        private System.Windows.Forms.ToolStripButton onButton_Arc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton onButton_Select;
        private System.Windows.Forms.ToolStripButton onButton_Grid;
        private System.Windows.Forms.ToolStripButton onButtonLChange;
        private System.Windows.Forms.ToolStripButton onButton_NChange;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton onButton_Car;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton onButton_Play;
        private System.Windows.Forms.ToolStripButton onButton_Pause;
        private System.Windows.Forms.ToolStripButton onButton_Stop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton onButton_Save;
        private System.Windows.Forms.ToolStripButton onButton_Load;
        private System.Windows.Forms.ToolStripButton onButton_FitGrid;
        private System.Windows.Forms.ToolStripButton onButton_SetPath;
        private System.Windows.Forms.ToolStripButton onButton_AStart;
        private System.Windows.Forms.ToolStripButton onButton_AutoPath;
        private System.Windows.Forms.ToolStripButton OnButton_DisplayPathInfo;
        private System.Windows.Forms.ToolStripButton onButton_Order;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        public System.Windows.Forms.PictureBox pictureBoxMap;
    }
}

