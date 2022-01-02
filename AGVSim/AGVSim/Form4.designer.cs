
namespace DWG_MAPVIEW
{
    partial class Form6
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form6));
            this.btn_Turn_To_Form1 = new System.Windows.Forms.Button();
            this.ReadDWG = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.myGroup1 = new AGVSim.myGroup(this.components);
            this.axAcCtrl1 = new AxACCTRLLib.AxAcCtrl();
            this.myGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcCtrl1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Turn_To_Form1
            // 
            this.btn_Turn_To_Form1.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btn_Turn_To_Form1.Location = new System.Drawing.Point(897, 25);
            this.btn_Turn_To_Form1.Name = "btn_Turn_To_Form1";
            this.btn_Turn_To_Form1.Size = new System.Drawing.Size(177, 41);
            this.btn_Turn_To_Form1.TabIndex = 0;
            this.btn_Turn_To_Form1.Text = "Leave";
            this.btn_Turn_To_Form1.UseVisualStyleBackColor = true;
            this.btn_Turn_To_Form1.Click += new System.EventHandler(this.btn_Turn_To_Form1_Click);
            // 
            // ReadDWG
            // 
            this.ReadDWG.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.ReadDWG.Location = new System.Drawing.Point(38, 25);
            this.ReadDWG.Margin = new System.Windows.Forms.Padding(2);
            this.ReadDWG.Name = "ReadDWG";
            this.ReadDWG.Size = new System.Drawing.Size(180, 42);
            this.ReadDWG.TabIndex = 3;
            this.ReadDWG.Text = "Load DWG file";
            this.ReadDWG.UseVisualStyleBackColor = true;
            this.ReadDWG.Click += new System.EventHandler(this.ReadDWG_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(222, 39);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(218, 27);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // myGroup1
            // 
            this.myGroup1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))));
            this.myGroup1.Controls.Add(this.ReadDWG);
            this.myGroup1.Controls.Add(this.textBox1);
            this.myGroup1.Controls.Add(this.btn_Turn_To_Form1);
            this.myGroup1.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.myGroup1.Location = new System.Drawing.Point(12, 1);
            this.myGroup1.Name = "myGroup1";
            this.myGroup1.Size = new System.Drawing.Size(1131, 74);
            this.myGroup1.TabIndex = 5;
            this.myGroup1.TabStop = false;
            this.myGroup1.Text = "Console ";
            // 
            // axAcCtrl1
            // 
            this.axAcCtrl1.Enabled = true;
            this.axAcCtrl1.Location = new System.Drawing.Point(12, 81);
            this.axAcCtrl1.Name = "axAcCtrl1";
            this.axAcCtrl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcCtrl1.OcxState")));
            this.axAcCtrl1.Size = new System.Drawing.Size(1131, 589);
            this.axAcCtrl1.TabIndex = 6;
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 682);
            this.Controls.Add(this.axAcCtrl1);
            this.Controls.Add(this.myGroup1);
            this.Name = "Form6";
            this.Text = "Form4";
            this.myGroup1.ResumeLayout(false);
            this.myGroup1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcCtrl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Turn_To_Form1;
        private System.Windows.Forms.Button ReadDWG;
        private System.Windows.Forms.TextBox textBox1;
        private AGVSim.myGroup myGroup1;
        private AxACCTRLLib.AxAcCtrl axAcCtrl1;
    }
}