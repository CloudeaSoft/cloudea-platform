using FilterAndMidWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.Map("/test", async appbuilder => {
    appbuilder.Use(async (context, next) => {
        // 1.1
        Console.WriteLine(1.1);
        await next.Invoke();
        // 1.2
        Console.WriteLine(1.2);
    });
    appbuilder.Use(async (context, next) => {
        // 2.1
        Console.WriteLine(2.1);
        await next.Invoke();
        // 2.2
        Console.WriteLine(2.2);
    });
    appbuilder.UseMiddleware<TestMiddleware>();
    appbuilder.Run(async context => {
        // 3.1
        Console.WriteLine(3.1);
        // 3.2
        Console.WriteLine(3.2);
    });
});


app.MapGet("/",()=>"Hello World!");
app.MapGet("/test",()=>"Test!");

app.UseAuthorization();

app.MapControllers();

app.Run();
