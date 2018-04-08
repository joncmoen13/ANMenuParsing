using Microsoft.VisualStudio.TestTools.UnitTesting;
using XmlParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Parser = XmlParser.XmlParser;

namespace XmlParserTests
{
	[TestClass]
	public class XmlParserTests
	{
		private static readonly string TestPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

		[TestCase("Empty.xml", "test", "Empty.txt")]
		[TestCase("OneItemNoSubItems.xml", "/Test.aspx", "OneItemActive.txt")]
		[TestCase("OneItemNoSubItems.xml", "/test1.aspx", "OneItemNotActive.txt")]
		[TestCase("ProvidedSchedAeroMenu.xml", "/Requests/OpenQuotes.aspx", "SchedAeroProvidedExample.txt")]
		[TestCase("ProvidedSchedAeroMenu.xml", "/aircraft/Aircraft.aspx", "SchedAeroLastSubActive.txt")]
		[TestCase("ProvidedWyvernMenu.xml", "/aircraft/fleet.aspx", "WyvernSubActive.txt")]
		[TestCase("ProvidedWyvernMenu.xml", "/TWR/AircraftSearch.aspx", "WyvernSubSubActive.txt")]
		[TestCase("ThreeItemsEachThreeSubItems.xml", "SecondTestItem/Tests/SubItems/test6.aspx", "ThreeItemsEachThreeSubItemsSixthSubActive.txt")]
		public void ParseXmlTest(string file, string toFind, string expected)
		{
			var expectedResult = File.ReadAllText(Path.Combine(TestPath, "ExpectedResults", expected));
			var actualResult = Parser.ParseXml(Path.Combine(TestPath, "TestXml", file), toFind);

			NUnit.Framework.Assert.AreEqual(expectedResult, actualResult);
		}
	}
}