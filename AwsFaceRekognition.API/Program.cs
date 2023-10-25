using AwsFaceRekognition.API.Services;
using AwsFaceRekognition.API.Services.Interfaces;
using Microsoft.Extensions.PlatformAbstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
    builder =>
    {
        builder.WithOrigins("http://localhost:8081")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
    var applicationName = PlatformServices.Default.Application.ApplicationName;
    var xmlDocumentPath = Path.Combine(applicationBasePath, $"{applicationName}.xml");

    if (File.Exists(xmlDocumentPath))
    {
        options.IncludeXmlComments(xmlDocumentPath);
    }
    }
    );

builder.Services.AddTransient<IUtilService, UtilService>();
builder.Services.AddTransient<IDetectFacesService, DetectFacesService>();
builder.Services.AddTransient<ICompareFacesService, CompareFacesService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(
        options => options.WithOrigins("http://localhost:8081").AllowAnyMethod()
    );

app.UseAuthorization();

app.MapControllers();

app.Run();
