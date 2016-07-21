using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Operations;

namespace Intacct.Entities.Reports
{
    public class ReadMore<T> : IntacctOperationResult<T>
    {
        public ReadMore(IntacctOperationResult<IntacctReadReport> readReportResult, IntacctClient client, IIntacctSession session)
        {
            var readReport = readReportResult.Value;
            if (readReport == null) throw new ArgumentNullException(nameof(readReportResult), $"Value cannot be null for parameter: {nameof(readReportResult)}");

            _reportId = readReport.ReportId;
            _status = readReport.Status;

            _client = client;
            _session = session;


            Value = new List<T>();
        }

        public ReadMore(string status, XElement data)
        {
            _status = status;

            if (data.HasAttributes)
            {
                int.TryParse(data.Attribute("totalcount").Value, out _totalRecords);
                int.TryParse(data.Attribute("numremaining").Value, out _recordsRemaining);

                Data = data.Descendants("report").Elements();
            }
        }

        public IEnumerable<XElement> Data { get; }

        private readonly int _totalRecords;
        public int TotalRecords => _totalRecords;
        private readonly int _recordsRemaining;
        public int RecordsRemaining => _recordsRemaining;

        private readonly string _reportId;
        private string _status;

        public string Status => _status;

        private readonly IIntacctSession _session;
        private readonly IntacctClient _client;

        public void Read()
        {
            _status = "";

            var readMoreResult =
                _client.ExecuteOperations(new[] {new ReadMoreOperation<CustomAging>(_session, _reportId)},
                    CancellationToken.None).Result;

            if (readMoreResult.OperationResults.Any())
            {
                var result = readMoreResult.OperationResults[0] as ReadMore<T>;
                if (result != null)
                {
                    switch (result.Status)
                    {
                        case "PENDING":
                            // wait 30 more seconds
                            var startTime = DateTime.Now;
                            while (DateTime.Now.Subtract(startTime).Seconds < 30)
                            {
                            }
                            Read();
                            break;
                        case "DONE":
                            if (result.TotalRecords > 0 && result.RecordsRemaining > 0)
                            {
                                var elements = result.Data;
                                foreach (var element in elements)
                                {
                                    var item = (T) Activator.CreateInstance(typeof(T), element);
                                    Value.Add(item);
                                }
                                Read();
                            }
                            break;
                    }
                }
                else
                {
                    throw new InvalidCastException("");
                }
            }
         
        }

        public new List<T> Value { get; private set; }

    }
}
