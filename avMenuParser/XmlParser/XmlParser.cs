using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XmlParser
{
	public class MenuItem
	{
		public string DisplayName { get; set; }
		public string PathValue { get; set; }
		public MenuItem SubMenuItem { get; set; }
		public bool IsActive { get; set; }
	}

	public class XmlParser
	{
		public static string ParseXml(string filePath, string pathToFind)
		{
			var returnString = new StringBuilder();
			var xDoc = XDocument.Load(filePath);
			var root = xDoc.Root;

			if (root == null)
				return "No valid nodes";

			foreach (var node in root.Elements("item"))
			{
				var menuItem = new MenuItem()
				{
					DisplayName = node.Element("displayName")?.Value,
					PathValue = node.Element("path")?.Attribute("value")?.Value,
					SubMenuItem = null
				};

				menuItem.IsActive = menuItem.PathValue == pathToFind;
				var activeText = menuItem.IsActive ? " ACTIVE" : "";
				returnString.Append(menuItem.DisplayName + ", " + menuItem.PathValue + activeText + '\n');
			}

			return returnString.ToString();
		}
	}
}
