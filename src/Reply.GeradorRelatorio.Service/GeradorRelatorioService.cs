using log4net;
using Reply.GeradorRelatorio.Repository;
using Reply.GeradorRelatorio.Repository.Interfaces;
using Reply.GeradorRelatorio.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Reply.GeradorRelatorio.Service
{
    public class GeradorRelatorioService : IGeradorRelatorioService
    {
        private static readonly string _NomeArquivo = string.Format("Relatorio_{0}.csv",
                DateTime.Now.ToLongTimeString().Replace(":", "-"));

        private static readonly ILog log = LogManager.GetLogger("Service de relatórios");
        private readonly IHistoricoService _historicoService;
        private readonly IRelatorioRepository _relatorioRepository;
        private readonly IConfiguracaoService _configuracaoService;
        public GeradorRelatorioService()
        {
            _historicoService = new HistoricoService();
            _relatorioRepository = new RelatorioRepository();
            _configuracaoService = new ConfiguracaoService();
        }

        public void GerarRelatorio()
        {
            log.Info("Obtendo Dados de configuração");

            var dados = _configuracaoService.ObterDadosConfiguracao();

            DateTime dataValidacao = Convert.ToDateTime(dados.Data);
            var diferencaData = DateTime.Compare(dataValidacao, DateTime.Now);
            if (diferencaData >= 0)
            {
                return;
            }

            var queries = ObterQueries(dados.CaminhoTxt);  

            var consultas = _relatorioRepository.ObterRelatorios(queries);

            for (int i = 0; i < consultas.Count; i++)
            {
               var nomeArquivo = GerarCsv(consultas[i], dados.CaminhoResultado);
                _historicoService.Salvar(nomeArquivo, queries[i]);
            }

            //File.Delete(dados.CaminhoTxt);
        }

        public List<string> ObterQueries(string caminhoArquivo)
        {
            List<string> retorno = new List<string>();
            if (File.Exists(caminhoArquivo))
            {
                string[] linhas = File.ReadAllLines(caminhoArquivo);
                foreach (string linha in linhas)
                {
                    retorno.Add(linha);
                }
            }

            return retorno;
        }

        private string GerarCsv(DataTable dt, string caminhoArquivo)
        {
            
            StringBuilder listaResultado = new StringBuilder();

            string[] columnNames = dt.Columns.Cast<DataColumn>()
                                .Select(column => column.ColumnName)
                                .ToArray();

            //Create headers
            listaResultado.AppendLine(string.Join(";", columnNames));

            //Append Line
            foreach (DataRow row in dt.Rows)
            {
                string[] fields = row.ItemArray.Select(field => field.ToString()).
                                                ToArray();
                listaResultado.AppendLine(string.Join(";", fields));
            }
            
            string caminhoArquivoFull = Path.Combine(caminhoArquivo, _NomeArquivo);
            File.WriteAllText(caminhoArquivoFull, listaResultado.ToString());
            return _NomeArquivo;
        }
    }
}
