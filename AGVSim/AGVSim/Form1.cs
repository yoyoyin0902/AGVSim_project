using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRect = System.Drawing.Rectangle;
using CString = System.String;
using CPoint = System.Drawing.Point;
using CTime = System.DateTime;
using System.Threading;
using System.Data.OleDb;
using DWG_MAPVIEW;
using System.IO;


namespace AGVSim
{


    public partial class Form1 : Form
    {
        bool m_grid, m_station, m_connection, m_path, m_word;
        public int m_icon_sel; // 0: Select 1:Station 2:Connect 3:Traffic 4:Floor 5 :Line 6:90Line 7:Arc
        int grid_x;
        int grid_y;
        CRect WorkArea;
        int VIEW_X = 2300;
        int VIEW_Y = 1200;

        int ArcDrawing;
        int m_map_id;
        CString m_map_name;
        
        //CSetPath* pSetPath;
       // CSetStop* pSetStop;
       // CSetVehicle* pSetAGV;
        bool bSetPath;
        int Cap_Type;
        int Cap_ID;
        bool CapMove;
        CRect CapRect;
        CPoint CapPos;
        List<int> OpenNodeSet = new List<int>();
        List<int> CloseNodeSet = new List<int>() ;
        List<int> A_Star_PathSet = new List<int>();
        bool b_AddVehcile, b_AutoSim, b_AStarWorking;
        long sim_clk;
        double m_AGV_Speed;
        double m_AGV_AngularSpeed;
        int m_sim_mode; // 0: Stop; 1: Start; 2: Pause
        //ofstream outfile;
        int b_A_StartVehicleTrackingID;
        int m_AGVAllowWaitingTime;
        List<int> BlockAGVs = new List<int>();
        bool b_LiDarShow;
        // Modify 2020/02/17 ---------Start-------------------------
        // For Drawing Picture Offesting
        CPoint m_DrawOrg;
        bool b_DisplayPathInfo;
        // Modify 2020/02/17 ---------End  -------------------------

        // Operations
        public List<CPath> Path_List = new List<CPath>();
        public List<CStop> Stop_List = new List<CStop>();        
        public List<CConnection> Connection_List = new List<CConnection>();
        public List<CVehicle> Vehicle_List = new List<CVehicle>();
        CPath tmp_Path = new CPath();
        int ArcPathRadius;
        private static System.Threading.Timer timer;

        public static Form1 form1;
        public Form1()
        {
            InitializeComponent();
            InitParameters();
            onButtonUpdate();
            form1 = this;
            this.pictureBoxMap.MouseWheel += new
                MouseEventHandler(PictureBox_MouseWheel);

           
        }

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        Point Start, End, Position;
        float ProportionSize, sizeActact, proportionX, proportionY;
        int MeasuringAble = 0;
        string url = System.Environment.CurrentDirectory;
        int a = 0;

        public float sizebox = 1;
        int PictureBoxMapHeight, PictureBoxMapWidth;
        int c = 0;
        int i = 0 ,j = 0;
        int cot = 0;
        CPoint record = new CPoint();
        Form5 f5 = new Form5();
        Form6 f6 = new Form6();

        private void pictureBoxMap_MouseEnter(object sender, EventArgs e)
        {            
           //this.pictureBoxMap.Focus();
        }

       private void pictureBoxMap_MouseLeave(object sender, EventArgs e)
       {
           //this.pictureBoxMap.Parent.Focus();
       }

        

        public void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            CPath cp = new CPath();
            cp.ss  = String.Format("{0:D2}-{1:D}", cp.m_ID, cp.OnPathAGVs.Count + "  " + cp.sizeAct.ToString() + "m");

            CPoint point = new CPoint(e.X, e.Y);

            if (c == 0)
            {
                PictureBoxMapHeight = pictureBoxMap.Height;
                PictureBoxMapWidth = pictureBoxMap.Width;
                c = 1;
            }
            


            if (e.Delta >= 1)
            {
                this.pictureBoxMap.Height = PictureBoxMapHeight;
                this.pictureBoxMap.Width = PictureBoxMapWidth;
                sizebox = (float)(sizebox * 1.1);
                this.pictureBoxMap.Height = (int)(pictureBoxMap.Height * sizebox);
                this.pictureBoxMap.Width = (int)(pictureBoxMap.Width * sizebox);
                //Console.WriteLine("放大");
                //Console.WriteLine(sizebox);
                //Console.WriteLine(pictureBoxMap.Height);
                //Console.WriteLine(pictureBoxMap.Width);
            }
            else
            {
                this.pictureBoxMap.Height = PictureBoxMapHeight;
                this.pictureBoxMap.Width = PictureBoxMapWidth;
                sizebox = (float)(sizebox / 1.1);
                this.pictureBoxMap.Height = (int)(pictureBoxMap.Height * sizebox);
                this.pictureBoxMap.Width = (int)(pictureBoxMap.Width * sizebox);
                //Console.WriteLine("縮小");
                //Console.WriteLine(sizebox);
                //Console.WriteLine(pictureBoxMap.Height);
                //Console.WriteLine(pictureBoxMap.Width);
            }



            CStop pStop = new CStop();
            pStop.CSm_cion_sel = m_icon_sel;
            pStop.m_DrawOrg = m_DrawOrg;
            CPoint tmp = new CPoint();
            
            for (int i = 0; i < Stop_List.Count; i++)
            {
                if (j < i)
                {
                    cot = 0;
                    j = i;
                }
                if (cot == 0)
                {
                    record.X = Stop_List[i].tmp.X;
                    record.Y = Stop_List[i].tmp.Y;
                    cot = 1;
                }
                tmp.X = (int)(float)(record.X * sizebox);
                tmp.Y = (int)(float)(record.Y * sizebox);
                Stop_List.RemoveAt(i);
                pStop.AddObj(GetID(10), i, tmp); // 10: Type Stop -> Station/ Connection; 0: Station Stop                                                 
                Stop_List.Add(pStop);
                //Stop_List[0].tmp.X = tmp.X;
                //Stop_List[0].tmp.Y = tmp.Y;
                Console.WriteLine(record);
                Console.WriteLine(Stop_List.Count);
            }
            j = 0;



            /*
            pStop.AddObj(GetID(10), 0, tmp); // 10: Type Stop -> Station/ Connection; 0: Station Stop
            Stop_List.Add(pStop);
            */


        }




        private void pictureBoxMap_MouseClick(object sender, MouseEventArgs e)
       {
            //切割資料
            StreamReader str = new StreamReader(url + "/Parameter.txt");
            string ReadLine1, ReadLine2, ReadLine3;
            ReadLine1 = str.ReadLine();
            ReadLine2 = str.ReadLine();
            ReadLine3 = str.ReadLine();

            string[] originsize = ReadLine1.Split(' ');
            string[] framesize = ReadLine2.Split(' ');
            string[] actsize = ReadLine3.Split(' ');
            //儲存XY
            string frameX = framesize[0];
            string frameY = framesize[1];
            string actX = actsize[0];
            string actY = actsize[1];
            //轉成float
            float FframeX = float.Parse(frameX);
            float FframeY = float.Parse(frameY);
            float FactX = float.Parse(actX);
            float FactY = float.Parse(actY);
            //計算比例
            proportionX = FactX / FframeX;
            proportionY = FactY / FframeY;
            str.Close();
        }


        private void pictureBoxMap_MouseDown(object sender, MouseEventArgs e)
        {/*
            
            if (MeasuringAble % 2 == 0)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Start = e.Location;
                    MeasuringAble++;
                }
            }

            else if (MeasuringAble % 2 != 0) 
            {
                if (e.Button == MouseButtons.Left)
                {
                    PictureBox pic = sender as PictureBox;
                    End = e.Location;
                    MeasuringAble++;
                }

                float sizeA = End.X - Start.X;
                float sizeB = End.Y - Start.Y;
                float sizeC = (float)Math.Pow(((float)Math.Pow(sizeA * proportionX, 2)) + ((float)Math.Pow(sizeB * proportionY, 2)), 0.5) * 2;
                float sizeAct = (float)Math.Round(sizeC, 2, MidpointRounding.AwayFromZero);
                Actral_Size_Lable.Text = sizeAct.ToString();
            }*/


            CPoint point = new CPoint(e.X, e.Y);
            int idx = 0;
            int type = 0;

            if (e.Button == MouseButtons.Left)
            {
                if (m_icon_sel == 0)
                {
                    Console.WriteLine("[pictureBoxMap_MouseDown] 0");
                    // Set LeftBTDown as a Start Node
                    if (bSetPath && Vehicle_List.Count == 1 && !Vehicle_List[0].A_Star_StartNode_Selected)
                    {
                        if (CursorInsideStation(point, ref idx, ref type))
                        {
                            // Captured
                            Cap_ID = idx;
                            Cap_Type = type; // Stop Type
                            if (Cap_Type == 10)
                            {
                                Vehicle_List[0].A_Star_StartNode = Stop_List[Cap_ID].m_ID;
                                Stop_List[Cap_ID].m_FromToStatus = 1; // A Destination
                                Vehicle_List[0].A_Star_StartNode_Selected = true;
                            }
                            pictureBoxMap.Invalidate(WorkArea);
                        }
                    }
                    else if (b_AddVehcile) /* AGV */
                    {
                        // Drag a Vehicle on a Stiop
                        if (CapObjects(point))
                        {
                            if (Cap_Type == 10)
                            {
                                // A Vehicle
                                CVehicle pVeh = new CVehicle();
                                pVeh.m_DrawOrg = m_DrawOrg;
                                CStop tmp = Stop_List[Cap_ID];                                
                                pVeh.AddObj(GetID(30), ref tmp, 0, 0, m_AGV_Speed, m_AGV_AngularSpeed); // 30: Type Vehicle
                                Stop_List[Cap_ID] = tmp;
                                if (!sqlserver.Check_AGV(pVeh.m_ID))
                                    sqlserver.Insert_AGV_State(pVeh);
                                Vehicle_List.Add(pVeh);
                                CapRect = new CRect(0, 0, 0, 0);
                                Cap_ID = 0;
                                Cap_Type = 0;
                            }
                        }
                        b_AddVehcile = false;
                        onButtonUpdate();
                    }
                    else
                    {
                        // Not Setting a Path
                        if (CapObjects(point))
                        {
                            CapMove = true;
                            CapPos = point;
                        }
                        else
                        {
                            CapMove = false;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("[pictureBoxMap_MouseDown] 1");
                    AddObj(point);
                }

            }
            else if (e.Button == MouseButtons.Right)
            {

                if (bSetPath && Vehicle_List.Count == 1 && !Vehicle_List[0].A_Star_EndNode_Selected)
                {
                    if (CursorInsideStation(point, ref idx, ref type))
                    {
                        // Captured
                        Cap_ID = idx;
                        Cap_Type = type; // Stop Type
                        if (Cap_Type == 10)
                        {
                            Vehicle_List[0].A_Star_EndNode = Stop_List[Cap_ID].m_ID;
                            Stop_List[Cap_ID].m_FromToStatus = 2; // A Destination
                            Vehicle_List[0].A_Star_EndNode_Selected = true;
                            bSetPath = false;
                        }
                    }
                }
                pictureBoxMap.Invalidate(WorkArea);

                

                if (m_icon_sel == 0)  //右鍵點位開啟視窗
                {
                    // Check if the cursor inside a Stop 
                    if (CursorInsideStation(point, ref idx, ref type) && type == 10)
                    {
                        CPath pPath = new CPath();
                        int tmp_id = idx;
                        tmp_Path.StationIDs[0] = tmp_id;

                        //f5.Visible = false;
                        f5.StationID.Text = "Station" + (tmp_id + 1).ToString();
                        f5.Visible = true;
                        f6.Visible = false;
                        f5.Focus();
                    }
                    else if (CursorInsideStation(point, ref idx, ref type) && type == 20)  
                    {
                        CPath pPath = new CPath();
                        int tmp_id = idx;
                        tmp_Path.StationIDs[0] = tmp_id;
                        
                        //f6.Visible = false;
                        f6.LineID.Text = "Line" + (tmp_id + 1).ToString();                        
                        f6.Visible = true;
                        f5.Visible = false;
                        f6.Focus();            
                    }
                    else
                    {
                        f5.Visible = false;
                        f6.Visible = false;
                    }
                    /*
                    CPath pPath = new CPath();
                    pPath.cpsizebox = sizebox;
                    pPath.m_DrawOrg = m_DrawOrg;
                    // Add a Path with the info of
                    // Path ID, Two Station IDs and a Conner Point
                    CStop tmp1 = Stop_List[tmp_Path.StationIDs[0]];
                    CStop tmp2 = Stop_List[tmp_Path.StationIDs[1]];
                    CPoint tmp3 = new CPoint();
                    int tmp4 = 0;
                    pPath.AddObj(GetID(20), 0, ref tmp1, ref tmp2, tmp3, tmp4, 0); // 20: Type Path
                    Stop_List[tmp_Path.StationIDs[0]] = tmp1;
                    Stop_List[tmp_Path.StationIDs[1]] = tmp2;
                    Path_List.Add(pPath);
                    // Reset
                    tmp_Path.Reset();
                    ArcDrawing = 0;
                    */
                }
            }
            
        }
        public string StationId;

        private void pictureBoxMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (MeasuringAble % 2 == 0)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (e.Button != MouseButtons.Left)
                        return;
                    End = e.Location;
                    pictureBoxMap.Invalidate();
                }
            }
        }
        

        private void pictureBoxMap_MouseUp(object sender, MouseEventArgs e)
        {/*
            if (MeasuringAble % 2 == 0)
            {
                if (e.Button == MouseButtons.Left)
                {
                    PictureBox pic = sender as PictureBox;
                    End = e.Location;
                }

                float sizeA = End.X - Start.X;
                float sizeB = End.Y - Start.Y;
                float sizeC = (float)Math.Pow(((float)Math.Pow(sizeA * proportionX, 2)) + ((float)Math.Pow(sizeB * proportionY, 2)), 0.5) * 2;
                //float sizeAct = sizeC * ProportionSize *199 / 100;
                float sizeActact = (float)Math.Round(sizeC, 2, MidpointRounding.AwayFromZero);
                Actral_Size_Lable.Text = sizeActact.ToString();
                

                ActSize[a] = sizeActact;
                G.Array = ActSize;
                                                
                if (a>0)
                {
                    Console.WriteLine(ActSize[a-1]);
                    Console.WriteLine(ActSize[a]);
                }

                a++;
            }*/

        }

        
        private void pictureBoxMap_Paint(object sender, PaintEventArgs e)
        {

            DrawGrid(e);
            DrawObjs(e);
            if (MeasuringAble % 2 != 0)
            {
                PictureBox pic = sender as PictureBox;

                Pen pen = new Pen(Color.DarkOrange, 2); //繪製線的顏色、粗細
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;//繪製線的格式

                if (pic.Image != null)
                {
                    //此處是為了在繪製時可以由上向下繪製，也可以由下向上繪製
                    e.Graphics.DrawLine(pen, Start.X, Start.Y, End.X, End.Y);
                }
                pen.Dispose();
            }
        }




        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        private void InitParameters()
        {
            m_icon_sel = 0;
            m_grid = false;
            WorkArea = new Rectangle(0, 0, VIEW_X, VIEW_Y);
            grid_x = 20;
            grid_y = 20;
            m_grid = false;
            ArcDrawing = 0;
            ArcPathRadius = 40;
            // Status Bar
            //pFrame = (CMainFrame*)AfxGetApp()->m_pMainWnd;
            //pStatus = &pFrame->m_wndStatusBar;
            //pStatus->SetPaneText(0, "message line for first pane");
            m_map_id = 100;
            m_map_name = "iVAM Lab";
            //pSetPath = new CSetPath;
            //pSetStop = new CSetStop;
            //pSetAGV = new CSetVehicle;
            Cap_ID = 0;
            Cap_Type = 0;
            CapMove = false;
            CapRect = new CRect(0, 0, 0, 0);
            bSetPath = false;
            b_AddVehcile = false;
            sim_clk = 0;
            sim_clk++;
            //str.Format("Simulation Clock: %05ld [Blocking %d AGVs]", sim_clk, BlockAGVs.GetSize());
            //pStatus->SetPaneText(1, str);
            m_AGV_Speed = 5.0f;
            m_AGV_AngularSpeed = 2.5f;
            m_sim_mode = 0; // 0: Stop; 1: Start; 2: Pause
            b_AutoSim = false;
           // CTime t = CTime::GetCurrentTime();
        /*    CString s;
            s.Format("c:\\Log\\log-%04d-%02d-%02d[%02d-%02d-%02d].txt",
                t.GetYear(), t.GetMonth(), t.GetDay(),
                t.GetHour(), t.GetMinute(), t.GetSecond());
            str.Format("Simulation done at %04d-%02d-%02d %02d:%02d:%02d",
                t.GetYear(), t.GetMonth(), t.GetDay(),
                t.GetHour(), t.GetMinute(), t.GetSecond());
            outfile.open(s);
            outfile << str << ".\n";
            */
        b_AStarWorking = false;
            b_A_StartVehicleTrackingID = 1;
            m_AGVAllowWaitingTime = 300;
            //BlockAGV.RemoveAll();
            b_LiDarShow = false;
            // Modify 2020/02/17 ---------Start-------------------------
            m_DrawOrg = new CPoint(0, 0);
            b_DisplayPathInfo = false;
            // Modify 2020/02/17 ---------End  -------------------------

        }




        private void onButton_Select_Click(object sender, EventArgs e)
        {
            m_icon_sel = 0;
            onButtonUpdate();
        }

        public void onButton_Station_Click(object sender, EventArgs e)
        {
            m_icon_sel = 1;
            onButtonUpdate();
        }

        private void onButton_Connection_Click(object sender, EventArgs e)
        {
            m_icon_sel = 2;
            onButtonUpdate();
        }

        private void onButton_Traffic_Click(object sender, EventArgs e)
        {
            m_icon_sel = 3;
            onButtonUpdate();
        }

        private void onButton_Floor_Click(object sender, EventArgs e)
        {
            m_icon_sel = 4;
            onButtonUpdate();
        }

        private void onButton_Line_Click(object sender, EventArgs e)
        {
            m_icon_sel = 5;
            onButtonUpdate();            
        }

        private void onButton_90Line_Click(object sender, EventArgs e)
        {
            m_icon_sel = 6;
            onButtonUpdate();
        }

        private void onButton_Arc_Click(object sender, EventArgs e)
        {
            m_icon_sel = 7;
            onButtonUpdate();
        }

        private void onButton_Grid_Click(object sender, EventArgs e)
        {
            m_grid = !m_grid;
            pictureBoxMap.Invalidate(WorkArea);
            onButtonUpdate();
        }

        private void onButtonLChange_Click(object sender, EventArgs e)
        {
            if (Cap_ID >= 0 && Cap_Type == 20) // If index >= 0 and a Path Selected
            {
                Path_List[Cap_ID].m_direction++;
                if (Path_List[Cap_ID].m_direction == 3)
                    Path_List[Cap_ID].m_direction = 0;
                Path_List[Cap_ID].Fit();
            }
            pictureBoxMap.Invalidate(WorkArea);
        }

        private void onButton_SetPath_Click(object sender, EventArgs e)
        {
            // Set LeftBTDown as a Start Node
            // Set LeftBTDown as an End Node
            m_icon_sel = 0;
            // Clear Previous Selection
            CapRect = new CRect(0, 0, 0, 0);
            Cap_ID = 0;
            Cap_Type = 0;
            // Set Path
            bSetPath = !bSetPath;
            if (bSetPath && Vehicle_List.Count == 1)
            {
                // Only one AGV
                ClearPathAttribute();
                // Reset Node
                Vehicle_List[0].A_Star_StartNode = 0;
                Vehicle_List[0].A_Star_EndNode = 0;
                Vehicle_List[0].A_Star_StartNode_Selected = false;
                Vehicle_List[0].A_Star_EndNode_Selected = false;
                for (int i = 0; i < Stop_List.Count; i++)
                {
                    // Reset From/ To Status
                    // Only Show the Last One
                    Stop_List[i].m_FromToStatus = 0;
                }
            }
            else
                MessageBox.Show("This manual test function only available for ONE Vehicle case only!!");
            pictureBoxMap.Invalidate(WorkArea);
        }

        private void onButton_NChange_Click(object sender, EventArgs e)
        {

        }

        private void onButtonUpdate()
        {
            MeasuringAble = 0;


            if (m_icon_sel == 0)
                onButton_Select.Checked = true;
            else
                onButton_Select.Checked = false;

            if (m_icon_sel == 1)
                onButton_Station.Checked = true;
            else
                onButton_Station.Checked = false;

            if (m_icon_sel == 2)
                onButton_Connection.Checked = true;
            else
                onButton_Connection.Checked = false;

            if (m_icon_sel == 3)
                onButton_Traffic.Checked = true;
            else
                onButton_Traffic.Checked = false;

            if (m_icon_sel == 4)
                onButton_Floor.Checked = true;
            else
                onButton_Floor.Checked = false;

            if (m_icon_sel == 5)
                onButton_Line.Checked = true;
            else
                onButton_Line.Checked = false;

            if (m_icon_sel == 6)
                onButton_90Line.Checked = true;
            else
                onButton_90Line.Checked = false;

            if (m_icon_sel == 7)
                onButton_Arc.Checked = true;
            else
                onButton_Arc.Checked = false;

            if (b_AddVehcile)
                onButton_Car.Checked = true;
            else
                onButton_Car.Checked = false;


            //-----------------------------------------------
            if (m_grid)
                onButton_Grid.Checked = true;
            else
                onButton_Grid.Checked = false;

            //------------------------------------------------
            if (m_sim_mode == 1)
            {
                onButton_Play.Checked = true;
                onButton_Stop.Checked = false;
            }
            else
            {
                onButton_Play.Checked = false;
                onButton_Stop.Checked = true;
            }

            onButton_AutoPath.Checked = b_AutoSim;
            OnButton_DisplayPathInfo.Checked = b_DisplayPathInfo;
        } 

        //------------------------------------------------------------------------------
        private void onButton_Car_Click(object sender, EventArgs e)
        {
            m_icon_sel = 0;
            b_AddVehcile = true;
            onButtonUpdate();
        }

        private void onButton_Play_Click(object sender, EventArgs e)
        {
            m_sim_mode = 1;
            sim_clk = 0;

            TimerCallback callback = new TimerCallback(onTimer);
            //1.function 2.開關  3.等多久再開始  4.隔多久反覆執行
            timer = new System.Threading.Timer(callback, null, 0, 10);
            onButtonUpdate();
        }

        private void onButton_Pause_Click(object sender, EventArgs e)
        {

        }

        private void onButton_AutoPath_Click(object sender, EventArgs e)
        {
            b_AutoSim = !b_AutoSim;
            onButtonUpdate();
        }

        

        private void onButton_AStart_Click(object sender, EventArgs e)
        {
            if (Vehicle_List.Count == 1)
            {
                CVehicle tmp = Vehicle_List[0];
                ExecuteA_Star(ref tmp);

                Vehicle_List[0] = tmp;
                pictureBoxMap.Invalidate(WorkArea);
            }
            else
                MessageBox.Show("This manual test function is only available for ONE Vehicle case only!!");
        }

        private void onButton_FitGrid_Click(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < Stop_List.Count; i++)
            {
                Stop_List[i].Fit(grid_x, grid_y);
            }
            for (i = 0; i < Path_List.Count; i++)
            {
                Path_List[i].Fit();
            }
            pictureBoxMap.Invalidate(WorkArea);
        }

        private void onButton_Stop_Click(object sender, EventArgs e)
        {
            m_sim_mode = 0;
            sim_clk = 0;
            timer.Change(-1, 0);
            pictureBoxMap.Invalidate(WorkArea);
            onButtonUpdate();
        }

        private void onButton_Save_Click(object sender, EventArgs e)
        {
            if (Stop_List.Count == 0) {
                DialogResult Result = MessageBox.Show("There are no object in the map.\r\n\r\nDo you want to clear all data of your SQL?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Result == DialogResult.Yes)
                {
                    sqlserver.Delete_Stop();
                    sqlserver.Delete_Path();
                }
                
            }else
            {
                DialogResult Result = MessageBox.Show("Do you want to save all data into your SQL?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (Result == DialogResult.Yes)
                {
                    sqlserver.Delete_Stop();
                    sqlserver.Add_Stop(ref Stop_List);
                    sqlserver.Delete_Path();
                    sqlserver.Add_Path(ref Path_List, ref Stop_List);
                }
            }
            pictureBoxMap.Invalidate();
        }

        private void onButton_Load_Click(object sender, EventArgs e)
        {
            sqlserver.Read_Stop(ref Stop_List);
            sqlserver.Read_Path(ref Path_List, ref Stop_List);
            pictureBoxMap.Invalidate();
        }

        //----------------------------------------------------------------------------------

        private void onTimer(object state)
        {
            AutoStep();
            pictureBoxMap.Invalidate(WorkArea);
        }

        void AutoStep()
        {
            SimStep();
           CString str;
            CString msg = "";
            //   str.Format("Simulation Clock: %05ld [Blocking %d AGVs]", sim_clk, BlockAGVs.Count);
            //  pStatus.SetPaneText(1, str);
            if (b_AutoSim)
            {
                CString tmps;
                for (int i = 0; i < Vehicle_List.Count; i++)
                {
                    if (Vehicle_List[i].A_Star_StartNode > 0 && Vehicle_List[i].A_Star_EndNode > 0)
                    {
                 /*       tmps.Format("[%2d(%d/%d):S%d/E%d],",
                            Vehicle_List[i].m_ID, Vehicle_List[i].m_status, Vehicle_List[i].m_TotalOrders,
                            Vehicle_List[i].A_Star_StartNode, Vehicle_List[i].A_Star_EndNode);
                        msg += tmps;*/
                    }
                }
                //str.Format("All Missions: %s", msg);
            }
            else
            {
                CString tmps;
                if (Vehicle_List.Count > 0)
                {
                    int i = 0;
                    if (Vehicle_List[i].A_Star_StartNode > 0 && Vehicle_List[i].A_Star_EndNode > 0)
                    {
                   /*     tmps.Format("[%2d(%d/%d):S%d/E%d],",
                            Vehicle_List[i]->m_ID, Vehicle_List[i]->m_status, Vehicle_List[i]->m_TotalOrders,
                            Vehicle_List[i]->A_Star_StartNode, Vehicle_List[i]->A_Star_EndNode);
                        msg += tmps;*/
                    }
                }
               // str.Format("All Missions: %s", msg);
            }
         //   pStatus->SetPaneText(0, str);
            
        }

        void SimStep()
        {
            // Get Active Vehicle Status
            // int m_Type; // 0: Forward Move; 1: Dual Direction Move
            // int m_status; // 0: No Job; 1: Mission to Start; 2: Mission Loading; 
            // 3: Mission to End; 4: Mission Unloading; 5: Mission to Home; 6: Failure 
            // BOOL b_MovingDir; // 0: The same with Original Set; // 1: Opposite to Original Set
            // 1. Check status (mission)
            // Need A* information
            // CUrrent Version: Only Get Mission From A Stop
            // Future Version: Can Start on any Path (But Need Go Home Status or Cancelling Job 
            CRect tmp_rect = new CRect();
            int i = 0;
            for ( i = 0; i < Vehicle_List.Count; i++)
            {
                switch (Vehicle_List[i].m_status)
                {
                    case 0: // No Job at Home
                        {
                            if (b_AutoSim)
                            {
                                // 
                                //if (b_AutoSim && !b_AStarWorking) 
                                if (b_AutoSim)
                                {
                                    CVehicle tmp = Vehicle_List[i];
                                    if (GenerateAnAGVOrder(ref tmp))
                                    {
                                        // Not Finished Yet!!!!!!!!!!!!!!!!!!!!!!!
                                        tmp.m_status = 1;
                                        tmp.b_RotateDone = false;
                                        Vehicle_List[i] = tmp;
                                    }
                                    Vehicle_List[i] = tmp;
                                }
                            }
                            break;
                        }
                    case 1: // Mission to Start
                        {
                            // Moving on the Path List: PathToFromSet
                            // Get Direction
                            // Check Type
                            // Update the coming path as current path
                            if (!Vehicle_List[i].b_PathOpen)
                            {
                                // Just Endtered a New Path
                                // Deal with Dual-Way Path
                                if (Vehicle_List[i].m_UpdateDuaWayPath == false)
                                {
                                    CVehicle tmp = Vehicle_List[i];
                                    A_StarDualWayStartStopAllocate(ref tmp, 1);
                                    Vehicle_List[i] = tmp;
                                    Vehicle_List[i].m_UpdateDuaWayPath = true;
                                    // Copy Path Memory Array
                                    tmp = Vehicle_List[i];
                                    ClearToFromPaths(ref tmp, 1);
                                    AddToFromPaths(ref tmp, 1);
                                    Vehicle_List[i] = tmp;
                                }
                                if (Vehicle_List[i].ToFromPathSet.Count > 0)
                                {
                                    Vehicle_List[i].pCurStop = null;
                                    Vehicle_List[i].pCurPath = Vehicle_List[i].ToFromPathSet[0];
                                    Vehicle_List[i].b_PathOpen = true;
                                    Vehicle_List[i].m_moving_ang = Vehicle_List[i].pCurPath.ArcStartAngle;
                                    Vehicle_List[i].b_RotateDone = false;
                                    Vehicle_List[i].m_pos_X = Vehicle_List[i].pCurPath.pStopStart.m_center.X;
                                    Vehicle_List[i].m_pos_Y = Vehicle_List[i].pCurPath.pStopStart.m_center.Y;
                                    Vehicle_List[i].b_RealeaseStop = false;
                                    // Movement of AGV
                                    // CallMoveAGV(.....)
                                    if (Vehicle_List[i].Rotate() == false)
                                    {
                                        CVehicle tmp = Vehicle_List[i];
                                        if (DetectObstacles(ref tmp) == 0)
                                        {
                                            Vehicle_List[i] = tmp;
                                            Vehicle_List[i].Move();
                                        }
                                            
                                    }
                                }
                                else
                                {
                                    // Jump to next Loading Status
                                    Vehicle_List[i].m_status = 2;
                                    Vehicle_List[i].m_UpdateDuaWayPath = false;
                                }
                            }
                            else if (Vehicle_List[i].pCurPath != null)
                            {
                                // On a existed Path Moving
                                if (Vehicle_List[i].Rotate() == false)
                                {
                                    if (Vehicle_List[i].m_cur_length <= Vehicle_List[i].pCurPath.Length)
                                    {
                                        // Not Reaching
                                        // Search TrafficControl Point
                                        if ((Vehicle_List[i].pCurPath.Length - Vehicle_List[i].m_cur_length) <
                                            Vehicle_List[i].pCurPath.m_CheckTrafficCntlDist)
                                        {
                                            // Entering a Ckeck Traffic Zone
                                            // Check the Vacncy of the Next Node
                                            // Vehicle_List[i]->pCurPath->pStopEnd->m_ID
                                            int tx = GetStopIndex(Vehicle_List[i].pCurPath.pStopEnd.m_ID);
                                            if (Stop_List[tx].m_AGV_ID > 0)
                                            {
                                                if (Stop_List[tx].m_AGV_ID == Vehicle_List[i].m_ID)
                                                {
                                                    CVehicle tmp = Vehicle_List[i];
                                                    if (DetectObstacles(ref tmp) == 0)
                                                    {
                                                        Vehicle_List[i] = tmp;
                                                        Vehicle_List[i].Move();
                                                    }
                                                        
                                                }
                                                else
                                                {
                                                    // No Vancy on the Next Stop, Waiting
                                                }
                                            }
                                            else
                                            {
                                                // Previously Available
                                                // Order the Stop
                                                if (!Vehicle_List[i].b_Avoidance)
                                                {
                                                    Stop_List[tx].m_AGV_ID = Vehicle_List[i].m_ID;
                                                    //SayFile("********* AGV ID, Stop ID, Ordered AGV ID", 
                                                    //	Vehicle_List[i]->m_ID, Vehicle_List[i]->pCurPath->pStopEnd->m_ID,
                                                    //	Stop_List[tx]->m_AGV_ID);
                                                }
                                                CVehicle tmp = Vehicle_List[i];
                                                if (DetectObstacles(ref tmp) == 0)
                                                {
                                                    Vehicle_List[i] = tmp;
                                                    Vehicle_List[i].Move();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Outside the checking zone
                                            CVehicle tmp = Vehicle_List[i];
                                            if (DetectObstacles(ref tmp) == 0)
                                            {
                                                Vehicle_List[i] = tmp;
                                                Vehicle_List[i].Move();
                                            }
                                            if (!Vehicle_List[i].b_RealeaseStop)
                                            {
                                                // Release the Stop as Available
                                                // First Translate Movement
                                                Vehicle_List[i].b_RealeaseStop = true;
                                                // No AGV becauee of leaving stop
                                                // AGV entering a Path
                                                Stop_List[GetStopIndex(Vehicle_List[i].pCurPath.pStopStart.m_ID)].m_AGV_ID = 0;
                                                Path_List[GetPathIndex(Vehicle_List[i].pCurPath.m_ID)].OnPathAGVs.Add(Vehicle_List[i].m_ID);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // AGV Leave Current Path
                                        int tmp = Vehicle_List[i].pCurPath.m_ID;
                                        Path_List[GetPathIndex(tmp)].RemoveRuningAGV(Vehicle_List[i].m_ID);
                                        // Reached a Intermediate Stop
                                        Vehicle_List[i].b_RealeaseStop = false;
                                        Vehicle_List[i].m_cur_length = 0;
                                        Vehicle_List[i].PathToFromSet.RemoveAt(0);
                                       // delete Vehicle_List[i]->ToFromPathSet[0];
                                        Vehicle_List[i].ToFromPathSet.RemoveAt(0);
                                        Vehicle_List[i].b_PathOpen = false;
                                    }
                                }
                            }
                            break;
                        }
                    case 2: // Mission Loading
                        {
                            if (!Vehicle_List[i].b_StopOpen)
                            {
                                int idx = CapStop(Vehicle_List[i].m_Pos);
                                if (idx >= 0)
                                {
                                    Vehicle_List[i].pCurStop = Stop_List[idx];
                                    Vehicle_List[i].pCurPath = null;
                                    Vehicle_List[i].b_StopOpen = true;
                                    Vehicle_List[i].m_pos_X = Stop_List[idx].m_center.X;
                                    Vehicle_List[i].m_pos_Y = Stop_List[idx].m_center.Y;
                                    //Stop_List[idx]->m_AGV_ID = Vehicle_List[i]->m_ID;
                                    // Set Loading Time
                                    Vehicle_List[i].pCurStop.m_RemainLUTime = Vehicle_List[i].pCurStop.m_LUTime;
                                    // Do Loading
                                    Vehicle_List[i].LoadUnloading();
                                }
                                else
                                {
                                    //KillTimer(1);
                                    timer.Change(-1,0);
                                    MessageBox.Show("Error 1: Unresolvable Stop Index!");
                                }
                            }
                            else
                            {
                                if (Vehicle_List[i].LoadUnloading() == true)
                                {
                                    // Stilling Loading
                                }
                                else
                                {
                                    // Finish Loading
                                    Vehicle_List[i].m_status = 3;
                                    Vehicle_List[i].b_StopOpen = false;
                                    //int tr = GetStopIndex(Vehicle_List[i]->pCurStop->m_ID);
                                    //Stop_List[tr]->m_AGV_ID = 0;
                                }
                            }
                            break;
                        }
                    case 3: // Mission to End
                        {
                            if (!Vehicle_List[i].b_PathOpen)
                            {
                                // Just Endtered a New Path
                                // Deal with Dual-Way Path
                                if (Vehicle_List[i].m_UpdateDuaWayPath == false)
                                {
                                    CVehicle tmp = Vehicle_List[i];
                                    A_StarDualWayStartStopAllocate(ref tmp, 2);
                                    Vehicle_List[i] = tmp;
                                    Vehicle_List[i].m_UpdateDuaWayPath = true;
                                    // Copy Path Memory Array
                                    tmp = Vehicle_List[i];
                                    ClearToFromPaths(ref tmp, 2);
                                    AddToFromPaths(ref tmp, 2);
                                    Vehicle_List[i] = tmp;
                                }
                                if (Vehicle_List[i].ToEndPathSet.Count > 0)
                                {
                                    Vehicle_List[i].pCurStop = null;
                                    Vehicle_List[i].pCurPath = Vehicle_List[i].ToEndPathSet[0];
                                    Vehicle_List[i].b_PathOpen = true;
                                    Vehicle_List[i].m_moving_ang = Vehicle_List[i].pCurPath.ArcStartAngle;
                                    Vehicle_List[i].b_RotateDone = false;
                                    Vehicle_List[i].m_pos_X = Vehicle_List[i].pCurPath.pStopStart.m_center.X;
                                    Vehicle_List[i].m_pos_Y = Vehicle_List[i].pCurPath.pStopStart.m_center.Y;
                                    Vehicle_List[i].b_RealeaseStop = false;
                                    // Movement of AGV
                                    // CallMoveAGV(.....)
                                    if (Vehicle_List[i].Rotate() == false)
                                    {
                                        CVehicle tmp = Vehicle_List[i];
                                        if (DetectObstacles(ref tmp) == 0)
                                        {
                                            Vehicle_List[i] = tmp;
                                            Vehicle_List[i].Move();
                                        }
                                    }
                                }
                                else
                                {
                                    // Jump to next Loading Status
                                    Vehicle_List[i].m_status = 4;
                                    Vehicle_List[i].m_UpdateDuaWayPath = false;
                                }
                            }
                            else if (Vehicle_List[i].pCurPath != null)
                            {
                                // On a existed Path Moving
                                if (Vehicle_List[i].Rotate() == false)
                                {
                                    if (Vehicle_List[i].m_cur_length <= Vehicle_List[i].pCurPath.Length)
                                    {
                                        // Not Reaching
                                        // Search TrafficControl Point
                                        if ((Vehicle_List[i].pCurPath.Length - Vehicle_List[i].m_cur_length) <
                                            Vehicle_List[i].pCurPath.m_CheckTrafficCntlDist)
                                        {
                                            // Entering a Ckeck Traffic Zone
                                            // Check the Vacncy of the Next Node
                                            // Vehicle_List[i]->pCurPath->pStopEnd->m_ID
                                            int tx = GetStopIndex(Vehicle_List[i].pCurPath.pStopEnd.m_ID);
                                            if (Stop_List[tx].m_AGV_ID > 0)
                                            {
                                                if (Stop_List[tx].m_AGV_ID == Vehicle_List[i].m_ID)
                                                {
                                                    CVehicle tmp = Vehicle_List[i];
                                                    if (DetectObstacles(ref tmp) == 0)
                                                        Vehicle_List[i] = tmp;
                                                    Vehicle_List[i].Move();
                                                }
                                                else
                                                {
                                                    // No Vancy on the Next Stop, Waiting
                                                }
                                            }
                                            else
                                            {
                                                // Previously Available
                                                // Order the Stop
                                                if (!Vehicle_List[i].b_Avoidance)
                                                {
                                                    Stop_List[tx].m_AGV_ID = Vehicle_List[i].m_ID;
                                                    //SayFile("%%%%%%%%%%% AGV ID, Stop ID, Ordered AGV ID", 
                                                    //	Vehicle_List[i]->m_ID, Vehicle_List[i]->pCurPath->pStopEnd->m_ID,
                                                    //	Stop_List[tx]->m_AGV_ID);
                                                }
                                                CVehicle tmp = Vehicle_List[i];
                                                if (DetectObstacles(ref tmp) == 0)
                                                {
                                                    Vehicle_List[i] = tmp;
                                                    Vehicle_List[i].Move();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Outside the checking zone
                                            CVehicle tmp = Vehicle_List[i];
                                            if (DetectObstacles(ref tmp) == 0)
                                            {
                                               Vehicle_List[i] = tmp;
                                               Vehicle_List[i].Move();
                                            }

                                            if (!Vehicle_List[i].b_RealeaseStop)
                                            {
                                                // Release the Stop as Available
                                                // First Translate Movement
                                                Vehicle_List[i].b_RealeaseStop = true;
                                                // No AGV becauee of leaving
                                                // AGV entering a Path
                                                Stop_List[GetStopIndex(Vehicle_List[i].pCurPath.pStopStart.m_ID)].m_AGV_ID = 0;
                                                Path_List[GetPathIndex(Vehicle_List[i].pCurPath.m_ID)].OnPathAGVs.Add(Vehicle_List[i].m_ID);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // AGV Leave Current Path
                                        Path_List[GetPathIndex(Vehicle_List[i].pCurPath.m_ID)].RemoveRuningAGV(Vehicle_List[i].m_ID);
                                        // Reached a Intermediate Stop
                                        Vehicle_List[i].b_RealeaseStop = false;
                                        Vehicle_List[i].m_cur_length = 0;
                                        Vehicle_List[i].PathToEndSet.RemoveAt(0);
                                        //delete Vehicle_List[i]->ToEndPathSet[0];
                                        Vehicle_List[i].ToEndPathSet.RemoveAt(0);
                                        Vehicle_List[i].b_PathOpen = false;
                                    }
                                }
                            }
                            break;
                        }
                    case 4: // Mission Unloading
                        {
                            if (!Vehicle_List[i].b_StopOpen)
                            {
                                int idx = CapStop(Vehicle_List[i].m_Pos);
                                if (idx >= 0)
                                {
                                    Vehicle_List[i].pCurStop = Stop_List[idx];
                                    Vehicle_List[i].pCurPath = null;
                                    Vehicle_List[i].b_StopOpen = true;
                                    Vehicle_List[i].m_pos_X = Stop_List[idx].m_center.X;
                                    Vehicle_List[i].m_pos_Y = Stop_List[idx].m_center.Y;
                                    //Stop_List[idx]->m_AGV_ID = Vehicle_List[i]->m_ID;
                                    // Set Unloading Time
                                    Vehicle_List[i].pCurStop.m_RemainLUTime = Vehicle_List[i].pCurStop.m_LUTime;
                                    // Do Unloading
                                    Vehicle_List[i].LoadUnloading();
                                }
                                else
                                {
                                    timer.Change(-1, 0);
                                    MessageBox.Show("Error 2: Unresolvable Stop Index!");
                                }
                            }
                            else
                            {
                                if (Vehicle_List[i].LoadUnloading() == true)
                                {
                                    // Stilling UnLoading
                                }
                                else
                                {
                                    // Finish UnLoading
                                    Vehicle_List[i].b_StopOpen = false;
                                    //int tr = GetStopIndex(Vehicle_List[i]->pCurStop->m_ID);
                                    //Stop_List[tr]->m_AGV_ID = 0;
                                    // If b_AutoSim Mode
                                    // if (b_AutoSim && !b_AStarWorking) 
                                    if (b_AutoSim)
                                    {
                                        // Do Unloading
                                        // ......................
                                        Vehicle_List[i].m_status = 0;
                                        // Clear From/ To Nodes
                                        // Vehicle_List[i]->A_Star_StartNode
                                        if (Vehicle_List[i].m_ID == b_A_StartVehicleTrackingID)
                                        {
                                            int indx1 = GetStopIndex(Vehicle_List[i].A_Star_StartNode);
                                            int indx2 = GetStopIndex(Vehicle_List[i].A_Star_EndNode);
                                            if (indx1 >= 0 && indx2 >= 0)
                                            {
                                                // Claer Displaying From To Node at Run Time
                                                Stop_List[indx1].m_FromToShow = 0;
                                                Stop_List[indx2].m_FromToShow = 0;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    case 5: // Mission to Home
                        {
                            break;
                        }
                    case 6: // Failure
                        {
                            break;
                        }
                    default:
                        break;
                }
                // 2. Chcek current position 
                if (!b_LiDarShow)
                {
                    tmp_rect = Vehicle_List[i].m_rect;
                    tmp_rect.X = Vehicle_List[i].m_Pos.X + Vehicle_List[i].m_rect.X;
                    tmp_rect.Y = Vehicle_List[i].m_Pos.Y + Vehicle_List[i].m_rect.Y;

                    tmp_rect.X = tmp_rect.X + m_DrawOrg.X;
                    tmp_rect.Y = tmp_rect.Y + m_DrawOrg.Y;
                    pictureBoxMap.Invalidate(tmp_rect);

                }
                if (Vehicle_List[i].b_SetResetDraw)
                {
                    tmp_rect = Vehicle_List[i].m_rect;
                    tmp_rect.X = Vehicle_List[i].m_PrePos.X + Vehicle_List[i].m_rect.X;
                    tmp_rect.Y = Vehicle_List[i].m_PrePos.Y + Vehicle_List[i].m_rect.Y;

                    tmp_rect.X = tmp_rect.X + m_DrawOrg.X;
                    tmp_rect.Y = tmp_rect.Y + m_DrawOrg.Y;
                    pictureBoxMap.Invalidate(tmp_rect);

                    Vehicle_List[i].b_SetResetDraw = false;
                }
            } // loop i
              // Update Timer
            if (b_LiDarShow)
            {
                pictureBoxMap.Invalidate(WorkArea);
            }

            for (i = 0; i < Stop_List.Count; i++)
            {

                tmp_rect = Stop_List[i].m_TagRect;
                tmp_rect.X = Stop_List[i].m_TagRect.X + m_DrawOrg.X;
                tmp_rect.Y = Stop_List[i].m_TagRect.Y + m_DrawOrg.Y;
                pictureBoxMap.Invalidate(tmp_rect);
            }
            for (i = 0; i < Path_List.Count; i++)
            {

                tmp_rect = Path_List[i].m_TagRect;
                tmp_rect.X = Path_List[i].m_TagRect.X + m_DrawOrg.X;
                tmp_rect.Y = Path_List[i].m_TagRect.Y + m_DrawOrg.Y;
                pictureBoxMap.Invalidate(tmp_rect);
            }
            sim_clk++;
            CheckAGVDeadlock();
        }

        bool GenerateAnAGVOrder(ref CVehicle pAGV)
        {
            Random r = new Random();
            int tmp_s = 0;
            int tmp_e = 0;
            while (tmp_s == tmp_e)
            {
                // Randomly generate a from to path

               // srand(pAGV.m_ID + time(null));
                tmp_s = r.Next() % Stop_List.Count + 1;
                //srand(pAGV->m_ID + sim_clk);
                tmp_e = r.Next() % Stop_List.Count + 1;
            }
            // Do GetAPath
            if (b_A_StartVehicleTrackingID == pAGV.m_ID)
                ClearPathAttribute();
            // Reset Node
            pAGV.A_Star_StartNode = 0;
            pAGV.A_Star_EndNode = 0;
            pAGV.A_Star_StartNode_Selected = false;
            pAGV.A_Star_EndNode_Selected = false;
            for (int i = 0; i < Stop_List.Count; i++)
            {
                // Reset From/ To Status
                // Only Show the Last One
                Stop_List[i].m_FromToStatus = 0;
                Stop_List[i].m_Failure = false;
            }
            int idx1 = GetStopIndex(tmp_s);
            int idx2 = GetStopIndex(tmp_e);
            if (idx1 >= 0 && idx2 >= 0)
            {
             //   SayFile("Random Generated Order [AGV ID, start, end]", pAGV->m_ID, tmp_s, tmp_e);
                if (pAGV.m_ID == b_A_StartVehicleTrackingID)
                {
                    // Display From To Node at Run Time
                    Stop_List[idx1].m_FromToShow = 1;
                    Stop_List[idx2].m_FromToShow = 2;
                }
                pAGV.A_Star_StartNode = tmp_s;
                pAGV.A_Star_StartNode_Selected = true;
                Stop_List[idx1].m_FromToStatus = 1; // A Destination
                pAGV.A_Star_EndNode = tmp_e;
                Stop_List[idx2].m_FromToStatus = 2; // A Destination
                pAGV.A_Star_EndNode_Selected = true;
                bSetPath = false;
                if (ExecuteA_Star(ref pAGV))
                {
                    pictureBoxMap.Invalidate(WorkArea);
                    return true;
                }
                else
                {
                    if (pAGV.m_ID == b_A_StartVehicleTrackingID)
                    {
                        // Display From To Node at Run Time
                        Stop_List[idx1].m_FromToShow = 0;
                        Stop_List[idx2].m_FromToShow = 0;
                    }
                    return false;
                }
            }
            return false;
        }

        void A_StarDualWayStartStopAllocate(ref CVehicle pVeh, int type)
        {
            if (type == 1)
            {
                // To From
                for (int i = 0; i < pVeh.PathToFromSet.Count; i++)
                {
                    int id = GetPathIndex(pVeh.PathToFromSet[i]);
                    if (Path_List[id].m_direction == 2)
                    {
                        // A Dual Way Path
                        if (i == 0)
                        {
                            // The First Path Segment
                            // Current Stop is the Start Stop
                            // pVeh->pCurStop
                            if (Path_List[id].pOrgStopStart.m_ID == pVeh.pCurStop.m_ID)
                            {
                                // Forward
                                Path_List[id].pStopStart = Path_List[id].pOrgStopStart;
                                Path_List[id].pStopEnd = Path_List[id].pOrgStopEnd;
                            }
                            else
                            {
                                // Reverse
                                Path_List[id].pStopStart = Path_List[id].pOrgStopEnd;
                                Path_List[id].pStopEnd = Path_List[id].pOrgStopStart;
                            }

                        }
                        else
                        {
                            // Find Previous Path End Point
                            if (Path_List[GetPathIndex(pVeh.PathToFromSet[i - 1])].pStopEnd.m_ID ==
                                Path_List[id].pOrgStopStart.m_ID)
                            {
                                // Forward
                                Path_List[id].pStopStart = Path_List[id].pOrgStopStart;
                                Path_List[id].pStopEnd = Path_List[id].pOrgStopEnd;
                            }
                            else
                            {
                                // Reverse
                                Path_List[id].pStopStart = Path_List[id].pOrgStopEnd;
                                Path_List[id].pStopEnd = Path_List[id].pOrgStopStart;
                            }
                        }
                        // Say("To Start", Path_List[id]->pStopStart->m_ID, Path_List[id]->pStopEnd->m_ID, 99);
                        Path_List[id].Fit(); // Unit Vector and Angle can be Updated
                    }
                }
            }
            else if (type == 2)
            {
                // To End
                for (int i = 0; i < pVeh.PathToEndSet.Count; i++)
                {
                    int id = GetPathIndex(pVeh.PathToEndSet[i]);
                    if (Path_List[id].m_direction == 2)
                    {
                        // A Dual Way Path
                        if (i == 0)
                        {
                            // The First Path Segment
                            // Current Stop is the Start Stop
                            // pVeh->pCurStop
                            if (Path_List[id].pOrgStopStart.m_ID == pVeh.pCurStop.m_ID)
                            {
                                // Forward
                                Path_List[id].pStopStart = Path_List[id].pOrgStopStart;
                                Path_List[id].pStopEnd = Path_List[id].pOrgStopEnd;
                            }
                            else
                            {
                                // Reverse
                                Path_List[id].pStopStart = Path_List[id].pOrgStopEnd;
                                Path_List[id].pStopEnd = Path_List[id].pOrgStopStart;
                            }

                        }
                        else
                        {
                            // Find Previous Path End Point
                            if (Path_List[GetPathIndex(pVeh.PathToEndSet[i - 1])].pStopEnd.m_ID ==
                                Path_List[id].pOrgStopStart.m_ID)
                            {
                                // Forward
                                Path_List[id].pStopStart = Path_List[id].pOrgStopStart;
                                Path_List[id].pStopEnd = Path_List[id].pOrgStopEnd;
                            }
                            else
                            {
                                // Reverse
                                Path_List[id].pStopStart = Path_List[id].pOrgStopEnd;
                                Path_List[id].pStopEnd = Path_List[id].pOrgStopStart;
                            }
                        }
                        // Say("To End", Path_List[id]->pStopStart->m_ID, Path_List[id]->pStopEnd->m_ID, 99);
                        Path_List[id].Fit(); // Unit Vector and Angle can be Updated
                    }
                }
            } // else if (type == 2)
        }

        void ClearToFromPaths(ref CVehicle pVeh, int type)
        {
            if (type == 1)
            {
                for (int i = 0; i < pVeh.ToFromPathSet.Count; i++)
                {

                    // delete pVeh.ToFromPathSet[i];
                    pVeh.ToFromPathSet.Clear();
                } // loop i 
            }
            else
            {
                for (int i = 0; i < pVeh.ToEndPathSet.Count; i++)
                {
                    //delete pVeh.ToEndPathSet[i];
                    pVeh.ToEndPathSet.Clear();
                } // loop i 
            }
        }

        void AddToFromPaths(ref CVehicle pVeh, int type)
        {
            if (type == 1)
            {
                for (int i = 0; i < pVeh.PathToFromSet.Count; i++)
                {
                    int id = GetPathIndex(pVeh.PathToFromSet[i]);
                    if (id >= 0)
                    {
                        CPath mp_path = new CPath();
                        mp_path.m_DrawOrg = m_DrawOrg;
                        mp_path.m_ID = Path_List[id].m_ID;
                        mp_path.StationIDs[0] = Path_List[id].StationIDs[0];
                        mp_path.StationIDs[1] = Path_List[id].StationIDs[1];
                        mp_path.m_type = Path_List[id].m_type;
                        mp_path.Length = Path_List[id].Length;
                        mp_path.m_angle = Path_List[id].m_angle;
                        mp_path.LengthSegment[0] = Path_List[id].LengthSegment[0];
                        mp_path.LengthSegment[1] = Path_List[id].LengthSegment[1];
                        mp_path.AngleSegment[0] = Path_List[id].AngleSegment[0];
                        mp_path.AngleSegment[1] = Path_List[id].AngleSegment[1];
                        mp_path.CornerPoint = Path_List[id].CornerPoint;
                        mp_path.CircleCenter = Path_List[id].CircleCenter;
                        mp_path.CornerNeighbors[0] = Path_List[id].CornerNeighbors[0];
                        mp_path.CornerNeighbors[1] = Path_List[id].CornerNeighbors[1];
                        mp_path.TerminatePoints[0] = Path_List[id].TerminatePoints[0];
                        mp_path.TerminatePoints[1] = Path_List[id].TerminatePoints[1];
                        mp_path.pStopStart = Path_List[id].pStopStart;
                        mp_path.pStopEnd = Path_List[id].pStopEnd;
                        mp_path.pOrgStopStart = Path_List[id].pOrgStopStart;
                        mp_path.pOrgStopEnd = Path_List[id].pOrgStopEnd;
                        mp_path.m_ArcRadius = Path_List[id].m_ArcRadius;
                        mp_path.ArcStartAngle = Path_List[id].ArcStartAngle;
                        mp_path.ArcRangeAngle = Path_List[id].ArcRangeAngle;
                        mp_path.m_uVectorXs = Path_List[id].m_uVectorXs;
                        mp_path.m_uVectorYs = Path_List[id].m_uVectorYs;
                        mp_path.m_uVectorXe = Path_List[id].m_uVectorXe;
                        mp_path.m_uVectorYe = Path_List[id].m_uVectorYe;
                        mp_path.m_CheckTrafficCntlDist = Path_List[id].m_CheckTrafficCntlDist;
                        mp_path.m_CheckTrafficPt = Path_List[id].m_CheckTrafficPt;
                        mp_path.m_RuningDirection = Path_List[id].m_RuningDirection;
                        pVeh.ToFromPathSet.Add(mp_path);
                    } // if (id >= 0)
                } // loop i
            }
            else if (type == 2)
            {
                for (int i = 0; i < pVeh.PathToEndSet.Count; i++)
                {
                    int id = GetPathIndex(pVeh.PathToEndSet[i]);
                    if (id >= 0)
                    {
                        CPath mp_path = new CPath();
                        mp_path.m_DrawOrg = m_DrawOrg;
                        mp_path.m_ID = Path_List[id].m_ID;
                        mp_path.StationIDs[0] = Path_List[id].StationIDs[0];
                        mp_path.StationIDs[1] = Path_List[id].StationIDs[1];
                        mp_path.m_type = Path_List[id].m_type;
                        mp_path.Length = Path_List[id].Length;
                        mp_path.m_angle = Path_List[id].m_angle;
                        mp_path.LengthSegment[0] = Path_List[id].LengthSegment[0];
                        mp_path.LengthSegment[1] = Path_List[id].LengthSegment[1];
                        mp_path.AngleSegment[0] = Path_List[id].AngleSegment[0];
                        mp_path.AngleSegment[1] = Path_List[id].AngleSegment[1];
                        mp_path.CornerPoint = Path_List[id].CornerPoint;
                        mp_path.CircleCenter = Path_List[id].CircleCenter;
                        mp_path.CornerNeighbors[0] = Path_List[id].CornerNeighbors[0];
                        mp_path.CornerNeighbors[1] = Path_List[id].CornerNeighbors[1];
                        mp_path.TerminatePoints[0] = Path_List[id].TerminatePoints[0];
                        mp_path.TerminatePoints[1] = Path_List[id].TerminatePoints[1];
                        mp_path.pStopStart = Path_List[id].pStopStart;
                        mp_path.pOrgStopStart = Path_List[id].pOrgStopStart;
                        mp_path.pOrgStopEnd = Path_List[id].pOrgStopEnd;
                        mp_path.pStopEnd = Path_List[id].pStopEnd;
                        mp_path.m_ArcRadius = Path_List[id].m_ArcRadius;
                        mp_path.ArcStartAngle = Path_List[id].ArcStartAngle;
                        mp_path.ArcRangeAngle = Path_List[id].ArcRangeAngle;
                        mp_path.m_uVectorXs = Path_List[id].m_uVectorXs;
                        mp_path.m_uVectorYs = Path_List[id].m_uVectorYs;
                        mp_path.m_uVectorXe = Path_List[id].m_uVectorXe;
                        mp_path.m_uVectorYe = Path_List[id].m_uVectorYe;
                        mp_path.m_CheckTrafficCntlDist = Path_List[id].m_CheckTrafficCntlDist;
                        mp_path.m_CheckTrafficPt = Path_List[id].m_CheckTrafficPt;
                        mp_path.m_RuningDirection = Path_List[id].m_RuningDirection;
                        pVeh.ToEndPathSet.Add(mp_path);
                    } // if (id >= 0)
                } // loop i 
            }
        }

        int DetectObstacles(ref CVehicle pVeh)
        {
            CRect rgn = new CRect();
            int idx = 0;
            int type = 0;
            if (CursorInsideStation(pVeh.m_Pos, ref idx, ref type))
            {
                // Captured
                if (type == 10)
                {
                    // On a Stop
                    // Highest Priority to Move Out
                    return 0;
                }
            }
            // The same direction, wait
            // Different direction, avoidance
            for (int i = 0; i < Vehicle_List.Count; i++)
            {
                if (Vehicle_List[i].m_ID != pVeh.m_ID)
                {
                    // Not itself
                    if (pVeh.b_Avoidance == false)
                    {
                        // Regular Movement

                        rgn.X = pVeh.LiDarpts[0].X - pVeh.LiDarpts[3].X;
                        rgn.Y = pVeh.LiDarpts[0].Y - pVeh.LiDarpts[3].Y;
                        rgn.Width = pVeh.LiDarpts[1].X - pVeh.LiDarpts[2].X;
                        rgn.Height = pVeh.LiDarpts[1].X - pVeh.LiDarpts[2].X;

                        //(pVeh->LiDarpts, 4, ALTERNATE);
                        if (rgn.Contains(Vehicle_List[i].m_Pos.X, Vehicle_List[i].m_Pos.Y))
                        {
                            // Vehicle in the LiDar Range has been detected
                            if (Vehicle_List[i].pCurPath == null)
                            {
                                // Detected AGV stopping at a Stop 
                                // Must Wait for leave
                                return Vehicle_List[i].m_ID;
                            }
                            else
                            {
                                // AGV On a Path
                                // Check if they are on the same path
                                if (Vehicle_List[i].pCurPath.m_ID == pVeh.pCurPath.m_ID)
                                {
                                    // The same path
                                    if (Vehicle_List[i].pCurPath.m_RuningDirection == pVeh.pCurPath.m_RuningDirection)
                                    {
                                        // THe same direction, waiting
                                        return Vehicle_List[i].m_ID;
                                    }
                                    else
                                    {
                                        // Check if the block vehicle is within a stop area
                                        // if yes, must avoid for that vehicle

                                        CPoint tmp = Vehicle_List[i].m_Pos;
                                        if (CursorInsideStation(tmp, ref idx, ref type))
                                        {
                                            // Captured
                                            if (type == 10)
                                            {
                                                pVeh.b_Avoidance = true;
                                                pVeh.AvoidSet();
                                             /*   SayFile("++++++++++ [1] AGV ID, Stop ID, Ordered AGV ID",
                                                    pVeh->m_ID, pVeh->pCurPath->pStopEnd->m_ID,
                                                    Stop_List[GetStopIndex(pVeh->pCurPath->pStopEnd->m_ID)]->m_AGV_ID);*/
                                                // Check if this AGV haved ordered a Stop
                                                if (Stop_List[GetStopIndex(pVeh.pCurPath.pStopEnd.m_ID)].m_AGV_ID == pVeh.m_ID)
                                                {
                                                    // The End Stop is ordered by this AGV
                                                    // Cancel this order
                                                    Stop_List[GetStopIndex(pVeh.pCurPath.pStopEnd.m_ID)].m_AGV_ID = 0;
                                                   /* SayFile("--------- [1] AGV ID, Stop ID, Ordered AGV ID",
                                                        pVeh->m_ID, pVeh->pCurPath->pStopEnd->m_ID,
                                                        Stop_List[GetStopIndex(pVeh->pCurPath->pStopEnd->m_ID)]->m_AGV_ID);*/
                                                }
                                                return Vehicle_List[i].m_ID;
                                            }
                                        }
                                        // Opposite directions
                                        if (pVeh.m_ID < Vehicle_List[i].m_ID)
                                        {
                                            // pVeh->m_ID: Lower ID has a higher priority
                                            return 0; // Without Stop
                                        }
                                        else
                                        {
                                            // pVeh->m_ID: Higher ID has a lower priority
                                            // Must Change Way
                                            // Shift to the Right
                                            pVeh.b_Avoidance = true;
                                            pVeh.AvoidSet();
                                            // Check if this AGV haved ordered a Stop
                                           /* SayFile("++++++++++ [1] AGV ID, Stop ID, Ordered AGV ID",
                                                pVeh->m_ID, pVeh->pCurPath->pStopEnd->m_ID,
                                                Stop_List[GetStopIndex(pVeh->pCurPath->pStopEnd->m_ID)]->m_AGV_ID);*/
                                            if (Stop_List[GetStopIndex(pVeh.pCurPath.pStopEnd.m_ID)].m_AGV_ID == pVeh.m_ID)
                                            {
                                                // The End Stop is ordered by this AGV
                                                // Cancel this order
                                                Stop_List[GetStopIndex(pVeh.pCurPath.pStopEnd.m_ID)].m_AGV_ID = 0;
                                              /*  SayFile("--------- [1] AGV ID, Stop ID, Ordered AGV ID",
                                                    pVeh->m_ID, pVeh->pCurPath->pStopEnd->m_ID,
                                                    Stop_List[GetStopIndex(pVeh->pCurPath->pStopEnd->m_ID)]->m_AGV_ID);*/
                                            }
                                            return Vehicle_List[i].m_ID;
                                        }
                                    }
                                }
                                else
                                {
                                    // Differfent Paths
                                    // Check Front LiDar
                                    // Multiple Path Use the same Track
                                    // Consider direction from the AGV Heading
                                    if (Math.Abs(pVeh.m_angle - Vehicle_List[i].m_angle) <= 15.0)
                                    {
                                        // The same direction
                                        // Stop
                                        return Vehicle_List[i].m_ID;
                                    }
                                    else
                                    {
                                        // Opposite Direction
                                        if (pVeh.m_ID < Vehicle_List[i].m_ID)
                                        {
                                            // pVeh->m_ID: Lower ID has a higher priority
                                            return 0; // Without Stop
                                        }
                                        else
                                        {
                                            pVeh.b_Avoidance = true;
                                            pVeh.AvoidSet();
                                            // Check if this AGV haved ordered a Stop
                                            /*SayFile("^^^^^^^^ AGV ID, Stop ID, Ordered AGV ID",
                                                pVeh->m_ID, pVeh->pCurPath->pStopEnd->m_ID,
                                                Stop_List[GetStopIndex(pVeh->pCurPath->pStopEnd->m_ID)]->m_AGV_ID);*/
                                            if (Stop_List[GetStopIndex(pVeh.pCurPath.pStopEnd.m_ID)].m_AGV_ID == pVeh.m_ID)
                                            {
                                                // The End Stop is ordered by this AGV
                                                // Cancel this order
                                                Stop_List[GetStopIndex(pVeh.pCurPath.pStopEnd.m_ID)].m_AGV_ID = 0;
                                              /*  SayFile("-^-^-^-^-^- AGV ID, Stop ID, Ordered AGV ID",
                                                    pVeh->m_ID, pVeh->pCurPath->pStopEnd->m_ID,
                                                Stop_List[GetStopIndex(pVeh->pCurPath->pStopEnd->m_ID)]->m_AGV_ID);*/
                                            }
                                            return Vehicle_List[i].m_ID;
                                        }
                                    }
                                }
                            } // Blocked AGV on the Path
                        } // Another AGV In the region
                    }
                    else if (pVeh.b_Avoidance == true)
                    {
                        // Avoidance Movement
                        if (!CheckSideLIDar(ref pVeh))
                        {
                            // Without Vehcile at Side
                            // Stop Avoidance
                            // Return to original Path
                            pVeh.b_Avoidance = false;
                            pVeh.AvoidReturn();
                            return 0;
                        }
                        else
                        {
                            // Detect Side AGV
                            // Stop
                            return Vehicle_List[i].m_ID;
                        }
                    }
                }
            }
            return 0;
        }

        bool CheckSideLIDar(ref CVehicle pVeh)
        {
            for (int i = 0; i < Vehicle_List.Count; i++)
            {
                if (Vehicle_List[i].m_ID != pVeh.m_ID &&
                    !Vehicle_List[i].b_Avoidance)
                {
                    // Not itself
                    CRect rgn1 = new CRect();
                    CRect rgn2 = new CRect();
                    rgn1.X = pVeh.SideRLiDarpts[0].X - pVeh.SideRLiDarpts[3].X;
                    rgn1.Y = pVeh.SideRLiDarpts[0].Y - pVeh.SideRLiDarpts[3].Y;
                    rgn1.Width = pVeh.SideRLiDarpts[1].X - pVeh.SideRLiDarpts[2].X;
                    rgn1.Height = pVeh.SideRLiDarpts[1].Y - pVeh.SideRLiDarpts[2].Y;

                    rgn2.X = pVeh.SideLLiDarpts[0].X - pVeh.SideLLiDarpts[3].X;
                    rgn2.Y = pVeh.SideLLiDarpts[0].Y - pVeh.SideLLiDarpts[3].Y;
                    rgn2.Width = pVeh.SideLLiDarpts[1].X - pVeh.SideLLiDarpts[2].X;
                    rgn2.Height = pVeh.SideLLiDarpts[1].Y - pVeh.SideLLiDarpts[2].Y;
                    //rgn1.CreatePolygonRgn(pVeh->SideRLiDarpts, 4, ALTERNATE);
                    //rgn2.CreatePolygonRgn(pVeh->SideLLiDarpts, 4, ALTERNATE);

                    if (rgn2.Contains(Vehicle_List[i].m_Pos.X, Vehicle_List[i].m_Pos.Y) ||
                        rgn1.Contains(Vehicle_List[i].m_Pos.X, Vehicle_List[i].m_Pos.Y)) 
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        int CapStop(CPoint pt)
        {
            // Return stop index
            int i;
            int ans = -1;
            for (i = 0; i < Stop_List.Count; i++)
            {
                if (Stop_List[i].m_obj_rect_large.Contains(pt))
                {
                    return i;
                }
            }
            return ans;
        }

        void CheckAGVDeadlock()
        {
            for (int i = 0; i < Vehicle_List.Count; i++)
            {
                if (Vehicle_List[i].b_Avoidance == false)
                {
                    // On the Path or Stop
                    if (Vehicle_List[i].pCurPath != null)
                    {
                        // On the path
                        if (Vehicle_List[i].PathToFromSet.Count > 0 ||
                            Vehicle_List[i].PathToEndSet.Count > 0)
                        {
                            // Mission Not Finished
                            if (Vehicle_List[i].m_Pos == Vehicle_List[i].m_PreStatePos)
                            {
                                // AGV Not Moved
                                // Counting Deadlock
                                Vehicle_List[i].m_DeadlockTime++;
                                if (Vehicle_List[i].m_DeadlockTime >= m_AGVAllowWaitingTime)
                                {
                                    AddBlockAGV(Vehicle_List[i].m_ID);
                                }
                            }
                            else
                            {
                                // AGV Moved
                                // Cancel Counting
                                Vehicle_List[i].m_DeadlockTime = 0;
                            }
                        }
                        else
                        {
                            // Mission Finished
                            // Cancel Counting
                            Vehicle_List[i].m_DeadlockTime = 0;
                        }
                    }
                    else
                    {
                        // On the Stop
                        // Cancel Counting
                        Vehicle_List[i].m_DeadlockTime = 0;
                    }
                }
                else
                {
                    // Avoiding Yield Way
                    // Cancel Counting
                    Vehicle_List[i].m_DeadlockTime = 0;
                }
            } // loop i
        }

        bool AddBlockAGV(int id)
        {
            for (int i = 0; i < BlockAGVs.Count; i++)
            {
                if (BlockAGVs[i] == id)
                {
                    return false;
                }
            }
            BlockAGVs.Add(id);
            return true;
        }

        //A*------------------------------------------------------------------------------------------------------------------

        bool ExecuteA_Star(ref CVehicle pAGV)
        {
            // Execute A* Search 
            if (pAGV.A_Star_StartNode > 0 && pAGV.A_Star_EndNode > 0 && Stop_List.Count > 0)
            {
                pAGV.m_TotalOrders++;
                if (Stop_List[GetStopIndex(pAGV.A_Star_StartNode)].m_Failure)
                    return false;
                if (Stop_List[GetStopIndex(pAGV.A_Star_EndNode)].m_Failure)
                    return false;
                // Call the fisrt segment
                int tmp_Get = GetAPath(ref pAGV, pAGV.A_Star_StartNode, pAGV.A_Star_EndNode);
                if (tmp_Get != 0)
                {
                    CVehicle tmp = pAGV;
                    return true;
                }
                else
                {
                    // Modify 2020/02/17 ---------Start-------------------------
                    // If the way cannot reach the destination, delete the subtree
                    // Track Back from the last point, 

                    // Modify 2020/02/17 ---------End  -------------------------


                    // No Available path to End Stop
                    pAGV.PathToFromSet.Clear();
                    pAGV.PathToEndSet.Clear();
                    if (b_A_StartVehicleTrackingID == pAGV.m_ID)
                        ClearPathAttribute();
                                                                                    // KillTimer(1);
                                                                                    // b_AutoSim = 0;
                                                                                    // AfxMessageBox("Warning: no complete path way found!");
                    //*********SayFile("Warning: no complete path to From stop way found! [AGV ID, start, end]",
                     //   pAGV->m_ID, pAGV->A_Star_StartNode, pAGV->A_Star_EndNode);
                    return false;
                }
            }


            pictureBoxMap.Invalidate(WorkArea);
            return false;
        }

        double A_Star_Admin(ref CVehicle pV, int start, int end)
        {
            b_AStarWorking = true;
            double cost_min = 0;
            // Initialize All Outgoint Paths and Costs
            //SayFile("A New A* Search [AGV ID, start, end]", pV.m_ID, start, end);
            if (start > 0 && end > 0 && start != end)
            {
                int i = 0;
                for (i = 0; i < Path_List.Count; i++)
                {
                    Path_List[i].b_Selected = false;
                }
                for (i = 0; i < Stop_List.Count; i++)
                {
                    // Modify 2020/02/16 ---------Start-------------------------
                    Stop_List[i].m_Failure = false;
                    // Modify 2020/02/16 ---------End---------------------------
                    Stop_List[i].AdjacentCost.Clear();
                    Stop_List[i].AdjacentPathID.Clear();
                    Stop_List[i].AdjacentStopID.Clear();
                    Stop_List[i].m_ArcNumber = 0;
                    // check not itself
                    if (Stop_List[i].m_ID != end)
                    {
                        // Get Heuristic Dist of All Stops to Destination
                        Stop_List[i].HeuristicDist = GetHeuristicLength(Stop_List[i].m_ID, end);
                        // Evaluate Stops one by one
                        for (int j = 0; j < Path_List.Count; j++)
                        {
                            // One Way
                            if (Path_List[j].pStopStart.m_ID == Stop_List[i].m_ID)
                            {
                                // Add to the lsit 
                                Stop_List[i].AdjacentCost.Add(Path_List[j].Length);
                                Stop_List[i].AdjacentPathID.Add(Path_List[j].m_ID);
                                Stop_List[i].AdjacentStopID.Add(Path_List[j].pStopEnd.m_ID);
                            }
                            if (Path_List[j].m_direction == 2)
                            {
                                // Dual Way // Additional One
                                if (Path_List[j].pStopEnd.m_ID == Stop_List[i].m_ID)
                                {
                                    // Add to the lsit 
                                    Stop_List[i].AdjacentCost.Add(Path_List[j].Length);
                                    Stop_List[i].AdjacentPathID.Add(Path_List[j].m_ID);
                                    Stop_List[i].AdjacentStopID.Add(Path_List[j].pStopStart.m_ID);
                                }
                            }
                            // Modify 2020/02/17 ---------Start-------------------------
                            // Count Connected Arc Numbers
                            if (Path_List[j].pStopStart.m_ID == Stop_List[i].m_ID ||
                                Path_List[j].pStopEnd.m_ID == Stop_List[i].m_ID)
                            {
                                // Add arc count number 
                                Stop_List[i].m_ArcNumber++;
                            }
                            // Modify 2020/02/17 ---------End  -------------------------
                        } // loop j
                    }
                } // loop i
                  // Modify 2020/02/16 ---------Start-------------------------
                DeleteTunnelSubTrees(ref pV, start, end);
                // Modify 2020/02/16 ---------End---------------------------
                // Set Max. Three Tracks
                double Path_cost;
                CString ta = "";
                CString sa = "";
                // Modify 2020/02/17 ---------Start-------------------------
                // Remove Multi-tier
                Path_cost = A_Star(start, end, 0);
                if (Path_cost > 0)
                {
                    // Get a feasible path 
                    return cost_min;
                }
                else
                {
                    // Set the Unreachable Path Nodes as Failure before the Brench Point
                    // Iterate from the dead node; i.e., the last node in the CloseNodeSet
                    for (int dx = CloseNodeSet.Count; dx >= 0; dx--)
                    {
                        if (dx == CloseNodeSet.Count)
                        {
                            Stop_List[GetStopIndex(CloseNodeSet[dx-1])].m_Failure = true;
                        }
                        else
                        {
                            int stop_idx = GetStopIndex(CloseNodeSet[dx]);
                            if (Stop_List[stop_idx].m_ArcNumber > 2)
                            {
                                // Tree Candidate as a Possible Brench node
                                // Potentail Risk could be improved in the future!!!!!
                                // Stop: CloseNodeSet[dx]
                                // Find Any Path Connected to This Stop
                                if (CheckPathBackDeleteNode(dx, stop_idx)) // 1->Delete Node; 0: Reserve Node=> MUST STOP DEL NODE Process
                                {
                                    Stop_List[GetStopIndex(CloseNodeSet[dx])].m_Failure = true;
                                }
                                else
                                {
                                    dx = -1;
                                }
                            }
                            else
                            {
                                // Continue ...... 
                                Stop_List[GetStopIndex(CloseNodeSet[dx])].m_Failure = true;
                            }
                        }
                    } // loop dx
                      // -----------------ReOperate A* with the newly updtaed Failure Node -----------------// 
                    return A_Star_ReAdmin(ref pV, start, end);
                    // -----------------ReOperate A* with the newly updtaed Failure Node -----------------// 
                }
                // Modify 2020/02/17 ---------End  -------------------------
                // Just investigation

                if (Path_cost > 0)
                    ta = "Pass: ";
                else
                    ta = "Failed: ";
                for (int d = 0; d < CloseNodeSet.Count(); d++)
                {
                    //sa.Format("%d->", CloseNodeSet[d]);
                   // ta += sa;
                }
                double len_total = 0;
                for (int d = 0; d < A_Star_PathSet.Count; d++)
                {
                   // sa.Format("[%d, %f]->", A_Star_PathSet[d], Path_List[GetPathIndex(A_Star_PathSet[d])].Length);
                   // ta += sa;
                    // Calcualte Path Length
                    len_total += Path_List[GetPathIndex(A_Star_PathSet[d])].Length;
                }
                // Say(ta, CloseNodeSet.GetSize(), (int)len_total, tmp_cost[i]);
                // Mark the Path
            } // if (start > 0 && end > 0)
            b_AStarWorking = false;
            return cost_min;
        }

        double A_Star_ReAdmin(ref CVehicle pV, int start, int end)
        {
            b_AStarWorking = true;
            double cost_min = 0;
            // Initialize All Outgoint Paths and Costs
            //SayFile("ReEvaluate A* Search [AGV ID, start, end]", pV.m_ID, start, end);
            if (start > 0 && end > 0 && start != end)
            {
                // Set Max. Three Tracks
                List<int>[] tmp_path = new List<int>[3] { new List<int>(), new List<int>(),new List<int>() };
                List<int>[] tmp_stop = new List<int>[3] { new List<int>(), new List<int>(), new List<int>()};

                

                double []tmp_cost = { 1000000.0f, 1000000.0f, 1000000.0f };
                CString ta = "";
                CString sa = "";
                int C = Stop_List[GetStopIndex(start)].AdjacentStopID.Count;
                for (int i = 0; i < C; i++)
                {
                    if (i < 1) // (i < 3) // Just Once, No Tier
                    {
                        // ----------------------   A* Executing Here  -------------------------------// 
                        tmp_cost[i] = A_Star(start, end, i);
                        // ----------------------   A* Executing Here  -------------------------------// 
                        if (tmp_cost[i] > 0)
                        {
                            for (int j = 0; j < A_Star_PathSet.Count; j++)
                            {
                                tmp_path[i].Add(A_Star_PathSet[j]);
                            }
                            for (int j = 0; j < CloseNodeSet.Count; j++)
                            {
                                tmp_stop[i].Add(CloseNodeSet[j]);
                            }
                        }
                        else
                        {
                            // Set the Unreachable Path Nodes as Failure before the Brench Point
                            // Iterate from the dead node; i.e., the last node in the CloseNodeSet
                            for (int dx = CloseNodeSet.Count; dx >= 0; dx--)
                            {
                                if (dx == CloseNodeSet.Count)
                                {
                                    Stop_List[GetStopIndex(CloseNodeSet[dx-1])].m_Failure = true;
                                }
                                else
                                {
                                    int stop_idx = GetStopIndex(CloseNodeSet[dx]);
                                    if (Stop_List[stop_idx].m_ArcNumber > 2)
                                    {
                                        // Tree Candidate as a Possible Brench node
                                        // Potentail Risk could be improved in the future!!!!!
                                        // Stop: CloseNodeSet[dx]
                                        // Find Any Path Connected to This Stop
                                        if (CheckPathBackDeleteNode(dx, stop_idx)) // 1->Delete Node; 0: Reserve Node=> MUST STOP DEL NODE Process
                                        {
                                            Stop_List[GetStopIndex(CloseNodeSet[dx])].m_Failure = true;
                                        }
                                        else
                                        {
                                            dx = -1;
                                        }
                                    }
                                    else
                                    {
                                        // Continue ...... 
                                        Stop_List[GetStopIndex(CloseNodeSet[dx])].m_Failure = true;
                                    }
                                }
                            } // loop dx					
                              // -----------------ReOperate A* Again (2) with the newly updtaed Failure Node -----------// 
                            return A_Star_ReAdminAgain(ref pV, start, end);
                            // -----------------ReOperate A* with the newly updtaed Failure Node ---------------------// 
                        }
                        // Modify 2020/02/17 ---------End  -------------------------
                        // Just investigation
                        if (tmp_cost[i] > 0)
                            ta = "Pass: ";
                        else
                            ta = "Failed: ";
                        for (int d = 0; d < CloseNodeSet.Count; d++)
                        {
                        //    sa.Format("%d->", CloseNodeSet[d]);
                          //  ta += sa;
                        }
                        double len_total = 0;
                        for (int d = 0; d < A_Star_PathSet.Count; d++)
                        {
                          //  sa.Format("[%d, %f]->", A_Star_PathSet[d], Path_List[GetPathIndex(A_Star_PathSet[d])]->Length);
                           // ta += sa;
                            // Calcualte Path Length
                            len_total += Path_List[GetPathIndex(A_Star_PathSet[d])].Length;
                        }
                        // Say(ta, CloseNodeSet.GetSize(), (int)len_total, tmp_cost[i]);
                    }
                }  // loop i
                   // Make comaprison
                int idx = 0;
                cost_min = tmp_cost[0];
                for (int i = 1; i < 3; i++)
                {
                    if (tmp_cost[i] < cost_min && tmp_cost[i] != 0)
                    {
                        cost_min = tmp_cost[i];
                        idx = i;
                    }
                }
                // Mark the Path
                CloseNodeSet.Clear();
                A_Star_PathSet.Clear();

                for (int i = 0; i < tmp_stop[idx].Count; i++)
                {
                    CloseNodeSet.Add(tmp_stop[idx][i]);
                }
                for (int i = 0; i < tmp_path[idx].Count; i++)
                {
                    A_Star_PathSet.Add(tmp_path[idx][i]);
                }
            } // if (start > 0 && end > 0)
            b_AStarWorking = false;
            return cost_min;
        }

        double A_Star_ReAdminAgain(ref CVehicle pV, int start, int end)
        {
            b_AStarWorking = true;
            double cost_min = 0;
            // Initialize All Outgoint Paths and Costs
            //SayFile("ReEvaluate A* Search [AGV ID, start, end]", pV.m_ID, start, end);
            if (start > 0 && end > 0 && start != end)
            {
                // Set Max. Three Tracks
                List<int>[] tmp_path = new List<int>[3] { new List<int>(), new List<int>(), new List<int>() };
                List<int>[] tmp_stop = new List<int>[3] { new List<int>(), new List<int>(), new List<int>() };
                double []tmp_cost = { 1000000.0f, 1000000.0f, 1000000.0f };
                CString ta = "";
                CString sa = "";
                for (int i = 0; i < Stop_List[GetStopIndex(start)].AdjacentStopID.Count; i++)
                {
                    if (i < 1) // (i < 3) // Just Once, No Tier
                    {
                        // ----------------------   A* Executing Here  -------------------------------// 
                        tmp_cost[i] = A_Star(start, end, i);
                        // ----------------------   A* Executing Here  -------------------------------// 
                        if (tmp_cost[i] > 0)
                        {
                            for (int j = 0; j < A_Star_PathSet.Count; j++)
                            {
                                tmp_path[i].Add(A_Star_PathSet[j]);
                            }
                            for (int j = 0; j < CloseNodeSet.Count; j++)
                            {
                                tmp_stop[i].Add(CloseNodeSet[j]);
                            }
                        }
                        else
                        {
                      //      SayFile("####### From Stop to End Stop Path [AGV ID, Start, End] Failure Three Evlauation Tests",
                     //           pV.m_ID, start, end);
                            return 0; // Tried Twice, but still no Path!!
                        }
                        // Just investigation
                        if (tmp_cost[i] > 0)
                            ta = "Pass: ";
                        else
                            ta = "Failed: ";
                        for (int d = 0; d < CloseNodeSet.Count; d++)
                        {
                         //   sa.Format("%d->", CloseNodeSet[d]);
                         //   ta += sa;
                        }
                        double len_total = 0;
                        for (int d = 0; d < A_Star_PathSet.Count; d++)
                        {
                        //    sa.Format("[%d, %f]->", A_Star_PathSet[d], Path_List[GetPathIndex(A_Star_PathSet[d])].Length);
                        //    ta += sa;
                            // Calcualte Path Length
                            len_total += Path_List[GetPathIndex(A_Star_PathSet[d])].Length;
                        }
                        // Say(ta, CloseNodeSet.GetSize(), (int)len_total, tmp_cost[i]);
                    }
                }  // loop i
                   // Make comaprison
                int idx = 0;
                cost_min = tmp_cost[0];
                for (int i = 1; i < 3; i++)
                {
                    if (tmp_cost[i] < cost_min && tmp_cost[i] != 0)
                    {
                        cost_min = tmp_cost[i];
                        idx = i;
                    }
                }
                // Mark the Path
                CloseNodeSet.Clear();
                A_Star_PathSet.Clear();
                for (int i = 0; i < tmp_stop[idx].Count; i++)
                {
                    CloseNodeSet.Add(tmp_stop[idx][i]);
                }
                for (int i = 0; i < tmp_path[idx].Count; i++)
                {
                    A_Star_PathSet.Add(tmp_path[idx][i]);
                }
            } // if (start > 0 && end > 0)
            b_AStarWorking = false;
            return cost_min;
        }

        bool CheckPathBackDeleteNode(int closenode_idx, int stop_idx)
        {
            // 1. Check all connected paths
            // 2. Check Path List (enter and leave by path array -> Not counted
            // 3. Check other paths (excluding condition 2) if they are
            // 3.1 If Connected to a Failure Node -> Not Counted
            // 3.2 Both direction -> Counted
            // 3.3 One Way -> Check If Outward -> Counted 
            // Path Attributes: pStopStart, pStopEnd 
            // CloseNodeSet, A_Star_PathSet
            int counted = 0;
            if (closenode_idx < 1)
                return true; // Delete
            for (int j = 0; j < Path_List.Count; j++)
            {
                // Current idx for stop list: closenode_idx
                if (Path_List[j].pStopStart.m_ID == Stop_List[stop_idx].m_ID ||
                    Path_List[j].pStopEnd.m_ID == Stop_List[stop_idx].m_ID)
                {
                    // Connected Path to the examined stop
                    if (Path_List[j].m_ID != A_Star_PathSet[closenode_idx] &&
                        Path_List[j].m_ID != A_Star_PathSet[closenode_idx - 1])
                    {
                        // Find another node in this patth
                        if (Path_List[j].pStopStart.m_ID == Stop_List[stop_idx].m_ID)
                        {
                            if (!Stop_List[GetStopIndex(Path_List[j].pStopEnd.m_ID)].m_Failure)
                            {
                                // Checking direction
                                if (Path_List[j].m_direction == 2)
                                {
                                    // Dual Way
                                    counted++;
                                }
                                else // One way
                                {
                                    // Inward 
                                    if (Stop_List[stop_idx].m_ID == Path_List[j].pStopStart.m_ID)
                                    {
                                        counted++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Must not a dead node
                            if (!Stop_List[GetStopIndex(Path_List[j].pStopStart.m_ID)].m_Failure)
                            {
                                // Checking direction
                                if (Path_List[j].m_direction == 2)
                                {
                                    // Dual Way
                                    counted++;
                                }
                                else // One way
                                {
                                    // Inward 
                                    if (Stop_List[stop_idx].m_ID == Path_List[j].pStopStart.m_ID)
                                    {
                                        counted++;
                                    }
                                }
                            } // Not a Failure Neighbor Node
                        } // else loop
                    } // Not on the failed path
                }
            } // loop i
            if (counted > 0)
                return false; // Reserve Node
            else
                return true; // Delete Node
        }
        // Modify 2020/02/17 ---------End  -------------------------
        double A_Star(int start, int end, int seq)
        {
            // Evaluate Net
            // Generate Adjacency_list
            // Seq only for the first tier
            int tier = 0;
            int d = 0;
            CString sa;
            CString ta;
            if (start > 0 && end > 0)
            {
                int i;
                // Reset All
                OpenNodeSet.Clear();
                CloseNodeSet.Clear();
                A_Star_PathSet.Clear();
                // Initially put the next goal to the OpenNodeSet
                for (i = 0; i < Stop_List[GetStopIndex(start)].AdjacentStopID.Count; i++)
                {
                    // Available Stop Add to OpenNodeSet
                    int kx = GetStopIndex(Stop_List[GetStopIndex(start)].AdjacentStopID[i]);
                    if (!Stop_List[kx].m_Failure)
                        OpenNodeSet.Add(Stop_List[GetStopIndex(start)].AdjacentStopID[i]);
                }  // loop i
                CloseNodeSet.Add(start); //   /////////////The Fisrt Node
                int prev_n = start;
                int cur_n = 0;
                double new_G_cost = 0;
                double real_cost = 0;
                double heurist_cost = 0;
                // Modify 2020/02/17 ---------Start-------------------------
                // Delete Rallback Function
                while (OpenNodeSet.Count > 0)
                {
                    // Search from this OpenNodeSet to Evaluate Cost G & H
                    double cost_min = 1000000.0f;
                    double cost_tmp = 0;
                    int sel_id = -1;
                    // int idx_squ[3] = {-1, -1, -1};
                    // double cost_seq[3] = {1000000.0f, 1000000.0f, 1000000.0f};
                    // Test All Situations
                    for (int j = 0; j < OpenNodeSet.Count; j++)
                    {
                        cur_n = OpenNodeSet[j];
                        // Check if a destination node
                        if (cur_n == end)
                        {
                            // Ending 
                            CloseNodeSet.Add(cur_n); // The Last Node ///////////////////////////
                            int path_id_sel = GetPathID(prev_n, cur_n);
                            A_Star_PathSet.Add(path_id_sel);
                            // Path_List[GetPathIndex(path_id_sel)]->b_OnPath = 1;
                            new_G_cost += GetNodeLength(prev_n, cur_n);
                            OpenNodeSet.Clear();
                            return new_G_cost; ////////////////////////////////////////////Pass///////
                        }
                        else
                        {
                            // Check if a revisited node
                            if (!IsAClosedNode(cur_n)) // Un-visited
                            {
                                // Get cost function
                                // G: Previous Distance prev_n->cur_n
                                // H: cur_n -> Goal
                                cost_tmp = GetNodeLength(prev_n, cur_n) + Stop_List[GetStopIndex(cur_n)].HeuristicDist;
                                if (cost_tmp < cost_min)
                                {
                                    // Update Min. value
                                    cost_min = cost_tmp;
                                    sel_id = cur_n;
                                    real_cost = GetNodeLength(prev_n, cur_n);
                                    heurist_cost = Stop_List[GetStopIndex(cur_n)].HeuristicDist;
                                } // if (cost_tmp < cost_min)
                            } // if (!IsAClosedNode(cur_n))
                        } // if (cur_n != end)
                    } // loop j for all Open Nodes
                      // Finishing all iterations in the current Open Nodes
                      // Find the min. cost and candidate
                    if (cost_min > 0 && sel_id > 0) // Valid
                    {
                        // Find Candidate
                        // Arrange a New OpenNodeSet
                        OpenNodeSet.Clear();
                        CloseNodeSet.Add(sel_id);
                        int path_id_sel = GetPathID(prev_n, sel_id);
                        A_Star_PathSet.Add(path_id_sel);
                        // Path_List[GetPathIndex(path_id_sel)]->b_OnPath = 1;
                        new_G_cost += real_cost;

                        for (d = 0; d < CloseNodeSet.Count; d++)
                        {
                            //sa.Format("%d->", CloseNodeSet[d]);
                            //ta += sa;
                        }
                        // Arrange the Next Open Nodes
                        if (Stop_List[GetStopIndex(sel_id)].AdjacentStopID.Count > 0)
                        {
                            // Next Stops Available
                            prev_n = sel_id;
                            for (int k = 0; k < Stop_List[GetStopIndex(prev_n)].AdjacentStopID.Count; k++)
                            {
                                // Add to OpenNodeSet
                                int kx = GetStopIndex(Stop_List[GetStopIndex(prev_n)].AdjacentStopID[k]);
                                if (!Stop_List[kx].m_Failure)
                                    OpenNodeSet.Add(Stop_List[GetStopIndex(prev_n)].AdjacentStopID[k]);
                            }  // loop k
                        }
                        else // if (Stop_List[GetStopIndex(sel_id)]->AdjacentStopID.GetSize() <= 0)
                        {
                            // No Next Stops
                            // Terminate and Fail

                            ta = "??????????????????????????????************* Incomplete Path Stops: ";
                            for (d = 0; d < CloseNodeSet.Count; d++)
                            {
                               // sa.Format("%d->", CloseNodeSet[d]);
                               // ta += sa;
                            }
                            for (d = 0; d < A_Star_PathSet.Count; d++)
                            {
                              //  sa.Format("[%d]->", A_Star_PathSet[d]);
                              //  ta += sa;
                            }
                            //SayFile(ta, start, end, 999);
                            OpenNodeSet.Clear();
                            return 0;
                        }
                    }
                    else // if (cost_min <= 0 && sel_id <= 0)
                    {
                        // No Candidate
                        // Terminate and Fail
                        // Copy Current Closed Net even unfinished
                        ta = "!!!!!!!!!!!!!!!!!!!!!!!!************* Incomplete Path Stops: ";
                        for (d = 0; d < CloseNodeSet.Count; d++)
                        {
                           // sa.Format("%d->", CloseNodeSet[d]);
                           // ta += sa;
                        }
                        for (d = 0; d < A_Star_PathSet.Count; d++)
                        {
                          //  sa.Format("[%d]->", A_Star_PathSet[d]);
                          //  ta += sa;
                        }
                        //SayFile(ta, start, end, 999);
                        OpenNodeSet.Clear ();
                        return 0;
                    }
                } // loop while (OpenNodeSet.GetSize() > 0)
                  // Modify 2020/02/17 ---------End  -------------------------
            } // if (start > 0 && end > 0)
            return 0;
        }

        bool IsAClosedNode(int node)
        {
            for (int i = 0; i < CloseNodeSet.Count; i++)
            {
                if (CloseNodeSet[i] == node)
                    return true;
            }
            return false;
        }

        double GetNodeLength(int a, int b)
        {
            int idx = GetStopIndex(a);
            if (idx >= 0)
            {
                for (int i = 0; i < Stop_List[idx].AdjacentStopID.Count; i++)
                {
                    if (Stop_List[idx].AdjacentStopID[i] == b)
                        return Stop_List[idx].AdjacentCost[i];
                }
            }
            return -1;
        }

        int GetPathID(int a, int b)
        {
            for (int i = 0; i < Path_List.Count; i++)
            {
                if (Path_List[i].pStopStart.m_ID == a &&
                    Path_List[i].pStopEnd.m_ID == b)
                    return Path_List[i].m_ID;
                if (Path_List[i].m_direction == 2)
                {
                    if (Path_List[i].pStopStart.m_ID == b &&
                        Path_List[i].pStopEnd.m_ID == a)
                        return Path_List[i].m_ID;
                }
            }
            return 0;
        }

        int GetPathIndex(int a)
        {
            for (int i = 0; i < Path_List.Count; i++)
            {
                if (Path_List[i].m_ID == a)
                    return i;
            }
            return -1;
        }

        void DeleteTunnelSubTrees(ref CVehicle pV, int start, int end)
        {
            // Delete All Unreachable Subtrees
            // Via making Stop_List[?]->m_Failure = 1;
            // Procedures
            // 0. Checking if it is a Start or End Stop
            // 1. Checking All stops about its outward paths
            // 1.1 If Outward paths == 0; Stop_List[?]->m_Failure = 1;
            // 1.2 If Outward paths == 1 & Path is a Dual-Way; Stop_List[?]->m_Failure = 1;
            // 1.3 If Outward paths > 1; Checking All Outward Path Connected Stops Availabilityt/ WIth Considering Dual Way Situation (Ref 1.2)
            // Above Iterating until No SubTree End in the map
            // Functions: Path_List[GetPathIndex(?)], Stop_List[GetStopIndex(?)]
            int m_SubTreeEnd = 1; // Making it available to enter while loop
            while (m_SubTreeEnd > 0)
            {
                m_SubTreeEnd = 0; // Reset
                for (int i = 0; i < Stop_List.Count; i++)
                {
                    if (Stop_List[i].m_ID != end && Stop_List[i].m_ID != start && !Stop_List[i].m_Failure)
                    {
                        // Beging to check
                        if (Stop_List[i].AdjacentStopID.Count == 0)
                        {
                            Stop_List[i].m_Failure = true;
                            m_SubTreeEnd++;
                        }
                        else if (Stop_List[i].AdjacentStopID.Count == 1)
                        {
                            if (Path_List[GetPathIndex(Stop_List[i].AdjacentPathID[0])].m_direction == 2)
                            {
                                Stop_List[i].m_Failure = true; // Dual Way, a comming and returning way
                                m_SubTreeEnd++;
                            }
                        }
                        else
                        {
                            List <int> tmp_path_idx = new List<int>();
                            List <int> tmp_stop_idx = new List<int>();
                            for (int j = 0; j < Stop_List[i].AdjacentStopID.Count; j++)
                            {
                                if (!Stop_List[GetStopIndex(Stop_List[i].AdjacentStopID[j])].m_Failure)
                                {
                                    tmp_path_idx.Add(Stop_List[i].AdjacentPathID[j]);
                                    tmp_stop_idx.Add(Stop_List[i].AdjacentStopID[j]);
                                }
                            } // loop j
                            if (tmp_path_idx.Count == 0)
                            {
                                Stop_List[i].m_Failure = true;
                                m_SubTreeEnd++;
                            }
                            else if (tmp_path_idx.Count == 1)
                            {
                                if (Path_List[GetPathIndex(tmp_path_idx[0])].m_direction == 2)
                                {
                                    Stop_List[i].m_Failure = true; // Dual Way, a comming and returning way 
                                    m_SubTreeEnd++;
                                }
                            }
                            else
                            {
                                // At least two possible outward ways
                                // If one of them is possibly a comming and returning way, one more alternative outward way
                            }
                            tmp_path_idx.Clear();
                            tmp_stop_idx.Clear();
                        } // Checking Stop_List[i]->AdjacentStopID.GetSize()
                    } // Checking start, end and previously assigned failure
                } // loop i
            } // End While
        }

        double GetHeuristicLength(int a, int b)
        {
            int idxa = GetStopIndex(a);
            int idxb = GetStopIndex(b);
            if (idxa >= 0 && idxb >= 0)
            {
                // Node a Stop_List[idxa]->m_center
                // Node b Stop_List[idxa]->m_center
                return Math.Sqrt(((Stop_List[idxa].m_center.X - Stop_List[idxb].m_center.X) *
                                  (Stop_List[idxa].m_center.X - Stop_List[idxb].m_center.X) +
                                  (Stop_List[idxa].m_center.Y - Stop_List[idxb].m_center.Y) *
                                  (Stop_List[idxa].m_center.Y - Stop_List[idxb].m_center.Y)));
            }
            return -1;
        }

        int GetStopIndex(int ID)
        {
            for (int i = 0; i < Stop_List.Count; i++)
            {
                if (Stop_List[i].m_ID == ID)
                    return i;
            }
            return -1;
        }

        int GetAPath(ref CVehicle pVeh, int from_id, int to_id)
        {
            CString str;
            CTime t = new CTime();
           // str.Format("Get A New From/To Path [AGV ID, From, To] on %04d-%02d-%02d %02d:%02d:%02d",
            //    t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second);
                                        //SayFile(str, pVeh->m_ID, from_id, to_id);
            if (from_id == to_id)
                return 0;
            pVeh.PathToFromSet.Clear();
            pVeh.PathToEndSet.Clear();
            // Step 1 Find a path from vehcile approaching stop to StartStop
            // 1.1 Get Vehicle Next Stop 
            int start_id = 0;
            int end_id = from_id;
            if (pVeh.pCurPath != null)
            {
                // On a Path
                // Get Moving Direction
                if (pVeh.b_MovingDir) // Opsitite
                {
                    start_id = pVeh.pCurPath.pOrgStopStart.m_ID;
                }
                else
                {
                    start_id = pVeh.pCurPath.pOrgStopEnd.m_ID;
                }
            }
            else if (pVeh.pCurStop != null)
            {
                // On a Stop
                start_id = pVeh.pCurStop.m_ID;
            }
            else
                return 0; // Failure
                          // Use A*
            if (start_id > 0 && end_id > 0 && Stop_List.Count > 0)
            {
                CString sa, ta;
                int d = 0;
                // Clear Previous Results
                CloseNodeSet.Clear();
                A_Star_PathSet.Clear();
                //  --------------------------    Do A* Search Admin -----------------------//
                double cost_done = A_Star_Admin(ref pVeh, start_id, end_id);
                //  --------------------------    Do A* Search Admin -----------------------//
                if (cost_done > 0)
                    ta = "To From Passed: ";
                else
                    ta = "To From Failed: ";
                for (d = 0; d < CloseNodeSet.Count; d++)
                {
                  //  sa.Format("%d->", CloseNodeSet[d]);
                  //  ta += sa;
                }
                for (d = 0; d < A_Star_PathSet.Count; d++)
                {
                    pVeh.PathToFromSet.Add(A_Star_PathSet[d]);
                  //  sa.Format("[%d]->", A_Star_PathSet[d]);
                    //ta += sa;
                    // Set on path
                    if (b_A_StartVehicleTrackingID == pVeh.m_ID)
                        Path_List[GetPathIndex(A_Star_PathSet[d])].m_OnPath = 1;
                }
                                                                                                        // Say(ta, CloseNodeSet.GetSize(), 0, cost_done);
              ///  SayFile("Current Position to From Stop Path [AGV ID, Start, End]", pVeh->m_ID, start_id, end_id);
                // SayFile(ta, 99, 99, 99);
            }
            // Step 2 Find a path from StartStop to EndStop
            // 2.1 Get Vehicle Next Stop 
            start_id = from_id;
            end_id = to_id;
            // Use A*
            if (start_id > 0 && end_id > 0 && Stop_List.Count > 0)
            {
                CString sa, ta;
                int d = 0;
                // Clear Previous Results
                CloseNodeSet.Clear();
                A_Star_PathSet.Clear();
                //  --------------------------    Do A* Search Admin -----------------------//
                double cost_done = A_Star_Admin(ref pVeh, start_id, end_id);
                //  --------------------------    Do A* Search Admin -----------------------//
                if (cost_done > 0)
                    ta = "To End Passed: ";
                else
                    ta = "To End Failed: ";
                for (d = 0; d < CloseNodeSet.Count; d++)
                {
                  //  sa.Format("%d->", CloseNodeSet[d]);
                   // ta += sa;
                }
                for (d = 0; d < A_Star_PathSet.Count; d++)
                {
                    pVeh.PathToEndSet.Add(A_Star_PathSet[d]);
                    pVeh.m_status = 1; // Order to Move
                    //sa.Format("[%d]->", A_Star_PathSet[d]);
                    pVeh.b_PathOpen = false;
                    pVeh.b_RotateDone = false;
                   // ta += sa;
                    // Set on path
                    if (b_A_StartVehicleTrackingID == pVeh.m_ID)
                        Path_List[GetPathIndex(A_Star_PathSet[d])].m_OnPath = 2;
                }
              //  SayFile("From Stop to End Stop Path [AGV ID, Start, End]", pVeh.m_ID, start_id, end_id);
               // SayFile(ta, 99, 99, 99);
                                                // Say(ta, CloseNodeSet.GetSize(), 0, cost_done);
            }
            // Fail Condition
            // From Set Empty excluding Start = Current
            if (pVeh.pCurStop.m_ID == from_id)
            {
                // To Set MUST NOT BE empty
                if (pVeh.PathToEndSet.Count != 0)
                    return 1;
                else
                    return 0;
            }
            else
            {
                // Start Node is not the current node
                // Both Sets should have paths
                if (pVeh.PathToFromSet.Count == 0 || pVeh.PathToEndSet.Count == 0)
                    return 0;
                else
                    return 1;
            }
         return 0;
        }

        void ClearPathAttribute()
        {
            for (int i = 0; i < Path_List.Count; i++)
            {
                Path_List[i].m_OnPath = 0;
                Path_List[i].b_Selected = false;
            }
        }

        bool CursorInsideStation(CPoint pt, ref int idx, ref int type)
        {
            int i;
            CRect cmp_rect = new CRect();
            for (i = 0; i < Stop_List.Count; i++)
            {
                cmp_rect = Stop_List[i].m_obj_rect;
                cmp_rect.X = Stop_List[i].m_obj_rect.X + m_DrawOrg.X;
                cmp_rect.Y = Stop_List[i].m_obj_rect.Y + m_DrawOrg.Y;

                if (cmp_rect.Contains(pt))
                {
                    type = 10;
                    idx = i;
                    return true;
                }
            }
            for (i = 0; i < Path_List.Count; i++)
            {
                cmp_rect = Path_List[i].m_obj_rect;
                cmp_rect.X = Path_List[i].m_obj_rect.X + m_DrawOrg.X;
                cmp_rect.Y = Path_List[i].m_obj_rect.Y + m_DrawOrg.Y;

                if (cmp_rect.Contains(pt))
                {
                    type = 20;
                    idx = i;
                    return true;
                }
            }
            for (i = 0; i < Vehicle_List.Count; i++)
            {
                cmp_rect = Vehicle_List[i].m_CurRect;
                cmp_rect.X = Vehicle_List[i].m_CurRect.X + m_DrawOrg.X;
                cmp_rect.Y = Vehicle_List[i].m_CurRect.Y + m_DrawOrg.Y;

                if (cmp_rect.Contains(pt))
                {
                    type = 30; // Vehicle
                    idx = i;
                    return true;
                }
            }
            idx = -1;
            type = -1;
            return false;
        }

        bool CapObjects(CPoint point)
        {
            // Get Object ID
            int idx = 0;
            int type = 0;
            bool ans;
            int i = 0;
            for (i = 0; i < Path_List.Count; i++)
            {
                Path_List[i].b_Selected = false;
            }
            for (i = 0; i < Vehicle_List.Count; i++)
            {
                Vehicle_List[i].b_Selected = false;
            }
            if (CursorInsideStation(point, ref idx, ref type))
            {
                // Captured
                Cap_ID = idx;
                Cap_Type = type; // Stop Type
                if (type == 10)
                    CapRect = Stop_List[idx].m_obj_rect;
                else if (type == 20)
                    Path_List[idx].b_Selected = true;
                else if (type == 30)
                    Vehicle_List[idx].b_Selected = true;
                ans = true;
            }
            else
            {
                CapRect = new CRect(0, 0, 0, 0);
                Cap_ID = 0;
                Cap_Type = 0;
                ans = false;
                for (i = 0; i < Path_List.Count; i++)
                {
                    Path_List[i].b_Selected = false;
                }
            }
            pictureBoxMap.Invalidate(WorkArea);
            return ans;
        }

        int GetID(int type)
        {
            int cur_id = 0;
            int k = 0;
            int max = 0;
            switch (type)
            {
                case 10: // Station
                    {
                        if (Stop_List.Count <= 0)
                        {
                            cur_id = 1;
                        }
                        else
                        {
                            max = Stop_List[0].m_ID;
                            for (k = 0; k <= Stop_List.Count-1; k++)
                            {
                                if (Stop_List[k].m_ID > max)
                                {
                                    max = Stop_List[k].m_ID;
                                }
                            }
                            cur_id = max + 1;
                        }
                        break;
                    }
                case 20: // Path
                    {
                        if (Path_List.Count<= 0)
                        {
                            cur_id = 1;
                        }
                        else
                        {
                            max = Path_List[0].m_ID;
                            for (k = 0; k <= Path_List.Count-1; k++)
                            {
                                if (Path_List[k].m_ID > max)
                                {
                                    max = Path_List[k].m_ID;
                                }
                            }
                            cur_id = max + 1;
                        }
                        break;
                    }
                case 30: // Vehicle
                    {
                        if (Vehicle_List.Count <= 0)
                        {
                            cur_id = 1;
                        }
                        else
                        {
                            max = Vehicle_List[0].m_ID;
                            for (k = 0; k <= Vehicle_List.Count-1; k++)
                            {
                                if (Vehicle_List[k].m_ID > max)
                                {
                                    max = Vehicle_List[k].m_ID;
                                }
                            }
                            cur_id = max + 1;
                        }
                        break;
                    }
                /*	case 4: // W
                        {
                            if (W_List.GetSize() <= 0)
                            {
                                cur_id = 1;
                            } else
                            {
                                max = W_List[0]->OBJ_ID;
                                for (k = 1; k <= W_List.GetUpperBound(); k++)
                                {
                                    if (W_List[k]->OBJ_ID > max)
                                    {
                                        max = W_List[k]->OBJ_ID;
                                    }
                                }
                                cur_id = max + 1;
                            }
                            break;
                        }
                */
                default:
                    break;
            }
            return cur_id;
        }



        private void onButton_Order_Click(object sender, EventArgs e)
        {
            Form2 OrderPage = new Form2();
            OrderPage.Show();
            pictureBoxMap.Invalidate();
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Stop_List.Clear();
            Path_List.Clear();
            pictureBoxMap.Invalidate();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        private void FuntoolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        public void pictureBoxMap_Click(object sender, EventArgs e)
        {
        }



        private void toolStripButton1_Click_2(object sender, EventArgs e)
        {
            Form f2 = new Form4();
            f2.Visible = true;
        }

        

        void AddObj(CPoint point)
        {
            Console.WriteLine($"[AddObj] {m_icon_sel}");
            switch (m_icon_sel)
            {
                case 1: // Station
                    {                        
                        CStop pStop = new CStop();
                        pStop.CSm_cion_sel = m_icon_sel;
                        pStop.m_DrawOrg = m_DrawOrg;
                        CPoint tmp = new CPoint();
                        tmp.X = point.X - m_DrawOrg.X;
                        tmp.Y = point.Y - m_DrawOrg.Y;
                        pStop.AddObj(GetID(10), 0, tmp); // 10: Type Stop -> Station/ Connection; 0: Station Stop
                        Stop_List.Add(pStop);
                        break;
                    }
                case 2: // Connection
                    {
                        CStop pStop = new CStop();
                        pStop.CSm_cion_sel = m_icon_sel;
                        pStop.m_DrawOrg = m_DrawOrg;
                        CPoint tmp = new CPoint();
                        tmp.X = point.X - m_DrawOrg.X;
                        tmp.Y = point.Y - m_DrawOrg.Y;
                        pStop.AddObj(GetID(10), 0, tmp); // 10: Type Stop -> Station/ Connection; 1 Connection Stop
                        Stop_List.Add(pStop);
                        break;
                    }
                case 5: // Path (Line)
                    {
                        int idx = 0;
                        int type = 0;
                        // Check if the cursor inside a Stop 
                        if (CursorInsideStation(point, ref idx, ref type))
                        {
                            // Two Points
                            if (ArcDrawing == 0)
                            {
                                // The fist Stop
                                int tmp_id = idx;
                                tmp_Path.StationIDs[0] = tmp_id;
                                ArcDrawing++;
                            }
                            else if (ArcDrawing == 1)
                            {
                                // Finish
                                int tmp_id = idx;
                                tmp_Path.StationIDs[1] = tmp_id;
                                // Greate a New Path
                                CPath pPath = new CPath();
                                pPath.cpsizebox = sizebox;
                                pPath.m_DrawOrg = m_DrawOrg;
                                // Add a Path with the info of
                                // Path ID, Two Station IDs and a Conner Point
                                CStop tmp1 = Stop_List[tmp_Path.StationIDs[0]];
                                CStop tmp2 = Stop_List[tmp_Path.StationIDs[1]];
                                CPoint tmp3 = new CPoint();
                                int tmp4 = 0; 
                                pPath.AddObj(GetID(20), 0,ref tmp1,ref tmp2, tmp3, tmp4, 0); // 20: Type Path
                                Stop_List[tmp_Path.StationIDs[0]] = tmp1;
                                Stop_List[tmp_Path.StationIDs[1]] = tmp2;
                                Path_List.Add(pPath);
                                // Reset
                                tmp_Path.Reset();
                                ArcDrawing = 0;
                            }
                        } // If the cursor is inside a Stop
                        else
                        {
                            // Reset
                            Console.Beep(1000, 100);
                            tmp_Path.Reset();
                            ArcDrawing = 0;
                            MessageBox.Show("Error!");
                        }
                        break;
                    }
                case 7: // Path (Arc)
                    {
                        int idx = 0;
                        int type = 0;
                        // Check if the cursor inside a Stop 
                        // Two Points
                        if (ArcDrawing == 0 && CursorInsideStation(point, ref idx, ref type))
                        {
                            // The fist Stop
                            tmp_Path.StationIDs[0] = idx;
                            ArcDrawing++;
                        }
                        else if (ArcDrawing == 1 && !CursorInsideStation(point, ref idx, ref type))
                        {
                            // The Corner Point
                            tmp_Path.CornerPoint = point;
                            ArcDrawing++;
                        }
                        else if (ArcDrawing == 2 && CursorInsideStation(point, ref idx, ref type))
                        {
                            // Finish
                            tmp_Path.StationIDs[1] = idx;
                            // Greate a New Path
                            CPath pPath = new CPath();
                            pPath.m_DrawOrg = m_DrawOrg;
                            // Add a Path with the info of
                            // Path ID, Two Station IDs and a Conner Point
                            CPoint tmp = new CPoint();
                            tmp.X = tmp_Path.CornerPoint.X - m_DrawOrg.X;
                            tmp.Y = tmp_Path.CornerPoint.Y - m_DrawOrg.Y;
                            CStop tmp1 = Stop_List[tmp_Path.StationIDs[0]];
                            CStop tmp2 = Stop_List[tmp_Path.StationIDs[1]];

                            pPath.AddObj(GetID(20), 1, ref tmp1, ref tmp2 , tmp, ArcPathRadius, 0); // 20: Type Path
                            Stop_List[tmp_Path.StationIDs[0]] = tmp1;
                            Stop_List[tmp_Path.StationIDs[1]] = tmp2;
                            Path_List.Add(pPath);
                            // Reset
                            tmp_Path.Reset();
                            ArcDrawing = 0;
                        }
                        else
                        {
                            Console.Beep(1000, 100);
                            tmp_Path.Reset();
                            ArcDrawing = 0;
                            MessageBox.Show("Error!");
                        }
                        break;
                    }
                case 6: // Path (Circle)
                    {
                        int idx = 0;
                        int type = 0;
                        // Check if the cursor inside a Stop 
                        // Two Points
                        if (ArcDrawing == 0 && CursorInsideStation(point, ref idx, ref type))
                        {
                            // The fist Stop
                            tmp_Path.StationIDs[0] = idx;
                            ArcDrawing++;
                        }
                        else if (ArcDrawing == 1 && !CursorInsideStation(point, ref idx, ref type))
                        {
                            // The Corner Point
                            tmp_Path.CornerPoint = point;
                            ArcDrawing++;
                        }
                        else if (ArcDrawing == 2 && CursorInsideStation(point, ref idx, ref type))
                        {
                            // Finish
                            tmp_Path.StationIDs[1] = idx;
                            // Greate a New Path
                            CPath pPath = new CPath();
                            pPath.m_DrawOrg = m_DrawOrg;
                            // Add a Path with the info of
                            // Path ID, Two Station IDs and a Conner Point
                            CStop tmp1 = Stop_List[tmp_Path.StationIDs[0]];
                            CStop tmp2 = Stop_List[tmp_Path.StationIDs[1]];
                            CPoint tmp = new CPoint();
                            tmp.X = tmp_Path.CornerPoint.X - m_DrawOrg.X;
                            tmp.Y = tmp_Path.CornerPoint.Y - m_DrawOrg.Y;

                            pPath.AddObj(GetID(20), 2, ref tmp1,ref tmp2, tmp , ArcPathRadius, 0); // 20: Type Path
                            Stop_List[tmp_Path.StationIDs[0]] = tmp1;
                            Stop_List[tmp_Path.StationIDs[1]] = tmp2;
                            Path_List.Add(pPath);
                            // Reset
                            tmp_Path.Reset();
                            ArcDrawing = 0;
                        }
                        else
                        {
                            Console.Beep(1000, 100);
                            tmp_Path.Reset();
                            ArcDrawing = 0;
                            MessageBox.Show("Error!");
                        }
                        break;
                    }
                case 8: // Text
                    {
                        /*			CText* pText;
                                    pText = new CText;
                                    pText->AddObj(GetID(4), "Please enter a caption!", pt);
                                    CDlgText dlg;
                                    dlg.pText = pText;
                                    dlg.DoModal();
                                    if (dlg.b_apply)
                                    {
                                        W_List.Add(dlg.pText);
                                    } else
                                    {
                                        delete pText;
                                    }*/
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            pictureBoxMap.Invalidate(WorkArea);
        }

        //--------------------------------------------------------------------------------
        private void OnButton_DisplayPathInfo_Click(object sender, EventArgs e)
        {
            // Currently not used
            b_DisplayPathInfo = !b_DisplayPathInfo;
            if (!b_DisplayPathInfo)
            {
                Cap_ID = 0;
                CapRect = new CRect(0, 0, 0, 0);
            }
            onButtonUpdate();
            pictureBoxMap.Invalidate(WorkArea);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)//Deleat
            {
                if (Cap_Type > 0)
                {
                    switch (Cap_Type)
                    {
                        case 10: // Stop Type
                            {
                                for (int i = 0; i < Path_List.Count; i++)
                                {
                                    if (Path_List[i].pStopStart.m_ID == Stop_List[Cap_ID].m_ID ||
                                        Path_List[i].pStopEnd.m_ID == Stop_List[Cap_ID].m_ID)
                                    {
                                        Path_List.RemoveAt(i);
                                        i--;
                                    }
                                }
                                // delete Stop_List[Cap_ID];
                                Stop_List.RemoveAt(Cap_ID);
                                break;
                            }
                        case 20:
                            {
                                if (Cap_ID >= 0)
                                {
                                    //delete Path_List[Cap_ID];
                                    Path_List.RemoveAt(Cap_ID);
                                }
                                break;
                            }
                        case 30:
                            {
                                if (Cap_ID >= 0)
                                {
                                    //delete Vehicle_List[Cap_ID];
                                    Vehicle_List.RemoveAt(Cap_ID);
                                }
                                break;
                            }
                        default:
                            break;
                    }
                    CapRect = new CRect(0, 0, 0, 0);
                    Cap_ID = 0;
                    Cap_Type = 0;
                    pictureBoxMap.Invalidate(WorkArea);
                }
            }
            else if (e.KeyCode == Keys.Escape)//ESC
            {
                CapRect = new CRect(0, 0, 0, 0);
                Cap_ID = 0;
                Cap_Type = 0;
                b_AddVehcile = false;
                for (int i = 0; i < Path_List.Count; i++)
                {
                    Path_List[i].b_Selected = false;
                }
                pictureBoxMap.Invalidate(WorkArea);
            }

            else if (e.KeyCode == Keys.Left)//Deleat
            {
                if (!b_DisplayPathInfo)
                {
                  /*  
                   *  m_DrawOrg.x -= 20;
                    UpdateObjsOrgPosition(m_DrawOrg);
                    */
                }
                else
                {
                    if (Cap_Type > 0 && Cap_Type == 10)
                    {
                        Stop_List[Cap_ID].m_center.X -= grid_x;
                        CapRect.X = CapRect.X - grid_x;
                        onButton_FitGrid_Click(this,e);
                    }
                }
                
                pictureBoxMap.Invalidate(WorkArea);
            }

            else if (e.KeyCode == Keys.Right)//Deleat
            {
                if (!b_DisplayPathInfo)
                {
                    /*  
                     *  m_DrawOrg.x += 20;
                      UpdateObjsOrgPosition(m_DrawOrg);
                      */
                }
                else
                {
                    if (Cap_Type > 0 && Cap_Type == 10)
                    {
                        Stop_List[Cap_ID].m_center.X += grid_x;
                        CapRect.X = CapRect.X + grid_x;
                        onButton_FitGrid_Click(this, e);
                    }
                }

                pictureBoxMap.Invalidate(WorkArea);
            }

            else if (e.KeyCode == Keys.Up)//Deleat
            {
                if (!b_DisplayPathInfo)
                {
                    /*  
                     *  m_DrawOrg.y -= 20;
                      UpdateObjsOrgPosition(m_DrawOrg);
                      */
                }
                else
                {
                    if (Cap_Type > 0 && Cap_Type == 10)
                    {
                        Stop_List[Cap_ID].m_center.Y -= grid_y;
                        CapRect.Y = CapRect.Y - grid_y;
                        onButton_FitGrid_Click(this, e);
                    }
                }

                pictureBoxMap.Invalidate(WorkArea);
            }

            else if (e.KeyCode == Keys.Down)//Deleat
            {
                if (!b_DisplayPathInfo)
                {
                    /*  
                     *  m_DrawOrg.y += 20;
                      UpdateObjsOrgPosition(m_DrawOrg);
                      */
                }
                else
                {
                    if (Cap_Type > 0 && Cap_Type == 10)
                    {
                        Stop_List[Cap_ID].m_center.Y += grid_y;
                        CapRect.Y = CapRect.Y + grid_y;
                        onButton_FitGrid_Click(this, e);
                    }
                }

                pictureBoxMap.Invalidate(WorkArea);
            }
        }

        

        private void DrawGrid(PaintEventArgs e)
        {
            Graphics Map = e.Graphics;

            if (m_grid)
            {
                int c_x = 0, c_y = 0;

 
                Color c = Color.FromArgb(255, 128, 168, 168);
                Color c2 = Color.FromArgb(255, 200, 200, 200);

                Pen newPen = new Pen(c, 1);
                Pen newPen2 = new Pen(c2, 1);
                while (c_x < VIEW_X)
                {
                    c_x = c_x + 5 * grid_x;
                    Map.DrawLine(newPen, c_x, 0, c_x, VIEW_Y);
                }

                while (c_y < VIEW_Y)
                {
                    c_y = c_y + 5 * grid_y;
                    Map.DrawLine(newPen, 0, c_y, VIEW_X, c_y);
                }

                c_x = 0; c_y = 0;
                int gc = 0;
                while (c_x < VIEW_X)
                {
                    gc++;
                    c_x = c_x + grid_x;
                    if (gc == 5)
                        gc = 0;
                    else
                        Map.DrawLine(newPen2, c_x, 0, c_x, VIEW_Y);
                }
                gc = 0;
                while (c_y < VIEW_Y)
                {
                    gc++;
                    c_y = c_y + grid_y;
                    if (gc == 5)
                        gc = 0;
                    else
                        Map.DrawLine(newPen2, 0, c_y, VIEW_X, c_y);
                }
            }

        }

        private void DrawObjs(PaintEventArgs e)
        {
            Graphics Map = e.Graphics;

            int i;
            /*	for (i = 0; i < W_List.GetSize(); i++)   //繪製文字注釋
                {
                    W_List[i]->Draw(pDC);
                }*/

            
            //Console.WriteLine($"Path Size: {Stop_List.Count}");
            for (i = 0; i < Stop_List.Count; i++)   //繪製Place
            {
                Stop_List[i].Draw(Map);
            }
            for (i = 0; i < Path_List.Count; i++)   //繪製Place
            {
                Path_List[i].Draw(Map);
            }
            /*
            for (i = 0; i < A_List.GetSize(); i++)   //繪製Arc
            {
                A_List[i]->Draw(pDC);
            }*/
            // Draw Captured Objects
            if (Cap_Type == 10)
            {
                Color c = Color.FromArgb(255, 255, 0, 0);
                Pen newPen = new Pen(c, 1);
                CRect tmp = CapRect;
                tmp.X = tmp.X + m_DrawOrg.X;
                tmp.Y = tmp.Y + m_DrawOrg.X;
                Map.DrawRectangle(newPen,tmp);
                
            }
            for (i = 0; i < Vehicle_List.Count; i++)   //繪製Transition
            {
                Vehicle_List[i].Draw(Map);
            }
        }

    }
}
