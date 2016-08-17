using System;
using System.ServiceProcess;
using System.Windows;

namespace process
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() { }

        private void Start(object sender, RoutedEventArgs e)
        {
            ServerDetection();
            this.Hide();
            Panel p = new Panel();
            p.Show();

        }

        private void ServerDetection()
        {
            string myServiceName = "MSSQL$SQLEXPRESS";
            string status;
            ServiceController myService = new ServiceController(myServiceName);
            try
            {
                status = myService.Status.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Service not found. It is probably not installed. [exception=" + ex.Message + "]");
                Application.Current.Shutdown();
            }


            if (myService.Status.Equals(ServiceControllerStatus.Stopped) || myService.Status.Equals(ServiceControllerStatus.StopPending))
            {
                try
                {
                    myService.Start();
                    myService.WaitForStatus(ServiceControllerStatus.Running);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in starting the service: " + ex.Message);
                    Application.Current.Shutdown();
                }
            }
        }


    }
}