using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intacct.Operations;

namespace Intacct.Entities.Reports
{
    public class ReadMore<T> : IntacctOperationResult<T>
    {
        public ReadMore(List<T> items)
        {
            Value.AddRange(items);
        }

        public new List<T> Value { get; set; }
    }
}
