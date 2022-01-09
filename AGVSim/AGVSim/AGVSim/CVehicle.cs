using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CPoint = System.Drawing.Point;
using CRect = System.Drawing.Rectangle;
using CString = System.String;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace AGVSim
{	public class CVehicle
	{
		public CPoint m_PrePos;
		public CPoint[] Bodypts = new CPoint[4];
		public CPoint[] Headpts = new CPoint[4];
		public CPoint[] LiDarpts = new CPoint[4];
		public CPoint[] SideRLiDarpts = new CPoint[4];
		public CPoint[] SideLLiDarpts = new CPoint[4];

		public int m_W, m_L; // Total 2L, 2W for convenience
		public double m_angle, m_init_angle;
		public double m_SlopeLen;
		public bool b_SetResetDraw;
		public bool b_LiDarShow;
		public bool b_Avoidance;
		public bool m_MoveDir;

		public int m_ID;
		public int CurPath;
		public int FromStop;
		public CStop pCurStop;

		public CPath pCurPath;
		public int m_init_stop_id;
		public int ToStop;
		public CPoint m_Pos;
		public double m_pos_X;
		public double m_pos_Y;

		public int m_Type; // 0: Forward Move; 1: Dual Direction Move
		public int m_status;
		// 0: No Job at Home; 1: Mission to Start; 2: Mission Loading; 
		// 3: Mission to End; 4: Mission Unloading; 5: Mission to Home; 6: Failure 
		public CRect m_rect;
		public bool b_MovingDir; // 0: The same with Original Set; // 1: Opposite to Original Set
		public List<int> PathToFromSet =  new List<int>();
		public List<int> PathToEndSet = new List<int>();
		public List<CPath> ToFromPathSet = new List<CPath>();
		public List<CPath> ToEndPathSet= new List<CPath>();
		public double m_Speed;
		public double m_AngularSpeed; // deg/sec
		public double m_cur_length;
		public int A_Star_StartNode, A_Star_EndNode;
		public bool A_Star_StartNode_Selected, A_Star_EndNode_Selected;
		public bool b_PathOpen, b_StopOpen, b_RotateDone;
		public double tolerance;
		public double m_moving_ang;
		public int m_TotalOrders;
		public CRect m_SelRect, m_CurRect;
		public bool b_Selected;
		public bool m_UpdateDuaWayPath;
		public bool b_RealeaseStop;
		public int m_SafetyDist;
		public CPoint m_Head;
		public CPoint m_PreStatePos;
		public int m_DeadlockTime;
		public double tmp_x, tmp_y;
		// Modify 2020/02/17 ---------Start-------------------------
		public CPoint m_DrawOrg;
		// Modify 2020/02/17 ---------End  -------------------------
		public CString str;
		public Color StringColor;
		public Pen SelectPen;
		public Brush SelectBrush;
		//Image img = global::AGVSim.Properties.Resources.pic_difcar;
		//Image img = Image.FromFile(@"D:\0304\派車模擬系統\AGVSim\icon\pic_difcar.png"); //要貼的圖 
		Image img = Image.FromFile(Param.AGV_img_path); //要貼的圖

		// Modify 2020/03/15 ---------Order-------------------------
		public COrder pOrder;
		public int Cur_Route_Order_ID;
		public double move_length;
		// Modify 2020/03/15 ---------Order-------------------------



		public CVehicle()
		{
			m_ID = 0;
			CurPath = 0;
			FromStop = 0;
			ToStop = 0;
			m_Pos = new CPoint(0, 0);
			m_SelRect = new CRect(0, 0, 30, 20);
			m_CurRect = new CRect(0, 0, 0, 0);
			m_angle = 0;
			m_MoveDir = false; // 0: Forward; 1: Reverse
			pCurPath = null;
			pCurStop = null;
			m_W = 10;
			m_L = 16;
			m_SlopeLen = Math.Sqrt(m_W * m_W + m_L * m_L);
			m_Type = 0;
			m_cur_length = 0;
			b_PathOpen = false;
			b_StopOpen = false;
			tolerance = 5.0f;
			b_RotateDone = false;
			A_Star_StartNode = 0;
			A_Star_EndNode = 0;
			m_TotalOrders = 0;
			b_Selected = false;
			m_UpdateDuaWayPath = false;
			b_RealeaseStop = false;
			m_SafetyDist = 30;
			move_length = 0;
		}



		public void AvoidReturn()
		{
			double x, y;
			m_PrePos = m_Pos;
			x = (double)(Bodypts[0].X - Bodypts[3].X) / GetDist(Bodypts[3], Bodypts[0]);
			y = (double)(Bodypts[0].Y - Bodypts[3].Y) / GetDist(Bodypts[3], Bodypts[0]);
			double tmpx = m_pos_X - x * 3 * m_W;
			double tmpy = m_pos_Y - y * 3 * m_W;
			m_Pos = new CPoint((int)tmpx, (int)tmpy);
			GetRegions();
			b_SetResetDraw = true;
		}

		public void AvoidSet()
		{
			double x, y;
			m_PrePos = m_Pos;
			x = (double)(Bodypts[0].X - Bodypts[3].X) / GetDist(Bodypts[3], Bodypts[0]);
			y = (double)(Bodypts[0].Y - Bodypts[3].Y) / GetDist(Bodypts[3], Bodypts[0]);
			double tmpx = m_pos_X + x * 3 * m_W;
			double tmpy = m_pos_Y + y * 3 * m_W;
			m_Pos = new CPoint((int)tmpx, (int)tmpy);
			GetRegions();
			b_SetResetDraw = true;
			//AddObj(5,ref a,5)
		}

		public void AddObj(int id, ref CStop pStop, int type, int angle, double speed, double angular_speed)
		{
			m_Pos = pStop.m_center;
			m_PreStatePos = m_Pos;
			m_pos_X = m_Pos.X;
			m_pos_Y = m_Pos.Y;
			m_Speed = speed;
			m_AngularSpeed = angular_speed;
			m_ID = id;
			m_angle = 0.0f;
			pCurPath = null;
			m_init_angle = angle;
			pCurStop = pStop;
			m_init_stop_id = pStop.m_ID;
			m_status = 0;
			b_MovingDir = false;
			m_rect = new Rectangle(0,0, (int)(2 * m_L)*2, (int)(2 * m_L)*2);
			m_Type = type;
			b_Avoidance = false;
			m_DeadlockTime = 0;
			GetRegions();
			b_LiDarShow = false;
		}


		double GetDist(CPoint P1, CPoint P2)
		{
			return (Math.Sqrt((P1.X - P2.X) * (P1.X - P2.X) + (P1.Y - P2.Y) * (P1.Y - P2.Y)));
		}
		double GetRadius(double deg)
		{
			return Math.PI * deg / 180.0f;
		}

		void GetRegions()
		{
			m_Pos = new CPoint((int)m_pos_X, (int)m_pos_Y);
			// Get Main Body Region
			// Pts 0 and 2
			double x, y;
			CPoint tmp_pos;
			double init_ang = GetRadius(m_angle) + Math.Atan2(m_W, m_L);
			x = m_SlopeLen * Math.Cos(init_ang);
			y = -m_SlopeLen * Math.Sin(init_ang);
			tmp_pos = new CPoint((int)x, (int)y);
			Bodypts[0] = new CPoint(m_Pos.X + tmp_pos.X, m_Pos.Y + tmp_pos.Y);
			x = m_SlopeLen * Math.Cos(Math.PI + init_ang);
			y = -m_SlopeLen * Math.Sin(Math.PI + init_ang);
			tmp_pos = new CPoint((int)x, (int)y);
			Bodypts[2] = new CPoint(m_Pos.X + tmp_pos.X, m_Pos.Y + tmp_pos.Y);
			// Pts 1 and 3
			init_ang = GetRadius(m_angle) + Math.PI - Math.Atan2(m_W, m_L);
			x = m_SlopeLen * Math.Cos(init_ang);
			y = -m_SlopeLen * Math.Sin(init_ang);
			tmp_pos = new CPoint((int)x, (int)y);
			Bodypts[1] = new CPoint(m_Pos.X + tmp_pos.X, m_Pos.Y + tmp_pos.Y);
			x = m_SlopeLen * Math.Cos(Math.PI + init_ang);
			y = -m_SlopeLen * Math.Sin(Math.PI + init_ang);
			tmp_pos = new CPoint((int)x, (int)y);
			Bodypts[3] = new CPoint(m_Pos.X + tmp_pos.X, m_Pos.Y + tmp_pos.Y);
			// Head Polygon
			double m_slopelen;
			double m_offset = 0.6;
			double m_l = 0.2 * m_L;
			double m_w = 0.8 * m_W;
			x = m_L * m_offset * Math.Cos(GetRadius(m_angle));
			y = -m_L * m_offset * Math.Sin(GetRadius(m_angle));
			m_Head = new CPoint(m_Pos.X + (int)x, m_Pos.Y + (int)y);
			m_slopelen = Math.Sqrt(m_w * m_w + m_l * m_l);
			// Pts 0 and 2
			init_ang = GetRadius(m_angle) + Math.Atan2(m_w, m_l);
			x = m_slopelen * Math.Cos(init_ang);
			y = -m_slopelen * Math.Sin(init_ang);
			tmp_pos = new CPoint((int)x, (int)y);
			Headpts[0] = new CPoint(m_Head.X + tmp_pos.X, m_Head.Y + tmp_pos.Y);
			x = m_slopelen * Math.Cos(Math.PI + init_ang);
			y = -m_slopelen * Math.Sin(Math.PI + init_ang);
			tmp_pos = new CPoint((int)x, (int)y);
			Headpts[2] = new CPoint(m_Head.X + tmp_pos.X, m_Head.Y + tmp_pos.Y);
			// Pts 1 and 3
			init_ang = GetRadius(m_angle) + Math.PI - Math.Atan2(m_w, m_l);
			x = m_slopelen * Math.Cos(init_ang);
			y = -m_slopelen * Math.Sin(init_ang);
			tmp_pos = new CPoint((int)x, (int)y);
			Headpts[1] = new CPoint(m_Head.X + tmp_pos.X, m_Head.Y + tmp_pos.Y);
			x = m_slopelen * Math.Cos(Math.PI + init_ang);
			y = -m_slopelen * Math.Sin(Math.PI + init_ang);
			tmp_pos = new CPoint((int)x, (int)y);
			Headpts[3] = new CPoint(m_Head.X + tmp_pos.X, m_Head.Y + tmp_pos.Y);
			// Get Front LiDar Region
			// m_SafetyDist
			LiDarpts[1] = Bodypts[0];
			LiDarpts[2] = Bodypts[3];
			x = (double)(Bodypts[0].X - Bodypts[1].X) / GetDist(Bodypts[1], Bodypts[0]);
			y = (double)(Bodypts[0].Y - Bodypts[1].Y) / GetDist(Bodypts[1], Bodypts[0]);
			tmp_pos = new CPoint(Bodypts[0].X + (int)(x * m_SafetyDist), Bodypts[0].Y + (int)(y * m_SafetyDist));
			LiDarpts[0] = tmp_pos;
			x = (double)(Bodypts[3].X - Bodypts[2].X) / GetDist(Bodypts[2], Bodypts[3]);
			y = (double)(Bodypts[3].Y - Bodypts[2].Y) / GetDist(Bodypts[2], Bodypts[3]);
			tmp_pos = new CPoint(Bodypts[3].X + (int)(x * m_SafetyDist), Bodypts[3].Y + (int)(y * m_SafetyDist));
			LiDarpts[3] = tmp_pos;
			// Get Right-hand-side LiDar Region
			// Point 3 First, exttend from 0
			// Point 2 Next, exttend from 1
			x = (double)(Bodypts[0].X - Bodypts[1].X) / GetDist(Bodypts[1], Bodypts[0]);
			y = (double)(Bodypts[0].Y - Bodypts[1].Y) / GetDist(Bodypts[1], Bodypts[0]);
			SideRLiDarpts[0] = new CPoint(Bodypts[3].X + (int)(x * 1.2 * m_SafetyDist), Bodypts[3].Y + (int)(y * 1.2 * m_SafetyDist));
			SideRLiDarpts[1] = new CPoint(Bodypts[2].X - (int)(x * 0.8 * m_SafetyDist), Bodypts[3].Y - (int)(y * 0.8 * m_SafetyDist));
			// Point 0 , exttend from 3'
			// Point 1 , exttend from 2'
			x = (double)(Bodypts[0].X - Bodypts[3].X) / GetDist(Bodypts[3], Bodypts[0]);
			y = (double)(Bodypts[0].Y - Bodypts[3].Y) / GetDist(Bodypts[3], Bodypts[0]);
			SideRLiDarpts[3] = new CPoint(SideRLiDarpts[0].X - (int)(x * 4 * m_W), SideRLiDarpts[0].Y - (int)(y * 4 * m_W));
			SideRLiDarpts[2] = new CPoint(SideRLiDarpts[1].X - (int)(x * 4 * m_W), SideRLiDarpts[0].Y - (int)(y * 4 * m_W));
			// Get Left-hand-side LiDar Region
			// Point 3 First, exttend from 0
			// Point 2 Next, exttend from 1
			x = (double)(Bodypts[0].X - Bodypts[1].X) / GetDist(Bodypts[1], Bodypts[0]);
			y = (double)(Bodypts[0].Y - Bodypts[1].Y) / GetDist(Bodypts[1], Bodypts[0]);
			SideLLiDarpts[3] = new CPoint(Bodypts[0].X + (int)(x * 1.2 * m_SafetyDist), Bodypts[0].Y + (int)(y * 1.2 * m_SafetyDist));
			SideLLiDarpts[2] = new CPoint(Bodypts[1].X - (int)(x * 0.8 * m_SafetyDist), Bodypts[1].Y - (int)(y * 0.8 * m_SafetyDist));
			// Point 0 , exttend from 3'
			// Point 1 , exttend from 2'
			x = (double)(Bodypts[0].X - Bodypts[3].X) / GetDist(Bodypts[3], Bodypts[0]);
			y = (double)(Bodypts[0].Y - Bodypts[3].Y) / GetDist(Bodypts[3], Bodypts[0]);
			SideLLiDarpts[0] = new CPoint(SideLLiDarpts[3].X + (int)(x * 4 * m_W), SideLLiDarpts[3].Y + (int)(y * 4 * m_W));
			SideLLiDarpts[1] = new CPoint(SideLLiDarpts[2].X + (int)(x * 4 * m_W), SideLLiDarpts[2].Y + (int)(y * 4 * m_W));
		}


		int DetectObstacles()
		{
			return 0; // ID of AGV
		}

		public bool LoadUnloading()
		{
			if (pCurStop != null)
			{
				pCurStop.m_RemainLUTime--;
				if (pCurStop.m_RemainLUTime > 0)
				{
					return true;
				}
				else
					return false;
			}
			return false;
		}

		void Say()//CString str, int i, int j, int c
		{
		/*
			CString s;
			s.Format("%s: %d, %d, %d", str, i, j, c);
			AfxMessageBox(s);
		*/
		}

		public bool Rotate()
		{
			if (!b_RotateDone)
			{
				// Normalize to 0-359
				int ang_path = (720 + (int)pCurPath.m_angle) % 360;
				int ang_agv = (720 + (int)m_angle) % 360;
				// if fabs(ang_path - ang_agv) > 180
				// if ang_path - ang_agv < -180; e.g., 3 - 359 = -356, -356+360=4 (CCW)  
				// if ang_path - ang_agv > 180; e.g., 359 - 3 = 356, 356-360=-4 (CW)
				if (Math.Abs(ang_path - ang_agv) <= tolerance)
				{
					// In Path Angle
					b_RotateDone = true;
					return false;
				}
				else if (Math.Abs(ang_path - ang_agv) <= 180)
				{
					// As >0 CCW; < 0 CW
					if (ang_path > ang_agv)
					{
						// Only Rotate CCW
						m_angle += m_AngularSpeed;
						GetRegions();
					}
					else if (ang_path < ang_agv)
					{
						// Only Rotate CW
						m_angle -= m_AngularSpeed;
						GetRegions();
					}
					return true;
				}
				else if (Math.Abs(ang_path - ang_agv) > 180)
				{
					// Inverse As >0 CW; < 0 CCW
					if (ang_path > ang_agv)
					{
						// Only Rotate CCW
						m_angle -= m_AngularSpeed;
						GetRegions();
					}
					else if (ang_path < ang_agv)
					{
						// Only Rotate CW
						m_angle += m_AngularSpeed;
						GetRegions();
					}
					return true;
				}
			}
			return false; // Still Rotating
		}

		public void Move()
		{
			// Direction the same
			m_PreStatePos = m_Pos;

			if (pCurPath.m_type == 0)
			{
				m_pos_X += (m_Speed * pCurPath.m_uVectorXs);
				m_pos_Y += (m_Speed * pCurPath.m_uVectorYs);

				m_cur_length += m_Speed;
				m_angle = pCurPath.AngleSegment[0];
			}
			else if (pCurPath.m_type == 1 || pCurPath.m_type == 2) // Arc or Circular Path
			{
				if (m_cur_length < pCurPath.LengthSegment[0])
				{
					// Line
					m_pos_X += (m_Speed * pCurPath.m_uVectorXs);
					m_pos_Y += (m_Speed * pCurPath.m_uVectorYs);

					m_cur_length += m_Speed;
					m_angle = pCurPath.AngleSegment[0];
				}
				else if (m_cur_length < pCurPath.LengthSegment[1])
				{
					// Circular
					// m_AngularSpeed
					// CircleCenter.x, CircleCenter.y, m_ArcRadius
					// Start Angle pCurPath->ArcStartAngle
					// Angle Range pCurPath->ArcRangeAngle;
					if (pCurPath.ArcRangeAngle < 0)
					{
						m_moving_ang -= m_AngularSpeed; // CW 
						m_angle -= m_AngularSpeed;
					}
					else
					{
						m_moving_ang += m_AngularSpeed; // CCW 
						m_angle += m_AngularSpeed;
					}
					m_pos_X = pCurPath.CircleCenter.X + pCurPath.m_ArcRadius * Math.Cos(GetRadius(m_moving_ang));
					m_pos_Y = pCurPath.CircleCenter.Y - pCurPath.m_ArcRadius * Math.Sin(GetRadius(m_moving_ang));
					m_cur_length += Math.Abs(pCurPath.m_ArcRadius * GetRadius(m_AngularSpeed));
				}
				else
				{
					// Line
					m_pos_X +=  (m_Speed * pCurPath.m_uVectorXe);
					m_pos_Y +=  (m_Speed * pCurPath.m_uVectorYe);
	
					m_cur_length += m_Speed;
					m_angle = pCurPath.AngleSegment[1];
				}
			}

			m_Pos = new CPoint((int)m_pos_X, (int)m_pos_Y);
			// Get Body Refion
			// Get LiDar Region
			GetRegions();
		}

		public void Draw(Graphics e)
		{
			CPoint[] m_LiDarpts = new CPoint[4];
			CPoint[] m_SideRLiDarpts = new CPoint[4];
			CPoint[] m_SideLLiDarpts = new CPoint[4];
			CPoint m_tag = new CPoint(-m_L, m_W - 40);
			CPoint[] m_Bodypts = new CPoint[4];
			CPoint[] m_Headpts = new CPoint[4];
			for (int i = 0; i < 4; i++)
			{
				m_Bodypts[i].X = Bodypts[i].X + m_DrawOrg.X;
				m_Bodypts[i].Y = Bodypts[i].Y + m_DrawOrg.Y;
				m_Headpts[i].X = Headpts[i].X + m_DrawOrg.X;
				m_Headpts[i].Y = Headpts[i].Y + m_DrawOrg.Y;
			} // loop i
			if (b_Avoidance)
				StringColor = Color.FromArgb(255, 255, 0, 0);
			else
				StringColor = Color.FromArgb(255, 0, 128, 64);

			//pDC->SetBkMode(TRANSPARENT);

			Color c1 = Color.FromArgb(255, 128, 0, 0);
			Color c2 = Color.FromArgb(255, 128, 255, 255);
			Color c3 = Color.FromArgb(255, 255, 255, 0);
			Color c4 = Color.FromArgb(255, 0, 255, 0);
			Color c5 = Color.FromArgb(255, 128, 0, 128);
			Color c6 = Color.FromArgb(255, 255, 0, 0);

			Pen penBody = new Pen(c1, 1);
			Brush brushBody = new SolidBrush(c2);
			Brush brushIdle = new SolidBrush(c3);
			Brush brushMission = new SolidBrush(c4);
			Brush brushLoadUnLoad = new SolidBrush(c5);
			Brush brushFailure = new SolidBrush(c6);
			Pen pOldPen = penBody; // Draw Body
			Brush pOldBrush;
			SelectPen = penBody;
			// Draw Body
			m_tag.X = m_Pos.X + m_tag.X;
			m_tag.Y = m_Pos.Y + m_tag.Y;

			m_CurRect = m_SelRect;
			m_CurRect.X = m_tag.X + m_SelRect.X;
			m_CurRect.Y = m_tag.Y + m_SelRect.Y;
			//---------------------------------------------test picture pase
			float dx = m_CurRect.X;// - (float)((img.Width / 2) * Math.Cos(m_angle * Math.PI / 180.0f));
			float dy = m_CurRect.Y;// + (float)((img.Height/ 2) * Math.Sin(m_angle * Math.PI / 180.0f));
			dx = (m_Bodypts[0].X + m_Bodypts[1].X + m_Bodypts[2].X + m_Bodypts[3].X) / 4.0f;
			dy = (m_Bodypts[0].Y + m_Bodypts[1].Y + m_Bodypts[2].Y + m_Bodypts[3].Y) / 4.0f;
			//Bitmap bmp = new Bitmap(img.Width, img.Height);
			//bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);
			//e = Graphics.FromImage(bmp);
			//Console.WriteLine(img.Width + " " + img.Height);
			//e.DrawArc(SelectPen,new Rectangle((int)(dx-10),(int)(dy-10),20,20),0,360);
			
			e.TranslateTransform(dx, dy);
			e.RotateTransform((float)(360-m_angle));
			e.TranslateTransform(-dx, -dy);
			//e.InterpolationMode = InterpolationMode.HighQualityBicubic;
			e.DrawImage(img, dx-17,dy-15);
			e.ResetTransform();
	
			//-------------------------------------------------------------
			if (b_Selected)
			{
				CRect tmpRect = new CRect();
				tmpRect = m_CurRect;
				tmpRect.X = m_CurRect.X + m_DrawOrg.X;
				tmpRect.Y = m_CurRect.Y + m_DrawOrg.Y;
				SelectPen = new Pen(brushBody);
				//e.DrawRectangle(SelectPen, tmpRect);
			}
			if (b_LiDarShow)
			{
				for (int s = 0; s < 4; s++)
				{
					m_LiDarpts[s].X = LiDarpts[s].X + m_DrawOrg.X;
					m_LiDarpts[s].Y = LiDarpts[s].Y + m_DrawOrg.Y;
					
					m_SideRLiDarpts[s].X = SideRLiDarpts[s].X + m_DrawOrg.X;
					m_SideRLiDarpts[s].Y = SideRLiDarpts[s].Y + m_DrawOrg.Y;
					
					m_SideLLiDarpts[s].X = SideLLiDarpts[s].X + m_DrawOrg.X;
					m_SideLLiDarpts[s].Y = SideLLiDarpts[s].Y + m_DrawOrg.Y;
				} // loop s
				  //pDC->SelectStockObject(NULL_BRUSH);
				//e.DrawPolygon(SelectPen , m_LiDarpts);
				//e.DrawPolygon(SelectPen, m_SideRLiDarpts);
				//e.DrawPolygon(SelectPen, m_SideLLiDarpts);
			}

			//CBrush* pOldBrush = pDC->SelectObject(&brushBody);
			pOldBrush = brushBody; 
			e.DrawPolygon(SelectPen, m_Bodypts);
			//////////////////////////////////////
			// Draw Head
			if (m_status == 0) // Idle
				SelectBrush = brushIdle;
			else if (m_status == 1 || m_status == 3)
				SelectBrush = brushMission;
			else if (m_status == 2 || m_status == 4)
				SelectBrush = brushLoadUnLoad;
			else
				SelectBrush = brushFailure;
			SelectPen = new Pen(SelectBrush);
			//e.DrawPolygon(SelectPen, m_Headpts);


			StringColor = Color.FromArgb(255, 0, 64, 64);
			str = String.Format("[{0:D2}]", m_ID);
			SelectBrush = new SolidBrush(StringColor);
			Font drawFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
			CPoint tmp_P = new CPoint();
			tmp_P.X = m_tag.X + m_DrawOrg.X;
			tmp_P.Y = m_tag.Y + m_DrawOrg.Y;
			e.DrawString(str, drawFont, SelectBrush, tmp_P.X, tmp_P.Y);

			SelectPen = pOldPen;
			SelectBrush = pOldBrush;
			StringColor = Color.FromArgb(255, 0, 0, 0);
			if (Param.SQL_Update_AGV_Status)
				sqlserver.Update_AGV_State(this);
		}



		//**************************************************************Order*****************************************//
	
		public void CheckInOrder(COrder m_pOder)
		{
			COrder pOrder = new COrder();
			pOrder.m_Order_ID = m_pOder.m_Order_ID;
			pOrder.m_Product_ID = m_pOder.m_Product_ID;
			pOrder.m_workPieceID = m_pOder.m_workPieceID;
			pOrder.m_Quantity = m_pOder.m_Quantity;
			pOrder.m_Due = m_pOder.m_Due;
			pOrder.m_Done = m_pOder.m_Done;
			pOrder.m_OdrStatus = m_pOder.m_OdrStatus;
			pOrder.cur_unique_id = m_pOder.cur_unique_id;
			pOrder.cur_process_id = m_pOder.cur_process_id;
			pOrder.cur_stop_id = m_pOder.cur_stop_id;
			for(int i = 0; i < m_pOder.all_machines.Count; i++)
            {
				pOrder.all_machines.Add(m_pOder.all_machines[i]);
				pOrder.all_types.Add(m_pOder.all_types[i]);
            }
			pOrder.AddRoute(m_pOder.pRoute);
			move_length = 0;
		}

		public void CheckOutOrder()
		{
			pOrder.ReleaseRoute();
			pOrder.all_machines.Clear();
			pOrder.all_types.Clear();
			pOrder = null;
			
		}

		//**************************************************************Order*****************************************//

	}
}
