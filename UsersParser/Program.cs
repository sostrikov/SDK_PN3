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
        public List<Department> DepList { get; set; }

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

    public class Department
    {
        [XmlAttribute]
        public string prof { get; set; }

        [XmlAttribute]
        public Guid profguid { get; set; }

        [XmlAttribute]
        public string marks { get; set; }

        [XmlAttribute]
        public Guid struct_guid { get; set; }

        [XmlAttribute]
        public Guid vak_guid { get; set; }

        [XmlAttribute]
        public string tabel { get; set; }

        [XmlAttribute]
        public string nomerprikaza { get; set; }

        [XmlAttribute]
        public string dataprikaza { get; set; }

        public Department
            (
            string prof1,
            Guid profguid1,
            string marks1,
            Guid struct_guid1,
            Guid vak_guid1,
            string tabel1,
            string nomerprikaza1,
            string dataprikaza1
            )
        {
            prof = prof1;
            profguid = profguid1;
            marks = marks1;
            struct_guid = struct_guid1;
            vak_guid = vak_guid1;
            tabel = tabel1;
            nomerprikaza = nomerprikaza1;
            dataprikaza = dataprikaza1;

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
                           // Console.Write($"{attr21.Value}\t");

                            XmlNode attr22 = xnode2.Attributes.GetNamedItem("profguid");//GUID профессии  совпадает с OU   professionUID
                            // Console.Write($"{attr22.Value}\t");

                            XmlNode attr23 = xnode2.Attributes.GetNamedItem("marks");//Категория договора
                           // Console.Write($"{attr23.Value}\t");

                            XmlNode attr24 = xnode2.Attributes.GetNamedItem("struct_guid");//GUID структуры совпадает с OU guid
                           // Console.Write($"{attr24.Value}\t");

                            XmlNode attr25 = xnode2.Attributes.GetNamedItem("vak_guid");//GUID вакансии
                           // Console.Write($"{attr25.Value}\t");

                            XmlNode attr26 = xnode2.Attributes.GetNamedItem("guid");//GUID - department
                           //Console.Write($"{attr26.Value}\t");

                            XmlNode attr27 = xnode2.Attributes.GetNamedItem("tabel");//
                            //Console.Write($"{attr27.Value}\t");

                            XmlNode attr28 = xnode2.Attributes.GetNamedItem("nomerprikaza");//
                            //Console.Write($"{attr28.Value}\t");

                            XmlNode attr29 = xnode2.Attributes.GetNamedItem("dataprikaza");//
                            //Console.Write($"{attr28.Value}\t");


                            Console.WriteLine($"prof- {attr21.Value} profguid- {attr22.Value} marks- {attr23.Value} struct_guid- {attr24.Value} vak_guid- {attr25.Value} guid- {attr26.Value}");

                            



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


