using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VakifBankTask1.Models;
using VakifBankTask1.Services;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NorthwindDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); //Hali hazirda var olan veri tabanini dogru connectionstring ile Service olarak programa ekledik

builder.Services.AddSingleton<ILoggerService, ConsoleLogger>(); // Ilk once hangi siniftan kalitim aldigini ardindan hangi sinifin calisacagini sirayla iceriye verdik.

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); //Viewmodel'ler ve entity'leri birbirlerine mapleyebilmek uzere AutoMapper servisini programa ekledik.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCustomExceptionMiddleware(); // custom olarak yazdigimiz middleware'i ekledik.

app.MapControllers();
app.Run();


//scaffold-dbcontext "Server=(localdb)\MSSQLLocalDB;Database=NORTHWIND;Trusted_Connection=True;Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -Output:Models -Context:NorthwindDbContext