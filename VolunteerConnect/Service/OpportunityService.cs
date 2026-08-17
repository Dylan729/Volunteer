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
            ImageUrl = "beach_cleanup.jpg"
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
            ImageUrl = "food_bank.jpg"
        },
        new Opportunity
        {
            Id = 3,
            Title = "Community Garden Planting",
            Category = "Environment",
            Date = DateTime.Today.AddDays(7),
            Time = "1:00 PM",
            Location = "Grey Lynn Community Garden",
            ShortDescription = "Join the team in planting vegetables and maintaining the community garden.",
            AvailablePlaces = 8,
            ImageUrl = "community_garden.jpg"
        },
        new Opportunity
        {
            Id = 4,
            Title = "Elderly Support Visit",
            Category = "Community",
            Date = DateTime.Today.AddDays(10),
            Time = "2:00 PM",
            Location = "Mt Eden Retirement Village",
            ShortDescription = "Spend time with elderly residents, offering companionship and conversation.",
            AvailablePlaces = 5,
            ImageUrl = "elderly_support.jpg"
        },
        new Opportunity
        {
            Id = 5,
            Title = "Animal Shelter Care",
            Category = "Animals",
            Date = DateTime.Today.AddDays(14),
            Time = "11:00 AM",
            Location = "Auckland Animal Rescue",
            ShortDescription = "Help feed, clean, and care for rescued animals waiting for adoption.",
            AvailablePlaces = 3,
            ImageUrl = "animal_shelter.jpg"
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
