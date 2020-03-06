using System.Xml.Serialization;

namespace EmuMoviesApi.Models
{
	[XmlRoot(ElementName = "Results")]
	public class Results
	{
		[XmlElement(ElementName = "Result")]
		public Result Result { get; set; }
	}
}
