using System;
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



namespace AGVSim
{
    class Param
    {
        string url = System.Environment.CurrentDirectory;
        
        
        public static string station_img_path = @"C:/Users/user/Downloads/AGVSim_project/AGVSim/icon/pic_Station.png";
        //public static string AGV_img_path = @"E:/派車模擬系統/AGVSim/icon/pic_difcar.png";
        public static string AGV_img_path = @"C:/Users/user/Downloads/AGVSim_project/AGVSim/icon/pic_difcar.png";

        public static bool SQL_Update_AGV_Status = false;


        public void T()
        {
            Console.WriteLine(url);
            string station_img_path = url + "icon/pic_Station.png";
        }
    }

    
}
