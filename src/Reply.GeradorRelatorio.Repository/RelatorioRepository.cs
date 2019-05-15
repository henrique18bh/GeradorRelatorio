using log4net;
using Reply.GeradorRelatorio.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Reply.GeradorRelatorio.Repository
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly string _ConnectionString = ConfigurationManager.ConnectionStrings["BancoRelatorioConnectionString"].ConnectionString;
        private static readonly ILog log = LogManager.GetLogger("Service de relatórios");

        public IList<DataTable> ObterRelatorios(IList<string> queries)
        {
            List<DataTable> retorno = new List<DataTable>();
            try
            {

                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {

                    connection.Open();
                    foreach (string query in queries)
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        try
                        {
                            using (SqlDataReader dataReader = command.ExecuteReader())
                            {
                                DataTable dt = new DataTable();
                                dt.Load(dataReader);
                                retorno.Add(dt);
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error(string.Format("Erro ao Realizar consulta {0}", query), ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Erro Banco de Dados", ex);
            }

            return retorno;
        }
    }
}
