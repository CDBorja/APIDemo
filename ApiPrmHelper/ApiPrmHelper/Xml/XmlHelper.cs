using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApiPrmHelper.Xml
{
    class XmlHelper
    {
        public static void WriteXML(Book overview)
        {
            /*
            Book overview = new Book();
            overview.key = "LauValue";
            overview.value = "ABC";
            */

            var path = Environment.CurrentDirectory + "//ApiPrmHelper-data.xml";
            FileStream file = File.Create(path);

            XmlSerializer writer = new XmlSerializer(typeof(Book));
            writer.Serialize(file, overview);
            file.Close();
        }

        public static Book ReadXML()
        {
            // First write something so that there is something to read ...  
            /*var b = new Book { key = "LauValue" };
            var writer = new XmlSerializer(typeof(Book));
            var wfile = new StreamWriter(@"c:\temp\SerializationOverview.xml");
            writer.Serialize(wfile, b);
            wfile.Close();*/

            Book overview = new Book() { json = "", key = "" };

            // Now we can read the serialized book ...  
            try
            {
                XmlSerializer reader = new XmlSerializer(typeof(Book));
                StreamReader file = new StreamReader(Environment.CurrentDirectory + "//ApiPrmHelper-data.xml");

                overview = (Book)reader.Deserialize(file);
                file.Close();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            //Console.WriteLine(overview.title);
            return overview;
        }
    }
}
