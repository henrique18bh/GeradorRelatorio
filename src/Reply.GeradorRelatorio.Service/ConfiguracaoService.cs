using Reply.GeradorRelatorio.Entity.DTO;
using Reply.GeradorRelatorio.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Reply.GeradorRelatorio.Service
{
    public class ConfiguracaoService : IConfiguracaoService
    {
        public DadosConfiguracaoDTO ObterDadosConfiguracao()
        {
            var dados = new DadosConfiguracaoDTO();
            var xml = XElement.Load("dadosIniciais.xml");
            var atributo = xml.FirstAttribute;
            return dados;
        }
    }
}
