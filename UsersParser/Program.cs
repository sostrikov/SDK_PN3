using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace UsersParser
{

    [Serializable]
    [XmlRoot("persons")]
    public class persons
    {
        [XmlArray("personsList"), XmlArrayItem(typeof(Person), ElementName = "Person")]
        public List<Person> personsList { get; set; }
    }

    [Serializable]
    public class Person
    {
        [XmlAttribute]
        public string lastname { get; set; }
        [XmlAttribute]
        public string firstname { get; set; }
        [XmlAttribute]
        public string middlename { get; set; }
        [XmlAttribute]
        public string id { get; set; }
        [XmlAttribute]
        public string birthday { get; set; }
        [XmlAttribute]
        public Guid personguid { get; set; }

        public Person() { }
        public Person(string lastname01, string firstname01, string middlename01, string id01, string birthday01, Guid personguid01)
        {
            lastname = lastname01;
            firstname = firstname01;
            middlename = middlename01;
            id = id01;
            birthday = birthday01;
            personguid = personguid01;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            //Создаем объект для загрузки сотрудников
            persons f2 = new persons();
            f2.personsList = new List<Person>();

            //=================== persons =====================================
            //List<Person> personList = new List<Person>();

            XmlDocument xDoc4 = new XmlDocument();
            xDoc4.Load("persons.xml");
            // получим корневой элемент
            XmlElement xRootPerson = xDoc4.DocumentElement;
            XmlNodeList personsnodes = xRootPerson.SelectNodes("person");
            foreach (XmlNode xnode in personsnodes)
            {

                if (xnode.Attributes.Count > 0)
                {
                    // получаем атрибут lastname
                    XmlNode attr1 = xnode.Attributes.GetNamedItem("lastname");
                    Console.WriteLine($"{attr1.Value}\t");

                    // получаем атрибут firstname
                    XmlNode attr2 = xnode.Attributes.GetNamedItem("firstname");
                    //Console.Write($"{attr2.Value}\t");

                    // получаем атрибут firstname
                    XmlNode attr3 = xnode.Attributes.GetNamedItem("middlename");
                    //Console.Write($"{attr3.Value}\t");

                    // получаем атрибут id
                    XmlNode attr4 = xnode.Attributes.GetNamedItem("id");
                    //Console.Write($"{attr4.Value}\t");

                    // получаем атрибут birthday
                    XmlNode attr5 = xnode.Attributes.GetNamedItem("birthday");
                    //Console.Write($"{attr5.Value}\t");

                    // получаем атрибут birthday
                    XmlNode attr6 = xnode.Attributes.GetNamedItem("guid");

                    //Console.Write($"{attr6.Value}");
                    //Console.WriteLine();
                    //Заносим результат в список объектов MyOrgUnitp
                    if (attr1 != null && attr2 != null && attr3 != null && attr4 != null && attr5 != null && attr6 != null)
                        f2.personsList.Add(new Person() { lastname = attr1.Value, firstname = attr2.Value, middlename = attr3.Value, id = attr4.Value, birthday = attr5.Value, personguid = new Guid(attr6.Value) });
                    //personList.Add(new Person(attr1.Value, attr2.Value, attr3.Value, attr4.Value, attr5.Value, new Guid(attr6.Value)));

                    //department
                    foreach (XmlNode xnode2 in xnode.ChildNodes)
                    {
                        if (xnode2.Attributes.Count > 0)
                        {
                            XmlNode attr21 = xnode2.Attributes.GetNamedItem("prof");//профессия
                            Console.Write($"{attr21.Value}\t");

                            XmlNode attr22 = xnode2.Attributes.GetNamedItem("profguid");//GUID профессии
                            Console.Write($"{attr22.Value}\t");

                            XmlNode attr23 = xnode2.Attributes.GetNamedItem("marks");//Категория договора
                            Console.Write($"{attr23.Value}\t");

                            XmlNode attr24 = xnode2.Attributes.GetNamedItem("struct_guid");//GUID структуры
                            Console.Write($"{attr24.Value}\t");

                            XmlNode attr25 = xnode2.Attributes.GetNamedItem("vak_guid");//GUID вакансии
                            Console.Write($"{attr25.Value}\t");

                            XmlNode attr26 = xnode2.Attributes.GetNamedItem("guid");//GUID ???
                            Console.Write($"{attr26.Value}\t");

                            // tabel = "143565" nomerprikaza = "02.13-03/1088" dataprikaza = "2018-02-07"



                        }
                    }

                    Console.ReadKey();

                    //Сериализация в XML
                    // передаем в конструктор тип класса
                    XmlSerializer s2 = new XmlSerializer(typeof(persons));

                    //Чистим пространство имен в XML
                    XmlSerializerNamespaces ns2 = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

                    // получаем поток, куда будем записывать сериализованный объект
                    using (FileStream fs2 = new FileStream("persons2.xml", FileMode.OpenOrCreate))
                    {
                        s2.Serialize(fs2, f2);
                    }

                }
            }
        }
    }
}


