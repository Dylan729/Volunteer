using VolunteerConnect.Models;
using VolunteerConnect.Service;
using VolunteerConnect.Views;

namespace VolunteerConnect.Views;

public partial class OpportunitiesPage : ContentPage
{
    private readonly OpportunityService _service;
    private List<Opportunity> _allOpportunities = new();

    public OpportunitiesPage(OpportunityService service)
    {
        InitializeComponent();
        _service = service;

        LoadCommands();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _service.InitializeAsync();
        await LoadData();
        LoadFilters();
    }

    private async Task LoadData()
    {
        _allOpportunities = await _service.GetOpportunitiesAsync();
        OpportunitiesCollection.ItemsSource = _allOpportunities;
    }

    private void LoadFilters()
    {
        CategoryPicker.ItemsSource = _allOpportunities
            .Select(o => o.Category)
            .Distinct()
            .ToList();

        LocationPicker.ItemsSource = _allOpportunities
            .Select(o => o.Location)
            .Distinct()
            .ToList();

        AvailabilityPicker.ItemsSource = new List<string>
        {
            "Available",
            "Full"
        };
    }

    private void LoadCommands()
    {
        BindingContext = new
        {
            ViewDetailsCommand = new Command<int>(async (id) =>
            {
                // UPDATED: Correct Shell route navigation
                await Shell.Current.GoToAsync($"{nameof(OpportunitiesDetailsPage)}?id={id}");
            })
        };
    }

    private void OnSearchTextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilters();
    }

    private void OnFilterChanged(object? sender, EventArgs e)
    {
        ApplyFilters();
    }

    private void OnSortChanged(object? sender, EventArgs e)
    {
        ApplyFilters();
    }

    private void ApplyFilters()
    {
        IEnumerable<Opportunity> filtered = _allOpportunities;

        // Search
        if (!string.IsNullOrWhiteSpace(SearchBarControl.Text))
        {
            filtered = filtered.Where(o =>
                o.Title.Contains(SearchBarControl.Text, StringComparison.OrdinalIgnoreCase));
        }

        // Category
        if (CategoryPicker.SelectedItem is string category)
        {
            filtered = filtered.Where(o => o.Category == category);
        }

        // Location
        if (LocationPicker.SelectedItem is string location)
        {
            filtered = filtered.Where(o => o.Location == location);
        }

        // Availability
        if (AvailabilityPicker.SelectedItem is string availability)
        {
            filtered = filtered.Where(o =>
                availability == "Available"
                    ? o.AvailablePlaces > 0
                    : o.AvailablePlaces == 0);
        }

        // Sort
        if (SortPicker.SelectedIndex == 0)
        {
            filtered = filtered.OrderBy(o => o.Date);
        }
        else if (SortPicker.SelectedIndex == 1)
        {
            filtered = filtered.OrderByDescending(o => o.Date);
        }

        OpportunitiesCollection.ItemsSource = filtered.ToList();
    }
}
