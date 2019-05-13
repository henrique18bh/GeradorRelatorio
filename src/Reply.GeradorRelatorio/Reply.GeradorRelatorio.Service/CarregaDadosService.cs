using Reply.GeradorRelatorio.Repository;
using Reply.GeradorRelatorio.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reply.GeradorRelatorio.Service
{
    public class CarregaDadosService : ICarregaDadosService
    {
        public void GerarRelatorio()
        {
            var caminhoTxt = "";
            var caminhoRetorno = "";
            var reposotorio = new GeradorRelatorioDbContext();

            reposotorio.ObterDadosTxt(caminhoTxt, caminhoRetorno);
        }
    }
}
