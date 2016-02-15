using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Idnator
{
    class XmlHelper
    {
        private static List<UsedAttribute> _usedAttributes = new List<UsedAttribute>();

        public static string AddIds(string file)
        {
            _usedAttributes = new List<UsedAttribute>();

            XDocument doc = XDocument.Load(file);
            foreach (var node in doc.Descendants())
            {
                var atrs = node.Attributes().Where(a => a.Name.LocalName == "id").ToList();
                if (atrs.Count == 0)
                {
                    string name = node.Name.LocalName.ToString();
                    int id = 0;
                    var lastNode = _usedAttributes.FindLast(f => f.Name == name);

                    if (lastNode == null)
                    {
                        id = 1;
                        _usedAttributes.Add(new UsedAttribute() { Name = name, Id = 1 });
                    }
                    else
                    {
                        id = lastNode.Id + 1;
                        _usedAttributes.Add(new UsedAttribute() { Name = name, Id = id });
                    }

                    node.SetAttributeValue("{http://schemas.android.com/apk/res/android}id", "@+id/" + name + id);
                }
            }
            return doc.Beautify();
        }

    }
}
