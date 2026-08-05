using Microsoft.Extensions.Logging;
using VolunteerConnect.Service;
using VolunteerConnect.Views;

namespace VolunteerConnect
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register services
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<OpportunityService>();
            builder.Services.AddSingleton<RegistrationService>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<PrivacyService>();


            // Register pages
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<OpportunitiesPage>();
            builder.Services.AddSingleton<MyRegistrationsPage>();
            builder.Services.AddSingleton<OpportunitiesDetailsPage>();
            builder.Services.AddSingleton<RegistrationPage>();
            builder.Services.AddSingleton<PrivacyInformationPage>();

            // Register Database



#if DEBUG
                builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }

}
