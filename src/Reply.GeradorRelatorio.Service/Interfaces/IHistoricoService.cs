using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reply.GeradorRelatorio.Service.Interfaces
{
    interface IHistoricoService
    {
        void Salvar(string nome, string query);
    }
}
