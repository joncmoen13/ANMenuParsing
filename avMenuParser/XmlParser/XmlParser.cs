using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XmlParser
{
	public class XmlParser
	{
		public static string ParseXml(string filePath, string pathToFind)
		{
			var xDoc = XDocument.Load(filePath);

			return string.Empty;
		}
	}
}
