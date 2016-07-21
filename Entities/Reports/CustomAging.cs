using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Infrastructure;

namespace Intacct.Entities.Reports
{
    [IntacctName("CustomAging")]
    public class CustomAging
    {
        public CustomAging(XElement data)
        {
            this.SetPropertyValue(x => Aging030, data);
            this.SetPropertyValue(x => Aging3160, data);
            this.SetPropertyValue(x => Aging6190, data);
            this.SetPropertyValue(x => Aging91120, data);
            this.SetPropertyValue(x => Aging121, data);
            this.SetPropertyValue(x => TotalEntered, data);
            this.SetPropertyValue(x => TotalDue, data);
            this.SetPropertyValue(x => TotalPaid, data);
            this.SetPropertyValue(x => CustomerId, data);
            this.SetPropertyValue(x => CustEntity, data);
            this.SetPropertyValue(x => CustomerName, data);
            this.SetPropertyValue(x => WhenCreated, data);
            this.SetPropertyValue(x => RecordId, data);
            this.SetPropertyValue(x => WhenDue, data);
        }
        public string Status { get; set; }
        [IntacctName("AGING0_30")]
        public decimal Aging030 { get; set; }
        [IntacctName("AGING31_60")]
        public decimal Aging3160 { get; set; }
        [IntacctName("AGING61_90")]
        public decimal Aging6190 { get; set; }
        [IntacctName("AGING91_120")]
        public decimal Aging91120 { get; set; }
        [IntacctName("AGING121_")]
        public decimal Aging121 { get; set; }
        public decimal TotalEntered { get; set; }
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal CustomerId { get; set; }
        public string CustEntity { get; set; }
        public string CustomerName { get; set; }
        public DateTime WhenCreated { get; set; }
        public int RecordId { get; set; }
        public DateTime WhenDue { get; set; }
        
    }
}
