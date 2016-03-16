using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Infrastructure;

namespace Intacct.Entities.Supporting_Documents
{
    [IntacctName("supdoc")]
    public class IntacctSupportingDocument : IntacctObject
    {
        public string RecordNo { get; private set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string FolderName { get; set; }
        public string Description { get; set; }
        public IEnumerable<Attachment> Attachments { get; set; } 

        internal override XObject[] ToXmlElements()
        {
            var elements = new List<XObject>()
            {
                new XElement("supdocid", Id),
                new XElement("supdocname", Name),
                new XElement("supdocfoldername", FolderName),
                new XElement("supdocdescription", Description)
            };

            if (Attachments.Any())
            {
                elements.Add(new XElement("attachments", Attachments.Select(a => new XElement("attachment", a.ToXmlElements().Cast<object>()))));
            }

            return elements.ToArray();
        }
    }
}
