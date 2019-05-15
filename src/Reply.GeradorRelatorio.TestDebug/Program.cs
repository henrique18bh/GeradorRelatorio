using log4net;
using log4net.Config;
using Reply.GeradorRelatorio.Service;
using Reply.GeradorRelatorio.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reply.GeradorRelatorio.TestDebug
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger("Service de relatórios");

        private static readonly IGeradorRelatorioService _service = new GeradorRelatorioService();
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            log.Info("Entering application.");
            var service = new GeradorRelatorioService();
            service.GerarRelatorio();
            log.Info("Exiting application.2");
        }
    }
}
