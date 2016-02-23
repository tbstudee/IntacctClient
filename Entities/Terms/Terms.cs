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
        public string DueFrom { get; set; } 
        public Terms(string netDays, string dueFrom)
        {
            NetDays = netDays;
            DueFrom = dueFrom;
        }

        public Terms(XElement data)
        {
            this.SetPropertyValue(x => x.NetDays, data);
        }
        internal override XObject[] ToXmlElements()
        {
            var elements = new List<XObject>()
            {
                new XElement("dueday", NetDays),
                new XElement("duefrom", DueFrom)
            };

            return elements.ToArray();
        }
    }
}
