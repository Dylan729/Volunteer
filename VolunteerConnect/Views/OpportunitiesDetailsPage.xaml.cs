using VolunteerConnect.Models;
using VolunteerConnect.Service;

namespace VolunteerConnect.Views;

[QueryProperty(nameof(OpportunityId), "id")]
public partial class OpportunitiesDetailsPage : ContentPage
{
    private int _opportunityId;
    private readonly OpportunityService _service;

    public int OpportunityId
    {
        get => _opportunityId;
        set
        {
            _opportunityId = value;
            LoadDetails();
        }
    }

    public OpportunitiesDetailsPage()
    {
        InitializeComponent();
        _service = new OpportunityService();
        WireUpRegisterButton();
    }

    private async void LoadDetails()
    {
        var opportunities = await _service.GetOpportunitiesAsync();
        var opportunity = opportunities.FirstOrDefault(o => o.Id == _opportunityId);

        if (opportunity == null)
            return;

        TitleLabel.Text = opportunity.Title;
        CategoryLabel.Text = opportunity.Category;
        ImageControl.Source = opportunity.ImageUrl;

        DateLabel.Text = $"Date: {opportunity.Date:dd MMM yyyy}";
        TimeLabel.Text = $"Time: {opportunity.Time}";
        LocationLabel.Text = $"Location: {opportunity.Location}";

        FullDescriptionLabel.Text = opportunity.ShortDescription;
        RequirementsLabel.Text = opportunity.Requirements;

        AvailablePlacesLabel.Text = $"Available Places: {opportunity.AvailablePlaces}";
    }

    private void WireUpRegisterButton()
    {
        RegisterButton.Clicked += async (s, e) =>
        {
            var opportunities = await _service.GetOpportunitiesAsync();
            var opportunity = opportunities.FirstOrDefault(o => o.Id == _opportunityId);

            if (opportunity == null)
                return;

            // UPDATED: Shell navigation instead of DisplayAlert
            await Shell.Current.GoToAsync(nameof(RegistrationPage), true, new Dictionary<string, object>
            {
                { "Opportunity", opportunity }
            });
        };
    }
}
