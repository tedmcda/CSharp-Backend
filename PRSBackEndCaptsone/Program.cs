using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using PRSBackEndCaptsone.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();


string connStr = builder.Configuration.GetConnectionString("PRSConnectionString");
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connStr));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("*");

app.UseAuthorization();

app.MapControllers();

app.Run();
