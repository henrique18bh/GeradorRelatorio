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
    }
}
