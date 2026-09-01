using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // OPTIONS
        var appOptions = new AppOptions()
        {
            Credential = GoogleCredential.FromFile("src/Api/Environment/service_account-firebase_admin_config.json")
        };

        // CONFIGS
        FirebaseApp.Create(appOptions);
        



        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}