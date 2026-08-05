using VolunteerConnect.Service;
using VolunteerConnect.Models;
using VolunteerConnect.Views;

namespace VolunteerConnect.Views;

public partial class HomePage : ContentPage
{
    private readonly OpportunityService _opportunityService;

    public HomePage(OpportunityService opportunityService)
    {
        InitializeComponent();
        _opportunityService = opportunityService;

        LoadFeaturedOpportunity();
        LoadOpportunityCount();
    }

    private async void LoadFeaturedOpportunity()
    {
        var featured = await _opportunityService.GetFeaturedOpportunityAsync();

        FeaturedOpportunityTitle.Text = featured.Title;
        FeaturedOpportunityDescription.Text = featured.ShortDescription;

        FeaturedOpportunityButton.Clicked += async (s, e) =>
        {
            // UPDATED: Shell navigation instead of PushAsync
            await Shell.Current.GoToAsync($"{nameof(OpportunitiesDetailsPage)}?id={featured.Id}");
        };
    }

    private async void LoadOpportunityCount()
    {
        var count = await _opportunityService.GetOpportunityCountAsync();
        OpportunityCountLabel.Text = $"{count} opportunities available";
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        WireNavigation();
    }

    private void WireNavigation()
    {
        BrowseOpportunitiesButton.Clicked -= NavigateToBrowseOpportunities;
        MyRegistrationsButton.Clicked -= NavigateToMyRegistrations;

        BrowseOpportunitiesButton.Clicked += NavigateToBrowseOpportunities;
        MyRegistrationsButton.Clicked += NavigateToMyRegistrations;
    }

    private async void NavigateToBrowseOpportunities(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("/OpportunitiesPage");
    }

    private async void NavigateToMyRegistrations(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("/MyRegistrationsPage");
    }

}
