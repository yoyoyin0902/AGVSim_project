
namespace AGVSim
{
    partial class Form5
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
            this.myGroup1 = new AGVSim.myGroup(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.Output = new System.Windows.Forms.Button();
            this.StayTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StationID = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.Delect_Order = new System.Windows.Forms.Button();
            this.myGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // myGroup1
            // 
            this.myGroup1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))));
            this.myGroup1.Controls.Add(this.label2);
            this.myGroup1.Controls.Add(this.Cancel);
            this.myGroup1.Controls.Add(this.Output);
            this.myGroup1.Controls.Add(this.StayTime);
            this.myGroup1.Controls.Add(this.label1);
            this.myGroup1.Controls.Add(this.StationID);
            this.myGroup1.Controls.Add(this.label12);
            this.myGroup1.Controls.Add(this.button4);
            this.myGroup1.Controls.Add(this.Delect_Order);
            this.myGroup1.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.myGroup1.Location = new System.Drawing.Point(1, -5);
            this.myGroup1.Name = "myGroup1";
            this.myGroup1.Size = new System.Drawing.Size(244, 132);
            this.myGroup1.TabIndex = 5;
            this.myGroup1.TabStop = false;
            this.myGroup1.Text = "Information";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label2.Location = new System.Drawing.Point(166, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 27);
            this.label2.TabIndex = 61;
            this.label2.Text = "s";
            // 
            // Cancel
            // 
            this.Cancel.Font = new System.Drawing.Font("Calibri", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.Cancel.ForeColor = System.Drawing.Color.Black;
            this.Cancel.Location = new System.Drawing.Point(127, 86);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(98, 40);
            this.Cancel.TabIndex = 60;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Output
            // 
            this.Output.Font = new System.Drawing.Font("Calibri", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.Output.ForeColor = System.Drawing.Color.Black;
            this.Output.Location = new System.Drawing.Point(13, 86);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(98, 40);
            this.Output.TabIndex = 59;
            this.Output.Text = "Access";
            this.Output.UseVisualStyleBackColor = true;
            this.Output.Click += new System.EventHandler(this.Output_Click);
            // 
            // StayTime
            // 
            this.StayTime.Location = new System.Drawing.Point(104, 53);
            this.StayTime.Name = "StayTime";
            this.StayTime.Size = new System.Drawing.Size(61, 27);
            this.StayTime.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.Location = new System.Drawing.Point(46, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 27);
            this.label1.TabIndex = 51;
            this.label1.Text = "Stay";
            // 
            // StationID
            // 
            this.StationID.AutoSize = true;
            this.StationID.Font = new System.Drawing.Font("Calibri", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.StationID.Location = new System.Drawing.Point(61, 23);
            this.StationID.Name = "StationID";
            this.StationID.Size = new System.Drawing.Size(104, 27);
            this.StationID.TabIndex = 50;
            this.StationID.Text = "Station ID";
            // 
            // label12
            // 
            this.label12.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label12.Location = new System.Drawing.Point(357, -171);
            this.label12.Name = "label12";
            this.label12.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label12.Size = new System.Drawing.Size(646, 143);
            this.label12.TabIndex = 1;
            this.label12.Text = "label12";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(755, 390);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(195, 54);
            this.button4.TabIndex = 17;
            this.button4.Text = "Clear";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // Delect_Order
            // 
            this.Delect_Order.Location = new System.Drawing.Point(7, 412);
            this.Delect_Order.Name = "Delect_Order";
            this.Delect_Order.Size = new System.Drawing.Size(271, 43);
            this.Delect_Order.TabIndex = 13;
            this.Delect_Order.Text = "Delet Order";
            this.Delect_Order.UseVisualStyleBackColor = true;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 128);
            this.Controls.Add(this.myGroup1);
            this.Name = "Form5";
            this.Text = "Form5";
            this.Load += new System.EventHandler(this.Form5_Load);
            this.myGroup1.ResumeLayout(false);
            this.myGroup1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Delect_Order;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.Label StationID;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox StayTime;
        private System.Windows.Forms.Button Output;
        private myGroup myGroup1;
        private System.Windows.Forms.Button Cancel;
        public System.Windows.Forms.Label label2;
    }
}