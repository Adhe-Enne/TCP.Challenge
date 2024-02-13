using TCP.DataBaseContext.IoC;
using TCP.Business.IoC;
using TCP.Repository.IoC;
using Serilog;
using TCP.Api;

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseIISIntegration();
builder.Host.UseSerilogFromSettings();
//Injectamos las dependencias
builder.Services.AddDatabaseContext(builder.Configuration.GetConnectionString("DbContext"));
builder.Services.AddRepository();
builder.Services.AddBusiness();
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(Program));

Log.Information("Inicializando FacturadorDB.");

//cors publico
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
    builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();