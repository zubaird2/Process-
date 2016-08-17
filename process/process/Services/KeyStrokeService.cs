using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel;
namespace process.Services
{
    public class KeyStrokeService
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
        public static bool Status { get; set; }

        public static void StartLogging()
        {

            Status = true;

            while (Status)
            {
                //sleeping for while, this will reduce load on cpu
                //Thread.Sleep(10);
                for (Int32 i = 0; i < 255; i++)
                {
                    int keyState = GetAsyncKeyState(i);
                    if (keyState == 1 || keyState == -32767)
                    {
                        //Console.WriteLine((Keys)i);
                        Panel.po.keyStroke++;
                        break;
                    }
                }
            }
        }



        



    }
}
