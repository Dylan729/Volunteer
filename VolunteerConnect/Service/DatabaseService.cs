using SQLite;
using VolunteerConnect.Models;

namespace VolunteerConnect.Service
{
    public class DatabaseService
    {
    private readonly SQLiteAsyncConnection _db;

    public DatabaseService()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "volunteerconnect.db3");
        _db = new SQLiteAsyncConnection(dbPath);
    }

    public async Task InitializeAsync()
    {
        await _db.CreateTableAsync<VolunteerOpportunity>();
        await _db.CreateTableAsync<VolunteerRegistration>();
        await SeedSampleDataAsync();
    }

    private async Task SeedSampleDataAsync()
    {
        var existing = await _db.Table<VolunteerOpportunity>().CountAsync();
        if (existing > 0)
            return; // Already seeded

        List<VolunteerOpportunity> samples = new()
        {
            new VolunteerOpportunity
            {
                Title = "Community Garden Helper",
                Category = "Environment",
                Date = DateTime.Today.AddDays(7),
                Time = "9:00 AM",
                Location = "Local Community Garden",
                Description = "Assist with planting, watering, and maintaining garden beds.",
                Requirements = "Able to work outdoors.",
                AvailablePlaces = 10,
                ImageName = "garden.png",
                IsAvailable = true
            },
             new VolunteerOpportunity
            {
                Title = "Library Support Volunteer",
                Category = "Education",
                Date = DateTime.Today.AddDays(10),
                Time = "1:00 PM",
                Location = "City Library",
                Description = "Help with shelving books and assisting visitors.",
                Requirements = "Friendly and organised.",
                AvailablePlaces = 5,
                ImageName = "library.png",
                IsAvailable = true
            },
            new VolunteerOpportunity
            {
                Title = "Food Bank Packing Assistant",
                Category = "Community",
                Date = DateTime.Today.AddDays(3),
                Time = "10:00 AM",
                Location = "Food Bank Warehouse",
                Description = "Pack food parcels for families in need.",
                Requirements = "Able to lift light boxes.",
                AvailablePlaces = 15,
                ImageName = "foodbank.png",
                IsAvailable = true
            },
            new VolunteerOpportunity
            {
                Title = "Beach Clean-up Volunteer",
                Category = "Environment",
                Date = DateTime.Today.AddDays(14),
                Time = "8:00 AM",
                Location = "City Beach",
                Description = "Join a team to clean up litter along the shoreline.",
                Requirements = "Comfortable walking long distances.",
                AvailablePlaces = 20,
                ImageName = "beach.png",
                IsAvailable = true
            },
            new VolunteerOpportunity
            {
                Title = "Community Event Assistant",
                Category = "Events",
                Date = DateTime.Today.AddDays(21),
                Time = "12:00 PM",
                Location = "Community Hall",
                Description = "Help set up and run a local community event.",
                Requirements = "Good communication skills.",
                AvailablePlaces = 8,
                ImageName = "event.png",
                IsAvailable = true
            },
            new VolunteerOpportunity
            {
                Title = "Digital Skills Support Volunteer",
                Category = "Technology",
                Date = DateTime.Today.AddDays(5),
                Time = "2:00 PM",
                Location = "Community Centre",
                Description = "Assist seniors with basic computer and smartphone skills.",
                Requirements = "Patient and tech-savvy.",
                AvailablePlaces = 6,
                ImageName = "digital.png",
                IsAvailable = true
            }
        };

        await _db.InsertAllAsync(samples);
    }

    public SQLiteAsyncConnection Connection => _db;
    }
}
