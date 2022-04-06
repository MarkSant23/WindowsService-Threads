using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
//using System.Threading;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LogWritter("Service is started");
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 10000;
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            LogWritter("Service stopped.." + DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
        }
        public void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            int i = 0;
            while(true)
            {
                i++;
                timer.Interval = 10000;
                LogWritter("Nešto radim....");
                timer.Enabled = false;
            }
            
        }

        public static void LogWritter(string msg)
        {
            string name = DateTime.Now.ToString("dd.MM.yyyy")+"_IKR.Log";
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int index = appPath.LastIndexOf('\\');
            appPath = appPath.Remove(index);
            string path = Path.Combine(appPath, "IKRLog" + DateTime.Now.Year.ToString());
            if (!Directory.Exists(path))
            { 
                Directory.CreateDirectory(path);

            }
            File.AppendAllText(path + "\\" + name, "\r\n"+ DateTime.Now.ToString("dd HH:mm:ss")+"\t"+msg);
        }
    }
}
