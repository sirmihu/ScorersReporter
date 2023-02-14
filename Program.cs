using ScorersReporter.Services;
using ScorersReporter.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IScorersReporterService, ScorersReporterService>();
builder.Services.AddDbContext<ScorersReportDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<FileReader>();
builder.Services.AddTransient<RateExchange>();
builder.Services.AddTransient<ScorerDetailsDtos>();
builder.Services.AddTransient<ReportFromDatabase>();
builder.Services.AddTransient<FileWriter>();

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
