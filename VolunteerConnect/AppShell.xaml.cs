using VolunteerConnect.Views;

namespace VolunteerConnect;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register routes for navigation
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(OpportunitiesPage), typeof(OpportunitiesPage));
        Routing.RegisterRoute(nameof(OpportunitiesDetailsPage), typeof(OpportunitiesDetailsPage));
        Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
        Routing.RegisterRoute(nameof(MyRegistrationsPage), typeof(MyRegistrationsPage));
        Routing.RegisterRoute(nameof(PrivacyInformationPage), typeof(PrivacyInformationPage));

    }
}
