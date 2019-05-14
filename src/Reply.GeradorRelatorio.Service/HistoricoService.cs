using Reply.GeradorRelatorio.Entity;
using Reply.GeradorRelatorio.Repository;
using Reply.GeradorRelatorio.Repository.Interfaces;
using Reply.GeradorRelatorio.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reply.GeradorRelatorio.Service
{
    public class HistoricoService : IHistoricoService
    {
        private readonly IHistoricoRepository _historicoRepository;

        public HistoricoService()
        {
            _historicoRepository = new HistoricoRepository();
        }

        public void Salvar(string nome, string query)
        {
            var entity = new HistoricoRelatorio
            {
                ConsultaRealizada = query,
                DataGeracao = DateTime.Now,
                Nome = nome
            };

            _historicoRepository.Salvar(entity);
        }
    }
}
