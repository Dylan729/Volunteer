using VolunteerConnect.Models;
using VolunteerConnect.Service;

namespace VolunteerConnect.Views;

[QueryProperty(nameof(Opportunity), "Opportunity")]
public partial class RegistrationPage : ContentPage
{
    private Opportunity _opportunity;
    private readonly RegistrationService _registrationService;

    // ✅ Parameterless constructor for Shell navigation
    public RegistrationPage()
    {

        InitializeComponent();
        _registrationService = new RegistrationService();
        WireUpSubmitButton();
    }

    // ✅ Shell automatically sets this property when navigating
    public Opportunity Opportunity
    {
        get => _opportunity;
        set
        {
            _opportunity = value;
            OpportunityTitleLabel.Text = value?.Title ?? "Unknown Opportunity";
        }
    }

    private void WireUpSubmitButton()
    {
        SubmitButton.Clicked += async (s, e) =>
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
                string.IsNullOrWhiteSpace(ContactEntry.Text))
            {
                await DisplayAlertAsync("Missing Information",
                    "Please enter your name and contact information.",
                    "OK");
                return;
            }

            // Validate contact info (simple check)
            bool validContact =
                ContactEntry.Text.Contains('@') ||
                ContactEntry.Text.Any(char.IsDigit);

            if (!validContact)
            {
                await DisplayAlertAsync("Invalid Contact",
                    "Please enter a valid email or phone number.",
                    "OK");
                return;
            }

            // Validate privacy consent
            if (!ConsentCheckBox.IsChecked)
            {
                await DisplayAlertAsync("Consent Required",
                    "You must agree to the privacy policy to continue.",
                    "OK");
                return;
            }

            // Defensive check for null opportunity
            if (_opportunity == null)
            {
                await DisplayAlertAsync("Error",
                    "No opportunity selected. Please return and try again.",
                    "OK");
                return;
            }

            // Build registration object
            var registration = new VolunteerRegistration
            {
                OpportunityId = _opportunity.Id,
                PreferredName = NameEntry.Text,
                ContactDetail = ContactEntry.Text,
                Availability = AvailabilityEntry.Text,
                Notes = NoteEditor.Text,
                RegisteredOn = DateTime.Now
            };

            // Save registration
            await _registrationService.AddRegistrationAsync(registration);

            await DisplayAlertAsync("Success",
                "Your registration has been submitted.",
                "OK");

            await Navigation.PopAsync();
        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        WireNavigation();
    }
    private void WireNavigation()
    {
        PrivacyInfoButton.Clicked -= NavigateToPrivacyInfo;

        PrivacyInfoButton.Clicked += NavigateToPrivacyInfo;
    }
    private async void NavigateToPrivacyInfo(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("/PrivacyInformationPage");
    }
}
