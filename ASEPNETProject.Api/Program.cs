using ASEPNETProject.Data.Models;
using ASEPNETProject.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("default");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PersonContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddTransient<IPersonRepository, PersonRepository>();
builder.Services.AddDbContext<ArticleContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddTransient<IArticleRepository, ArticleRepository>();
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddTransient<IProductRepository, ProductRepository>();



var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
