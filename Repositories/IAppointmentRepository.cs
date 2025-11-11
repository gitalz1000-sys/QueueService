using QueueService.Models;

namespace QueueService.Repositories;

public interface IAppointmentRepository
{
    Task<List<Appointment>> GetAllAsync();
    Task<Appointment?> GetByIdAsync(string id);
    Task<Appointment> CreateAsync(Appointment appointment);
    Task<bool> UpdateAsync(string id, Appointment appointment);
    Task<bool> DeleteAsync(string id);
}
