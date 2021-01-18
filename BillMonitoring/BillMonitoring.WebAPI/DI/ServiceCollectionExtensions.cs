using BillMonitoring.BLL;
using BillMonitoring.BLL.Contracts;
using BillMonitoring.DAL.Contracts;
using BillMonitoring.DAL.UnitOfWork;
using BillMonitoring.WebAPI.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillMonitoring.WebAPI.DI
{
	public static class ServiceCollectionExtensions
	{
		public static void RegisterManagersAndRepositories(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IPeriodManager, PeriodManager>();
			services.AddScoped<IBillManager, BillManager>();
			services.AddScoped<ICategoryManager, CategoryManager>();
		}

		public static void AddAutoMapper(this IServiceCollection services)
		{
			services.AddScoped<BMAutoMapper>();
		}
	}
}
