using Newtonsoft.Json.Linq;
using System;
using System.Management.Automation;
using System.Net.Http;

namespace RavenDB.PowerShell.Commands
{
	[Cmdlet(VerbsCommon.Get, "RavenDocument")]
	public class GetRavenDocument 
		: RavenCmdlet
	{
		[Parameter(Mandatory = true)]
		public string Database { get; set; }

		[Parameter(Mandatory = true)]
		public string Id { get; set; }

		[Parameter(Mandatory = true)]
		public string DocumentType { get; set; }

		protected override async void BeginProcessing()
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(this.Url);

				HttpResponseMessage response = await client.GetAsync(Database + "/" + DocumentType + "/" + Id);

				if (response.IsSuccessStatusCode)
				{
					dynamic result = JObject.Parse(await response.Content.ReadAsStringAsync());

					WriteObject(result);
				}
				else
				{
					WriteError(new ErrorRecord(new Exception("Received error from REST API" + response.StatusCode), response.StatusCode.ToString(), ErrorCategory.ProtocolError, DocumentType ));
				}
			}
		}
	}
}
