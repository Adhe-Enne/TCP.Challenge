using TCP.DataBaseContext.IoC;
using TCP.Business.IoC;
using TCP.Repository.IoC;
using TCP.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilogFromSettings();

var app = builder.Build();

//Injectamos las dependencias
builder.Services.AddDatabaseContext(builder.Configuration.GetConnectionString("DbContext"));
builder.Services.AddRepository();
builder.Services.AddBusiness();
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(Program));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();