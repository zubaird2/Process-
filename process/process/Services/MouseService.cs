using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace process.Services
{
    class MouseService
    {

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out System.Windows.Point lpPoint);
        public static System.Windows.Point GetCursorPosition()
        {
            System.Windows.Point lpPoint;
            GetCursorPos(out lpPoint);
            return lpPoint;
        }




        static public void MouseMoveDetection(object o, DoWorkEventArgs e)
        {
            System.Windows.Point point = GetCursorPosition();
            Panel.DispatcherTimerMouse.Tick += delegate
            {
                Application.Current.MainWindow.CaptureMouse();
                if (point != GetCursorPosition())
                {

                    Panel.sw.Start();
                    if (Panel.sts)
                    {
                        Panel.po.MouseUseCount++;
                        Panel.sts = false;
                    }
                    point = GetCursorPosition();

                }
                else
                {
                    Panel.sw.Stop();
                    Panel.po.MouseTime =Panel.po.MouseTime+Panel.sw.Elapsed;
                    Panel.sw.Reset();
                    Panel.sts = true;
                }
                Application.Current.MainWindow.ReleaseMouseCapture();
            };
            
            Panel.DispatcherTimerMouse.Interval = new TimeSpan(0, 0, 0, 0, 300);
            Panel.DispatcherTimerMouse.Start();
        }
    }
}
