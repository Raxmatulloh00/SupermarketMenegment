using System;
using AddUserWithRols.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AddUserWithRols.Areas.Identity.IdentityHostingStartup))]
namespace AddUserWithRols.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<AddUserContext>(options =>
                  options.UseSqlServer(
                    context.Configuration.GetConnectionString("AddUserContextConnection")));

                services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                  .AddEntityFrameworkStores<AddUserContext>();
            });
        }
    }
}