using AspNetCoreFile.Example.Internals;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(v =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.GetFullPath(xmlFile, AppContext.BaseDirectory);
    v.IncludeXmlComments(xmlPath);
    v.SwaggerDoc("v1", new OpenApiInfo { Title = "KaizuyaHanbai", Version = "v1" });
    v.OperationFilter<AjaxRequestHeaderParameterOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
