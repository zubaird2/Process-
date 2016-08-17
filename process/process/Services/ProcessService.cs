using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.IO;
namespace process.Services
{
    class ProcessService
    {

        static ProcessObject preobject;
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);





        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int XULC;        // x position of upper-left corner  
            public int YULC;         // y position of upper-left corner  
            public int XLRC;       // x position of lower-right corner  
            public int YLRC;      // y position of lower-right corner  
        }


        static public void getAllProcess(object o, EventArgs e)
        {
            GC.Collect();
            IntPtr hWnd = GetForegroundWindow();
            uint processId;

            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            GetWindowText(hWnd, Buff, nChars);

            GetWindowThreadProcessId(hWnd, out processId);
            Process p = Process.GetProcessById((int)processId);
            Panel.po.StartActiveTime = DateTime.Now;
            Panel.po.ProcessName = p.ProcessName;
            Panel.po.ProcessTitle = Buff.ToString();
            Panel.po.ProcessId = p.Id;
            Panel.po.UId = 1;
            RECT re = new RECT();
            bool s = GetWindowRect(GetForegroundWindow(), out re);
            if (s.Equals(true))
            {
                Panel.po.P_Width = (re.XLRC - re.XULC);
                Panel.po.P_Hieght = (re.YLRC - re.YULC);
            }
            checkProcess(ref Panel.po);
            //ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
            //sc.InsertToDB(ref Panel.po);

            //sc.Close();
        }


        static public bool checkProcess(ref ProcessObject pD)
        {
            bool status = false;
            if ((preobject == null) || (preobject.EndActiveTime != null))
            {
                preobject = new ProcessObject();
                preobject.StartActiveTime = pD.StartActiveTime;
                preobject.Id = pD.Id;
                preobject.P_Hieght = pD.P_Hieght;
                preobject.P_Width = pD.P_Width;
                preobject.ProcessId = pD.ProcessId;
                preobject.ProcessName = pD.ProcessName;
                preobject.ProcessTitle = pD.ProcessTitle;
                preobject.UId = pD.UId;
                return true;

            }
            else if (preobject.ProcessTitle.Equals(pD.ProcessTitle) && (preobject.EndActiveTime == null)) { }
            else if ((!preobject.ProcessTitle.Equals(pD.ProcessTitle)) && ((preobject.EndActiveTime == null)))
            {
                preobject.EndActiveTime = pD.StartActiveTime;
                preobject.keyStroke = pD.keyStroke;
                preobject.MouseTime = pD.MouseTime;
                preobject.MouseUseCount = pD.MouseUseCount;
                //pdDAL.Save(ref preobject);
                //SaveToFile(ref preobject);
                FileStream fs = new FileStream("Processdata.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(preobject.ProcessId+","+preobject.ProcessName+","+preobject.ProcessTitle+","+preobject.StartActiveTime+","+preobject.EndActiveTime+","+preobject.keyStroke+","+preobject.MouseTime+","+preobject.MouseUseCount+","+preobject.P_Hieght+","+preobject.P_Width+","+preobject.UId);
                sw.Close();
                fs.Close();
                pD.keyStroke = 0;
                pD.MouseTime = TimeSpan.Zero;
                pD.MouseUseCount = 0;
                status = true;
            }
            return status;
        }
        static public void stop(ref ProcessObject p)
        {
            if (preobject.EndActiveTime == null)
            {
                preobject.EndActiveTime = DateTime.Now;
                preobject.keyStroke = p.keyStroke;
                preobject.MouseTime = p.MouseTime;
                preobject.MouseUseCount = p.MouseUseCount;
                p.MouseUseCount = 0;
                p.MouseTime = TimeSpan.Zero;
                p.keyStroke = 0;
            }
            FileStream fs = new FileStream("Processdata.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(preobject.ProcessId + "," + preobject.ProcessName + "," + preobject.ProcessTitle + "," + preobject.StartActiveTime + "," + preobject.EndActiveTime + "," + preobject.keyStroke + "," + preobject.MouseTime + "," + preobject.MouseUseCount);
            sw.Close();
            fs.Close();
        }
    }
}
