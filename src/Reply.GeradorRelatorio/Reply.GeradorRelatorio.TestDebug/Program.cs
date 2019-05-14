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
        private static readonly ICarregaDadosService _service = new CarregaDadosService();
        static void Main(string[] args)
        {
            var service = new CarregaDadosService();
            service.GerarRelatorio();
        }
    }
}
