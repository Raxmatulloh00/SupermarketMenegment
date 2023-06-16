using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WrbApp.Data;

[assembly: HostingStartup(typeof(SupermarketManagement.Areas.Identity.IdentityHostingStartup))]
namespace SupermarketManagement.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
	{
		public void Configure(IWebHostBuilder builder)
		{
			builder.ConfigureServices((context, services) => {
				services.AddDbContext<WrbAppContext>(options =>
					options.UseSqlServer(
						context.Configuration.GetConnectionString("WrbAppContextConnection")));

				services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
					.AddEntityFrameworkStores<WrbAppContext>();
			});
		}
	}
}