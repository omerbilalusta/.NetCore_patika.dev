using MiddlewarePractices.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

//app.Run()
// app.Run(async context => Console.WriteLine("Middleware 1."));
// app.Run(async context => Console.WriteLine("Middleware 2."));

// //app.Use
// app.Use(async(context, next)=>{
//     Console.WriteLine("Middleware 1 başladı!");
//     await next.Invoke();
//     Console.WriteLine("Middleware 1 sonlandırılıyor..");
// });

// app.Use(async(context, next)=>{
//     Console.WriteLine("Middleware 2 başladı!");
//     await next.Invoke();
//     Console.WriteLine("Middleware 2 sonlandırılıyor..");
// });

// app.Use(async(context, next)=>{
//     Console.WriteLine("Middleware 3 başladı!");
//     await next.Invoke();
//     Console.WriteLine("Middleware 3 sonlandırılıyor..");
// });

app.UseHello();

app.Use(async(context, next)=>{
    Console.WriteLine("Use Middleware tetiklendi.");
    await next.Invoke();
});
//app.Map()
app.Map("/example", internalApp =>
internalApp.Run(async context=>{
    Console.WriteLine("/exapmle middleware tetiklendi");
    await context.Response.WriteAsync("/exapmle middleware tetklendi");
}));

//app.MapWhen()
app.MapWhen(x=> x.Request.Method == "GET", internalApp =>{
    internalApp.Run(async context=>{
        Console.WriteLine("MapWhen Middleware tetiklendi");
        await context.Response.WriteAsync("MapWhen MiddleWare tetiklendi");
    });
});

app.MapControllers();

app.Run();

