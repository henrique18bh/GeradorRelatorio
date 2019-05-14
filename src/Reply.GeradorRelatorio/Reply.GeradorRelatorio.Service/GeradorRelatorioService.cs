using Reply.GeradorRelatorio.Repository;
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
        public void GerarRelatorio()
        {
            // Chamar o service de configuração e obter o objeto
            // fazer a validação se ta dentro da data / hora estipulada no arquivo

            string caminhoTxt = @"C:\Henrique\Projetos\GeradorRelatorio\static\query.txt";
            string caminhoRetorno = @"C:\Henrique\Projetos\GeradorRelatorio\static";
            RelatorioRepository reposotorio = new RelatorioRepository();

            var queries = ObterQueries(caminhoTxt);
            
            // apagar o arquivo com as queries .txt

            var consultas = reposotorio.ObterRelatorios(queries);
            foreach (var dt in consultas)
            {
                GerarCsv(dt, caminhoRetorno);
            }
            // fazer a chamada do HistoriocoRelatorioRepository para salvar
            // O serviço Windows deverá gera logs das principais atividades executadas.
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

        private void GerarCsv(DataTable dt, string caminhoArquivo)
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
        }
    }
}
