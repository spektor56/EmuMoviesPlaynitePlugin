using System.Xml.Serialization;

namespace EmuMoviesApi.Models
{
	[XmlRoot(ElementName = "Media")]
	public class Media
	{
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "Extensions")]
		public string Extensions { get; set; }
	}
}
