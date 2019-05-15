using Reply.GeradorRelatorio.Entity.DTO;
using Reply.GeradorRelatorio.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Reply.GeradorRelatorio.Service
{
    public class ConfiguracaoService : IConfiguracaoService
    {
        public DadosConfiguracaoDTO ObterDadosConfiguracao()
        {
            var dados = new DadosConfiguracaoDTO();
            XmlSerializer serializer = new XmlSerializer(typeof(DadosConfiguracaoDTO));
            using (FileStream fileStream = new FileStream(@"C:\Henrique\Projetos\GeradorRelatorio\static\DadosIniciais.xml", FileMode.Open))
            {
                dados = (DadosConfiguracaoDTO)serializer.Deserialize(fileStream);
            }
            return dados;

        }
    }
}
