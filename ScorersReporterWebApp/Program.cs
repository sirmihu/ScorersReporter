using ScorersReporterApi.Utils;
using SrApi = ScorersReporterApi;
using ScorersReporterApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<IScorersReporterHttpClient, ScorersReporterHttpClient>();
builder.Services.AddTransient<SrApi.IScorersRepoterApi, SrApi.ScorersReporterApi>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("ScorersReporterApiUrl"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Scorers}/{action=Index}/{id?}");

app.Run();

