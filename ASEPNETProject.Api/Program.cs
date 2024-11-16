using ASEPNETProject.Data.Models;
using ASEPNETProject.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PersonContext>();
builder.Services.AddTransient<IPersonRepository, PersonRepository>();
builder.Services.AddDbContext<ArticleContext>();
builder.Services.AddTransient<IArticleRepository, ArticleRepository>();
builder.Services.AddDbContext<ProductContext>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();



var app = builder.Build();

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
