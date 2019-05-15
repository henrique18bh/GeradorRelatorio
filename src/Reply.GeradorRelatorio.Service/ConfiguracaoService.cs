using log4net;
using Reply.GeradorRelatorio.Entity.DTO;
using Reply.GeradorRelatorio.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private static readonly ILog log = LogManager.GetLogger("Service de relatórios");
        public DadosConfiguracaoDTO ObterDadosConfiguracao()
        {
            try
            {
                var dados = new DadosConfiguracaoDTO();
                XmlSerializer serializer = new XmlSerializer(typeof(DadosConfiguracaoDTO));
                using (FileStream fileStream = new FileStream(ConfigurationManager.AppSettings["caminhoXML"], FileMode.Open))
                {
                    dados = (DadosConfiguracaoDTO)serializer.Deserialize(fileStream);
                }
                return dados;
            }
            catch (Exception ex)
            {
                log.Error("Erro ao obterDados de configuração XML", ex);
            }
            return null;
        }

        public void AtualizarDadosConfiguracao(DadosConfiguracaoDTO dados)
        {
            FileStream stream = new FileStream(ConfigurationManager.AppSettings["caminhoXML"], FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(dados.GetType());
            serializer.Serialize(stream, dados);
        }
    }
}
