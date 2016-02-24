using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Infrastructure;

namespace Intacct.Entities.Terms
{
    public class Terms : IntacctObject
    {
        [IntacctName("dueday")]
        public string NetDays { get; set; }
        public TermsDueFrom DueFrom { get; set; } 
        public Terms(string netDays, TermsDueFrom dueFrom)
        {
            NetDays = netDays;
            DueFrom = dueFrom;
        }

        public Terms(XElement data)
        {
            var netDays = data.Element("daysforward");
            if (netDays != null && !netDays.IsEmpty)
            {
                NetDays = netDays.ToString();
            }
        }
        internal override XObject[] ToXmlElements()
        {
            var elements = new List<XObject>()
            {
                new XElement("dueday", NetDays),
                new XElement("duefrom", DueFrom.ToIntacctOptionString())
            };

            return elements.ToArray();
        }
    }
}
