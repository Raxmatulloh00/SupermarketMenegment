using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Puligns.DataStore.InMemory;
using UseCases;
using UseCases.CategoriesUseCases;
using UseCases.UseCaseInterfaces;
using UseCases.DataStorePulignInterfaces;
using UseCases.ProductsUseCaseInterfaces;
using UseCases.ProductsUseCase;
using WrbApp.Data;
using UseCases.TransactionsUseCase;
using UseCases.TransactionsUseCaseInterfaces;
using Plugins.DataStore.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System;
using AntDesign.Charts;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MarketContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnaction")
    ));
builder.Services.AddDbContext<WrbAppContext> (options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnaction")
    ));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", p => p.RequireClaim("Position", "Admin"));
    options.AddPolicy("CashierOnly", p => p.RequireClaim("Position", "Cashier"));
});

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//.AddEntityFrameworkStores<WrbAppContext>();


builder.Services.AddAntDesign();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<WeatherForecastService>();
builder.Services.AddScoped<UserManegerService>();
builder.Services.AddScoped<UsersService>();
//******************************************************************************

//builder.Services.AddScoped<ICategoryRepository, CategoryInMemoryRepository>();
//builder.Services.AddScoped<IProductRepository, ProductInMemoryRepository>();
//builder.Services.AddScoped<ITransactionRepository, TransactionMemoryRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

//******************************************************************************
builder.Services.AddTransient<IViewCategoriesUseCase, ViewCategoriesUseCase>();
builder.Services.AddTransient<IAddCategoryUseCase, AddCategoryUseCase>();
builder.Services.AddTransient<IEditCategoryUseCase, EditCategoryUseCase>();
builder.Services.AddTransient<IGetCategoryByIdUseCase, GetCategoryByIdUseCase>();
builder.Services.AddTransient<IDeleteCategoryUseCase, DeleteCategoryUseCase>();
//******************************************************************************
builder.Services.AddTransient<IViewProductsUseCase, ViewProductsUseCase>();
builder.Services.AddTransient<IAddProductsUseCase, AddProductsUseCase>();
builder.Services.AddTransient<IEditProductsUseCase, EditProductsUseCase>();
builder.Services.AddTransient<IGetProductsByIdUseCase,GetProductsByIdUseCase>();
builder.Services.AddTransient<IDeleteProductsUseCase, DeleteProductsUseCase>();
builder.Services.AddTransient<IViewProductsByCategoryId, ViewProductsByCategoryId>();
builder.Services.AddTransient<ISellProductsUseCase, SellProductsUseCase>();
//******************************************************************************
builder.Services.AddTransient<IRecordTransactionUseCase, RecordTransactionUseCase>();
builder.Services.AddTransient<IGetTodayTransactionsUseCase, GetTodayTransactionsUseCase>();
builder.Services.AddTransient<IGetTransactionsUseCase, GetTransactionsUseCase>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}
else
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
	endpoints.MapBlazorHub();
	endpoints.MapFallbackToPage("/_Host");
});

app.Run();
