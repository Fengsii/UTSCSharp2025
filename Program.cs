using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Services;

var builder = WebApplication.CreateBuilder(args);


var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

// Replace 'YourDbContext' with the name of your own DbContext derived class.
builder.Services.AddDbContext<ApplicationContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(builder.Configuration.GetConnectionString("MySQLconnection"), serverVersion)
    // The following three options help with debugging, but should
    // be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

builder.Services.AddScoped<ProdukService>();
builder.Services.AddScoped<PesananService>();
// Tambahkan ini untuk mengakses wwwroot
builder.Services.AddSingleton<IWebHostEnvironment>(builder.Environment);

//builder.Services.AddAuthentication("BasicAuthentication")
//    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

//builder.Services.AddAuthorization(options => {
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .AddAuthenticationSchemes("BasicAuthentication")
//        .Build();
//}
//);







// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>

    options
    .WithOrigins("http://localhost:5173", "https://localhost:5173") // Port Vite
     .AllowAnyMethod()
                   .AllowAnyHeader()
        
);

// Aktifkan static files
app.UseStaticFiles();

//// Tambahkan setelah app.UseRouting() (kalau ada)
//app.UseRouting();
//app.UseCors("AllowFrontendLocalhost");


app.UseHttpsRedirection();

//app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
