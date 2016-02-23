using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Infrastructure;

namespace Intacct.Entities.Terms
{
    public class Discount : IntacctObject
    {
        public string DaysForward { get; set; }
        public string PercentAmount { get; set; }
        public string GraceDays { get; set; }

        public Discount(string daysForward, string percentAmount, string graceDays)
        {
            DaysForward = daysForward;
            PercentAmount = percentAmount;
            GraceDays = graceDays;
        }

        public Discount(XElement data)
        {
            this.SetPropertyValue(d => d.DaysForward, data);
            this.SetPropertyValue(d => d.PercentAmount, data);
            this.SetPropertyValue(d => d.GraceDays, data);
        }

        internal override XObject[] ToXmlElements()
        {
            var elements = new List<XObject>()
            {
                new XElement("daysforward", DaysForward),
                new XElement("percentamount", PercentAmount),
                new XElement("gracedays", GraceDays),
            };

            return elements.ToArray();
        }
    }
}
