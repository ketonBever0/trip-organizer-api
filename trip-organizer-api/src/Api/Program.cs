using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using trip_organizer_api.src.Api.Middlewares;
using trip_organizer_api.src.Application;
using trip_organizer_api.src.Application.Interfaces;
using trip_organizer_api.src.Infrastructure.Config;
using trip_organizer_api.src.Infrastructure.Context;
using trip_organizer_api.src.Infrastructure.Firebase;
using trip_organizer_api.src.Infrastructure.Firebase.Repositories;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

        // CONFIGS
        var firebaseOptions = builder.Configuration.GetSection("Firebase").Get<FirebaseOptions>() ?? throw new Exception("Firebase configuration is missing.");
        var firebaseClientConfig = builder.Configuration.GetSection("FirebaseClientConfig").Get<FirebaseClientConfig>() ?? throw new Exception("Firebase Client configuration is missing.");
        FirebaseInstance.Initialize(firebaseOptions, firebaseClientConfig);

        // GLOBAL REPOSITORIES

        // FIREBASE REPOSITORIES
        builder.Services.AddScoped<FirebaseAuthRepository>();
        builder.Services.AddScoped<FirebaseUserRepository>();

        builder.Services.AddControllers();
        builder.Services.AddSingleton<DBSelector>();
        builder.Services.AddScoped<IDBContext, DBContext>();


        // AUTH
        builder.Services
            .AddAuthentication()
            .AddBearerToken();

        //builder.Services
        //    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer(options => builder.Configuration.Bind("JwtSettings", options))
        //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        //        options => builder.Configuration.Bind("CookieSettings", options)
        //        );

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // GLOBAL EXCEPTION HANDLER
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        var app = builder.Build();

        // ENV
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}