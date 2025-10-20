using LibraryApi.Connection;
using LibraryApi.Interfaces;
using LibraryApi.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContextDB>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("StringDB")), ServiceLifetime.Scoped);

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReaderService, ReaderService>();
builder.Services.AddScoped<IGenreBooksService, GenreBooksService>();
builder.Services.AddScoped<IHistoryRentalBooksService, HistoryRentalBooksService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
