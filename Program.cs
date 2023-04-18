using ScorersReporter.Services;
using ScorersReporter.Entities;
using ScorersReporter.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IScorersReporterService, ScorersReporterService>();
builder.Services.AddDbContext<ScorersReportDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IFileServices, FileServices>();
builder.Services.AddTransient<NBPApiService>();
builder.Services.AddTransient<IScorerMapToScorerDetails, ScorerMapToScorerDetails>();
builder.Services.AddTransient<IReportFromDatabase, ReportFromDatabase>();

builder.Services.AddOptions();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("WriteFilePath"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("NbpApiActions"));

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

app.Run();
