using AuthenticationWebAPI.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add Authentication configuration
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie(options =>
 {
     options.Cookie.Name = "AuthCookie";
     options.LoginPath = "/api/Account/login";
     options.Cookie.HttpOnly = true;
     options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
     // Extended the original TimeSpan if added again
     options.SlidingExpiration = true;
 });
builder.Services.AddDbContext<MyDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
// Add authorization policies
builder.Services.AddAuthorization(options =>
{
    // Policy for specific username "TC"
    options.AddPolicy("OnlyTC", policy =>
        policy.RequireClaim(ClaimTypes.Name, "TC"));

    // Policy for Admin role
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    // Policy for User role
    options.AddPolicy("UserOnly", policy =>
        policy.RequireRole("User"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Must b4 authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
