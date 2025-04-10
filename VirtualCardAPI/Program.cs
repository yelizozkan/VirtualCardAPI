using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using VirtualCardAPI.Context;
using VirtualCardAPI.Extensions;
using VirtualCardAPI.Repositories.Abstract;
using VirtualCardAPI.Repositories.Concrete;
using VirtualCardAPI.Services.Abstract;
using VirtualCardAPI.Services.Concrete;
using VirtualCardAPI.Validators;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VirtualCardDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<VirtualCardValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IVirtualCardRepository, VirtualCardRepository>();

builder.Services.AddScoped<IAuthService, FakeAuthService>();

builder.Services.AddHttpContextAccessor();


builder.Services.ConfigureFakeServices();




builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/api/auth/login";
        options.LogoutPath = "/api/auth/logout";
    });


builder.Services.AddAuthorization();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(new ErrorDetails
        {
            StatusCode = 404,
            Message = "Page not found."
        }.ToString());
    }
});

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
