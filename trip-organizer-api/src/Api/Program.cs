using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
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
        var firebaseOptions = builder.Configuration.GetSection("Firebase").Get<FirebaseOptions>();
        FirebaseInitializer.Initialize(firebaseOptions);



        // GLOBAL REPOSITORIES

        // FIREBASE REPOSITORIES
        builder.Services.AddScoped<FirebaseAuthRepository>();
        builder.Services.AddScoped<FirebaseUserRepository>();

        builder.Services.AddControllers();
        builder.Services.AddSingleton<DBSelector>();
        builder.Services.AddScoped<IDBContext, DBContext>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // ENV
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}