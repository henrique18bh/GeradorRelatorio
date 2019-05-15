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
        
        public IList<DataTable> ObterRelatorios(IList<string> queries)
        {
            List<DataTable> retorno = new List<DataTable>();
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
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return retorno;
        }
    }
}
