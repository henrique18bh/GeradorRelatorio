using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reply.GeradorRelatorio.Repository
{
    public class GeradorRelatorioDbContext : DbContext
    {
        public GeradorRelatorioDbContext() : base("GeradorRelatorioDbConnectionString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<GeradorRelatorioDbContext>());

            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseIfModelChanges<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseAlways<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new SchoolDBInitializer());
        }
        public DbSet<HistorioRelatorio> HistorioRelatorios { get; set; }

        public void ObterDadosTxt(string caminhoTxt, string caminhoResultado)
        {
            try
            {
                if (File.Exists(caminhoTxt))
                {
                    var listaConsultas = File.ReadAllLines(caminhoTxt);


                    var connectionString = "";
                    using (SqlConnection connection =  new SqlConnection(connectionString))
                    {
                        foreach (var item in listaConsultas)
                        {
                            SqlCommand command = new SqlCommand(item, connection);

                            try
                            {
                                connection.Open();
                                using (SqlDataReader dataReader = command.ExecuteReader())
                                {
                                    var listaResultado = new StringBuilder();

                                    var columnNames = Enumerable.Range(0, dataReader.FieldCount)
                                                   .Select(dataReader.GetName)
                                                   .ToList();

                                    //Create headers
                                    listaResultado.Append(string.Join(",", columnNames));

                                    //Append Line
                                    listaResultado.AppendLine();

                                    while (dataReader.Read())
                                    {
                                        for (int i = 0; i < dataReader.FieldCount; i++)
                                        {
                                            string value = dataReader[i].ToString();
                                            if (value.Contains(","))
                                                value = "\"" + value + "\"";

                                            listaResultado.Append(value.Replace(Environment.NewLine, " ") + ",");
                                        }
                                        listaResultado.Length--;
                                        listaResultado.AppendLine();
                                    }
                                    File.WriteAllText(caminhoResultado, listaResultado.ToString());
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
