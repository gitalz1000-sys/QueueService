using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QueueService.Configuration;
using QueueService.Models;

namespace QueueService.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly IMongoCollection<Appointment> _appointmentsCollection;

    public AppointmentRepository(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _appointmentsCollection = mongoDatabase.GetCollection<Appointment>(
            mongoDbSettings.Value.AppointmentsCollectionName);
    }

    public async Task<List<Appointment>> GetAllAsync()
    {
        return await _appointmentsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(string id)
    {
        return await _appointmentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Appointment> CreateAsync(Appointment appointment)
    {
        appointment.CreatedAt = DateTime.UtcNow;
        appointment.UpdatedAt = DateTime.UtcNow;
        await _appointmentsCollection.InsertOneAsync(appointment);
        return appointment;
    }

    public async Task<bool> UpdateAsync(string id, Appointment appointment)
    {
        appointment.Id = id;
        appointment.UpdatedAt = DateTime.UtcNow;
        var replaceOptions = new ReplaceOptions { IsUpsert = false };
        var result = await _appointmentsCollection.ReplaceOneAsync(x => x.Id == id, appointment, replaceOptions);
        return result.IsAcknowledged && result.MatchedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _appointmentsCollection.DeleteOneAsync(x => x.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}
