using ScorersReporter.Services;
using ScorersReporter.Entities;
using ScorersReporter.Models;
using ScorersReporter.Application;
using FileServices;
using NbpApiServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ScorersReportDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IScorersReporterApplication, ScorersReporterApplication>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<INbpApiService, NbpApiService>();
builder.Services.AddTransient<IScorerMapToScorerDetails, ScorerMapToScorerDetails>();
builder.Services.AddTransient<IReportFromDatabase, ReportFromDatabase>();
builder.Services.AddTransient<IScorersDbService, ScorersDbService>();


builder.Services.AddOptions();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("WriteFilePath"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
