using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WarFareWPF
{
    public static class Serialize
    {
        public static string SerializeObject<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, toSerialize);

            return textWriter.ToString();
        }

        public static T DeserializeObject<T>(string serObj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(serObj))
            {
                T objet = (T)xmlSerializer.Deserialize(reader);
                return objet;
            }
        }
    }
}
