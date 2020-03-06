using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EmuMoviesApi.Models
{
    [XmlRoot(ElementName = "ROOTNODE")]
    public class SearchBulkResponse : IResult
    {
        public Dictionary<string, string> Images { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        
        [XmlElement(ElementName = "Result")]
        public Result Result { get; set; }
    }
}
