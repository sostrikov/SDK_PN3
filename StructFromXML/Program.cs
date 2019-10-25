using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace StructFromXML
{

	
	class Program
	{
		public static string GetExecutingDirectorybyAppDomain()
		{
			string path = AppDomain.CurrentDomain.BaseDirectory;
			return path;
		}
		static void Main(string[] args)
		{
			

			string XMLPath = Path.Combine(GetExecutingDirectorybyAppDomain(), "struct.xml");
			Console.WriteLine(XMLPath);
			Stream BMSTUstructXML = File.OpenRead(XMLPath);

			XmlDocument docXML = new XmlDocument(); // XML-документ
			docXML.Load(BMSTUstructXML);// загрузить XML

			XmlNodeList nodeList = docXML.GetElementsByTagName("item");


			XmlNode  my = docXML.GetElementsByTagName("item")[0];

			string attrName = docXML.DocumentElement.SelectNodes("//item/item")[2].Attributes["name"].Value; // получаем значение атрибута "name";
			string attrguid = docXML.DocumentElement.SelectNodes("//item/item")[2].Attributes["guid"].Value; // получаем значение атрибута "name";


			Console.WriteLine($"{attrName} - {attrguid}");

			List<string> nameList = new List<string> { };
				
			foreach (XmlNode item in docXML)
			{
				int i = 0;
				//nameList.Add(item.DocumentElement.SelectNodes("//item/item")[i].Attributes["name"].Value); ;// добавляем текст в ListBox
				i++;
			}

			foreach (string k in nameList)
			{
				Console.WriteLine($"{k}");
			}

			Console.ReadKey();
		}
	}
}
