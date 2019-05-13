using Reply.GeradorRelatorio.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reply.GeradorRelatorio.Entity
{
    public class HistoricoRelatorio : EntityBase
    {
        public string Nome { get; set; }
        public DateTime DataGeracao { get; set; }
        public string ConsultaRealizada { get; set; }
    }
}
