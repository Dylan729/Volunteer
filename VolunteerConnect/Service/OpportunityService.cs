using VolunteerConnect.Models;
using VolunteerConnect.Service;
using VolunteerConnect.Views;

namespace VolunteerConnect.Service;

public class OpportunityService
{
    private readonly List<Opportunity> _mockData = new()
    {
        new Opportunity
        {
            Id = 1,
            Title = "Beach Cleanup",
            Category = "Environment",
            Date = DateTime.Today.AddDays(3),
            Time = "10:00 AM",
            Location = "Mission Bay",
            ShortDescription = "Help clean up the beach and protect marine life.",
            AvailablePlaces = 12,
            ImageUrl = "beach_cleanup.png"
        },
        new Opportunity
        {
            Id = 2,
            Title = "Food Bank Assistance",
            Category = "Community",
            Date = DateTime.Today.AddDays(5),
            Time = "9:00 AM",
            Location = "Central Auckland",
            ShortDescription = "Assist with sorting and distributing food donations.",
            AvailablePlaces = 0,
            ImageUrl = "food_bank.png"
        }
    };

    private readonly DatabaseService _database = new();

    public async Task InitializeAsync()
    {
        await _database.InitializeAsync();
    }

    public Task<List<VolunteerOpportunity>> GetAllAsync()
        => _database.Connection.Table<VolunteerOpportunity>().ToListAsync();

    public Task<VolunteerOpportunity> GetByIdAsync(int id)
        => _database.Connection.Table<VolunteerOpportunity>()
            .FirstOrDefaultAsync(o => o.Id == id);

    public Task<int> AddAsync(VolunteerOpportunity opportunity)
        => _database.Connection.InsertAsync(opportunity);

    public Task<int> UpdateAsync(VolunteerOpportunity opportunity)
        => _database.Connection.UpdateAsync(opportunity);

    public Task<int> DeleteAsync(int id)
        => _database.Connection.DeleteAsync<VolunteerOpportunity>(id);

    public Task<List<Opportunity>> GetOpportunitiesAsync()
        => Task.FromResult(_mockData);

    public Task<Opportunity> GetFeaturedOpportunityAsync()
        => Task.FromResult(_mockData.First());

    public Task<int> GetOpportunityCountAsync()
        => Task.FromResult(_mockData.Count);
}
