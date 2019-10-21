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
				//ID = Guid.Empty,
				LAST_NAME = "Трамп2",
				FIRST_NAME = "Дональд",
				MIDDLE_NAME = "Обамович",
				TAB_NUM = "guest-0002",
				ORG_ID = res.Value.RootOrgUnitID

				//ORG_ID = Guid.Parse(myRootOrgUnitID)
				
			};

			//2. Создаем посетителя из физлица (Голубая иконка)
			//integrService.CreatePerson(mySessionIDGuid, myGuest);
			//integrService.CreateVehicleAsync(mySessionIDGuid, myGuest);
			integrService.CreateVisitor(mySessionIDGuid, myGuest);

			textBox2.Text = "Ok!";
			/*

						//3. Создаем заявку на проход
						VisitorRequest myVisitor = new VisitorRequest();

						myVisitor.NUMBER = 777;
						myVisitor.PERSON_INFO = "Очень важный пассажир";
						myVisitor.PURPOSE = "Деловая"; //Цель визита
													   //myVisitor.STATUS =;
													   //myVisitor.ADMIT_START =;
													   //myVisitor.ADMIT_END =;
						myVisitor.DATE = new DateTime(2019, 10, 21);

						//Отправляем запрос на Parsec Заявку
						var res3 = integrService.CreateVisitorRequest(mySessionIDGuid, myVisitor);
			*/
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
