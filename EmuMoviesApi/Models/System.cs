using System.Xml.Serialization;

namespace EmuMoviesApi.Models
{
	[XmlRoot(ElementName = "System")]
	public class System
	{
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "Maker")]
		public string Maker { get; set; }
		[XmlAttribute(AttributeName = "Lookup")]
		public string Lookup { get; set; }
		[XmlAttribute(AttributeName = "Media")]
		public string Media { get; set; }
		[XmlAttribute(AttributeName = "MediaUpdated")]
		public string MediaUpdated { get; set; }
		[XmlAttribute(AttributeName = "GameExName")]
		public string GameExName { get; set; }
	}
}
