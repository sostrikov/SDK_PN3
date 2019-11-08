using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using ParsecIntegrationClient.IntegrationWebService;

namespace GetOrgUnitsHierarhyWithPersons
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
            BaseObject[] array01 = integrService.GetOrgUnitsHierarhyWithPersons(sessionGUID);
            for (int i = 0; i < array01.Length; i++)
            {
                if (array01[i].GetType().Name is "Person")
                {
                    Person pers = (Person)array01[i];
                    //Console.WriteLine($"{i} - Person {pers.ID} ");
                }
                else
                {
                    OrgUnit orgu = (OrgUnit)array01[i];
                    Console.WriteLine($"{i} - OrgUnit {orgu.DESC} ");
                }
                //else Console.WriteLine($"{i} - OrgUnit");
               //Console.WriteLine($"{i} -> {array01[i].GetType().Name}");
            }

               
            //if (array01[2].GetType() is Person) Console.WriteLine($"0 - Person");
            Console.ReadKey();
        }
    }
}
