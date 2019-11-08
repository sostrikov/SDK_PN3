using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using ParsecIntegrationClient.IntegrationWebService;
using System.Reflection;

namespace ExtraFieldTemplates
{

    

       
    
    
    class Program
    {


        static void Main(string[] args)
        {
          
           
               
        

            // ===============PARSEC + ==========================================================
            IntegrationService integrService = new IntegrationService();
            string domain = "SYSTEM";
            string userName = "Ostrikov";
            string password = "QAZwsx111";

            //Подключаемся к серверу Parsec
            SessionResult res = integrService.OpenSession(domain, userName, password);
            Guid sessionGUID = res.Value.SessionID;
            string mySessionID = res.Value.SessionID.ToString();
            Console.WriteLine($"SessionId: {mySessionID}");

            PersonExtraFieldTemplate[] myTemplates = integrService.GetPersonExtraFieldTemplates(sessionGUID);
           var bmstuID_templ = myTemplates[0].ID; //Uid в ЭУ
           var bmstuName_templ = myTemplates[1].ID; //Номер личного дела

            foreach (var k in myTemplates)
            {
                Console.WriteLine($"{k.ID} - {k.NAME} - {k.TYPE}");
            }
            OrgUnit Root = integrService.GetRootOrgUnit(sessionGUID);
            OrgUnit[] parsecHierarhy = integrService.GetOrgUnitsHierarhy(sessionGUID);

            //Для хранения GUID OU Студенты
            Stack<Guid> ouGuid = new Stack<Guid>(); 
            foreach (OrgUnit i in parsecHierarhy)
            {

                if (i.NAME == "Студенты")
                {
                    ouGuid.Push(i.ID);
                    Console.WriteLine($"Студенты: {i.ID}");
                }
            }
           
           /*
            foreach (Person per in persons)
            {
               


               
                    // var pg = per.ID;
                    var res1 = integrService.GetPersonExtraFieldValue(sessionGUID, per.ID, bmstuID_templ);
                    var res2 = integrService.GetPersonExtraFieldValue(sessionGUID, per.ID, bmstuName_templ);
                    Console.WriteLine($"{per.LAST_NAME} {res1}, {res2}");
                
            }
            */
            Console.ReadKey();
        }
    }
}
