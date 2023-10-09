using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.DBOperations;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);
var JwtConfig = builder.Configuration.GetSection("JwtConfig");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DataGenerator.cs 'de eklediğimiz Database servisini burada enject ettik. (Dependency Enjection)
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
builder.Services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDbContext>());
//Automapper dependency 'sini servis olarak inject etmeliyiz kullanabilmek için. (Dependency Enjection)
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//Yazdığımız servisleri DI Container'a verdik(doğru bir cümle olmamış olabilir.)
builder.Services.AddSingleton<ILoggerService, DbLogger>(); // İlk önce hangi sınıftan kalıtım aldığını ardından hangi sınıfın çalışacağını sırayla içeriye verdik.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=>{
    opt.TokenValidationParameters= new TokenValidationParameters{
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = JwtConfig.GetSection("Token:Issuer").Value,
        ValidAudience = JwtConfig.GetSection("Token:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my custom secret key for authnetication")),
        ClockSkew = TimeSpan.Zero
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCustomExceptionMiddleware(); // Aldığımız exceptionları hem konsolda hemde cilent'e döndürmek için bu middleware'i yazdık. Böylece controller'da her methhod için Catch exception kısmını yazmamıza gerek kalmadan validate.validateAndThrow metdonun attığı hataları middleware ile yakaladık.

app.MapControllers();

// DataGenerator.cs 'de yazdığımız işlemlerin program ayağa kalkamadan gerçekleşmesi için yazdık.
using(var scope = app.Services.CreateScope()){
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services); 
}

app.Run();
