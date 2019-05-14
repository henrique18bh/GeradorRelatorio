using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reply.GeradorRelatorio.Repository.Interfaces
{
    public interface IRelatorioRepository
    {
        IList<DataTable> ObterRelatorios(IList<string> queries);
    }
}
