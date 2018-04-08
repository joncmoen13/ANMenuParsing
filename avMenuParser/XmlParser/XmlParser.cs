using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XmlParser
{
	public class MenuItem
	{
		public string DisplayName { get; set; }
		public string PathValue { get; set; }
		public bool IsActive { get; set; }
		public List<MenuItem> SubMenuItems { get; set; }
	}

	public class XmlParser
	{
		private static StringBuilder _returnString;
		private static string _pathToFind;

		/// <summary>
		/// Builds result string from provided xml file
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="pathToFind"></param>
		/// <returns>Properly formatted result string</returns>
		public static string ParseXml(string filePath, string pathToFind)
		{
			_returnString = new StringBuilder();
			_pathToFind = pathToFind;

			try
			{
				var xDoc = XDocument.Load(filePath);
				var root = xDoc.Root;
				var menuItems = root?.Elements("item").Select(CreateMenuItem).ToList();

				DisplayMenuItems(menuItems);
			}
			catch (Exception)
			{
				return "No valid nodes";
			}

			return _returnString.ToString().TrimEnd('\r', '\n');
		}

		/// <summary>
		/// Creates MenuItem from provided "item" node
		/// </summary>
		/// <param name="item"></param>
		/// <returns>MenuItem with all properties set accordingly</returns>
		private static MenuItem CreateMenuItem(XElement item)
		{
			var menuItem = new MenuItem
			{
				DisplayName = item.Element("displayName")?.Value,
				PathValue = item.Element("path")?.Attribute("value")?.Value,
				SubMenuItems = GetSubMenuItems(item)
			};

			menuItem.IsActive = menuItem.PathValue == _pathToFind || menuItem.SubMenuItems.Any(smi => smi.IsActive);

			return menuItem;
		}

		/// <summary>
		/// Gets list of all SubMenuItems under "item" node
		/// </summary>
		/// <param name="item"></param>
		/// <returns>List of MenuItems</returns>
		private static List<MenuItem> GetSubMenuItems(XElement item)
		{
			return item.Elements("subMenu").Elements("item").Select(CreateMenuItem).ToList();
		}

		/// <summary>
		/// Display all MenuItems.
		/// </summary>
		/// <param name="menuItems"></param>
		private static void DisplayMenuItems(List<MenuItem> menuItems)
		{
			menuItems.ForEach(mi => GetDisplayStringWithIndent(mi, ""));
		}

		/// <summary>
		/// Appends the Return String with the Display String of each MenuItem with the appropriate indentation level.
		/// </summary>
		/// <param name="menuItem"></param>
		/// <param name="indent"></param>
		private static void GetDisplayStringWithIndent(MenuItem menuItem, string indent)
		{
			_returnString.Append(indent + BuildMenuItemDisplayString(menuItem));
			indent = indent + '\t';
			menuItem.SubMenuItems.ForEach(smi => GetDisplayStringWithIndent(smi, indent));
		}

		/// <summary>
		/// Builds the Display String for provided MenuItem.
		/// </summary>
		/// <param name="menuItem"></param>
		/// <returns>Formatted Display String</returns>
		private static string BuildMenuItemDisplayString(MenuItem menuItem)
		{
			var menuItemDisplayString = new StringBuilder();
			var activeText = menuItem.IsActive ? " ACTIVE" : "";

			menuItemDisplayString.Append(menuItem.DisplayName + ", " + menuItem.PathValue + activeText + "\r\n");

			return menuItemDisplayString.ToString();
		}
	}
}
