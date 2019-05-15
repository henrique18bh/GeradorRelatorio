using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using System.Runtime.InteropServices;
using System.Configuration;
using System;
using Reply.GeradorRelatorio.Service.Interfaces;
using Reply.GeradorRelatorio.Service;
using log4net;
using log4net.Config;

namespace Reply.GeradorRelatorio
{
    public partial class RelatorioService : ServiceBase
    {
        private int eventId = 1;

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };
        private readonly IGeradorRelatorioService _geradorRelatorioService;
        private static readonly ILog log = LogManager.GetLogger("Service de relatórios");

        public RelatorioService()
        {
            _geradorRelatorioService = new GeradorRelatorioService();
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("RelatorioService"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "RelatorioService", "RelatorioServiceNewLog");
            }
            eventLog1.Source = "RelatorioService";
            eventLog1.Log = "RelatorioServiceNewLog";
        }

        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            XmlConfigurator.Configure();
            eventLog1.WriteEntry("Start do serviço.");
            log.Info("Start do serviço.");
            Timer timer = new Timer();
            timer.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["intervaloExecucao"]);
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();

            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            eventLog1.WriteEntry("Início da execução do serviço.", EventLogEntryType.Information, eventId++);
            log.Info("Início da execução do serviço.");
            _geradorRelatorioService.GerarRelatorio();
            log.Info("Fim da execução do serviço.");
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Stop do serviço.");
            log.Info("Stop do serviço.");
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);
    }
}
