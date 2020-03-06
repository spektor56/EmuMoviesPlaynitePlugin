using System.Xml.Serialization;

namespace EmuMoviesApi.Models
{
	[XmlRoot(ElementName = "Result")]
	public class Result
	{
		[XmlAttribute(AttributeName = "Success")]
		public bool Success { get; set; }
		[XmlAttribute(AttributeName = "Session")]
		public string Session { get; set; }
		[XmlAttribute(AttributeName = "Error")]
		public string Error { get; set; }
		[XmlAttribute(AttributeName = "MSG")]
		public string MSG { get; set; }
		[XmlAttribute(AttributeName = "TimeTaken")]
		public string TimeTaken { get; set; }
		[XmlAttribute(AttributeName = "Found")]
		public string Found { get; set; }
		[XmlAttribute(AttributeName = "Cached")]
		public string Cached { get; set; }
		[XmlAttribute(AttributeName = "URL")]
		public string URL { get; set; }
		[XmlAttribute(AttributeName = "CRC")]
		public string CRC { get; set; }
	}
}
