using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LINQ03
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReadXML();
            //ReadFirstXML();
            //UpdateXML();
            //WriteXML();
            Console.WriteLine("Это все...");
            Console.ReadLine();
        }

        // ПРИМЕР 1. ЧТЕНИЕ ДАННЫХ ИЗ ДОКУМЕНТА XML
        static void ReadXML()
        {
// С помощью метода Load() создаем новый XDocument из файла.
// Создаем запрос, который возвращает всех персонажей, найденных в документе.
// Метод Descendants() возвращает фильтрованную коллекцию подчиненных узелов для данного 
// документа или элемента в порядке следования документов. Только элементы, совпадающим с 
// аргументом метода, входят в состав коллекции.
// Сначала выводим количество элементов.
// Затем выводим список персонажей.
            XDocument xdoc = XDocument.Load(@"C:\Documents and Settings\Admin\Мои документы\Visual Studio 2008\Projects\LINQ03\LINQ03\hamlet.xml");
            var query = from people in xdoc.Descendants("PERSONA")
                        select people.Value; 
            Console.WriteLine("Найдено  {0} персонажей", query.Count());
            Console.WriteLine();
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

        // ПРИМЕР 2. ЧТЕНИЕ ОДНОГО ЭЛЕМЕНТА ИЗ ДОКУМЕНТА XML
        static void ReadFirstXML()
        {
// В этом фрагменте кода сначала берется элемент <PLAY>, производится переход к элементу <PERSONAE>
// и затем используется элемент <PERSONA>. В результате применения такого запроса будет выведена только одна строка.
// Объясняется это тем, что хотя элементов <PERSONA> присутствует  много, обрабатывается только тот из них, который 
// встречается первым при вызове Element().Value
            XDocument xdoc = XDocument.Load(@"C:\Documents and Settings\Admin\Мои документы\Visual Studio 2008\Projects\LINQ03\LINQ03\hamlet.xml");
            Console.WriteLine(xdoc.Element("PLAY").Element("PERSONAE").Element("PERSONA").Value);
            Console.ReadLine();
        }
        
        // ПРИМЕР 3. МОДИФИКАЦИЯ ДАННЫХ В ДОКУМЕНТЕ XML
        static void UpdateXML()
        {
            XDocument xdoc = XDocument.Load(@"C:\Documents and Settings\Admin\Мои документы\Visual Studio 2008\Projects\LINQ03\LINQ03\hamlet.xml");
            xdoc.Element("PLAY").Element("PERSONAE").Element("PERSONA").SetValue("Иван IV Грозный, первый русский царь");
            Console.WriteLine(xdoc.Element("PLAY").Element("PERSONAE").Element("PERSONA").Value);
            Console.ReadLine();
        }

        // ПРИМЕР 4. ЗАПИСЬ ДАННЫХ В ДОКУМЕНТ XML
        static void WriteXML()
        {
            XDocument xdoc = XDocument.Load(@"C:\Documents and Settings\Admin\Мои документы\Visual Studio 2008\Projects\LINQ03\LINQ03\hamlet.xml");
            XElement xe = new XElement("PERSONA", "Иван IV Грозный, первый русский царь");
            xdoc.Element("PLAY").Element("PERSONAE").Add(xe);
            var query = from people in xdoc.Descendants("PERSONA")
                        select people.Value; 
            Console.WriteLine("Найдено {0} персонажей", query.Count());
            Console.WriteLine();
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}
