using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DataGenerator.cs 'de eklediğimiz Database servisini burada enject ettik. (Dependency Enjection)
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
//Automapper dependency 'sini servis olarak inject etmeliyiz kullanabilmek için. (Dependency Enjection)
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// DataGenerator.cs 'de yazdığımız işlemlerin program ayağa kalkamadan gerçekleşmesi için yazdık.
using(var scope = app.Services.CreateScope()){
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services); 
}

app.Run();
