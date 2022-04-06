using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eventlog
{
    public partial class WinServis : ServiceBase
    {
        EventLog log;
        public WinServis()
        {
            InitializeComponent();
            log = new EventLog();
            if (!EventLog.SourceExists("Labos10"))
            {
                EventLog.CreateEventSource("Labos 10", "Application");
            }
            log.Source = "Labos 10";
            log.Log = "Application";
        }

        protected override void OnStart(string[] args)
        {
            WaitCallback delegat = method;
            bool k = true;
            if(ThreadPool.QueueUserWorkItem(delegat))
            {
                log.WriteEntry($"Krenuli smo u {DateTime.Now.ToString()}!");
            }
        }

        protected override void OnStop()
        {
            log.WriteEntry("Service closed " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            bool k = false;
        }
        void method(object msg)
        {
            int i = 0;
            while(true)
            {
                i++;
                Thread.Sleep(10000);
                log.WriteEntry($"Prolaz petlje: {i} u {DateTime.Now.ToString("dd.MM.yyyy HH:mm")}");
            }
        }
    }
}
