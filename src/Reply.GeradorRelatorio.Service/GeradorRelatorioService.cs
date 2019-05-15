using log4net;
using Reply.GeradorRelatorio.Repository;
using Reply.GeradorRelatorio.Repository.Interfaces;
using Reply.GeradorRelatorio.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
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
            try
            {
                log.Info("Obtendo Dados de configuração");
                Entity.DTO.DadosConfiguracaoDTO dados = _configuracaoService.ObterDadosConfiguracao();

                if (dados == null)
                {
                    return;
                }

                if(string.IsNullOrWhiteSpace(dados.Data))
                {
                    log.Info("Campo Data vazio no XML de configuração");
                    return;
                }
                DateTime dataValidacao = Convert.ToDateTime(dados.Data);
                int diferencaData = DateTime.Compare(dataValidacao, DateTime.Now);
                if (diferencaData >= 0)
                {
                    log.Info("Fora do Horário de Execução.");
                    return;
                }

                if (!Directory.Exists(dados.CaminhoResultado))
                {
                    log.Error($"O caminho {dados.CaminhoResultado} informado no xml de configuração não é válido.");
                    return;
                }

                log.Info("Obtendo Consultas do Arquivo.");
                List<string> queries = ObterQueries(dados.CaminhoTxt);
                if (queries.Count == 0)
                {
                    return;
                }

                log.Info("Obtendo Retorno Consulta SQL.");
                IList<DataTable> consultas = _relatorioRepository.ObterRelatorios(queries);

                for (int i = 0; i < consultas.Count; i++)
                {

                    log.Info("Gravando Arquivo CSV.");
                    string nomeArquivo = GerarCsv(consultas[i], dados.CaminhoResultado);
                    try
                    {

                        log.Info("Gravando LOG da geração do relatório.");
                        _historicoService.Salvar(nomeArquivo, queries[i]);

                    }
                    catch (Exception ex)
                    {
                        log.Error("Erro ao Salvar Log no Banco de Dados.", ex);
                    }
                }

                log.Info("Deletando Arquivo Txt para não gerar novamente os relatórios");

                File.Delete(dados.CaminhoTxt);

                dados.Data = string.Empty;
                _configuracaoService.AtualizarDadosConfiguracao(dados);
            }
            catch (Exception ex)
            {
                log.Error("Erro na geração do relatório", ex);
            }
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

                if (!retorno.Any())
                {
                    log.Info("O arquivo contendo as consultas está vazio.");
                }
            }
            else
            {
                log.Info("O arquivo contendo as consultas não foi encontrado.");
            }

            return retorno;
        }

        private string GerarCsv(DataTable dt, string caminhoArquivo)
        {
            try
            {
                StringBuilder listaResultado = new StringBuilder();

                string[] columnNames = dt.Columns.Cast<DataColumn>()
                                    .Select(column => column.ColumnName)
                                    .ToArray();

                listaResultado.AppendLine(string.Join(";", columnNames));

                foreach (DataRow row in dt.Rows)
                {
                    string[] fields = row.ItemArray.Select(field => field.ToString()).
                                                    ToArray();
                    listaResultado.AppendLine(string.Join(";", fields));
                }
                    string caminhoArquivoFull = Path.Combine(caminhoArquivo, _NomeArquivo);
                File.WriteAllText(caminhoArquivoFull, listaResultado.ToString());
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Erro ao gerar arquivo CSV {0}", _NomeArquivo), ex);
            }
            return _NomeArquivo;
        }
    }
}
