using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CPoint = System.Drawing.Point;


namespace AGVSim
{
    public class sqlserver
    {
        public List<COrder> ReceiveOrder = new List<COrder>();
        public static string server_name = "LAPTOP-FATAMY\\SQLEXPRESS";//server 名稱
        public static string Database_name = "AGVSim";//資料庫(存站點還有路徑的)
        public static string Database_table_AGV_name = "AGV";
        public static string Database_table_Path_name = "Path";
        public static string Database_table_Stop_name = "Stop";
        public static string Database_table_Order_name;// = "TOrder";
        public static string Database_table_Order_Update_name;// = "UpOrder";
        public static string userName = "sa";
        public static string password = "ivam";

        public static bool Read_Stop(ref List<CStop> vStop)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection("server=" + server_name + ";database =" + Database_name + ";uid=" + userName + ";pwd=" + password))
                {
                    sqlcon.Open();

                    String cmdText = "select * from " + Database_table_Stop_name + "";

                    using (SqlCommand cmd = new SqlCommand(cmdText, sqlcon))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    CStop pStop = new CStop();
                                    CPoint tmp = new CPoint();
                                    tmp.X = Convert.ToInt32(reader["PosX"]);
                                    tmp.Y = Convert.ToInt32(reader["PosY"]);
                                    pStop.AddObj(Convert.ToInt32(reader["Stopid"]), Convert.ToInt32(reader["Type"]), tmp);
                                    vStop.Add(pStop);
                                }
                            }
                            reader.Close();
                        }
                    }
                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            return true;
        }
        //**************************************************************************************Order***************************************************************************************//
        public static bool Add_Order(ref COrder Order) 
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection("server = " + server_name + ";database =" + Database_name + ";uid=" + userName + ";pwd=" + password))
                {
                    //Console.WriteLine("123456");
                    sqlcon.Open();
                    //foreach(COrder mOrder in vOrder)
                    //{
                        String cmdText = "INSERT INTO [dbo].[" + Database_table_Order_name + "] (Product_ID, Quantity, Due, Priority, Status, Done)" +
                            "VALUES(@Product_ID, @Quantity, @Due, @Priority, @Status, @Done)";         
                    using (SqlCommand cmd = new SqlCommand(cmdText, sqlcon))
                    {
                            cmd.Parameters.AddWithValue("@Product_ID", Order.m_Product_ID);
                            cmd.Parameters.AddWithValue("@Quantity", Order.m_Quantity);
                            cmd.Parameters.AddWithValue("@Due", Order.m_Due);
                            cmd.Parameters.AddWithValue("@Priority", Order.m_Priority);
                            cmd.Parameters.AddWithValue("@Status", Order.m_OdrStatus);
                            cmd.Parameters.AddWithValue("@Done", Order.m_Done);
                            int rows = cmd.ExecuteNonQuery();
                            //Console.WriteLine(Order.m_Order_ID + " " + Order.m_Quantity + " " + Order.m_Due);
                    }
                    //}
                    sqlcon.Close();
                    sqlcon.Dispose();
                }       
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }



        public static bool Read_Order(ref List<COrder> ReceiveOrder)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection("server=" + server_name + ";database =" + Database_name + ";uid=" + userName + ";pwd=" + password))
                {
                    sqlcon.Open();

                    String cmdText = "select * from " + Database_table_Order_name + ""; // Where Map_Location=@Map_location";

                    using (SqlCommand cmd = new SqlCommand(cmdText, sqlcon))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    COrder pOrder = new COrder();
                                    pOrder.m_Product_ID = Convert.ToInt32(reader["Product_ID"]);
                                    pOrder.m_Quantity = Convert.ToInt32(reader["Quantity"]);
                                    pOrder.m_Priority = Convert.ToInt32(reader["Priority"]);
                                    pOrder.m_Due = Convert.ToInt32(reader["Due"]);
                                    pOrder.m_Done = Convert.ToInt32(reader["Done"]);
                                    pOrder.m_OdrStatus = Convert.ToInt32(reader["Status"]);
                                    ReceiveOrder.Add(pOrder);
                                    Console.WriteLine(ReceiveOrder);
                                }

                            }
                            reader.Close();
                        }
                    }
                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            return true;
        }

        public static bool Delete_Order(int delet_order)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection("server=" + server_name + ";database =" + Database_name + ";uid=" + userName + ";pwd=" + password))
                {
                    sqlcon.Open();
                    String cmdText = "DELETE From  [dbo].[" + Database_table_Order_name + "]  where Product_ID = @Product_ID ";

                    using (SqlCommand cmd = new SqlCommand(cmdText, sqlcon))
                    {
                        cmd.Parameters.AddWithValue("@Product_ID", delet_order);
                        int rows = cmd.ExecuteNonQuery();
                    }

                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }


        //public static bool Update_Order(ref List<COrder> ReceiveOrder)
        //{
        //    try
        //    {
        //        using (SqlConnection sqlcon = new SqlConnection("server=" + server_name + ";database =" + Database_name + ";uid=sa;pwd=" + password))
        //        {
        //            sqlcon.Open();
        //            String cmdText = "Update " + Database_table_Order_Update_name + " set  Quantity = @Quantity, Due = @Due, Priority = @Priority, Status = @Status, Done = @Done where Product_ID = @Product_ID";

        //            using (SqlCommand cmd = new SqlCommand(cmdText, sqlcon))
        //            {

        //                cmd.Parameters.AddWithValue("@Product_ID", ReceiveOrder);
        //                cmd.Parameters.AddWithValue("@Quantity", ReceiveOrder.RESET_Quantity);
        //                cmd.Parameters.AddWithValue("@Due", ReceiveOrder.RESET_Due);
        //                cmd.Parameters.AddWithValue("@Priority", ReceiveOrder.RESET_Priority);
        //                cmd.Parameters.AddWithValue("@Status", ReceiveOrder.RESET_OdrStatus);
        //                cmd.Parameters.AddWithValue("@Done", ReceiveOrder.RESET_Due);
        //                int rows = cmd.ExecuteNonQuery();
        //            }

        //            sqlcon.Close();
        //            sqlcon.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        return false;
        //    }
        //    return true;
        //}



        //**************************************************************************************Order***************************************************************************************//

        public static bool Add_Stop(ref List<CStop> vStop)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection("server=" + server_name + ";database =" + Database_name + ";uid=" + userName + ";pwd=" + password))
                {
                    sqlcon.Open();
                    foreach (CStop mStop in vStop)
                    {
                        String cmdText = "INSERT INTO [dbo].[" + Database_table_Stop_name + "] (Stopid, Type, PosX, PosY, MapID)" +
                            "VALUES(@Stopid, @Type, @PosX, @PosY, @MapID)";

                        using (SqlCommand cmd = new SqlCommand(cmdText, sqlcon))
                        {
                            cmd.Parameters.AddWithValue("@Stopid", mStop.m_ID);
                            cmd.Parameters.AddWithValue("@Type", mStop.m_type);
                            cmd.Parameters.AddWithValue("@PosX", mStop.m_center.X);
                            cmd.Parameters.AddWithValue("@PosY", mStop.m_center.Y);
                            cmd.Parameters.AddWithValue("@MapID", -1);
                            int rows = cmd.ExecuteNonQuery();
                        }
                    }
                   
                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        public static bool Delete_Stop()
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection("server=" + server_name + ";database =" + Database_name + ";uid=" + userName + ";pwd=" + password))
                {
                    sqlcon.Open();
                    String cmdText = "DELETE From [dbo].[" + Database_table_Stop_name + "]";

                    using (SqlCommand cmd = new SqlCommand(cmdText, sqlcon))
                    {
                        int rows = cmd.ExecuteNonQuery();
                    }

                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        public static bool Read_Path(ref List<CPath> vPath, ref List<CStop> vStop)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection("server=" + server_name + ";database =" + Database_name + ";uid=" + userName + ";pwd=" + password))
                {
                    sqlcon.Open();

                    String cmdText = "select * from " + Database_table_Path_name + "";// Where Map_Location=@Map_location";

                    using (SqlCommand cmd = new SqlCommand(cmdText, sqlcon))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    CPath pPath = new CPath();
                                    CPoint tmp = new CPoint();
                                    tmp.X = Convert.ToInt32(reader["CornerPosX"]);
                                    tmp.Y = Convert.ToInt32(reader["CornerPosY"]);
                                    CStop _station1 = find_stop(ref vStop, Convert.ToInt32(reader["Stop1ID"]));
                                    CStop _station2 = find_stop(ref vStop, Convert.ToInt32(reader["Stop2ID"]));
                                    if (_station1 == null || _station2 == null)
                                    {
                                        MessageBox.Show("SQL Server Path loading error");
                                        break;
                                    }
                                    pPath.AddObj(Convert.ToInt32(reader["Pathid"]), Convert.ToInt32(reader["Type"]), ref _station1, ref _station2, tmp, Convert.ToInt32(reader["Radius"]), Convert.ToInt32(reader["Direction"]));
                                    vPath.Add(pPath);
                                }

                            }
                            reader.Close();
                        }
                    }
                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            return true;
        }
        public static bool Add_Path(ref List<CPath> vPath, ref List<CStop> vStop)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection("server=" + server_name + ";database =" + Database_name + ";uid=" + userName + ";pwd=" + password))
                {
                    sqlcon.Open();
                    foreach (CPath mPath in vPath)
                    {
                        //CPath m_station1 = vStop.Find(tPath => tPath == mPath.pOrgStopStart);
                        String cmdText = "INSERT INTO [dbo].[" + Database_table_Path_name + "] (MapID, Pathid, Type, Stop1ID, Stop2ID, CornerPosX, CornerPosY, Radius, Direction)" +
                        "VALUES(@MapID ,@Pathid, @Type, @Stop1ID, @Stop2ID, @CornerPosX, @CornerPosY, @Radius, @Direction)";

                        using (SqlCommand cmd = new SqlCommand(cmdText, sqlcon))
                        {
                            cmd.Parameters.AddWithValue("@MapID", -1);
                            cmd.Parameters.AddWithValue("@Pathid", mPath.m_ID);
                            cmd.Parameters.AddWithValue("@Type", mPath.m_type);
                            cmd.Parameters.AddWithValue("@Stop1ID", mPath.pOrgStopStart.m_ID);
                            cmd.Parameters.AddWithValue("@Stop2ID", mPath.pOrgStopEnd.m_ID);
                            cmd.Parameters.AddWithValue("@CornerPosX", mPath.CornerPoint.X);
                            cmd.Parameters.AddWithValue("@CornerPosY", mPath.CornerPoint.Y);
                            cmd.Parameters.AddWithValue("@Radius", mPath.m_ArcRadius);
                            cmd.Parameters.AddWithValue("@Direction", mPath.m_direction);
                            int rows = cmd.ExecuteNonQuery();
                        }
                    }
                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        public static CStop find_stop(ref List<CStop> vStop, int iStopid)
        {
            return vStop.Find(mStop => mStop.m_ID == iStopid);
        }
        public static bool Delete_Path()
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection("server=" + server_name + ";database =" + Database_name + ";uid=" + userName + ";pwd=" + password))
                {
                    sqlcon.Open();
                    String cmdText = "DELETE From [dbo].[" + Database_table_Path_name + "]";

                    using (SqlCommand cmd = new SqlCommand(cmdText, sqlcon))
                    {
                        int rows = cmd.ExecuteNonQuery();
                    }

                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        public static bool Check_AGV(int id)
        {
            bool check = false;
            Console.WriteLine("[SQL] Checking");
            try
            {
                using (SqlConnection sqlcon = new SqlConnection("server=" + server_name + ";database =" + Database_name + ";uid=" + userName + ";pwd=" + password))
                {
                    sqlcon.Open();

                    String cmdText = "select * from " + Database_table_AGV_name + " Where AGVid=@AGVid";// Where Map_Location=@Map_location";

                    using (SqlCommand cmd = new SqlCommand(cmdText, sqlcon))
                    {
                        cmd.Parameters.AddWithValue("@AGVid", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                check = true;
                            }
                            else
                            {
                                check = false;
                            }

                            reader.Close();
                        }
                    }
                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            return check;
        }
        public static bool Insert_AGV_State(CVehicle AGV)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection("server=" + server_name + ";database =" + Database_name + ";uid=sa;pwd=" + password))
                {
                    sqlcon.Open();
                    String cmdText = "INSERT INTO [dbo].[" + Database_table_AGV_name + "] (AGVid, Angle, StopID, MapID, Type)" +
                           "VALUES(@AGVid, @Angle, @StopID, @MapID, @Type)";

                    using (SqlCommand cmd = new SqlCommand(cmdText, sqlcon))
                    {
                        cmd.Parameters.AddWithValue("@AGVid", AGV.m_ID);
                        cmd.Parameters.AddWithValue("@Angle", AGV.m_angle);
                        cmd.Parameters.AddWithValue("@StopID", AGV.pCurStop.m_ID); // ????
                        cmd.Parameters.AddWithValue("@MapID", -1);
                        cmd.Parameters.AddWithValue("@Type", AGV.m_Type);
                        int rows = cmd.ExecuteNonQuery();
                    }

                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }
        public static bool Update_AGV_State(CVehicle AGV)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection("server=" + server_name + ";database =" + Database_name + ";uid=sa;pwd=" + password))
                {
                    sqlcon.Open();
                    String cmdText = "Update " + Database_table_AGV_name + " set Angle = @Angle, StopID = @StopID, MapID = @MapID, Type = @Type where AGVid = @AGVid";

                    using (SqlCommand cmd = new SqlCommand(cmdText, sqlcon))
                    {
                        cmd.Parameters.AddWithValue("@AGVid", AGV.m_ID);
                        cmd.Parameters.AddWithValue("@Angle", AGV.m_angle);
                        cmd.Parameters.AddWithValue("@StopID", AGV.pCurStop.m_ID); // ????
                        cmd.Parameters.AddWithValue("@MapID", -1);
                        cmd.Parameters.AddWithValue("@Type", AGV.m_Type);
                        int rows = cmd.ExecuteNonQuery();
                    }

                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

//***********************************************************************************************************************//
    }
}
