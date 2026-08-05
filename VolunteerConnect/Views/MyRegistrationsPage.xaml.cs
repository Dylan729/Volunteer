using VolunteerConnect.Models;
using VolunteerConnect.Service;

namespace VolunteerConnect.Views;

public partial class MyRegistrationsPage : ContentPage
{
    private readonly RegistrationService _registrationService;
    private readonly OpportunityService _opportunityService;

    public MyRegistrationsPage(RegistrationService registrationService, OpportunityService opportunityService)
    {
        InitializeComponent();
        _registrationService = registrationService;
        _opportunityService = opportunityService;

        LoadCommands();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _registrationService.InitializeAsync();
        await LoadRegistrations();
    }

    private async Task LoadRegistrations()
    {
        var registrations = await _registrationService.GetUserRegistrationsAsync();
        RegistrationsCollection.ItemsSource = registrations;
    }

    private void LoadCommands()
    {
        BindingContext = new
        {
            ViewDetailsCommand = new Command<int>(async (id) =>
            {
                var opportunity = (await _opportunityService.GetOpportunitiesAsync())
                    .FirstOrDefault(o => o.Id == id);

                if (opportunity != null)
                {
                    // UPDATED: Shell route navigation with query parameter
                    await Shell.Current.GoToAsync($"{nameof(OpportunitiesDetailsPage)}?id={opportunity.Id}");
                }
            }),

            EditCommand = new Command<int>(async (id) =>
            {
                var opportunity = (await _opportunityService.GetOpportunitiesAsync())
                    .FirstOrDefault(o => o.Id == id);

                if (opportunity != null)
                {
                    // UPDATED: Shell route navigation passing object
                    await Shell.Current.GoToAsync(nameof(RegistrationPage), true, new Dictionary<string, object>
                    {
                        { "Opportunity", opportunity }
                    });
                }
            }),

            DeleteCommand = new Command<int>(async (id) =>
            {
                bool confirm = await DisplayAlertAsync("Confirm Delete",
                    "Are you sure you want to cancel this registration?",
                    "Yes", "No");

                if (!confirm) return;

                await _registrationService.DeleteRegistrationAsync(id);
                await DisplayAlertAsync("Deleted", "Registration removed.", "OK");
                await LoadRegistrations();
            })
        };
    }
}
