using ENSEKTest.Models.EFModels;
using ENSEKTest.Services;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ENSEKContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ENSEKDatabase"));
});

//DI configuration:
builder.Services.AddScoped<IMeterReadingUploadService, MeterReadingUploadService>();
builder.Services.AddScoped<IParserService<IFormFile, IEnumerable<MeterReading>>, CSVParserService>();
builder.Services.AddScoped<IUploadService<MeterReading>, DatabaseUploadService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "ENSEK Technical Test",
        Description = "An ASP.NET Core Web API for uploading meter readings",
    });
});

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
