using Reply.GeradorRelatorio.Entity;
using Reply.GeradorRelatorio.Repository.EntityFramework;
using Reply.GeradorRelatorio.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reply.GeradorRelatorio.Repository
{
    public class HistoricoRepository : Interfaces.IHistoricoRepository
    {
        public void Salvar(HistoricoRelatorio historico)
        {
            using (var ctx = new GeradorRelatorioDbContext())
            {
                ctx.HistorioRelatorios.Add(historico);
                ctx.SaveChanges();
            }
        }
    }
}
