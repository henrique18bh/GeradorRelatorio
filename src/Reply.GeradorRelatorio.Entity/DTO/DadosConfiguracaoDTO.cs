using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Reply.GeradorRelatorio.Entity.DTO
{
    [XmlRoot("dados")]
    public class DadosConfiguracaoDTO
    {
        [XmlElement("CaminhoTxt")]
        public string CaminhoTxt { get; set; }

        [XmlElement("Data")]
        public string Data { get; set; }

        [XmlElement("Hora")]
        public string Hora { get; set; }

        [XmlElement("CaminhoResultado")]
        public string CaminhoResultado{ get; set; }
    }
}
