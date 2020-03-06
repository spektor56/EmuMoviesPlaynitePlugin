using System.Xml.Serialization;

namespace EmuMoviesApi.Models
{ 
    interface IResults
    {
        [XmlElement(ElementName = "Results")]
        Results Results { get; set; }
    }
}
