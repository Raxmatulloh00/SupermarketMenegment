using DataStore.SQL;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UseCases.Actions;
using UseCases.DataStore;
using Web_Hotel.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAntDesign();



builder.Services.AddDbContext<Travel>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TravelConnection"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<Travel>();

builder.Services.AddScoped<UserMenegerServise>();
builder.Services.AddScoped<RoleMenegerServise>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddTransient<ICountryActions,  CountryActions>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddTransient<ICityActions, CityActions>();
builder.Services.AddScoped<IHotelRepository,  HotelRepository >();
builder.Services.AddTransient<IHotelActions, HotelActions>();
builder.Services.AddScoped<ImageRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");

});

app.Run();
