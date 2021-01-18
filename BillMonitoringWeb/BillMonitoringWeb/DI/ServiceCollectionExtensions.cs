using BillMonitoring.Client;
using BillMonitoring.Client.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace BillMonitoringWeb.DI
{
	public static class ServiceCollectionExtensions
	{
		public static void RegisterManagersAndServices(this IServiceCollection services)
		{
			services.AddSingleton<IAuthenticationService, AuthenticationService>();
		}
	}
}
