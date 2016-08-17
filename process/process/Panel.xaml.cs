using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows.Controls.DataVisualization.Charting;
using process.Services;

using Entity;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
namespace process
{
    /// <summary>
    /// Interaction logic for Panel.xaml
    /// </summary>
    public partial class Panel : Window
    {
        static public ProcessObject po;
        DispatcherTimer DispatcherTimerProcess;
        DispatcherTimer DispatcherTimerScreenShot;
        static public DispatcherTimer DispatcherTimerMouse;

        DispatcherTimer DispatcherTimerUpload;

        BackgroundWorker ProcessWorker;
        BackgroundWorker ScreenShotWorker;
        BackgroundWorker MouseWorker;
        BackgroundWorker UploadWorker;

        Thread KsT;
        public static Stopwatch sw;
        public static bool sts;


        public Panel()
        {
            InitializeComponent();
            po = new ProcessObject();
            po.keyStroke = 0;
            po.MouseTime = TimeSpan.Zero;
            po.MouseUseCount = 0;

            
            po.UId = 1;

            ProcessWorker = new BackgroundWorker();
            ScreenShotWorker = new BackgroundWorker();
            MouseWorker = new BackgroundWorker();
            UploadWorker = new BackgroundWorker();

            ProcessWorker.DoWork += GetProcessWorker;
            ScreenShotWorker.DoWork += GetScreenShotWorker;
            MouseWorker.DoWork += MouseService.MouseMoveDetection;
            UploadWorker.DoWork += GetUploadWorker;


            ProcessWorker.RunWorkerCompleted += ProcessDone;
            ScreenShotWorker.RunWorkerCompleted += ScreenShotDone;
            MouseWorker.RunWorkerCompleted += MouseDone;


            ScreenShotWorker.Disposed += ScreenShotdispose;

            string v = System.Configuration.ConfigurationManager.AppSettings["ProcessTimer"];
            int pdtv = Convert.ToInt32(v);

            DispatcherTimerProcess = new DispatcherTimer();
            DispatcherTimerProcess.Tick += new EventHandler(ProcessService.getAllProcess);
            DispatcherTimerProcess.Interval = new TimeSpan(0, 0, 0, 0, pdtv);

            v = System.Configuration.ConfigurationManager.AppSettings["ScreenShotTimer"];
            int ssdtv = Convert.ToInt32(v);
            DispatcherTimerScreenShot = new DispatcherTimer();
            DispatcherTimerScreenShot.Tick += new EventHandler(ScreenShotService.ScreenShot);
            DispatcherTimerScreenShot.Interval = new TimeSpan(0, 0, 0, 0, ssdtv);


            DispatcherTimerMouse = new DispatcherTimer();
            sw = new Stopwatch();
            sw.Reset();
            sts = true;


            DispatcherTimerUpload = new DispatcherTimer();
            DispatcherTimerUpload.Tick += new EventHandler(upload);
            DispatcherTimerUpload.Interval = new TimeSpan(0, 0, 5);

        }

        private void Start(object sender, RoutedEventArgs e)
        {
            ProcessWorker.RunWorkerAsync();
            ScreenShotWorker.RunWorkerAsync();
            MouseWorker.RunWorkerAsync();
           //UploadWorker.RunWorkerAsync();
            KsT = new Thread(new ThreadStart(KeyStrokeService.StartLogging));
            KsT.Start();
            
        }

        private void GetUploadWorker(object sender,DoWorkEventArgs e)
        {
            if (DispatcherTimerUpload.IsEnabled == false)
                DispatcherTimerUpload.Start();
        }
        private void GetProcessWorker(object sender, DoWorkEventArgs e)
        {
            if (DispatcherTimerProcess.IsEnabled == false)
                DispatcherTimerProcess.Start();
        }
        private void GetScreenShotWorker(object o, DoWorkEventArgs e)
        {
            if (DispatcherTimerScreenShot.IsEnabled == false)
                DispatcherTimerScreenShot.Start();
            
        }
        private void ProcessDone(object sender, RunWorkerCompletedEventArgs e)
        {
            this.p.Content = "Running";
        }
        private void ScreenShotDone(object sender, RunWorkerCompletedEventArgs e)
        {
            this.s.Content = "Running";
        }
        private void MouseDone(object sender, RunWorkerCompletedEventArgs e)
        {
            this.k.Content = "Running";
        }
       


        private void Stop(object sender, RoutedEventArgs e)
        {
            KsT.Abort();
            DispatcherTimerProcess.Stop();
            DispatcherTimerScreenShot.Stop();
            DispatcherTimerMouse.Stop();
            DispatcherTimerUpload.Stop();

            ProcessWorker.Dispose();
            MouseWorker.Dispose();
            ScreenShotWorker.Dispose();
            UploadWorker.Dispose();
            //ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
            //sc.StopApplication(ref po);
            //sc.Close();
            ProcessService.stop(ref po);
            this.s.Content = "Stop";
            this.p.Content = "Stop";
            this.k.Content = "Stop";

        }


        private void close(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
                sc.StopApplication(ref po);
                sc.Close();
                Application.Current.Shutdown();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void ScreenShotdispose(object o, EventArgs e)
        {
            GC.Collect();
        }


      

        private void upload(object o, EventArgs e)
        {
            bool sts = chkNet();
            if (sts)
            {
                FileStream fs = new FileStream("Processdata.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string s = sr.ReadLine();
                List<string> lst = new List<string>();
                List<string> LstLft = new List<string>();

                while (s != null)
                {
                    lst.Add(s);
                    s = sr.ReadLine();
                    if (lst.Count == 5)
                    {
                        s = sr.ReadLine();
                        while (s != null)
                        {
                            LstLft.Add(s);
                            s = sr.ReadLine();
                        }
                        break;
                    }
                }
                sr.Close();
                fs.Close();

                string[] filePaths = Directory.GetFiles("screenshots\\", "*.jpeg");
                Bitmap b = new Bitmap(filePaths[0]);
                string pa = filePaths[0];
                Bitmap bn = new Bitmap(b);
                bool re = false;
                if (lst != null && filePaths != null)
                {
                    ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
                    re= sc.InsertData(lst, bn, pa);
                    sc.Close();
                }
                 if (re)
                {
                    b.Dispose();
                    File.Delete(filePaths[0]);


                    File.Delete("Processdata.txt");
                    FileStream fs1 = new FileStream("Processdata.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter sw1 = new StreamWriter(fs1);
                    for (int i = 0; i < LstLft.Count; i++)
                    {
                        sw1.WriteLine(LstLft[i]);
                    }
                    sw1.Close();
                    fs1.Close();
                }
            }

        }
        static bool chkNet()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://service-21.apphb.com/Service1.svc"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }

}
