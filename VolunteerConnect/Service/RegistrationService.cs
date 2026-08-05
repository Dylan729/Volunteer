using SQLite;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Text;
using VolunteerConnect.Models;
using VolunteerConnect.Service;
using VolunteerConnect.Views;

namespace VolunteerConnect.Service;

public class RegistrationService
{
    private readonly SQLiteAsyncConnection _db;

    public RegistrationService()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "volunteerconnect.db3");
        _db = new SQLiteAsyncConnection(dbPath);
    }

    public async Task InitializeAsync()
    {
        await _db.CreateTableAsync<VolunteerRegistration>();
    }

    // CREATE
    public async Task AddRegistrationAsync(VolunteerRegistration registration)
    {
        registration.RegisteredOn = DateTime.Now;
        await _db.InsertAsync(registration);
    }

    // READ ALL
    public async Task<List<VolunteerRegistration>> GetUserRegistrationsAsync()
    {
        return await _db.Table<VolunteerRegistration>().ToListAsync();
    }

    // READ ONE
    public async Task<VolunteerRegistration> GetRegistrationAsync(int id)
    {
        return await _db.Table<VolunteerRegistration>()
                        .Where(r => r.Id == id)
                        .FirstOrDefaultAsync();
    }

    // UPDATE
    public async Task UpdateRegistrationAsync(VolunteerRegistration registration)
    {
        await _db.UpdateAsync(registration);
    }

    // DELETE
    public async Task DeleteRegistrationAsync(int id)
    {
        var registration = await GetRegistrationAsync(id);
        if (registration != null)
        {
            await _db.DeleteAsync(registration);
        }
    }
}
