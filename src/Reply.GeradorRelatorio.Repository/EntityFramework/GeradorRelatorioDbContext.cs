using Reply.GeradorRelatorio.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reply.GeradorRelatorio.Repository.EntityFramework
{
    public class GeradorRelatorioDbContext
    {
        //public GeradorRelatorioDbContext() : base("name=GeradorRelatorioDbConnectionString")
        //{
        //    Database.SetInitializer(new CreateDatabaseIfNotExists<GeradorRelatorioDbContext>());

        //    //Database.SetInitializer<GeradorRelatorioDbContext>(new DropCreateDatabaseIfModelChanges<GeradorRelatorioDbContext>());
        //    //Database.SetInitializer<GeradorRelatorioDbContext>(new DropCreateDatabaseAlways<GeradorRelatorioDbContext>());
        //}
        //public DbSet<HistoricoRelatorio> HistorioRelatorios { get; set; }
    }
}
