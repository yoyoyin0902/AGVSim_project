using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPoint = System.Drawing.Point;
using CRect = System.Drawing.Rectangle;
using System.Drawing;
using CString = System.String;


namespace AGVSim
{
    public class CConnection
	{
        
		// Attributes
		public int m_ID;
		public int m_size = 10;
		public int m_offest = 2;
		public int m_type; // 0: Station; 1: Connection
		public double HeuristicDist;
		// CPoint m_pos;
		public int m_FromToStatus = 0; // 0: None; 1: From; 2: To
		public int m_LUTime = 60;
		public int m_RemainLUTime;
		public int m_FromToShow = 0;
		public int m_AGV_ID = 0;
		public bool m_Failure = false;
		public CPoint m_center;
		public CRect m_obj_rect, m_obj_rect2, m_rect_station, m_rect_station2, m_rect_station_large, m_obj_rect_large, m_TagRect, TextR;
		// Modify 2020/02/17 ---------Start-------------------------
		public int m_ArcNumber;
		public CPoint m_DrawOrg;
		// Modify 2020/02/17 ---------End  -------------------------
		public Pen SelectPen;
		public Brush SelectBrush;
		public CRect tmp;
		public CString cap, cat;

		public CString m_msg;
		public List<double> AdjacentCost = new List<double>();
		public List<int> AdjacentPathID = new List<int>();
		public List<int> AdjacentConnectionID = new List<int>();
		public List<int> PARTS_QUEUE = new List<int>();
		//Image img = global::AGVSim.Properties.Resources.pic_Station;
		Image img = Image.FromFile(Param.connection_img_path); //要貼的圖 




		public CConnection()
		{
			m_rect_station = new CRect(-m_size, -m_size, m_size * 2, m_size * 2);
			m_rect_station2 = new CRect(-m_size + m_offest, -m_size + m_offest, (m_size - m_offest) * 2, (m_size - m_offest) * 2);
			m_rect_station_large = new CRect(-m_size, -m_size, (m_size + 2 * m_offest) * 2, (m_size + 2 * m_offest) * 2);
			TextR = new CRect(0, 0, 50, 20);
		}

		public void Fit(int x, int y)
		{
			int sx = m_center.X % x;
			int sy = m_center.Y % y;
			CPoint shift = new CPoint(sx, sy);
			m_center.X = m_center.X - shift.X;
			m_center.Y = m_center.Y - shift.Y;

			m_obj_rect.X = m_center.X - m_size;
			m_obj_rect.Y = m_center.Y - m_size;

			m_obj_rect2.X = m_center.X - m_size + m_offest;
			m_obj_rect2.Y = m_center.Y - m_size + m_offest;

			m_obj_rect_large.X = m_center.X;
			m_obj_rect_large.Y = m_center.Y;

			m_TagRect = TextR;
			m_TagRect.X = m_obj_rect.Left + 5 + TextR.X;
			m_TagRect.Y = m_obj_rect.Bottom + 4 + TextR.Y;
		}


		public void AddObj(int id, int type, CPoint pt)
		{
			m_ID = id;
			m_center = pt;
			m_type = type;

			m_rect_station.Offset(pt);
			m_obj_rect = m_rect_station;

			m_rect_station2.Offset(pt);
			m_obj_rect2 = m_rect_station2;

			m_rect_station_large.Offset(pt);
			m_obj_rect_large = m_rect_station_large;

			TextR.Offset(new Point(m_obj_rect.Left + 5, m_obj_rect.Bottom + 4));
			m_TagRect = TextR;
		}

		public void Draw(Graphics e)
		{

			Color c = Color.FromArgb(255, 0, 0, 0);
			Color c2 = Color.FromArgb(255, 255, 0, 255);
			Color c3 = Color.FromArgb(255, 128, 0, 0);
			Color c4 = Color.FromArgb(255, 255, 128, 0);
			Color c5 = Color.FromArgb(255, 255, 0, 128);
			Color c6 = Color.FromArgb(255, 170, 170, 80);

			Pen newPen_none = new Pen(c, 1);
			Pen newPen_start = new Pen(c2, 1);
			Pen newPen_end = new Pen(c3, 1);
			Pen newPen_failure = new Pen(c6, 1);
			Brush brushFromNode = new SolidBrush(c4);
			Brush brushEndNode = new SolidBrush(c5);
			Brush brushnull = new SolidBrush(c);

			Color StringColor = Color.FromArgb(255, 64, 0, 128);


			//pDC->SetBkMode(TRANSPARENT);

			if (m_FromToStatus == 0)
				SelectPen = newPen_none;
			else if (m_FromToStatus == 1) // Start node
				SelectPen = newPen_start;
			else if (m_FromToStatus == 2) // End node
				SelectPen = newPen_end;
			// Just Testing for Failure Node: Tunnel Subtree Nodes
			// Modify 2020/02/16 ---------Start-------------------------
			if (m_Failure)
			{
				// Draw Station as a Dash Line
				SelectPen = newPen_failure;
			}
			// Modify 2020/02/16 ---------End-------------------------
			if (m_FromToShow == 0)
			{
				SelectBrush = brushnull;
			}
			else if (m_FromToShow == 1)
			{
				SelectBrush = brushFromNode;
			}
			else if (m_FromToShow == 2)
			{
				SelectBrush = brushEndNode;
			}

			cap = String.Format("{0:D2}-{1:D}", m_ID, m_AGV_ID, StringColor);

			tmp = m_obj_rect;
			tmp.X = m_obj_rect.X + m_DrawOrg.X;
			tmp.Y = m_obj_rect.Y + m_DrawOrg.Y;


			//-----------------------------------------------------------------------------------------

			e.DrawImage(img, tmp.X, tmp.Y);
			e.ResetTransform();

			//-----------------------------------------------------------------------------------------

			//e.DrawEllipse(SelectPen, tmp);
			Font drawFont = new System.Drawing.Font("Arial", 10);
			e.DrawString(cap, drawFont, SelectBrush, m_obj_rect.Left + 5 + m_DrawOrg.X - 10, m_obj_rect.Bottom + 4 + m_DrawOrg.Y);

			switch (m_type)
			{
				case 0: // Station
					{
						cat = "S";
						tmp = m_obj_rect2;
						tmp.X = m_obj_rect2.X + m_DrawOrg.X;
						tmp.Y = m_obj_rect2.Y + m_DrawOrg.Y;

						//e.DrawEllipse(SelectPen, tmp);
						break;
					}
				case 1: // Connection
					{
						cat = "C";
						break;
					}
				default:
					break;
			} // End Switch
			  //	e.DrawString(cat, drawFont, SelectBrush, m_obj_rect.Right - 1 + m_DrawOrg.X , m_obj_rect.Top - 1 + m_DrawOrg.Y);
			SelectPen = newPen_none;
			StringColor = Color.FromArgb(255, 0, 0, 0);
			// ShowTokens(pDC);
		}
	}
}

