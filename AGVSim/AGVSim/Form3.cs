using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AGVSim;
using System.Reflection;

namespace DWG_MAPVIEW
{

    public partial class Form4 : Form
    {
        public int c = 0, d = 0; //c : 判斷選取框調整按鈕是否有作用
        int finalX = 0, finalY = 0;
        int Pen_Button_adjust = 1;
        int Pen_Bar_adjust = 3;
        int Pen_adjust_Topsum = 0;
        int Pen_adjust_Bottomsum = 0;
        int Pen_adjust_Leftsum = 0;
        int Pen_adjust_Rightsum = 0;
        int Pen_adjust_Topsum_before = 0;
        int Pen_adjust_Bottomsum_before = 0;
        int Pen_adjust_Leftsum_before = 0;
        int Pen_adjust_Rightsum_before = 0;
        int startXsize, startYsize, endXsize, endYsize;
        int UpBarValue = 50, BottomBarValue = 50, LeftBarValue = 50, RightBarValue = 50;


        string url = System.Environment.CurrentDirectory;
        // C:\Users\amy33\Documents\IVAM\AGVSim_project/model_map.jpg
        
        
        String strHostName = Dns.GetHostName(); // 取得本機名稱




        Point start, end , NowPosition;
        bool blnDraw;//在MouseMove事件中判斷是否繪製矩形框
 


        public struct StartAndEndPoint
        {
            Point Start;
            Point End;
            Point NowPosition;
        }


        public Form4()
        {
            InitializeComponent();
 //           this.myGroup2.MouseWheel += new MouseEventHandler(JPG_PictureBox_MouseWheel);  //滾輪事件
        }

        

        private void Form1_Load(object sender, EventArgs e)

        {
        }

        int form1picboxmaph, form1picboxmapw;

        private void Turn_to_Form2_Click(object sender, EventArgs e)
        {
            Form f6 = new Form6();
            f6.Visible = true;
        }

        
        

        public void ReadJPG_Click(object sender, EventArgs e)
        {
            this.JPG_PictureBox.Image = Image.FromFile(url + "/model_map.jpg");
            JPG_PictureBox.Size = Image.FromFile(url + "/model_map.jpg").Size;
            JPG_PictureBox.Width = JPG_PictureBox.Width * 60 / 100;
            JPG_PictureBox.Height = JPG_PictureBox.Height * 60 / 100;
            if (c==0)
            {
                form1picboxmaph = Form1.form1.pictureBoxMap.Height; 
                form1picboxmapw = Form1.form1.pictureBoxMap.Width;
                c = 1;
            }
            Form1.form1.pictureBoxMap.Image = Image.FromFile(url + "/model_map.jpg");
            Form1.form1.pictureBoxMap.Width = form1picboxmapw;
            Form1.form1.pictureBoxMap.Height = form1picboxmaph;

            /*
            if (c == 0) c = 1;
            this.JPG_PictureBox.Image = Image.FromFile(url+ "/model_map.jpg");
            JPG_PictureBox.Size = Image.FromFile(url + "/model_map.jpg").Size;
            Form1.form1.pictureBoxMap.Image = Image.FromFile(url + "/model_map.jpg");
            Form1.form1.pictureBoxMap.Size = Image.FromFile(url + "/model_map.jpg").Size;
            JPG_PictureBox.Width = JPG_PictureBox.Width * 60 / 100;
            JPG_PictureBox.Height = JPG_PictureBox.Height * 60 / 100;
            Form1.form1.pictureBoxMap.Width = Form1.form1.pictureBoxMap.Width * 70 / 100;
            Form1.form1.pictureBoxMap.Height = Form1.form1.pictureBoxMap.Height * 50 / 100;
            */


            /*
            var dirList = (from d in new System.IO.DirectoryInfo(@"C:/Users/og826/Desktop/專案用CAD檔").GetDirectories()
                           select d).ToList().OrderByDescending(d => d.LastAccessTime);
            foreach (var d in dirList)
            {
                MessageBox.Show(d.Name + ":" + d.LastAccessTime);
            }
            
            
            DirectoryInfo s = new System.IO.DirectoryInfo(@"C:/Users/og826/Desktop/專案用CAD檔").GetDirectories()
                .OrderByDescending(ccc => ccc.LastAccessTime).Take(1).Single();
            MessageBox.Show(s.Name + " : " + s.LastAccessTime);
            */

            /*
            Console.WriteLine("hello");
            string folderPath = "C:/Users/og826/Desktop/專案用CAD檔";
            int x=0;
            foreach (string file in Directory.EnumerateFiles(folderPath, "*.jpg"))
            {
                x++;
            }

            string[] dataname = new string[x];

            //foreach (string file in Directory.EnumerateFiles(folderPath, "*.jpg"))
            //{
            //    dataname[x] = file;
            //    Console.WriteLine(dataname[x]);
            //}
            */



            /*            OpenFileDialog openFileDialog1 = new OpenFileDialog
                        {
                            InitialDirectory = @"C:\Users\Serverler\Desktop\專案用CAD檔\台科大模擬專案測試用_20211210-Model.jpg",
                            CheckFileExists = true,
                            CheckPathExists = true,

                            DefaultExt = "jpg",
                            Filter = "jpg files (*.jpg)|*.jpg",
                            FilterIndex = 2,
                            RestoreDirectory = true,

                            ReadOnlyChecked = true,
                            ShowReadOnly = true,
                        };

                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            Textbox_JPG.Text = openFileDialog1.FileName;
                            JPG_PictureBox.Image = Image.FromFile(openFileDialog1.FileName);
                        }
            */
        }


        ///////////////////////////////圖片旋轉//////////////////////////////////

        private void button1_Click(object sender, EventArgs e)
        {
            JPG_PictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            JPG_PictureBox.Image = JPG_PictureBox.Image;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            JPG_PictureBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            JPG_PictureBox.Image = JPG_PictureBox.Image;
        }

        //////////////////////////////////滑鼠動作及繪製圖框///////////////////////////////

        private void JPG_PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && blnDraw == false)
            {
                start = e.Location;
            }

        }

        private void JPG_PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (blnDraw == false)
                {
                    if (e.Button != MouseButtons.Left)
                        return;
                    end = e.Location;
                    JPG_PictureBox.Invalidate();
                }
                if (blnDraw == true)
                {
                    if (d == 0)
                    {
                        NowPosition = e.Location;
                        startXsize = NowPosition.X - start.X;
                        startYsize = NowPosition.Y - start.Y;
                        endXsize = end.X - NowPosition.X;
                        endYsize = end.Y - NowPosition.Y;
                        d++;
                    }
                    NowPosition = e.Location;
                    start.X = NowPosition.X - startXsize;
                    start.Y = NowPosition.Y - startYsize;
                    end.X = NowPosition.X + endXsize;
                    end.Y = NowPosition.Y + endYsize;
                    JPG_PictureBox.Invalidate();
                }
            }
        }

        private void JPG_PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (blnDraw == false)
                {
                    PictureBox pic = sender as PictureBox;
                    if (e.Button == MouseButtons.Left)
                    {
                        end = e.Location;
                    }
                    if (pic.Image != null)
                    {
                        if (start != end)
                        {
                            StartAndEndPoint onepoint = new StartAndEndPoint();
                        }
                    } 
                    blnDraw = true;
                }
                else
                {
                    d = 0;
                    blnDraw = true;
                }
                if (c == 1) c = 2;

                Pen_Up_Bar.Enabled = true;
                Pen_Bottom_Bar.Enabled = true;
                Pen_Left_Bar.Enabled = true;
                Pen_Right_Bar.Enabled = true;
                
                pos();
            }
        }


        private void JPG_PictureBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = sender as PictureBox;

            Pen pen = new Pen(Color.Red , 3); //繪製線的顏色、粗細
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;//繪製線的格式

            if (pic.Image != null)
            {
                //此處是為了在繪製時可以由上向下繪製，也可以由下向上繪製
                e.Graphics.DrawRectangle(pen, Math.Min(start.X, end.X), Math.Min(start.Y, end.Y), Math.Abs(start.X - end.X), Math.Abs(start.Y - end.Y));
            }  
            pen.Dispose();
        }




        private void Pen_Delete_button_Click(object sender, EventArgs e)
        {
            if (c == 2) c = 1;
            d = 0;
            Pen_Up_Bar.Enabled = false;
            Pen_Bottom_Bar.Enabled = false;
            Pen_Left_Bar.Enabled = false;
            Pen_Right_Bar.Enabled = false;
            Start_End_Bar_reset();
            JPG_PictureBox.Invalidate();
            pos();
            blnDraw = false;

        }


        private void btn_Leave_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }


        //// picture縮放
        /*
        private void JPG_PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta >= 0)
            {
                JPG_PictureBox.Width = (int)(JPG_PictureBox.Width * 1.1);
                JPG_PictureBox.Height = (int)(JPG_PictureBox.Height * 1.1);
            }
            else
            {
                JPG_PictureBox.Width = (int)(JPG_PictureBox.Width / 1.1);
                JPG_PictureBox.Height = (int)(JPG_PictureBox.Height / 1.1);
            }
        }
        */
        private void JPG_PictureBox_MouseLeave(object sender, EventArgs e)
        {
            JPG_PictureBox.Parent.Focus();
        }

        private void JPG_PictureBox_MouseEnter(object sender, EventArgs e)
        {
            JPG_PictureBox.Focus();
        }

        //////////////////////////////////////////////////////////////////////////////
        //                               選取框操作                                 //
        //                                                                          //
        private void Pen_Top_Up_Btn_Click(object sender, EventArgs e)
        {
            if (c == 2)
            {
                start.Y = start.Y - Pen_Button_adjust;
                Bin();
                pos();
            }
        }

        private void Pen_Top_Down_Btn_Click(object sender, EventArgs e)
        {
            if (c == 2)
            {
                start.Y = start.Y + Pen_Button_adjust;
                Bin();
                pos();
            }
        }

        

        private void Pen_Bottom_Up_btn_Click(object sender, EventArgs e)
        {
            if (c == 2)
            {
                end.Y = end.Y - Pen_Button_adjust;
                Bin();
                pos();
            }
        }

        private void Pen_Bottom_Down_btn_Click(object sender, EventArgs e)
        {
            if (c == 2)
            {
                end.Y = end.Y + Pen_Button_adjust;
                Bin();
                pos();
            }
        }



        private void Pen_Left_Left_btn_Click(object sender, EventArgs e)
        {
            if (c == 2)
            {
                start.X = start.X - Pen_Button_adjust;
                Bin();
                pos();
            }
        }

        private void Pen_Right_Left_btn_Click(object sender, EventArgs e)
        {
            if (c == 2)
            {
                end.X = end.X - Pen_Button_adjust;
                Bin();
                pos();
            }
        }

        private void Pen_Left_Right_btn_Click_1(object sender, EventArgs e)
        {
            if (c == 2)
            {
                start.X = start.X + Pen_Button_adjust;
                Bin();
                pos();
            }
        }

        
        private void Pen_Right_Right_btn_Click(object sender, EventArgs e)
        {
            if (c == 2)
            {
                end.X = end.X + Pen_Button_adjust;
                Bin();
                pos();
            }
        }

        private void Pen_Up_Bar_Scroll(object sender, ScrollEventArgs e)
        {
            if (c == 2) 
            {
                if (Pen_Up_Bar.Value > UpBarValue)
                {
                    start.Y = start.Y - Pen_Bar_adjust;
                    UpBarValue = Pen_Up_Bar.Value;
                }
                if (Pen_Up_Bar.Value < UpBarValue)
                {
                    start.Y = start.Y + Pen_Bar_adjust;
                    UpBarValue = Pen_Up_Bar.Value;
                }
            }
            Bin();
            pos();
        }

        private void Pen_Bottom_Bar_Scroll(object sender, ScrollEventArgs e)
        {
            if (c == 2)
            {
                if (Pen_Bottom_Bar.Value > BottomBarValue)
                {
                    end.Y = end.Y - Pen_Bar_adjust;
                    BottomBarValue = Pen_Bottom_Bar.Value;
                }
                if (Pen_Bottom_Bar.Value < BottomBarValue)
                {
                    end.Y = end.Y + Pen_Bar_adjust;
                    BottomBarValue = Pen_Bottom_Bar.Value;
                }
            }
            Bin();
            pos();
        }

        private void Pen_Left_Bar_Scroll(object sender, ScrollEventArgs e)
        {
            if (c == 2)
            {
                if (Pen_Left_Bar.Value > LeftBarValue)
                {
                    start.X = start.X + Pen_Bar_adjust;
                    LeftBarValue = Pen_Left_Bar.Value;
                }
                if (Pen_Left_Bar.Value < LeftBarValue)
                {
                    start.X = start.X - Pen_Bar_adjust;
                    LeftBarValue = Pen_Left_Bar.Value;
                }
            }
            Bin();
            pos();
        }

        private void Pen_Right_Bar_Scroll(object sender, ScrollEventArgs e)
        {
            if (c == 2)
            {
                if (Pen_Right_Bar.Value > RightBarValue)
                {
                    end.X = end.X + Pen_Bar_adjust;
                    RightBarValue = Pen_Right_Bar.Value;
                }
                if (Pen_Right_Bar.Value < RightBarValue)
                {
                    end.X = end.X - Pen_Bar_adjust;
                    RightBarValue = Pen_Right_Bar.Value;
                }
            }
            Bin();
            pos();
        }


        //                                                                          //
        //                               選取框操作                                 //
        //////////////////////////////////////////////////////////////////////////////



        private void TXT_Output_Click(object sender, EventArgs e)
        {    
            {
                

                // 將字串寫入TXT檔
                StreamWriter str = new StreamWriter(url + "/Parameter.txt");
                string WriteWord1 = Image.FromFile(url + "/model_map.jpg").Width.ToString() + " " + Image.FromFile(url + "/model_map.jpg").Height.ToString();
                string WriteWord2 = finalX.ToString() + " " + finalY.ToString();
                string WriteWord3 = ActSize_X.Text + " " + ActSize_Y.Text;

                str.WriteLine(WriteWord1);
                str.WriteLine(WriteWord2);
                str.WriteLine(WriteWord3);
                str.Close();
                MessageBox.Show("The file is save！");
            }
        }


        void pos()
        {
            finalX = (Math.Abs(Math.Abs(end.X) - Math.Abs(start.X))) * 100 / 60 * 960 / JPG_PictureBox.Width;
            finalY = Math.Abs(Math.Abs(end.Y) - Math.Abs(start.Y)) * 100 / 60 * 720 / JPG_PictureBox.Height;
            Pen_Size_Lable.Text = finalX + "*" + finalY;
        }
        void Bin()
        {
            blnDraw = false;
            Pen_adjust_Bottomsum_before = Pen_adjust_Bottomsum;
            blnDraw = true;
            JPG_PictureBox.Invalidate();
        }
        void Start_End_Bar_reset()
        {
            start.X = 0;
            end.X = 0;
            start.Y = 0;
            end.Y = 0;
            Pen_Up_Bar.Value = 50;
            Pen_Bottom_Bar.Value = 50;
            Pen_Left_Bar.Value = 50;
            Pen_Right_Bar.Value = 50;
        }
    }
}
