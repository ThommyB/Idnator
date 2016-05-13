using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Idnator.Helpers
{
    public static class XmlHelper
    {
        private static List<UsedAttribute> UsedAttributes { get; set; } = new List<UsedAttribute>();

        public static string AddIds(string file)
        {
            XDocument doc = XDocument.Load(file);
            ScanDocument(doc);

            foreach (var node in doc.Descendants())
            {
                var atrs = node.Attributes().Where(a => a.Name.LocalName == "id").ToList();
                string elementName = node.Name.LocalName.ToString().Replace(".", "_");

                if (atrs.Count == 0)
                {
                    string attrId = MakeId(elementName);

                    node.SetAttributeValue("{http://schemas.android.com/apk/res/android}id", "@+id/" + attrId);
                }
            }
            return doc.Beautify();
        }

        /// <summary>
        /// Iterates trough whole xml document and saves all id attributes
        /// </summary>
        /// <param name="file"></param>
        private static void ScanDocument(XDocument doc)
        {
            UsedAttributes.Clear();
            foreach (var node in doc.Descendants())
            {
                var atrs = node.Attributes().Where(a => a.Name.LocalName == "id").ToList();

                if (atrs.Count != 0)
                {
                    string elementName = Regex.Match(atrs[0].Value.Replace("@+id/", ""), @"^[A-Za-z_+]+").Value;
                    string elementId = Regex.Match(atrs[0].Value.Replace("@+id/", ""), @"\d+").Value;

                    int id;
                    if (int.TryParse(elementId, out id))
                        UsedAttributes.Add(new UsedAttribute() { Name = elementName, Id = id });
                    else
                        UsedAttributes.Add(new UsedAttribute() { Name = elementName, Id = 1 });
                }
            }
        }

        private static string MakeId(string name)
        {
            int id = 1;
            string attrId = name + id;

            while (UsedAttributes.Where(f => f.ToString() == attrId).LastOrDefault() != null)
            {
                attrId = name + ++id;
            }

            UsedAttributes.Add(new UsedAttribute() { Name = name, Id = id });

            return attrId;
        }
    }
}
