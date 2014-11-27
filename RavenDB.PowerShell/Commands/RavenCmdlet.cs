using System.Management.Automation;
using System.Net;

namespace RavenDB.PowerShell.Commands
{
	public abstract class RavenCmdlet
		: Cmdlet
	{
		[Parameter(Mandatory = true)]
		public string Url { get; set; }

		[Parameter(ParameterSetName = "WithApiKey")]
		public string ApiKey { get; set; }

		[Parameter(ParameterSetName = "WithLogin")]
		public bool UseWindowsAuth { get; set; }

		[Parameter(ParameterSetName = "WithLogin")]
		public PSCredential Credentials { get; set; }

		/// <summary>
		/// Gets the credentials.
		/// </summary>
		/// <returns></returns>
		protected ICredentials GetCredentials()
		{
			if (this.UseWindowsAuth)
			{
				return CredentialCache.DefaultCredentials;
			}
			else
			{
				return Credentials.GetNetworkCredential();
			}
		}
	}
}
