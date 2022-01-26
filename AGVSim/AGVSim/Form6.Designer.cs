
namespace AGVSim
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
            this.myGroup1 = new AGVSim.myGroup(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.Canael = new System.Windows.Forms.Button();
            this.Output = new System.Windows.Forms.Button();
            this.StayTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LineID = new System.Windows.Forms.Label();
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
            this.myGroup1.Controls.Add(this.Canael);
            this.myGroup1.Controls.Add(this.Output);
            this.myGroup1.Controls.Add(this.StayTime);
            this.myGroup1.Controls.Add(this.label1);
            this.myGroup1.Controls.Add(this.LineID);
            this.myGroup1.Controls.Add(this.label12);
            this.myGroup1.Controls.Add(this.button4);
            this.myGroup1.Controls.Add(this.Delect_Order);
            this.myGroup1.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.myGroup1.Location = new System.Drawing.Point(2, -2);
            this.myGroup1.Name = "myGroup1";
            this.myGroup1.Size = new System.Drawing.Size(244, 132);
            this.myGroup1.TabIndex = 6;
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
            // Canael
            // 
            this.Canael.Font = new System.Drawing.Font("Calibri", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.Canael.ForeColor = System.Drawing.Color.Black;
            this.Canael.Location = new System.Drawing.Point(127, 86);
            this.Canael.Name = "Canael";
            this.Canael.Size = new System.Drawing.Size(98, 40);
            this.Canael.TabIndex = 60;
            this.Canael.Text = "Cancel";
            this.Canael.UseVisualStyleBackColor = true;
            this.Canael.Click += new System.EventHandler(this.Canael_Click);
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
            // LineID
            // 
            this.LineID.AutoSize = true;
            this.LineID.Font = new System.Drawing.Font("Calibri", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.LineID.Location = new System.Drawing.Point(80, 23);
            this.LineID.Name = "LineID";
            this.LineID.Size = new System.Drawing.Size(74, 27);
            this.LineID.TabIndex = 50;
            this.LineID.Text = "Line ID";
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
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 128);
            this.Controls.Add(this.myGroup1);
            this.Name = "Form6";
            this.Text = "Form6";
            this.myGroup1.ResumeLayout(false);
            this.myGroup1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private myGroup myGroup1;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Canael;
        private System.Windows.Forms.Button Output;
        private System.Windows.Forms.TextBox StayTime;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label LineID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button Delect_Order;
    }
}