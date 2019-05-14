using Reply.GeradorRelatorio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reply.GeradorRelatorio.Repository.Interfaces
{
    public interface HistoricoRelatorioRepository
    {
        void Salvar(HistoricoRelatorio historico);
    }
}
