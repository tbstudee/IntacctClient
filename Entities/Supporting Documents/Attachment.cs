using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Infrastructure;

namespace Intacct.Entities.Supporting_Documents
{
    [IntacctName("attachment")]
    public class Attachment : IntacctObject
    {
        public string Name { get; set; }
        public string FileExtension { get; set; }
        public string Data { get; set; }

        public Attachment() {  }
        public Attachment(XElement data)
        {
            Name = data.Element("attachmentname")?.Value;
            FileExtension = data.Element("attachmenttype")?.Value;
            Data = data.Element("attachmentdata")?.Value;
        }

        internal override XObject[] ToXmlElements()
        {
            var elements = new List<XObject>()
            {
                new XElement("attachmentname", Name),
                new XElement("attachmenttype", FileExtension),
                new XElement("attachmentdata", Data)
            };

            return elements.ToArray();
        }
    }
}
