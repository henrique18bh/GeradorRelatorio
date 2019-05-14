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
        private static readonly IGeradorRelatorioService _service = new GeradorRelatorioService();
        static void Main(string[] args)
        {
            var service = new GeradorRelatorioService();
            service.GerarRelatorio();
        }
    }
}
