using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParsecIntegrationClient.IntegrationWebService;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
namespace OrgUnitParsec
{

    [Serializable]
    [XmlRoot("BMSTUoU")]
    public class BMSTUoU
    {
        [XmlArray("ouList"), XmlArrayItem(typeof(MyOrgUnit), ElementName = "OrgUnit")]
        public List<MyOrgUnit> ouList { get; set; }
    }


    [Serializable]
    public class MyOrgUnit
    {
        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public string id { get; set; }
        [XmlAttribute]
        public string parentid { get; set; }
        //[XmlElement(ElementName = "BMSTUouGuid")]
        [XmlAttribute]
        public Guid ouguid { get; set; }

        [XmlAttribute(AttributeName = "OULevel")]
        public int oulevel { get; set; }


        public MyOrgUnit() { }
        public MyOrgUnit(string name01, string id01, string parentid01, Guid ouguid01, int oulevel01)
        {
            name = name01;
            id = id01;
            parentid = parentid01;
            ouguid = ouguid01;
            oulevel = oulevel01;
        }

    }
    class Program

    {
       
           
        
        static void Main(string[] args)
        {

            IntegrationService integrService = new IntegrationService();
            string domain = "SYSTEM";
            string userName = "Ostrikov";
            string password = "QAZwsx111";

            //Подключаемся к серверу Parsec
            SessionResult res = integrService.OpenSession(domain, userName, password);
            Guid sessionGUID = res.Value.SessionID;
            string mySessionID = res.Value.SessionID.ToString();
            Console.WriteLine($"SessionId: {mySessionID}");

            OrgUnit Root = integrService.GetRootOrgUnit(sessionGUID);
            OrgUnit[] parsecHierarhy = integrService.GetOrgUnitsHierarhy(sessionGUID);
           
            Console.WriteLine($"RootName: {Root.NAME.ToString()}");
            foreach (OrgUnit i in parsecHierarhy)
            {
               // Console.WriteLine($"ID: {i.ID} Name: {i.NAME} Parent: {i.PARENT_ID}");
               // if (i.NAME == "МГТУ") Console.WriteLine($"It works: {i.ID}");
            }

            for (int i = 0; i < parsecHierarhy.Length; i++)
            {
                if (parsecHierarhy[i].NAME == "МГТУ")
                {
                    Guid bmstuRoot = parsecHierarhy[i].ID;

                    //Создаем объект для загрузки структуры
                    BMSTUoU f = new BMSTUoU();
                    f.ouList = new List<MyOrgUnit>();

                    XmlDocument xDoc3 = new XmlDocument();
                    xDoc3.Load("struct.xml");
                    // получим корневой элемент
                    XmlElement xRoot3 = xDoc3.DocumentElement;
                    XmlNodeList nodes1 = xRoot3.SelectNodes("item/item");

                    List<Guid> PGuid1 = new List<Guid>();
                    List<Guid> PGuid2 = new List<Guid>();
                    List<Guid> PGuid3 = new List<Guid>();
                    // обход всех узлов в корневом элементе
                    foreach (XmlNode xnode in nodes1)
                    {

                        if (xnode.Attributes.Count > 0)
                        {

                            // получаем атрибут name
                            XmlNode attr1 = xnode.Attributes.GetNamedItem("name");

                            // получаем атрибут id
                            XmlNode attr2 = xnode.Attributes.GetNamedItem("id");

                            // получаем атрибут parentid
                            XmlNode attr3 = xnode.Attributes.GetNamedItem("parentid");

                            // получаем атрибут Guid
                            XmlNode attr4 = xnode.Attributes.GetNamedItem("guid");
                            PGuid1.Add(new Guid(attr4.Value)); //Формируем список Guid 1 уровня
                            Console.WriteLine(PGuid1.Last());

                            //Заносим результат в  Parsec OrgUnit
                            if (attr1 != null && attr2 != null && attr3 != null && attr4 != null)
                            {
                                OrgUnit newOU = new OrgUnit();
                                newOU.ID = new Guid(attr4.Value);
                                newOU.NAME = attr1.Value;
                                newOU.PARENT_ID = bmstuRoot;
                                //GuidResult ouresult = integrService.CreateOrgUnit(sessionGUID, newOU);
                            }

                        }
                        // выбираем все дочерние узлы 2-го уровня

                        foreach (XmlNode xnode2 in xnode.ChildNodes)
                        {
                            if (xnode.Attributes.Count > 0)
                            {

                                // получаем атрибут name
                                XmlNode attr1 = xnode2.Attributes.GetNamedItem("name");

                                // получаем атрибут id
                                XmlNode attr2 = xnode2.Attributes.GetNamedItem("id");

                                // получаем атрибут parentid
                                XmlNode attr3 = xnode2.Attributes.GetNamedItem("parentid");

                                // получаем атрибут Guid
                                XmlNode attr4 = xnode2.Attributes.GetNamedItem("guid");
                                PGuid2.Add(new Guid(attr4.Value));

                                //Заносим результат в  Parsec OrgUnit
                                if (attr1 != null && attr2 != null && attr3 != null && attr4 != null)
                                {
                                    OrgUnit newOU = new OrgUnit();
                                    newOU.ID = new Guid(attr4.Value);
                                    newOU.NAME = attr1.Value;
                                    newOU.PARENT_ID = PGuid1.Last();//Последний элемент в списке ГУИДов 1 уровня
                                    Console.WriteLine($"2ой уровень {newOU.ID} {newOU.NAME} {newOU.PARENT_ID}");

                                   // GuidResult ouresult = integrService.CreateOrgUnit(sessionGUID, newOU);
                                }
                                else Console.WriteLine("OU1 Empty");


                            }
                            // выбираем все дочерние узлы 3-го уровня
                            foreach (XmlNode xnode3 in xnode2.ChildNodes)
                            {
                                if (xnode.Attributes.Count > 0)
                                {

                                    // получаем атрибут name
                                    XmlNode attr1 = xnode3.Attributes.GetNamedItem("name");

                                    // получаем атрибут id
                                    XmlNode attr2 = xnode3.Attributes.GetNamedItem("id");

                                    // получаем атрибут parentid
                                    XmlNode attr3 = xnode3.Attributes.GetNamedItem("parentid");

                                    // получаем атрибут Guid
                                    XmlNode attr4 = xnode3.Attributes.GetNamedItem("guid");
                                    PGuid3.Add(new Guid(attr4.Value));

                                    //Заносим результат в  Parsec OrgUnit
                                    if (attr1 != null && attr2 != null && attr3 != null && attr4 != null)
                                    {
                                        OrgUnit newOU = new OrgUnit();
                                        newOU.ID = new Guid(attr4.Value);
                                        newOU.NAME = attr1.Value;
                                        newOU.PARENT_ID = PGuid2.Last();//Последний элемент в списке ГУИДов 2 уровня

                                       //GuidResult ouresult = integrService.CreateOrgUnit(sessionGUID, newOU);
                                    }
                                    else Console.WriteLine("OU2 Empty");
                                }

                                // выбираем все дочерние узлы 4-го уровня
                                foreach (XmlNode xnode4 in xnode3.ChildNodes)
                                {
                                    if (xnode.Attributes.Count > 0)
                                    {

                                        // получаем атрибут name
                                        XmlNode attr1 = xnode4.Attributes.GetNamedItem("name");

                                        // получаем атрибут id
                                        XmlNode attr2 = xnode4.Attributes.GetNamedItem("id");

                                        // получаем атрибут parentid
                                        XmlNode attr3 = xnode4.Attributes.GetNamedItem("parentid");

                                        // получаем атрибут Guid
                                        XmlNode attr4 = xnode4.Attributes.GetNamedItem("guid");


                                        //Заносим результат в  Parsec OrgUnit
                                        if (attr1 != null && attr2 != null && attr3 != null && attr4 != null)
                                        {
                                            OrgUnit newOU = new OrgUnit();
                                            newOU.ID = new Guid(attr4.Value);
                                            newOU.NAME = attr1.Value;
                                            newOU.PARENT_ID = PGuid3.Last();//Последний элемент в списке ГУИДов 3 уровня

                                            //GuidResult ouresult = integrService.CreateOrgUnit(sessionGUID, newOU);
                                        }


                                    }
                                }
                            }



                        }
                    }

                    //Создаем OU в Parsec
                   // GuidResult ouresult = integrService.CreateOrgUnit(sessionGUID, newOU);
                    Console.WriteLine($"It works TOO: {parsecHierarhy[i].ID}");
                }
            }
            
           
            
            Console.ReadLine();

        }
    }
}
