using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.Drawing.Imaging;
using System.Diagnostics;

using ParsecIntegrationClient.IntegrationWebService;

namespace ParsecIntegrationClient
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
			textBox1.Text = "SessionId";
			textBox2.Text = "OU";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			
			IntegrationService integrService = new IntegrationService();
			string domain = "SYSTEM";
			string userName = "Ostrikov";
			string password = "QAZwsx111";

			//Подключаемся к серверу Parsec
			SessionResult res = integrService.OpenSession(domain, userName, password);
			//Guid SessionID = new Guid(txtCommonSessionID.Text);


			
			string mySessionID = res.Value.SessionID.ToString();

			textBox1.Text = mySessionID;
			

			Guid mySessionIDGuid = res.Value.SessionID;
			string myRootOrgUnitID = res.Value.RootOrgUnitID.ToString();
			//txtTerraID.Text = res.Value.RootTerritoryID.ToString();

			textBox2.Text = myRootOrgUnitID;


			//1. Создаем физлицо будущего посетителя
			
			Person myGuest = new Person
			{
				ID = Guid.NewGuid(),
				LAST_NAME = "Трамп2",
				FIRST_NAME = "Дональд",
				MIDDLE_NAME = "Обамович",
				TAB_NUM = "guest-666",
				ORG_ID = res.Value.RootOrgUnitID

				//ORG_ID = Guid.Parse(myRootOrgUnitID)
				
			};

			//Сериализация в XML
			
			string directory = AppDomain.CurrentDomain.BaseDirectory;
			string fileXmlName = Path.Combine(directory, "MyVizitors.xml");
			Stream FileStreamXml = File.Create(fileXmlName);
			XmlSerializer serializer = new XmlSerializer(typeof(Person));
			serializer.Serialize(FileStreamXml, myGuest);
			FileStreamXml.Close();

			//Десериализация в XML
			XmlSerializer serializer2 = new XmlSerializer(typeof(Person));
			Stream FileStreamXml2 = File.OpenRead("MyVizitors.xml");
			Person MyFileVisitor = (Person)serializer2.Deserialize(FileStreamXml2);
			FileStreamXml.Close();

			//2. Создаем посетителя (машину) из физлица (Голубая иконка)
			//integrService.CreatePerson(mySessionIDGuid, myGuest);
			//integrService.CreateVehicle(mySessionIDGuid, myGuest);
			var res2 = integrService.CreateVisitor(mySessionIDGuid, myGuest);
		
			textBox2.Text = MyFileVisitor.LAST_NAME;


			//3. Создаем заявку на проход
			VisitorRequest myVisitor = new VisitorRequest
			{
				PERSON_ID = MyFileVisitor.ID,
				ORGUNIT_ID = MyFileVisitor.ORG_ID,
				NUMBER = 777,
				PERSON_INFO = "Очень важный пассажир",
				PURPOSE = "Деловая", //Цель визита
				//STATUS =,
				ADMIT_START = new DateTime(2019, 10, 22, 9, 00, 00),
				ADMIT_END =new DateTime(2019, 10, 22, 13, 00, 00),
				DATE = new DateTime(2019, 10, 22)
			};

			//Отправляем запрос на Parsec Заявку
			var res3 = integrService.CreateVisitorRequest(mySessionIDGuid, myVisitor);
			
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
		/// <summary>
		/// Выход по кажатию кнопки
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button2_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
