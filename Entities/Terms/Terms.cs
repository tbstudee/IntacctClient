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
        public string DaysForward { get; set; }
        public DueFrom DueFrom { get; set; } 
        public Terms(string netDays, DueFrom dueFrom)
        {
            DaysForward = netDays;
            DueFrom = dueFrom;
        }

        public Terms(XElement data)
        {
            var daysForward = data.Element("daysforward");
            //if daysForward element exists then we know its net days from invoice data
            if (daysForward != null)
            {
                DaysForward = daysForward.Value;
                DueFrom = DueFrom.InvoiceDate;
            }
            else
            {
                var dayOfMonth = data.Element("dayofmonth");
                if (dayOfMonth != null)
                {
                    DaysForward = dayOfMonth.Value;
                }
                var monthsForward = data.Element("monthsforward");
                if (monthsForward != null)
                {
                    switch (monthsForward.Value.ToLower())
                    {
                        case "0":
                            DueFrom = DueFrom.OfMonth;
                            break;
                        case "1":
                            DueFrom = DueFrom.OfNextMonth;
                            break;
                        case "2":
                            DueFrom = DueFrom.OfSecondMonth;
                            break;
                        case "3":
                            DueFrom = DueFrom.OfThirdMonth;
                            break;
                        case "4":
                            DueFrom = DueFrom.OfFourthMonth;
                            break;
                        case "5":
                            DueFrom = DueFrom.OfFifthMonth;
                            break;
                        case "6":
                            DueFrom = DueFrom.OfSixthMonth;
                            break;
                    }
                }
            }
        }

        internal override XObject[] ToXmlElements()
        {
            var elements = new List<XObject>()
            {
                new XElement("dueday", DaysForward),
                new XElement("duefrom", DueFrom.ToIntacctOptionString())
            };

            return elements.ToArray();
        }
    }
}
