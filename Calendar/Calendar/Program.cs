

using Calendar.Service.Facade.services;
using Calendar.Service.Imp.services;
using Calendar.DataAccess.Facade.provider;
using Calendar.DataAccess.Imp;
using Calendar.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var appSettings = builder.Configuration.Get<AppSettings>();
var connectionString = builder.Configuration.GetConnectionString("GoogleTokenDatabase");
builder.Services.RegisterGoogleToken(connectionString);
builder.Services.RegisterDal();
builder.Services.AddDistributedMemoryCache(); // You can also use other providers like Redis
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the timeout as needed
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICalendarService, CalendarService>();
builder.Services.AddScoped<IExternalCalendarSal, GoogleCalendarSal>();
builder.Services.AddSingleton(appSettings);

builder.Services.AddHttpClient<IExternalCalendarSal, GoogleCalendarSal>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
var app = builder.Build();
app.UseSession();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
