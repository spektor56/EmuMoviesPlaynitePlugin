using System.Collections.Generic;
using System.Xml.Serialization;

namespace EmuMoviesApi.Models
{
    [XmlRoot(ElementName = "Systems")]
    public class Systems
    {
        [XmlElement(ElementName = "System")]
        public List<System> System { get; set; }
    }
}
