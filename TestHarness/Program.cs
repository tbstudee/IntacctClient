﻿using System;
﻿using System.Collections.Generic;
﻿using System.IO;
using System.Net;
using System.Threading;
using Intacct;
using Intacct.Entities;
﻿using Intacct.Entities.Supporting_Documents;
﻿using Intacct.Entities.Terms;
﻿using Intacct.Entities.Terms.AP;
﻿using Intacct.Entities.Terms.AR;
using Intacct.Operations;
using Newtonsoft.Json;

namespace TestHarness
{
	internal class Program
	{
		private static void Main()
		{
			var settings = LoadSettings();

			var serverUri = new Uri(settings.ServerUri);
			var serverCredential = new NetworkCredential(settings.AccountUsername, settings.AccountPassword);
			var userCredential = new IntacctUserCredential(settings.CompanyName, settings.Username, settings.Password);

			var client = new IntacctClient(serverUri, serverCredential, "2.1");

			var session = client.InitiateApiSession(userCredential).Result;

			Console.WriteLine($"Session created, ID: {session.SessionId}, Endpoint: {session.EndpointUri}");

			// create customer
            /*
			var customer = new IntacctCustomer("C0021", "MT Test " + Guid.NewGuid())
				               {
					               ExternalId = "1337",
					               PrimaryContact = new IntacctContact(Guid.NewGuid().ToString(), "Random")
				               };
			var response = client.ExecuteOperations(new[] { new CreateCustomerOperation(session, customer) }, CancellationToken.None).Result;

			Console.WriteLine($"Customer created: {response.Success}");
			if (!response.Success) return;

			// retrieve customer
			response = client.ExecuteOperations(new[] { new GetEntityOperation<IntacctCustomer>(session, customer.Id) }, CancellationToken.None).Result;
			Console.WriteLine($"Customer retrieved: {response.Success}");
			if (!response.Success) return;

			// create invoice
			var invoice = new IntacctInvoice(customer.Id, new IntacctDate(1, 1, 2015), new IntacctDate(1, 1, 2015));
			var lineItem = IntacctLineItem.CreateWithAccountNumber("2000", 15);
			lineItem.Memo = "Services rendered";
			invoice.Items.Add(lineItem);
			response = client.ExecuteOperations(new[] { new CreateInvoiceOperation(session, invoice) }, CancellationToken.None).Result;

			Console.WriteLine($"Invoice created: {response.Success}");
            */

            /*
            var arterm = new IntacctARTerm()
            {
                Name = "NET15OfMonthWithDiscount",
                Description = "NET 15 of month test",
                Status = TermStatus.Active,
                Terms = new Terms("15", DueFrom.OfFifthMonth),
                DiscountCalculatedOn = DiscountCalculatedOn.InvoiceTotalWithAddedCharges,
                Discount = new Discount("15", DueFrom.OfSecondMonth, "5", DiscountAmountUnit.Percent, "0")
            };
            var apterm = new IntacctAPTerm()
            {
                Name = "NET15OfMonthWithDiscount",
                Description = "NET 15 of month AP test",
                Status = TermStatus.Active,
                Terms = new Terms("15", DueFrom.OfFifthMonth),
                DiscountCalculatedOn = APDiscountCalculatedOn.BillTotalIncludingAllCharges,
                Discount = new Discount("15", DueFrom.InvoiceDate, "5", DiscountAmountUnit.Percent, "0")
            };

		    var response = client.ExecuteOperations(new[] { new CreateAPTermOperation(session, apterm) }, CancellationToken.None).Result;

			response = client.ExecuteOperations(new[] { new GetEntityOperation<IntacctAPTerm>(session, "NET15OfMonthWithDiscount") }, CancellationToken.None).Result;

			response = client.ExecuteOperations(new[] { new GetEntityOperation<IntacctARTerm>(session, "NET15OfMonth") }, CancellationToken.None).Result;
			response = client.ExecuteOperations(new[] { new GetEntityOperation<IntacctARTerm>(session, "NET30") }, CancellationToken.None).Result;
			response = client.ExecuteOperations(new[] { new GetEntityOperation<IntacctARTerm>(session, "NET60") }, CancellationToken.None).Result;
            */

            throw new Exception("your own test code");
		    var path = @"path";
		    var extension = Path.GetExtension(path);
		    var bytes = File.ReadAllBytes(path);
		    var encodedFile = Convert.ToBase64String(bytes);

            var attachment = new Attachment()
            {
                Name = "Doc2",
                FileExtension = "jpg",
                Data = encodedFile
            };

		    var attachments = new List<Attachment>() { attachment };
		    var document = new IntacctSupportingDocument()
		    {
                Name = "Hey supporting Doc 2",
                Id = "102",
                Description = "This is a document 2 description",
                FolderName = "Certs",
                Attachments = attachments
		    };

		    var response = client.ExecuteOperations(new[] { new CreateSupportingDocumentOperation(session, document),  }, CancellationToken.None).Result;

		    Console.ReadLine();
		}

		private static TestHarnessSettings LoadSettings()
		{
			var settings = new TestHarnessSettings();
			if (File.Exists("Settings.json"))
			{
				settings = JsonConvert.DeserializeObject<TestHarnessSettings>(File.ReadAllText("Settings.json"));
			}

			Console.Write("Server URI: ");
			Console.Write(settings.ServerUri);
			settings.ServerUri = Console.ReadLine().OrDefault(settings.ServerUri);

			Console.Write("Account username: ");
			Console.Write(settings.AccountUsername);
			settings.AccountUsername = Console.ReadLine().OrDefault(settings.AccountUsername);
			Console.Write("Account password: ");
			Console.Write(settings.AccountPassword);
			settings.AccountPassword = Console.ReadLine().OrDefault(settings.AccountPassword);

			Console.Write("Company name: ");
			Console.Write(settings.CompanyName);
			settings.CompanyName = Console.ReadLine().OrDefault(settings.CompanyName);
			Console.Write("Username: ");
			Console.Write(settings.Username);
			settings.Username = Console.ReadLine().OrDefault(settings.Username);
			Console.Write("Password: ");
			Console.Write(settings.Password);
			settings.Password = Console.ReadLine().OrDefault(settings.Password);

			File.WriteAllText("Settings.json", JsonConvert.SerializeObject(settings));

			return settings;
		}
	}

	internal class TestHarnessSettings
	{
		public string ServerUri { get; set; }
		public string AccountUsername { get; set; }
		public string AccountPassword { get; set; }
		public string CompanyName { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}

	internal static class ReadLineExtension
	{
		public static string OrDefault(this string value, string defaultValue)
		{
			return (value.Length > 0) ? value : defaultValue;
		}
	}
}