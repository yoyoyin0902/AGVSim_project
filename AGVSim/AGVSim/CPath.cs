using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPoint = System.Drawing.Point;
using CRect = System.Drawing.Rectangle;
using System.Drawing;
using CString = System.String;
using System.Windows.Forms;


namespace AGVSim
{
	public class CPath
	{

		public CPoint CornerPoint; // Only for Arc Path
		public CPoint[] TerminatePoints = new CPoint[2];
		public int[] StationIDs = new int[2];
		public CStop pStopStart;
		public CStop pStopEnd;
		public int m_type;
		public int m_ID;
		public int m_direction; // 0: [0] -> [1]; 1: [1] -> [0]; 2: dual-way
		public double Length; // Cost for A* Search
		public double m_angle;
		public double[] LengthSegment = new double[2];
		public double[] AngleSegment = new double[2];
		public bool b_Selected;
		public int m_OnPath; // 0: Noe; 1: To From; 2: To End
		public int m_CheckTrafficCntlDist;
		public int m_RuningDirection;
		public CRect TextR;
		public List<int> OnPathAGVs = new List<int>();
		public CStop pOrgStopStart;
		public CStop pOrgStopEnd;
		public CPoint ArrowHead, ArrowTail1, ArrowTail2;
		public CPoint CircleCenter;
		public CPoint[] CornerNeighbors = new CPoint[2];
		public CRect m_TagRect;
		public int m_ArcRadius;
		public double ArcStartAngle, ArcRangeAngle;
		public CRect m_small_circle, m_obj_rect;
		public double m_uVectorXs, m_uVectorYs;
		public double m_uVectorXe, m_uVectorYe;
		public CPoint m_CheckTrafficPt, m_CheckTrafficPtDual;
		public CPoint temPT;
		public Pen SelectPen;
		public CRect tmp;
		// Modify 2020/02/17 ---------Start-------------------------
		public CPoint m_DrawOrg;
		// Modify 2020/02/17 ---------End  -------------------------
		public Color StringColor;
		public Brush brushString;
		public CPoint tmp_point;
		public CRect tmp_Arc;
		public CString ss;


		public CPath()
		{
			Reset();
		}

		public void Reset()
		{
			CornerPoint = new CPoint(0, 0);
			TerminatePoints[0] = new CPoint(0, 0);
			TerminatePoints[1] = new CPoint(0, 0);
			StationIDs[0] = 0;
			StationIDs[1] = 0;
			CornerPoint = new CPoint(0, 0);
			m_type = 0;
			pStopStart = null;
			pStopEnd = null;
			m_ID = 0;
			m_small_circle = new CRect(-8, -8, 8, 8);
			m_CheckTrafficPt = new CPoint(0, 0);
			b_Selected = false;
			m_OnPath = 0;
			m_CheckTrafficCntlDist = 40;
			m_RuningDirection = 1;
			TextR = new CRect(0, 0, 50, 20);
		}

		public bool RemoveRuningAGV(int id)
		{
			for (int i = 0; i < OnPathAGVs.Count; i++)
			{
				if (OnPathAGVs[i] == id)
				{
					OnPathAGVs.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		double GetDegree(double rad)
		{
			return 180.0f * rad / Math.PI;
		}

		void Say()//CString str, int i, int j, double c)
		{
			//CString s;
			//s.Format("%s: %d, %d, %f", str, i, j, c);
			//AfxMessageBox(s);
		}

		void ChagePathDirection()
		{

		}

		public void DrawCross(CPoint pt, Graphics e)
		{
			int len = 5;

			Color c1 = Color.FromArgb(255, 128, 64, 0);
			Pen newPen_none = new Pen(c1, 1);
			CPoint[] pts = new CPoint[4];

			pts[0] = new CPoint(pt.X - len, pt.Y);
			pts[1] = new CPoint(pt.X + len, pt.Y);
			pts[2] = new CPoint(pt.X, pt.Y - len);
			pts[3] = new CPoint(pt.X, pt.Y + len);

			e.DrawLine(newPen_none, pts[0].X, pts[0].Y, pts[1].X, pts[1].Y);
			e.DrawLine(newPen_none, pts[2].X, pts[2].Y, pts[3].X, pts[3].Y);
		}
		public void Fit()
		{
			int arrow_len = 10;
			CPoint[] centers = new CPoint[2];
			CPoint OffsetVector;
			int dx = 0;
			int dy = 0;
			double uintvect_x = 0;
			double uintvect_y = 0;
			// Get Direction
			if (m_direction == 0)
			{
				// Set Original
				pStopStart = pOrgStopStart;
				pStopEnd = pOrgStopEnd;
				m_RuningDirection = 0;
			}
			else if (m_direction == 1)
			{
				// Change Start/ End Stop Pointers
				pStopStart = pOrgStopEnd;
				pStopEnd = pOrgStopStart;
				m_RuningDirection = 1;
			}
			else if (m_direction == 2)
			{
				// Activated by A* Dynamic Allocation
				if (pStopStart == pOrgStopStart)
				{
					m_RuningDirection = 0;
				}
				else
				{
					m_RuningDirection = 1;
				}
			}
			// Get Two Station Centers
			centers[0] = pStopStart.m_center;
			centers[1] = pStopEnd.m_center;
			if (m_type == 0) // Line Path
			{
				// Get Vetcor [0] -> [1]
				dx = centers[1].X - centers[0].X;
				dy = centers[1].Y - centers[0].Y;
				uintvect_x = (double)dx / GetLength(dx, dy);
				uintvect_y = (double)dy / GetLength(dx, dy);
				m_uVectorXs = uintvect_x;
				m_uVectorYs = uintvect_y;
				OffsetVector = new CPoint((int)(pStopStart.m_size * uintvect_x), (int)(pStopStart.m_size * uintvect_y));
				// TerminatePoints[0] = pStopStart->m_center + OffsetVector;
				// TerminatePoints[1] = pStopEnd->m_center - OffsetVector;
				TerminatePoints[0].X = pStopStart.m_center.X + OffsetVector.X;
				TerminatePoints[0].Y = pStopStart.m_center.Y + OffsetVector.Y;
				TerminatePoints[1].X = pStopEnd.m_center.X - OffsetVector.X;
				TerminatePoints[1].Y = pStopEnd.m_center.Y - OffsetVector.Y;
				Length = GetLength(dx, dy);
				m_angle = GetDegree(Math.Atan2(-m_uVectorYs, m_uVectorXs));
				AngleSegment[0] = m_angle;
				// Get Arrow Coordinates
				// Arrow Head at the Center 
				// ArrowHead, ArrowTail1, ArrowTail2
				ArrowHead = new CPoint((int)((pStopStart.m_center.X + pStopEnd.m_center.X) / 2.0f),
					(int)((pStopStart.m_center.Y + pStopEnd.m_center.Y) / 2.0f));
				temPT.X = ArrowHead.X - (int)(arrow_len * uintvect_x);
				temPT.Y = ArrowHead.Y - (int)(arrow_len * uintvect_y);

				ArrowTail1.X = temPT.X + (int)(0.5 * arrow_len * uintvect_y);
				ArrowTail1.Y = temPT.Y + (int)(0.5 * arrow_len * -uintvect_x);

				ArrowTail2.X = temPT.X - (int)(0.5 * arrow_len * uintvect_y);
				ArrowTail2.Y = temPT.Y - (int)(0.5 * arrow_len * -uintvect_x);
				// m_CheckTrafficPt m_CheckTrafficCntlDist
				if (Length > m_CheckTrafficCntlDist)
				{
					OffsetVector = new CPoint((int)(m_CheckTrafficCntlDist * -uintvect_x), (int)(m_CheckTrafficCntlDist * -uintvect_y));
					m_CheckTrafficPt.X = TerminatePoints[1].X + OffsetVector.X;
					m_CheckTrafficPt.Y = TerminatePoints[1].Y + OffsetVector.Y;

					OffsetVector = new CPoint((int)(m_CheckTrafficCntlDist * uintvect_x), (int)(m_CheckTrafficCntlDist * uintvect_y));
					m_CheckTrafficPtDual.X = TerminatePoints[0].X + OffsetVector.X;
					m_CheckTrafficPtDual.Y = TerminatePoints[0].Y + OffsetVector.Y;

				}
				m_TagRect.X = ((TerminatePoints[0].X + TerminatePoints[1].X) / 2) + TextR.X;
				m_TagRect.Y = ((TerminatePoints[0].Y + TerminatePoints[1].Y) / 2) + TextR.Y;
			}
			else if (m_type == 1) // Arc Path
			{
				// Get Line Angles
				double ang1, ang2, diff_ang;
				double shift_len;
				ang1 = Math.Atan2(-(pStopStart.m_center.Y - CornerPoint.Y), (pStopStart.m_center.X - CornerPoint.X));
				ang2 = Math.Atan2(-(pStopEnd.m_center.Y - CornerPoint.Y), (pStopEnd.m_center.X - CornerPoint.X));
				diff_ang = (int)(180 * ang2 / Math.PI - 180 * ang1 / Math.PI + 360) % 360;
				if (diff_ang > 180) diff_ang = 360 - diff_ang;
				shift_len = m_ArcRadius / Math.Tan(Math.PI * (diff_ang * 0.5) / 180.0f);
				CPoint CornerVector;
				// Get the first segment	
				dx = CornerPoint.X - centers[0].X;
				dy = CornerPoint.Y - centers[0].Y;
				uintvect_x = (double)dx / GetLength(dx, dy);
				uintvect_y = (double)dy / GetLength(dx, dy);
				m_uVectorXs = uintvect_x;
				m_uVectorYs = uintvect_y;
				OffsetVector = new CPoint((int)(pStopStart.m_size * uintvect_x), (int)(pStopStart.m_size * uintvect_y));
				CornerVector = new CPoint((int)(shift_len * uintvect_x), (int)(shift_len * uintvect_y));
				TerminatePoints[0].X = pStopStart.m_center.X + OffsetVector.X;
				TerminatePoints[0].Y = pStopStart.m_center.Y + OffsetVector.Y;
				CornerNeighbors[0].X = CornerPoint.X - CornerVector.X;
				CornerNeighbors[0].Y = CornerPoint.Y - CornerVector.Y;
				m_angle = GetDegree(Math.Atan2(-m_uVectorYs, m_uVectorXs));
				AngleSegment[0] = m_angle;
				// Get Arrow Coordinates
				// Arrow Head at the Center 
				// ArrowHead, ArrowTail1, ArrowTail2
				ArrowHead = new CPoint((int)((TerminatePoints[0].X + CornerNeighbors[0].X) / 2.0f),
					(int)((TerminatePoints[0].Y + CornerNeighbors[0].Y) / 2.0f));
				temPT.X = ArrowHead.X - (int)(arrow_len * uintvect_x);
				temPT.Y = ArrowHead.Y - (int)(arrow_len * uintvect_y);
				ArrowTail1.X = temPT.X + (int)(0.5 * arrow_len * uintvect_y);
				ArrowTail1.Y = temPT.Y + (int)(0.5 * arrow_len * -uintvect_x);
				ArrowTail2.X = temPT.X - (int)(0.5 * arrow_len * uintvect_y);
				ArrowTail2.Y = temPT.Y - (int)(0.5 * arrow_len * -uintvect_x);
				// Get the last segment
				dx = centers[1].X - CornerPoint.X;
				dy = centers[1].Y - CornerPoint.Y;
				uintvect_x = (double)dx / GetLength(dx, dy);
				uintvect_y = (double)dy / GetLength(dx, dy);
				m_uVectorXe = uintvect_x;
				m_uVectorYe = uintvect_y;
				AngleSegment[1] = GetDegree(Math.Atan2(-m_uVectorYe, m_uVectorXe));
				OffsetVector = new CPoint((int)(pStopEnd.m_size * uintvect_x), (int)(pStopEnd.m_size * uintvect_y));
				CornerVector = new CPoint((int)(shift_len * uintvect_x), (int)(shift_len * uintvect_y));
				CornerNeighbors[1].X = CornerPoint.X + CornerVector.X;
				CornerNeighbors[1].Y = CornerPoint.Y + CornerVector.Y;
				TerminatePoints[1].X = pStopEnd.m_center.X - OffsetVector.X;
				TerminatePoints[1].Y = pStopEnd.m_center.Y - OffsetVector.Y;

				// CircleCenter
				double tx = (CornerNeighbors[0].X + CornerNeighbors[1].X) / 2.0f;
				double ty = (CornerNeighbors[0].Y + CornerNeighbors[1].Y) / 2.0f;
				double dxx = tx - (double)CornerPoint.X;
				double dyy = ty - (double)CornerPoint.Y;
				uintvect_x = dxx / GetLength(dxx, dyy);
				uintvect_y = dyy / GetLength(dxx, dyy);
				double lc = m_ArcRadius / Math.Sin(Math.PI * (diff_ang * 0.5) / 180.0f);
				CPoint VectCircleCenter = new CPoint((int)(lc * uintvect_x), (int)(lc * uintvect_y));
				// Get Drawing Arc Info
				CircleCenter.X = CornerPoint.X + VectCircleCenter.X;
				CircleCenter.Y = CornerPoint.Y + VectCircleCenter.Y;
				ang1 = Math.Atan2(-(CornerNeighbors[0].Y - CircleCenter.Y), (CornerNeighbors[0].X - CircleCenter.X));
				ang2 = Math.Atan2(-(CornerNeighbors[1].Y - CircleCenter.Y), (CornerNeighbors[1].X - CircleCenter.X));
				diff_ang = (int)(180 * ang2 / Math.PI - 180 * ang1 / Math.PI + 360) % 360;
				if (diff_ang > 180)
				{
					diff_ang = 360 - diff_ang;
					ArcStartAngle = 180 * ang1 / Math.PI;
					ArcRangeAngle = -diff_ang;
				}
				else
				{
					ArcStartAngle = 180 * ang1 / Math.PI;
					ArcRangeAngle = diff_ang;
				}
				LengthSegment[0] = GetLength((pStopStart.m_center.X - CornerNeighbors[0].X), (pStopStart.m_center.Y - CornerNeighbors[0].Y));
				LengthSegment[1] = LengthSegment[0] + Math.Abs(m_ArcRadius * Math.PI * ArcRangeAngle / 180.0f);
				Length = LengthSegment[1] + GetLength((pStopEnd.m_center.X - CornerNeighbors[1].X), (pStopEnd.m_center.Y - CornerNeighbors[1].Y));
				if (LengthSegment[0] > m_CheckTrafficCntlDist)
				{
					OffsetVector = new CPoint((int)(m_CheckTrafficCntlDist * m_uVectorXs), (int)(m_CheckTrafficCntlDist * m_uVectorYs));
					m_CheckTrafficPtDual.X = TerminatePoints[0].X + OffsetVector.X;
					m_CheckTrafficPtDual.Y = TerminatePoints[0].Y + OffsetVector.Y;
				}
				if (Length - LengthSegment[1] > m_CheckTrafficCntlDist)
				{
					OffsetVector = new CPoint((int)(m_CheckTrafficCntlDist * -m_uVectorXe), (int)(m_CheckTrafficCntlDist * -m_uVectorYe));
					m_CheckTrafficPt.X = TerminatePoints[1].X + OffsetVector.X;
					m_CheckTrafficPt.Y = TerminatePoints[1].Y + OffsetVector.Y;
				}
				m_TagRect.X = CornerPoint.X + 40 + TextR.X;
				m_TagRect.Y = CornerPoint.Y + TextR.Y;

			}
			else if (m_type == 2) // Circle Path
			{
				// Checking y-coordoinate
				if (Math.Abs(CornerPoint.Y - pStopStart.m_center.Y) < Math.Abs(CornerPoint.Y - pStopEnd.m_center.Y))
				{
					// Use Start Point y as a corner point
					CornerPoint.Y = pStopStart.m_center.Y;
					CornerPoint.X = pStopEnd.m_center.X;
				}
				else
				{
					// se End Point y as a corner point
					CornerPoint.Y = pStopEnd.m_center.Y;
					CornerPoint.X = pStopStart.m_center.X;
				}
				// Get Line Angles
				double ang1, ang2, diff_ang;
				double shift_len;
				ang1 = Math.Atan2(-(pStopStart.m_center.Y - CornerPoint.Y), (pStopStart.m_center.X - CornerPoint.X));
				ang2 = Math.Atan2(-(pStopEnd.m_center.Y - CornerPoint.Y), (pStopEnd.m_center.X - CornerPoint.X));
				diff_ang = (int)(180 * ang2 / Math.PI - 180 * ang1 / 3.14159 + 360) % 360;
				if (diff_ang > 180) diff_ang = 360 - diff_ang;
				shift_len = m_ArcRadius / Math.Tan(Math.PI * (diff_ang * 0.5) / 180.0f);
				CPoint CornerVector;
				// Get the first segment	
				dx = CornerPoint.X - centers[0].X;
				dy = CornerPoint.Y - centers[0].Y;
				uintvect_x = (double)dx / GetLength(dx, dy);
				uintvect_y = (double)dy / GetLength(dx, dy);
				m_uVectorXs = uintvect_x;
				m_uVectorYs = uintvect_y;
				OffsetVector = new CPoint((int)(pStopStart.m_size * uintvect_x), (int)(pStopStart.m_size * uintvect_y));
				CornerVector = new CPoint((int)(shift_len * uintvect_x), (int)(shift_len * uintvect_y));
				TerminatePoints[0].X = pStopStart.m_center.X + OffsetVector.X;
				TerminatePoints[0].Y = pStopStart.m_center.Y + OffsetVector.Y;
				CornerNeighbors[0].X = CornerPoint.X - CornerVector.X;
				CornerNeighbors[0].Y = CornerPoint.Y - CornerVector.Y;
				m_angle = GetDegree(Math.Atan2(-m_uVectorYs, m_uVectorXs));
				AngleSegment[0] = m_angle;
				// Get Arrow Coordinates
				// Arrow Head at the Center 
				// ArrowHead, ArrowTail1, ArrowTail2
				ArrowHead = new CPoint((int)((TerminatePoints[0].X + CornerNeighbors[0].X) / 2.0f),
					(int)((TerminatePoints[0].Y + CornerNeighbors[0].Y) / 2.0f));
				temPT.X = ArrowHead.X - (int)(arrow_len * uintvect_x);
				temPT.Y = ArrowHead.Y - (int)(arrow_len * uintvect_y);
				ArrowTail1.X = temPT.X + (int)(0.5 * arrow_len * uintvect_y);
				ArrowTail1.Y = temPT.Y + (int)(0.5 * arrow_len * -uintvect_x);
				ArrowTail2.X = temPT.X - (int)(0.5 * arrow_len * uintvect_y);
				ArrowTail2.Y = temPT.Y - (int)(0.5 * arrow_len * -uintvect_x);
				// Get the last segment
				dx = centers[1].X - CornerPoint.X;
				dy = centers[1].Y - CornerPoint.Y;
				uintvect_x = (double)dx / GetLength(dx, dy);
				uintvect_y = (double)dy / GetLength(dx, dy);
				m_uVectorXe = uintvect_x;
				m_uVectorYe = uintvect_y;
				OffsetVector = new CPoint((int)(pStopEnd.m_size * uintvect_x), (int)(pStopEnd.m_size * uintvect_y));
				CornerVector = new CPoint((int)(shift_len * uintvect_x), (int)(shift_len * uintvect_y));
				CornerNeighbors[1].X = CornerPoint.X + CornerVector.X;
				CornerNeighbors[1].Y = CornerPoint.Y + CornerVector.Y;
				TerminatePoints[1].X = pStopEnd.m_center.X - OffsetVector.X;
				TerminatePoints[1].Y = pStopEnd.m_center.Y - OffsetVector.Y;

				AngleSegment[1] = GetDegree(Math.Atan2(-m_uVectorYe, m_uVectorXe));
				// CircleCenter
				double tx = (CornerNeighbors[0].X + CornerNeighbors[1].X) / 2.0f;
				double ty = (CornerNeighbors[0].Y + CornerNeighbors[1].Y) / 2.0f;
				double dxx = tx - (double)CornerPoint.X;
				double dyy = ty - (double)CornerPoint.Y;
				uintvect_x = dxx / GetLength(dxx, dyy);
				uintvect_y = dyy / GetLength(dxx, dyy);
				double lc = m_ArcRadius / Math.Sin(Math.PI * (diff_ang * 0.5) / 180.0f);
				CPoint VectCircleCenter = new CPoint((int)(lc * uintvect_x), (int)(lc * uintvect_y));
				// Get Drawing Arc Info
				CircleCenter.X = CornerPoint.X + VectCircleCenter.X;
				CircleCenter.Y = CornerPoint.Y + VectCircleCenter.Y;

				ang1 = Math.Atan2(-(CornerNeighbors[0].Y - CircleCenter.Y), (CornerNeighbors[0].X - CircleCenter.X));
				ang2 = Math.Atan2(-(CornerNeighbors[1].Y - CircleCenter.Y), (CornerNeighbors[1].X - CircleCenter.X));
				diff_ang = (int)(180 * ang2 / Math.PI - 180 * ang1 / 3.14159 + 360) % 360;
				if (diff_ang > 180)
				{
					diff_ang = 360 - diff_ang;
					ArcStartAngle = 180 * ang1 / Math.PI;
					ArcRangeAngle = -diff_ang;
				}
				else
				{
					ArcStartAngle = 180 * ang1 / Math.PI;
					ArcRangeAngle = diff_ang;
				}
				LengthSegment[0] = GetLength((pStopStart.m_center.X - CornerNeighbors[0].X), (pStopStart.m_center.Y - CornerNeighbors[0].Y));
				LengthSegment[1] = LengthSegment[0] + Math.Abs(m_ArcRadius * Math.PI * ArcRangeAngle / 180.0f);
				Length = LengthSegment[1] + GetLength((pStopEnd.m_center.X - CornerNeighbors[1].X), (pStopEnd.m_center.Y - CornerNeighbors[1].Y));
				if (LengthSegment[0] > m_CheckTrafficCntlDist)
				{
					OffsetVector = new CPoint((int)(m_CheckTrafficCntlDist * m_uVectorXs), (int)(m_CheckTrafficCntlDist * m_uVectorYs));
					m_CheckTrafficPtDual.X = TerminatePoints[0].X + OffsetVector.X;
					m_CheckTrafficPtDual.Y = TerminatePoints[0].Y + OffsetVector.Y;
				}
				if (Length - LengthSegment[1] > m_CheckTrafficCntlDist)
				{
					OffsetVector = new CPoint((int)(m_CheckTrafficCntlDist * -m_uVectorXe), (int)(m_CheckTrafficCntlDist * -m_uVectorYe));
					m_CheckTrafficPt.X = TerminatePoints[1].X + OffsetVector.X;
					m_CheckTrafficPt.Y = TerminatePoints[1].Y + OffsetVector.Y;
				}
				m_TagRect.X = CornerPoint.X + 40 + TextR.X;
				m_TagRect.Y = CornerPoint.Y + TextR.Y;
			}
		}

		double GetDist(CPoint P1, CPoint P2)
		{
			return (Math.Sqrt((P1.X - P2.X) * (P1.X - P2.X) + (P1.Y - P2.Y) * (P1.Y - P2.Y)));
		}

		public void DrawArc(CPoint Center, CPoint A, CPoint B, Graphics e)
		{
			int x1 = A.X;
			int y1 = A.Y;
			int x2 = Center.X;
			int y2 = Center.Y;
			int x3 = B.X;
			int y3 = B.Y;
			///
			int x12 = x1 - x2;
			int x13 = x1 - x3;
			int y12 = y1 - y2;
			int y13 = y1 - y3;
			///
			int y31 = y3 - y1;
			int y21 = y2 - y1;
			int x31 = x3 - x1;
			int x21 = x2 - x1;
			// x1^2 - x3^2 
			int sx13 = x1 * x1 - x3 * x3;
			// y1^2 - y3^2 
			int sy13 = y1 * y1 - y3 * y3;
			///
			int sx21 = x2 * x2 - x1 * x1; // pow(x2, 2) - pow(x1, 2); 
			int sy21 = y2 * y2 - y1 * y1; // pow(y2, 2) - pow(y1, 2); 
			///
			double f = (double)((sx13) * (x12)
				 + (sy13) * (x12)
				 + (sx21) * (x13)
				 + (sy21) * (x13))
				/ (double)(2 * ((y31) * (x12) - (y21) * (x13)));
			double g = (double)((sx13) * (y12)
				 + (sy13) * (y12)
				 + (sx21) * (y13)
				 + (sy21) * (y13))
				/ (double)(2 * ((x31) * (y12) - (x21) * (y13)));
			double c = -x1 * x1 - y1 * y1 - 2 * g * x1 - 2 * f * y1;
			// eqn of circle be x^2 + y^2 + 2*g*x + 2*f*y + c = 0 
			// where centre is (h = -g, k = -f) and radius r 
			// as r^2 = h^2 + k^2 - c 
			double h = -g;
			double k = -f;
			double sqr_of_r = h * h + k * k - c;
			// r is the radius 
			double r = Math.Sqrt(sqr_of_r);
			CString str;
			StringColor = Color.FromArgb(255, 0, 0, 0);
			brushString = new SolidBrush(StringColor);
			str = String.Format("({0:f},{1:f}){2:c}{3:c}({4:f}, {5:f}), R = {6:f}", GetDist(Center, A), GetDist(Center, B), 0x0d, 0x0a, h, k, r, StringColor);
			Font drawFont = new System.Drawing.Font("Arial", 8);
			e.DrawString(str, drawFont, brushString, CornerPoint.X, CornerPoint.Y);
		}

		public void Draw(Graphics e)
		{
			CRect tmp_rect1 = new CRect();
			CRect tmp_rect2 = new CRect();
			StringColor = Color.FromArgb(255, 0, 64, 64);
			ss = String.Format("{0:D2}-{1:D}", m_ID, OnPathAGVs.Count);

			Color c1 = Color.FromArgb(255, 255, 0, 0);
			Color c2 = Color.FromArgb(255, 0, 0, 255);
			Color c3 = Color.FromArgb(255, 255, 0, 255);
			Color c4 = Color.FromArgb(255, 0, 255, 0);
			StringColor = Color.FromArgb(255, 0, 0, 0);
			Pen newPen_red = new Pen(c1, 1);
			Pen newPen_blue = new Pen(c2, 5);
			Pen newPen_path2from = new Pen(c3, 1);
			Pen newPen_path2end = new Pen(c4, 1);
			brushString = new SolidBrush(StringColor);
			if (b_Selected)
				SelectPen = newPen_red;
			else if (m_OnPath == 1)
				SelectPen = newPen_path2from;
			else if (m_OnPath == 2)
				SelectPen = newPen_path2end;
			else
				SelectPen = newPen_blue;

			if (m_type == 0) // Line Path
			{
				// Draw Vetcor [0] -> [1]
				Font drawFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
				e.DrawString(ss, drawFont, brushString, (TerminatePoints[0].X + TerminatePoints[1].X) / 2 + m_DrawOrg.X ,(TerminatePoints[0].Y + TerminatePoints[1].Y) / 2 + m_DrawOrg.Y);

				e.DrawLine(SelectPen, TerminatePoints[0].X + m_DrawOrg.X, TerminatePoints[0].Y + m_DrawOrg.Y,
									  TerminatePoints[1].X + m_DrawOrg.X, TerminatePoints[1].Y + m_DrawOrg.Y);

				// Draw Arrow
				if (m_direction != 2)
				{
					e.DrawLine(SelectPen, ArrowHead.X + m_DrawOrg.X, ArrowHead.Y + m_DrawOrg.Y,
										  ArrowTail1.X + m_DrawOrg.X, ArrowTail1.Y + m_DrawOrg.Y);

					e.DrawLine(SelectPen, ArrowTail1.X + m_DrawOrg.X, ArrowTail1.Y + m_DrawOrg.Y,
										  ArrowTail2.X + m_DrawOrg.X, ArrowTail2.Y + m_DrawOrg.Y);

					e.DrawLine(SelectPen, ArrowTail2.X + m_DrawOrg.X, ArrowTail2.Y + m_DrawOrg.Y,
										  ArrowHead.X + m_DrawOrg.X, ArrowHead.Y + m_DrawOrg.Y);


					SelectPen = newPen_red;
					tmp_rect1 = m_small_circle;
					tmp_rect1.X = m_CheckTrafficPt.X + m_small_circle.X/2;
					tmp_rect1.Y = m_CheckTrafficPt.Y + m_small_circle.Y/2;
					tmp = tmp_rect1;
					tmp.X = tmp_rect1.X + m_DrawOrg.X;
					tmp.Y = tmp_rect1.Y + m_DrawOrg.Y;
					e.DrawEllipse(SelectPen, tmp);
				}
				else
				{
					SelectPen = newPen_red;
					tmp_rect1 = m_small_circle;
					tmp_rect1.X = m_CheckTrafficPt.X + m_small_circle.X/2;
					tmp_rect1.Y = m_CheckTrafficPt.Y + m_small_circle.Y/2;
					tmp = tmp_rect1;
					tmp.X = tmp_rect1.X + m_DrawOrg.X;
					tmp.Y = tmp_rect1.Y + m_DrawOrg.Y;
					e.DrawEllipse(SelectPen, tmp);
					tmp_rect1 = m_small_circle;
					tmp_rect1.X = m_CheckTrafficPtDual.X + m_small_circle.X/2;
					tmp_rect1.Y = m_CheckTrafficPtDual.Y + m_small_circle.Y/2;
					tmp = tmp_rect1;
					tmp.X = tmp_rect1.X + m_DrawOrg.X;
					tmp.Y = tmp_rect1.Y + m_DrawOrg.Y;
					e.DrawEllipse(SelectPen, tmp);
				}
			}
			else if (m_type == 1) // Arc Path
			{
				// Show path ID

				StringColor = Color.FromArgb(255, 0, 64, 64);
				brushString = new SolidBrush(StringColor);
				ss = String.Format("{0:D2}-{1:D}", m_ID, OnPathAGVs.Count);
				Font drawFont = new System.Drawing.Font("Arial", 10,FontStyle.Bold);
				e.DrawString(ss, drawFont, brushString, CornerPoint.X + 40 + m_DrawOrg.X, CornerPoint.Y + m_DrawOrg.Y);

				tmp_rect1 = m_small_circle;
				tmp_rect1.X = m_small_circle.X + CornerPoint.X;
				tmp_rect1.Y = m_small_circle.Y + CornerPoint.Y;
			
				tmp_rect2.X = m_small_circle.X + CircleCenter.X;
				tmp_rect2.Y = m_small_circle.Y + CircleCenter.Y;
				// Draw Vetcor TerminatePoints[0] -> CornerNeighbors[0]
				e.DrawLine(SelectPen, TerminatePoints[0].X + m_DrawOrg.X, TerminatePoints[0].Y + m_DrawOrg.Y,
									  CornerNeighbors[0].X + m_DrawOrg.X, CornerNeighbors[0].Y + m_DrawOrg.Y);
				// Draw Arc CornerNeighbors[0] -> CornerNeighbors[1]

				tmp_point = new CPoint(CornerPoint.X + m_DrawOrg.X, CornerPoint.Y + m_DrawOrg.Y);
				DrawCross(tmp_point, e);
				tmp_point = new CPoint(CircleCenter.X + m_DrawOrg.X, CircleCenter.Y + m_DrawOrg.Y);
				DrawCross(tmp_point, e);
				tmp_Arc.X = CircleCenter.X + m_DrawOrg.X - m_ArcRadius;
				tmp_Arc.Y = CircleCenter.Y + m_DrawOrg.Y - m_ArcRadius;
				tmp_Arc.Width = m_ArcRadius*2;
				tmp_Arc.Height = m_ArcRadius*2;

				e.DrawArc(SelectPen, tmp_Arc, (float)(-ArcStartAngle), (float)(-ArcRangeAngle));

				//  DrawArc(blackPen, CircleCenter.X - m_ArcRadius, CircleCenter.Y - m_ArcRadius, m_ArcRadius * 2, m_ArcRadius * 2, (float)(-ArcStartAngle), (float)(-ArcRangeAngle));
				//pDC->AngleArc(CircleCenter.x + m_DrawOrg.x, CircleCenter.y + m_DrawOrg.y, m_ArcRadius, (float)(ArcStartAngle), (float)(ArcRangeAngle));


				// Draw Vetcor CornerNeighbors[1] -> TerminatePoints[1]
				e.DrawLine(SelectPen, CornerNeighbors[1].X + m_DrawOrg.X, CornerNeighbors[1].Y + m_DrawOrg.Y,
									  TerminatePoints[1].X + m_DrawOrg.X, TerminatePoints[1].Y + m_DrawOrg.Y);
				// Draw Arrow
				if (m_direction != 2)
				{
					e.DrawLine(SelectPen, ArrowHead.X + m_DrawOrg.X, ArrowHead.Y + m_DrawOrg.Y,
										  ArrowTail1.X + m_DrawOrg.X, ArrowTail1.Y + m_DrawOrg.Y);
					e.DrawLine(SelectPen, ArrowTail1.X + m_DrawOrg.X, ArrowTail1.Y + m_DrawOrg.Y,
										  ArrowTail2.X + m_DrawOrg.X, ArrowTail2.Y + m_DrawOrg.Y);
					e.DrawLine(SelectPen, ArrowTail2.X + m_DrawOrg.X, ArrowTail2.Y + m_DrawOrg.Y,
										   ArrowHead.X + m_DrawOrg.X, ArrowHead.Y + m_DrawOrg.Y);

					SelectPen = newPen_red;
					tmp_rect1 = m_small_circle;
					tmp_rect1.X = m_CheckTrafficPt.X + m_small_circle.X/2;
					tmp_rect1.Y = m_CheckTrafficPt.Y + m_small_circle.Y/2;
					tmp = tmp_rect1;
					tmp.X = tmp_rect1.X + m_DrawOrg.X;
					tmp.Y = tmp_rect1.Y + m_DrawOrg.Y;
					e.DrawEllipse(SelectPen, tmp);
				}
				else
				{
					SelectPen = newPen_red;
					tmp_rect1 = m_small_circle;
					tmp_rect1.X = m_CheckTrafficPt.X + m_small_circle.X/2;
					tmp_rect1.Y = m_CheckTrafficPt.Y + m_small_circle.Y/2;
					tmp = tmp_rect1;
					tmp.X = tmp_rect1.X + m_DrawOrg.X;
					tmp.Y = tmp_rect1.Y + m_DrawOrg.Y;
					e.DrawEllipse(SelectPen, tmp);

					tmp_rect1 = m_small_circle;
					tmp_rect1.X = m_CheckTrafficPtDual.X + m_small_circle.X/2;
					tmp_rect1.Y = m_CheckTrafficPtDual.Y + m_small_circle.Y/2;
					tmp = tmp_rect1;
					tmp.X = tmp_rect1.X + m_DrawOrg.X;
					tmp.Y = tmp_rect1.Y + m_DrawOrg.Y;
					e.DrawEllipse(SelectPen, tmp);
				}
			}
			else if (m_type == 2) // Circle Path
			{
				// First y, next x
				if (true) // Check Reasonable Path Length
				{
					// Show path ID
					brushString = new SolidBrush(StringColor);
					Font drawFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
					e.DrawString(ss, drawFont, brushString, CornerPoint.X + 40 + m_DrawOrg.X, CornerPoint.Y + m_DrawOrg.Y);
					// Qualified Path Length
					tmp_rect1.X = CornerPoint.X + m_small_circle.X;
					tmp_rect1.Y = CornerPoint.Y + m_small_circle.Y;
					tmp_rect2.X = CircleCenter.X + m_small_circle.X;
					tmp_rect2.Y = CircleCenter.Y + m_small_circle.Y;

					// Draw Vetcor TerminatePoints[0] -> CornerNeighbors[0]
					e.DrawLine(SelectPen, TerminatePoints[0].X + m_DrawOrg.X, TerminatePoints[0].Y + m_DrawOrg.Y,
										  CornerNeighbors[0].X + m_DrawOrg.X, CornerNeighbors[0].Y + m_DrawOrg.Y);

					// Draw Arc CornerNeighbors[0] -> CornerNeighbors[1]
					tmp_point = new CPoint(CornerPoint.X + m_DrawOrg.X, CornerPoint.Y + m_DrawOrg.Y);
					DrawCross(tmp_point, e);
					tmp_point = new CPoint(CircleCenter.X + m_DrawOrg.X, CircleCenter.Y + m_DrawOrg.Y);
					DrawCross(tmp_point, e);
					tmp_Arc.X = CircleCenter.X + m_DrawOrg.X - m_ArcRadius;
					tmp_Arc.Y = CircleCenter.Y + m_DrawOrg.Y - m_ArcRadius;
					tmp_Arc.Width = m_ArcRadius*2;
					tmp_Arc.Height = m_ArcRadius*2;

					e.DrawArc(SelectPen, tmp_Arc, (float)(-ArcStartAngle), (float)(-ArcRangeAngle));

					//pDC->MoveTo(CornerNeighbors[0] + m_DrawOrg);

					// Draw Vetcor CornerNeighbors[1] -> TerminatePoints[1]
					e.DrawLine(SelectPen, CornerNeighbors[1].X + m_DrawOrg.X, CornerNeighbors[1].Y + m_DrawOrg.Y,
										  TerminatePoints[1].X + m_DrawOrg.X, TerminatePoints[1].Y + m_DrawOrg.Y);
					// Draw Arrow
					if (m_direction != 2)
					{
						e.DrawLine(SelectPen, ArrowHead.X + m_DrawOrg.X, ArrowHead.Y + m_DrawOrg.Y,
											  ArrowTail1.X + m_DrawOrg.X, ArrowTail1.Y + m_DrawOrg.Y);
						e.DrawLine(SelectPen, ArrowTail1.X + m_DrawOrg.X, ArrowTail1.Y + m_DrawOrg.Y,
											  ArrowTail2.X + m_DrawOrg.X, ArrowTail2.Y + m_DrawOrg.Y);
						e.DrawLine(SelectPen, ArrowTail2.X + m_DrawOrg.X, ArrowTail2.Y + m_DrawOrg.Y,
											   ArrowHead.X + m_DrawOrg.X, ArrowHead.Y + m_DrawOrg.Y);

						SelectPen = newPen_red;
						tmp_rect1 = m_small_circle;
						tmp_rect1.X = m_CheckTrafficPt.X + m_small_circle.X/2;
						tmp_rect1.Y = m_CheckTrafficPt.Y + m_small_circle.Y/2;
						tmp = tmp_rect1;
						tmp.X = tmp_rect1.X + m_DrawOrg.X;
						tmp.Y = tmp_rect1.Y + m_DrawOrg.Y;
						e.DrawEllipse(SelectPen, tmp);
					}
					else
					{
						SelectPen = newPen_red;
						tmp_rect1 = m_small_circle;
						tmp_rect1.X = m_CheckTrafficPt.X + m_small_circle.X/2;
						tmp_rect1.Y = m_CheckTrafficPt.Y + m_small_circle.Y/2;
						tmp = tmp_rect1;
						tmp.X = tmp_rect1.X + m_DrawOrg.X;
						tmp.Y = tmp_rect1.Y + m_DrawOrg.Y;
						e.DrawEllipse(SelectPen, tmp);

						tmp_rect1 = m_small_circle;
						tmp_rect1.X = m_CheckTrafficPtDual.X + m_small_circle.X/2;
						tmp_rect1.Y = m_CheckTrafficPtDual.Y + m_small_circle.Y/2;
						tmp = tmp_rect1;
						tmp.X = tmp_rect1.X + m_DrawOrg.X;
						tmp.Y = tmp_rect1.Y + m_DrawOrg.Y;
						e.DrawEllipse(SelectPen, tmp);
					}
				}
				else
				{
					Console.Beep(1000, 100);
					MessageBox.Show("Error: Path Length too Short!");
				}
			}
			StringColor = Color.FromArgb(255, 0, 0, 0);
		}

		double GetLength(double x, double y)
		{
			return (Math.Sqrt(x * x + y * y));
		}

		public void AddObj(int id, int type, ref CStop station1, ref CStop station2, CPoint corner, int radius, int dir)
		{
			m_ID = id;
			CPoint[] centers = new CPoint[2];
			CPoint OffsetVector;

			int dx = 0;
			int dy = 0;
			int arrow_len = 10;
			double uintvect_x = 0;
			double uintvect_y = 0;
			m_ArcRadius = radius;
			m_direction = dir; // default direction [0] -> [1]
			m_type = type;
			pOrgStopStart = station1;
			pOrgStopEnd = station2;
			if (m_direction == 0)
			{
				// Set Original
				pStopStart = pOrgStopStart;
				pStopEnd = pOrgStopEnd;
				m_RuningDirection = 0;
			}
			else if (m_direction == 1)
			{
				// Change Start/ End Stop Pointers
				pStopStart = pOrgStopEnd;
				pStopEnd = pOrgStopStart;
				m_RuningDirection = 1;
			}
			else if (m_direction == 2)
			{
				// As a default
				pStopStart = pOrgStopStart;
				pStopEnd = pOrgStopEnd;
				m_RuningDirection = 0;
			}
			// Get Two Station Centers
			centers[0] = pStopStart.m_center;
			centers[1] = pStopEnd.m_center;
			if (type == 0) // Line Path
			{
				// Get Vetcor [0] -> [1]
				dx = centers[1].X - centers[0].X;
				dy = centers[1].Y - centers[0].Y;
				uintvect_x = (double)dx / GetLength(dx, dy);
				uintvect_y = (double)dy / GetLength(dx, dy);
				m_uVectorXs = uintvect_x;
				m_uVectorYs = uintvect_y;
				m_angle = GetDegree(Math.Atan2(-m_uVectorYs, m_uVectorXs));
				AngleSegment[0] = m_angle;
				// Get Arrow Coordinates
				// Arrow Head at the Center 
				// ArrowHead, ArrowTail1, ArrowTail2
				ArrowHead = new CPoint((int)((pStopStart.m_center.X + pStopEnd.m_center.X) / 2.0f),
					                   (int)((pStopStart.m_center.Y + pStopEnd.m_center.Y) / 2.0f));
				temPT.X = ArrowHead.X - (int)(arrow_len * uintvect_x);
				temPT.Y = ArrowHead.Y - (int)(arrow_len * uintvect_y);
				ArrowTail1.X = temPT.X + (int)(0.5 * arrow_len *  uintvect_y);
				ArrowTail1.Y = temPT.Y + (int)(0.5 * arrow_len * -uintvect_x);
				ArrowTail2.X = temPT.X - (int)(0.5 * arrow_len * uintvect_y);
				ArrowTail2.Y = temPT.Y - (int)(0.5 * arrow_len * -uintvect_x);
				OffsetVector = new CPoint((int)(pStopStart.m_size * uintvect_x), (int)(pStopStart.m_size * uintvect_y));
				TerminatePoints[0].X = pStopStart.m_center.X + OffsetVector.X;
				TerminatePoints[0].Y = pStopStart.m_center.Y + OffsetVector.Y;
				TerminatePoints[1].X = pStopEnd.m_center.X - OffsetVector.X;
				TerminatePoints[1].Y = pStopEnd.m_center.Y - OffsetVector.Y;
				Length = GetLength(dx, dy);
				m_obj_rect = new CRect((int)((TerminatePoints[0].X + TerminatePoints[1].X) / 2) - 20,
					                   (int)((TerminatePoints[0].Y + TerminatePoints[1].Y) / 2),
					                   (int)((TerminatePoints[0].X + TerminatePoints[1].X) / 2) + 20,
					                   (int)((TerminatePoints[0].Y + TerminatePoints[1].Y) / 2) + 20);
				m_TagRect.X = ( (TerminatePoints[0].X + TerminatePoints[1].X) / 2 )+ TextR.X;
				m_TagRect.X = ( (TerminatePoints[0].Y + TerminatePoints[1].Y) / 2) + TextR.Y;
			}
			else if (type == 1) // Arc Path
			{
				CornerPoint = corner;
				// Get Line Angles
				double ang1, ang2, diff_ang;
				double shift_len;
				ang1 = Math.Atan2(-(pStopStart.m_center.Y - CornerPoint.Y), (pStopStart.m_center.X - CornerPoint.X));
				ang2 = Math.Atan2(-(pStopEnd.m_center.Y - CornerPoint.Y), (pStopEnd.m_center.X - CornerPoint.X));
				diff_ang = (int)(180 * ang2 / Math.PI - 180 * ang1 / Math.PI + 360) % 360;
				if (diff_ang > 180) diff_ang = 360 - diff_ang;
				shift_len = m_ArcRadius / Math.Tan(Math.PI * (diff_ang * 0.5) / 180.0f);
				CPoint CornerVector;
				// Get the first segment	
				dx = CornerPoint.X - centers[0].X;
				dy = CornerPoint.Y - centers[0].Y;
				uintvect_x = (double)dx / GetLength(dx, dy);
				uintvect_y = (double)dy / GetLength(dx, dy);
				m_uVectorXs = uintvect_x;
				m_uVectorYs = uintvect_y;
				m_angle = GetDegree(Math.Atan2(-m_uVectorYs, m_uVectorXs));
				AngleSegment[0] = m_angle;
				OffsetVector = new CPoint((int)(pStopStart.m_size * uintvect_x), (int)(pStopStart.m_size * uintvect_y));
				CornerVector = new CPoint((int)(shift_len * uintvect_x), (int)(shift_len * uintvect_y));
				TerminatePoints[0].X = pStopStart.m_center.X + OffsetVector.X;
				TerminatePoints[0].Y = pStopStart.m_center.Y + OffsetVector.Y;
				CornerNeighbors[0].X = CornerPoint.X - CornerVector.X;
				CornerNeighbors[0].Y = CornerPoint.Y - CornerVector.Y;

				// Get Arrow Coordinates
				// Arrow Head at the Center 
				// ArrowHead, ArrowTail1, ArrowTail2
				ArrowHead = new CPoint((int)((TerminatePoints[0].X + CornerNeighbors[0].X) / 2.0f),
									   (int)((TerminatePoints[0].Y + CornerNeighbors[0].Y) / 2.0f));
				temPT.X = ArrowHead.X - (int)(arrow_len * uintvect_x);
				temPT.Y = ArrowHead.Y - (int)(arrow_len * uintvect_y);
				ArrowTail1.X = temPT.X + (int)(0.5 * arrow_len * uintvect_y);
				ArrowTail1.Y = temPT.Y + (int)(0.5 * arrow_len * -uintvect_x);
				ArrowTail2.X = temPT.X - (int)(0.5 * arrow_len * uintvect_y);
				ArrowTail2.Y = temPT.Y - (int)(0.5 * arrow_len * -uintvect_x);
				// Get the last segment
				dx = centers[1].X - CornerPoint.X;
				dy = centers[1].Y - CornerPoint.Y;
				uintvect_x = (double)dx / GetLength(dx, dy);
				uintvect_y = (double)dy / GetLength(dx, dy);
				m_uVectorXe = uintvect_x;
				m_uVectorYe = uintvect_y;
				AngleSegment[1] = GetDegree(Math.Atan2(-m_uVectorYe, m_uVectorXe));
				OffsetVector = new CPoint((int)(pStopEnd.m_size * uintvect_x), (int)(pStopEnd.m_size * uintvect_y));
				CornerVector = new CPoint((int)(shift_len * uintvect_x), (int)(shift_len * uintvect_y));
				CornerNeighbors[1].X = CornerPoint.X + CornerVector.X;
				CornerNeighbors[1].Y = CornerPoint.Y + CornerVector.Y; 
				TerminatePoints[1].X = pStopEnd.m_center.X - OffsetVector.X;
				TerminatePoints[1].Y = pStopEnd.m_center.Y - OffsetVector.Y;
				// CircleCenter
				double tx = (CornerNeighbors[0].X + CornerNeighbors[1].X) / 2.0f;
				double ty = (CornerNeighbors[0].Y + CornerNeighbors[1].Y) / 2.0f;
				double dxx = tx - (double)CornerPoint.X;
				double dyy = ty - (double)CornerPoint.Y;
				uintvect_x = dxx / GetLength(dxx, dyy);
				uintvect_y = dyy / GetLength(dxx, dyy);
				double lc = m_ArcRadius / Math.Sin(Math.PI * (diff_ang * 0.5) / 180.0f);
				CPoint VectCircleCenter = new CPoint((int)(lc * uintvect_x), (int)(lc * uintvect_y));
				// Get Drawing Arc Info
				CircleCenter.X = CornerPoint.X + VectCircleCenter.X;
				CircleCenter.Y = CornerPoint.Y + VectCircleCenter.Y;

				ang1 = Math.Atan2(-(CornerNeighbors[0].Y - CircleCenter.Y), (CornerNeighbors[0].X - CircleCenter.X));
				ang2 = Math.Atan2(-(CornerNeighbors[1].Y - CircleCenter.Y), (CornerNeighbors[1].X - CircleCenter.X));
				diff_ang = (int)(180 * ang2 / Math.PI - 180 * ang1 / Math.PI + 360) % 360;
				if (diff_ang > 180)
				{
					diff_ang = 360 - diff_ang;
					ArcStartAngle = 180 * ang1 / 3.14159;
					ArcRangeAngle = -diff_ang;
				}
				else
				{
					ArcStartAngle = 180 * ang1 / 3.14159;
					ArcRangeAngle = diff_ang;
				}
				LengthSegment[0] = GetLength((pStopStart.m_center.X - CornerNeighbors[0].X), (pStopStart.m_center.Y - CornerNeighbors[0].Y));
				LengthSegment[1] = LengthSegment[0] + Math.Abs(m_ArcRadius * Math.PI * ArcRangeAngle / 180.0f);
				Length = LengthSegment[1] + GetLength((pStopEnd.m_center.X - CornerNeighbors[1].X), (pStopEnd.m_center.Y - CornerNeighbors[1].Y));
				m_obj_rect = new CRect(CornerPoint.X + 30, CornerPoint.Y, CornerPoint.X + 70, CornerPoint.Y + 20);
				m_TagRect = TextR;
				m_TagRect.X = CornerPoint.X + 40 + TextR.X;
				m_TagRect.Y = CornerPoint.Y + TextR.Y;
			}
			else if (type == 2) // Circle Path
			{
				// Checking y-coordoinate
				if (Math.Abs(corner.Y - pStopStart.m_center.Y) < Math.Abs(corner.Y - pStopEnd.m_center.Y))
				{
					// Use Start Point y as a corner point
					CornerPoint.Y = pStopStart.m_center.Y;
					CornerPoint.X = pStopEnd.m_center.X;
				}
				else
				{
					// se End Point y as a corner point
					CornerPoint.Y = pStopEnd.m_center.Y;
					CornerPoint.X = pStopStart.m_center.X;
				}
				// Get Line Angles
				double ang1, ang2, diff_ang;
				double shift_len;
				ang1 = Math.Atan2(-(pStopStart.m_center.Y - CornerPoint.Y), (pStopStart.m_center.X - CornerPoint.X));
				ang2 = Math.Atan2(-(pStopEnd.m_center.Y - CornerPoint.Y), (pStopEnd.m_center.X - CornerPoint.X));
				diff_ang = (int)(180 * ang2 / Math.PI - 180 * ang1 / Math.PI + 360) % 360;
				if (diff_ang > 180) diff_ang = 360 - diff_ang;
				shift_len = m_ArcRadius / Math.Tan(Math.PI * (diff_ang * 0.5) / 180.0f);
				CPoint CornerVector;
				// Get the first segment	
				dx = CornerPoint.X - centers[0].X;
				dy = CornerPoint.Y - centers[0].Y;
				uintvect_x = (double)dx / GetLength(dx, dy);
				uintvect_y = (double)dy / GetLength(dx, dy);
				m_uVectorXs = uintvect_x;
				m_uVectorYs = uintvect_y;
				m_angle = GetDegree(Math.Atan2(-m_uVectorYs, m_uVectorXs));
				AngleSegment[0] = m_angle;
				OffsetVector = new CPoint((int)(pStopStart.m_size * uintvect_x), (int)(pStopStart.m_size * uintvect_y));
				CornerVector = new CPoint((int)(shift_len * uintvect_x), (int)(shift_len * uintvect_y));
				TerminatePoints[0].X = pStopStart.m_center.X + OffsetVector.X;
				TerminatePoints[0].Y = pStopStart.m_center.Y + OffsetVector.Y;
				CornerNeighbors[0].X = CornerPoint.X - CornerVector.X;
				CornerNeighbors[0].Y = CornerPoint.Y - CornerVector.Y;
				// Get Arrow Coordinates
				// Arrow Head at the Center 
				// ArrowHead, ArrowTail1, ArrowTail2
				ArrowHead = new CPoint((int)((TerminatePoints[0].X + CornerNeighbors[0].X) / 2.0f),
									   (int)((TerminatePoints[0].Y + CornerNeighbors[0].Y) / 2.0f));
				temPT.X = ArrowHead.X - (int)(arrow_len * uintvect_x);
				temPT.Y = ArrowHead.Y - (int)(arrow_len * uintvect_y);
				temPT.X = ArrowHead.X - (int)(arrow_len * uintvect_x);
				temPT.Y = ArrowHead.Y - (int)(arrow_len * uintvect_y);
				ArrowTail1.X = temPT.X + (int)(0.5 * arrow_len * uintvect_y);
				ArrowTail1.Y = temPT.Y + (int)(0.5 * arrow_len * -uintvect_x);
				ArrowTail2.X = temPT.X - (int)(0.5 * arrow_len * uintvect_y);
				ArrowTail2.Y = temPT.Y - (int)(0.5 * arrow_len * -uintvect_x);
				// Get the last segment
				dx = centers[1].X - CornerPoint.X;
				dy = centers[1].Y - CornerPoint.Y;
				uintvect_x = (double)dx / GetLength(dx, dy);
				uintvect_y = (double)dy / GetLength(dx, dy);
				m_uVectorXe = uintvect_x;
				m_uVectorYe = uintvect_y;
				AngleSegment[1] = GetDegree(Math.Atan2(-m_uVectorYe, m_uVectorXe));
				OffsetVector = new CPoint((int)(pStopEnd.m_size * uintvect_x), (int)(pStopEnd.m_size * uintvect_y));
				CornerVector = new CPoint((int)(shift_len * uintvect_x), (int)(shift_len * uintvect_y));
				CornerNeighbors[1].X = CornerPoint.X + CornerVector.X;
				CornerNeighbors[1].Y = CornerPoint.Y + CornerVector.Y;
				TerminatePoints[1].X = pStopEnd.m_center.X - OffsetVector.X;
				TerminatePoints[1].Y = pStopEnd.m_center.Y - OffsetVector.Y;

				// CircleCenter
				double tx = (CornerNeighbors[0].X + CornerNeighbors[1].X) / 2.0f;
				double ty = (CornerNeighbors[0].Y + CornerNeighbors[1].Y) / 2.0f;
				double dxx = tx - (double)CornerPoint.X;
				double dyy = ty - (double)CornerPoint.Y;
				uintvect_x = dxx / GetLength(dxx, dyy);
				uintvect_y = dyy / GetLength(dxx, dyy);
				double lc = m_ArcRadius / Math.Sin(Math.PI * (diff_ang * 0.5) / 180.0f);
				CPoint VectCircleCenter = new CPoint((int)(lc * uintvect_x), (int)(lc * uintvect_y));
				// Get Drawing Arc Info
				CircleCenter.X = CornerPoint.X + VectCircleCenter.X;
				CircleCenter.Y = CornerPoint.Y + VectCircleCenter.Y;

				ang1 = Math.Atan2(-(CornerNeighbors[0].Y - CircleCenter.Y), (CornerNeighbors[0].X - CircleCenter.X));
				ang2 = Math.Atan2(-(CornerNeighbors[1].Y - CircleCenter.Y), (CornerNeighbors[1].X - CircleCenter.X));
				diff_ang = (int)(180 * ang2 / Math.PI - 180 * ang1 / Math.PI + 360) % 360;
				if (diff_ang > 180)
				{
					diff_ang = 360 - diff_ang;
					ArcStartAngle = 180 * ang1 / Math.PI;
					ArcRangeAngle = -diff_ang;
				}
				else
				{
					ArcStartAngle = 180 * ang1 / Math.PI;
					ArcRangeAngle = diff_ang;
				}
				LengthSegment[0] = GetLength((pStopStart.m_center.X - CornerNeighbors[0].X), (pStopStart.m_center.Y - CornerNeighbors[0].Y));
				LengthSegment[1] = LengthSegment[0] + Math.Abs(m_ArcRadius * Math.PI * ArcRangeAngle / 180.0f);
				Length = LengthSegment[1] + GetLength((pStopEnd.m_center.X - CornerNeighbors[1].X), (pStopEnd.m_center.Y - CornerNeighbors[1].Y));
				m_obj_rect = new CRect(CornerPoint.X + 30, CornerPoint.Y, CornerPoint.X + 70, CornerPoint.Y + 20);
				m_TagRect.X = CornerPoint.X + 40 + TextR.X;
				m_TagRect.Y = CornerPoint.Y + TextR.Y;
			}
		}


		void GetArcPathFeatures()
		{ 
		
		}

		void GetLinePathFeatures()
		{ 
		
		}

		
	
	}
}
