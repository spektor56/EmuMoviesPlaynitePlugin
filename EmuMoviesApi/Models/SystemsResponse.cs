using System.Xml.Serialization;

namespace EmuMoviesApi.Models
{
    [XmlRoot(ElementName = "ROOTNODE")]
    public class SystemsResponse : IResults
    {
        [XmlElement(ElementName = "Systems")]
        public Systems Systems { get; set; }
         
        [XmlElement(ElementName = "Results")]
        public Results Results { get; set; }
    }
}
