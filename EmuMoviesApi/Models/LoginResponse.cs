using System.Xml.Serialization;

namespace EmuMoviesApi.Models
{
	[XmlRoot(ElementName = "Results")]
	public class LoginResponse
	{
		[XmlElement(ElementName = "Result")]
		public Result Result { get; set; }
	}
}
