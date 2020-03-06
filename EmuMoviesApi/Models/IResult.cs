using System.Xml.Serialization;

namespace EmuMoviesApi.Models
{
    interface IResult
    {
        [XmlElement(ElementName = "Result")]
        Result Result { get; set; }
    }
}
