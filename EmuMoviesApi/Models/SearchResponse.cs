using System.Xml.Serialization;

namespace EmuMoviesApi.Models
{
	[XmlRoot(ElementName = "ROOTNODE")]
	public class SearchResponse : IResults
	{
		[XmlElement(ElementName = "Results")]
		public Results Results { get; set; }
	}
}
