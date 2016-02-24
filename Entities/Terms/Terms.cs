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
        public string NetDays { get; set; }
        public TermsDueFrom DueFrom { get; set; } 
        public Terms(string netDays, TermsDueFrom dueFrom)
        {
            NetDays = netDays;
            DueFrom = dueFrom;
        }

        public Terms(XElement data)
        {
            var daysForward = data.Element("daysforward");
            //if daysForward element exists then we know its net days from invoice data
            if (daysForward != null)
            {
                NetDays = daysForward.ToString();
                DueFrom = TermsDueFrom.InvoiceDate;
            }
            else
            {
                var dayOfMonth = data.Element("dayofmonth");
                if (dayOfMonth != null)
                {
                    NetDays = dayOfMonth.Value;
                }
                var monthsForward = data.Element("monthsforward");
                if (monthsForward != null)
                {
                    switch (monthsForward.Value.ToLower())
                    {
                        case "0":
                            DueFrom = TermsDueFrom.OfMonth;
                            break;
                        case "1":
                            DueFrom = TermsDueFrom.OfNextMonth;
                            break;
                        case "2":
                            DueFrom = TermsDueFrom.OfSecondMonth;
                            break;
                        case "3":
                            DueFrom = TermsDueFrom.OfThirdMonth;
                            break;
                        case "4":
                            DueFrom = TermsDueFrom.OfFourthMonth;
                            break;
                        case "5":
                            DueFrom = TermsDueFrom.OfFifthMonth;
                            break;
                        case "6":
                            DueFrom = TermsDueFrom.OfSixthMonth;
                            break;
                    }
                }
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
