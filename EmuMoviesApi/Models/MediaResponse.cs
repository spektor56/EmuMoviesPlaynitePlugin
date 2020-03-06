using System.Xml.Serialization;

namespace EmuMoviesApi.Models
{
    [XmlRoot(ElementName ="ROOTNODE")]
    public class MediaResponse : IResults
    {
        [XmlElement(ElementName = "Medias")]
        public Medias Medias { get; set; }

        [XmlElement(ElementName = "Results")]
        public Results Results { get; set; }
    }
}
