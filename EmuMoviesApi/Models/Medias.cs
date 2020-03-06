using System.Collections.Generic;
using System.Xml.Serialization;

namespace EmuMoviesApi.Models
{
	[XmlRoot(ElementName = "Medias")]
	public class Medias
	{
		[XmlElement(ElementName = "Media")]
		public List<Media> Media { get; set; }
	}
}
